using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace lab_5_Conveyor
{
    class Program
    {
        static void Main(string[] args)
        {
            int nLines = 3;
            int nData = 5;

            // Создаем, генерируем и обрабатываем данные
            List<Args> allDataParallel = new List<Args>();
            GenData(nLines, nData, allDataParallel);
            Parallel(allDataParallel, nLines, 2000, 3000, Act1);
            Analysis.AnalyseTime("Threading: ", allDataParallel);

            // Тоже самое в однопоточном режиме
            List<Args> allDataLinear = new List<Args>();
            GenData(nLines, nData, allDataLinear);
            Linear(allDataLinear, nLines, 2000, 3000, Act1);
            Analysis.AnalyseTime("Linear   : ", allDataLinear);

            List<Args> allDataFullLinear = new List<Args>();
            GenData(nLines, nData, allDataFullLinear);
            Linear(allDataFullLinear, nLines, 2000, 3000, Act1);
            Analysis.AnalyseTime("FullLinear: ", allDataFullLinear);

            //Analysis.AnalyseDelta(3, 5);

            Console.WriteLine("thats all");
        }

        public static void Parallel(List<Args> allData, int nLines, int minDelay, int maxDelay, Action<int, Args, int> act)
        {
            Thread[] t = new Thread[nLines];
            Line[] c = new Line[nLines];

            // Создаем конвейеры 
            c[nLines - 1] = new Line(nLines - 1, minDelay, act);
            for (int i = nLines - 2; i > 0; i--)
            {
                c[i] = new Line(i, minDelay, c[i + 1], act);
            }
            c[0] = new Line(0, maxDelay, allData, (nLines > 1) ? c[1] : null, act);

            // Запускаем конвейеры каждый в своем потоке
            for (int i = 0; i < nLines; i++)
            {
                t[i] = new Thread(c[i].Run);
                t[i].Start();
            }

            // Ждем завершения всех потоков
            foreach (Thread thread in t)
            {
                thread.Join();
            }
        }

        public static void Linear(List<Args> allData, int nLines, int minDelay, int maxDelay, Action<int, Args, int> act)
        {
            Thread[] t = new Thread[nLines];
            Line[] c = new Line[nLines];

            // Создаем конвейеры 
            c[nLines - 1] = new Line(nLines - 1, minDelay, act);
            for (int i = nLines - 2; i > 0; i--)
            {
                c[i] = new Line(i, minDelay, c[i + 1], act);
            }
            c[0] = new Line(0, maxDelay, allData, (nLines > 1) ? c[1] : null, act);
            
            for (int i = 0; i < nLines; i++)
            {
                c[i].Run();
            }

        }

        public static void CompletelyLinear(List<Args> allData, int nLines, int minDelay, int maxDelay, Action<int, Args, int> act)
        {
            Line[] c = new Line[nLines];

            // Создаем конвейеры 
            c[nLines - 1] = new Line(nLines - 1, minDelay, act);
            for (int i = nLines - 2; i > 0; i--)
            {
                c[i] = new Line(i, minDelay, c[i + 1], act);
            }
            c[0] = new Line(0, maxDelay, allData, (nLines > 1) ? c[1] : null, act);

            for (int i = 0; i < nLines; i++)
            {
                c[i].RunCompletelyLinear();
            }
        }

        public static void GenData(int nLines, int size, List<Args> data)
        {
            for (int i = 0; i < size; i++)
            {
                data.Add(new Args(nLines, i));
            }
            data.Add(new Args(0, -1, true));
        }

        public static void Act1(int i, Args a, int milliseconds)
        {
            a.ts[i] = DateTime.Now.Ticks;
            Console.WriteLine(i.ToString() + " start " + a.id.ToString() + " " + a.ts[i].ToString());
            Thread.Sleep(milliseconds);
            a.te[i] = DateTime.Now.Ticks;
            Console.WriteLine(i.ToString() + " end   " + a.id.ToString() + " " + a.te[i].ToString());
        }

        public static void Act2(int i, Args a, int milliseconds)
        {
            a.ts[i] = DateTime.Now.Ticks;
            Thread.Sleep(milliseconds);
            a.te[i] = DateTime.Now.Ticks;
        }
    }

    class Args
    {
        private bool last;
        public int id;
        public long[] ts;
        public long[] te;

        public Args(int n, int id,  bool lastArg = false)
        {
            ts = new long[n];
            te = new long[n];
            last = lastArg;
            this.id = id;
        }

        public bool IsLast()
        {
            return last;
        }
    }
}
