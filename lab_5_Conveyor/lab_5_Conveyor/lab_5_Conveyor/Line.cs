using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace lab_5_Conveyor
{
    enum state {finish, empty, ok};

    class Line
    {
        int id;
        int sleepTime;

        Queue<Args> q;
        Line nextLine;
        Action<int, Args, int> conveyorAction;

        public Line(int id, int sleepTime, Action<int, Args, int> a)
        {
            this.id = id;
            this.sleepTime = sleepTime;
            conveyorAction = a;
            q = new Queue<Args>();
        }

        public Line(int id, int sleepTime, List<Args> elements, Line nextLine, Action<int, Args, int> a)
        {
            this.id = id;
            this.sleepTime = sleepTime;
            q = new Queue<Args>(elements);
            this.nextLine = nextLine;
            conveyorAction = a;
        }

        public Line(int id, int sleepTime, Line nextLine, Action<int, Args, int> a)
        {
            this.id = id;
            this.sleepTime = sleepTime;
            this.nextLine = nextLine;
            q = new Queue<Args>();
            conveyorAction = a;
        }

        public void SetQueue(Queue<Args> q)
        {
            this.q = q;
        }

        public void Run()
        {
            state ret = state.ok;
            while(ret != state.finish)
            {
                ret = ProcessElement();
                if (ret == state.empty)
                    Thread.Sleep(500);
            }
        }

        public state ProcessElement()
        {
            Args el = null;
            lock(q)
            {
                if (q.Count > 0)
                {
                    el = q.Dequeue();
                }
            }
            
            if (el != null)
            {
                if (el.IsLast())
                {
                    if (nextLine != null)
                        nextLine.AddElement(el);
                    return state.finish;
                }
                conveyorAction(id, el, sleepTime);
                if (nextLine != null)
                    nextLine.AddElement(el);
            }
            else
            {
                return state.empty;
            }

            return state.ok;
        }

        public void RunCompletelyLinear()
        {
            Args el = q.Dequeue();
            if (el.IsLast())
                return;
            conveyorAction(id, el, sleepTime);
            if (nextLine != null)
                nextLine.q.Enqueue(el);
        }


        public void AddElement(Args arg)
        {
            lock (q)
            {
                q.Enqueue(arg);
            }
        }
    }
}
