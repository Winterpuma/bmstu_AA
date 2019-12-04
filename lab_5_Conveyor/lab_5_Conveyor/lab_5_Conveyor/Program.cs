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
            Thread[] t = new Thread[nLines];
            Line[] c = new Line[nLines];
            // Создаем и генерируем данные
            List<Args> allData = new List<Args>();
            GenData(nLines, 5, allData);

            // Создаем конвейеры 
            c[nLines - 1] = new Line(nLines - 1, 100, Act1);
            for (int i = nLines - 2; i > 0; i--)
            {
                c[i] = new Line(i, 200, c[i+1], Act1);
            }
            c[0] = new Line(0, 300, allData, (nLines > 1) ? c[1] : null, Act1);

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

            Analysis.Analyse("Threading: ", allData);

            // Тоже самое в однопоточном режиме
            List<Args> allDataLinear = new List<Args>();
            GenData(nLines, 5, allDataLinear);

            c[0].SetQueue(new Queue<Args>(allDataLinear));
            for (int i = 0; i < nLines; i++)
            {
                c[i].Run();
            }
            Analysis.Analyse("Linear   : ", allDataLinear);

            Console.WriteLine("thats all");

        }   

        static void GenData(int nLines, int size, List<Args> data)
        {
            for (int i = 0; i < size; i++)
            {
                data.Add(new Args(nLines, i));
            }
            data.Add(new Args(0, -1, true));
        }

        static void Act1(int i, Args a, int milliseconds)
        {
            a.ts[i] = DateTime.Now.Ticks;
            Console.WriteLine(i.ToString() + " start " + a.id.ToString() + " " + a.ts[i].ToString());
            Thread.Sleep(milliseconds);
            a.te[i] = DateTime.Now.Ticks;
            Console.WriteLine(i.ToString() + " end   " + a.id.ToString() + " " + a.te[i].ToString());
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
