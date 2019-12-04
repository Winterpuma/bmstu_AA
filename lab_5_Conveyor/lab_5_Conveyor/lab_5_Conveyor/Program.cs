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
            Conveyor[] c = new Conveyor[nLines];
            // Создаем и генерируем данные
            List<Args> allData = new List<Args>();
            GenData(nLines, 1000, allData);

            // Создаем конвейеры 
            c[1] = new Conveyor(allData);
            for (int i = 1; i < nLines; i++)
            {
                c[i] = new Conveyor();
            }
            

            /*foreach (Thread thread in t)
            {
                thread.Join();//Или закрываем, дожидаясь сигнала?
            }*/

        }   

        static void GenData(int nLines, int size, List<Args> data)
        {
            for (int i = 0; i < size; i++)
            {
                data.Add(new Args(nLines));
            }
        }
    }

    class Args
    {
        public long[] ts;
        public long[] te;

        public Args(int n)
        {
            ts = new long[n];
            te = new long[n];
        }
    }
}
