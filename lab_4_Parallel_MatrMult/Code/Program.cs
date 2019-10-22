using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace lab_4_Parallel_MatrMult
{
    class Program
    {
        static Random rand = new Random();

        static void Main(string[] args)
        {
            /*
            int[][] a = new int[1][];
            a[0] = new int[1] { 2 };
            int[][] b = new int[1][];
            b[0] = new int[1] { 3 };

            var res = ParallelMult.ParallelMultVin(a, b, 10);*/

            Time(Mult.MultVin, FillMatr, "Vin0.txt", 1, 0);
            Time(ParallelMult.ParallelMultVin, FillMatr, "ParallelVinMOpt0_1.txt", 1, 0);
            Time(ParallelMult.ParallelMultVin, FillMatr, "ParallelVinMOpt0_2.txt", 2, 0);
            Time(ParallelMult.ParallelMultVin, FillMatr, "ParallelVinMOpt0_4.txt", 4, 0);
            Time(ParallelMult.ParallelMultVin, FillMatr, "ParallelVinMOpt0_8.txt", 8, 0);
            Time(ParallelMult.ParallelMultVin, FillMatr, "ParallelVinMOpt0_16.txt", 16, 0);
            

            //Time(ParallelMult.ParallelMultVin, FillMatr, "ParallelVinMOpt1_1.txt", 1, 1);
            //Time(ParallelMult.ParallelMultVin, FillMatr, "ParallelVinMOpt1_2.txt", 2, 1);
            //Time(ParallelMult.ParallelMultVin, FillMatr, "ParallelVinMOpt1_4.txt", 4, 1);
            //Time(ParallelMult.ParallelMultVin, FillMatr, "ParallelVinMOpt1_8.txt", 8, 1);
            //Time(ParallelMult.ParallelMultVin, FillMatr, "ParallelVinMOpt1_16.txt", 16, 1);

        }

        static void Time(Func<int[][], int[][], int, int[][]> multFunc, Func<int, int, int[][]> genFunc, string filename, int nThreads, int odd = 0)
        {

            int N_REP = 5;
            Stopwatch stopWatch = new Stopwatch();
            List<string> lines = new List<string>();

            for (int size = 100 + odd; size <= 1000 + odd; size += 100)
            {
                long ts = 0;
                for (int repetitions = 1; repetitions <= N_REP; repetitions++)
                {
                    var a = genFunc(size, size);
                    var b = genFunc(size, size);
                    stopWatch.Start();

                    multFunc(a, b, nThreads);

                    stopWatch.Stop();
                    ts += stopWatch.Elapsed.Ticks;
                }
                lines.Add(size.ToString() + " " + (ts / N_REP).ToString());
            }
            File.AppendAllLines(filename, lines);
        }

        public static int[][] FillMatr(int n, int m)
        {
            int[][] matr = new int[n][];
            int[] tmp;

            for (int i = 0; i < n; i++)
            {
                tmp = new int[m];
                for (int j = 0; j < m; j++)
                    tmp[j] = rand.Next(1000);
                matr[i] = tmp;
            }
            return matr;
        }
    }
}
