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
            TestCorrect();
            //Time(Sorting.QuickSort, Sorting.GenerateSame, "QuickSame.txt");
            //TimeOnOneFunction(Sorting.QuickSort, "Quick");
            //TimeOnOneFunction(Sorting.BubbleSort, "Bubble");
            //TimeOnOneFunction(Sorting.InserionSort, "Inserion");
        }

        static void TestCorrect()
        {
            int n = 10;
            int[] a = Sorting.GenerateRnd(n);
            int[] quick = new int[n];
            a.CopyTo(quick, 0);
            int[] bubble = new int[n];
            a.CopyTo(bubble, 0);
            int[] insertion = new int[n];
            a.CopyTo(insertion, 0);
            Sorting.QuickSort(quick, 0, quick.Count()-1);
            Sorting.BubbleSort(bubble);
            Sorting.InserionSort(insertion);

            Console.WriteLine("random: \n" + string.Join(" ", a));
            Console.WriteLine("sorted:");
            Console.WriteLine("Bubble\n" + string.Join(" ", bubble));
            Console.WriteLine("Insertion\n" + string.Join(" ", insertion));
            Console.WriteLine("Quick\n" + string.Join(" ", quick));
        }

        static void TimeOnOneFunction(Action<int[], int, int> sortFunc, string funcName)
        {
            Time(sortFunc, Sorting.GenerateDec, funcName + "Dec.txt");
            Time(sortFunc, Sorting.GenerateAsc, funcName + "Asc.txt");
            Time(sortFunc, Sorting.GenerateRnd, funcName + "Rnd.txt");
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
                    a = genFunc(size);
                    stopWatch.Start();

                    sortFunc(a, 0, a.Length - 1);

                    stopWatch.Stop();
                    ts += stopWatch.Elapsed.Ticks;
                }
                lines.Add(size.ToString() + " " + (ts / N_REP).ToString());
            }
            File.AppendAllLines(filename, lines);
        }

    }
}
