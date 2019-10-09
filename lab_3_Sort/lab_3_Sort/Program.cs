using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lab_3_Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            Time(Sorting.QuickSort, Sorting.GenerateDec, "QuickDec.txt");
        }

        static void Time(Action<int[], int, int> sortFunc, Func<int, int[]> genFunc, string filename)
        {
            int[] a;

            int N_REP = 100;
            Stopwatch stopWatch = new Stopwatch();
            List<string> lines = new List<string>();

            for (int size = 100; size <= 1000; size += 100)
            {
                long ts = 0;
                for (int repetitions = 1; repetitions <= N_REP; repetitions++)
                {
                    a = genFunc(size);//Sorting.GenerateDec(size);
                    stopWatch.Start();

                    sortFunc(a, 0, a.Length - 1);
                    //Sorting.QuickSort(a, 0, a.Length - 1);

                    stopWatch.Stop();
                    ts += stopWatch.Elapsed.Ticks;
                }
                lines.Add((ts / N_REP).ToString());
            }
            File.AppendAllLines(filename, lines);
        }

    }
}
