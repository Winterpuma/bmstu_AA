using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2_MatrMult
{
    public class Program
    {
        static Random rand = new Random();

        static void Main(string[] args)
        {
            /*
            Time(Mult.MultStand, FillMatr, "Stand0.txt");
            Time(Mult.MultStand, FillMatr, "Stand1.txt", 1);
            Time(Mult.MultVin, FillMatr, "Vin0.txt");
            Time(Mult.MultVin, FillMatr, "Vin1.txt", 1);
            Time(Mult.MultVinOpt, FillMatr, "VinOpt0.txt");
            Time(Mult.MultVinOpt, FillMatr, "VinOpt1.txt", 1);
            */
        }
        
        static void Time(Func<int[][], int[][], int[][]> multFunc, Func<int, int, int[][]> genFunc, string filename, int odd = 0)
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

                    multFunc(a, b);

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
