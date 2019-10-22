using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace lab_4_Parallel_MatrMult
{
    class ParallelMult
    {
        public static void MainCycle(object obj)
        {
            AllParameters p = (AllParameters)obj;

            for (int i = p.St; i < p.End; i++)
            {
                for (int j = 0; j < p.End2; j++)
                {
                    p.res[i][j] = -p.mulH[i] - p.mulV[j];
                    for (int k = 0; k < p.End3 / 2; k++)
                    {
                        p.res[i][j] = p.res[i][j] + (p.matr1[i][2 * k + 1] + p.matr2[2 * k][j]) * (p.matr1[i][2 * k] + p.matr2[2 * k + 1][j]);
                    }
                }
            }
        }

        // This addresation works faster
        public static void MainCycleOptimize(object obj)
        {
            AllParameters p = (AllParameters)obj;
            int[][] res = p.res, matr1 = p.matr1, matr2 = p.matr2;
            int[] mulH = p.mulH, mulV = p.mulV;
            int st = p.St, end = p.End;
            int end2 = p.End2, end3 = p.End3;

            for (int i = st; i < end; i++)
            {
                for (int j = 0; j < end2; j++)
                {
                    res[i][j] = -mulH[i] - mulV[j];
                    for (int k = 0; k < end3 / 2; k++)
                    {
                        res[i][j] = res[i][j] + (matr1[i][2 * k + 1] + matr2[2 * k][j]) * (matr1[i][2 * k] + matr2[2 * k + 1][j]);
                    }
                }
            }
        }

        public static int[][] ParallelMultVin(int[][] matr1, int[][] matr2, int nThreads)
        {
            int n1 = matr1.Length;
            int n2 = matr2.Length;

            if (n1 == 0 || n2 == 0)
                return null;

            int m1 = matr1[0].Length;
            int m2 = matr2[0].Length;

            if (m1 != n2)
                return null;

            int[] mulH = new int[n1];
            int[] mulV = new int[m2];

            int[][] res = new int[n1][];
            for (int i = 0; i < n1; i++)
                res[i] = new int[m2];

            Thread[] t = new Thread[nThreads];

            /*
            // Count MulV
            int colForThread = m2 / nThreads;
            int m2St = 0;
            for (int i = 0; i < nThreads; i++)
            {
                int m2End = m2St + colForThread;
                if (i == nThreads - 1)
                    m2End = m2;
                Parameters p = new Parameters(matr2, mulV, m2St, m2End, n2);

                t[i] = new Thread(CountMulV);
                t[i].Start(p);

                m2St = m2End;
            }

            foreach (Thread thread in t)
            {
                thread.Join();
            }

            // Count MulH
            int rowsForThread = n1 / nThreads;
            int n1St = 0;
            for (int i = 0; i < nThreads; i++)
            {
                int n1End = n1St + rowsForThread;
                if (i == nThreads - 1)
                    n1End = n1;
                Parameters p = new Parameters(matr1, mulH, n1St, n1End, m1);
                
                t[i] = new Thread(CountMulH);
                t[i].Start(p);

                n1St = n1End;
            }
            foreach (Thread thread in t)
            {
                thread.Join();
            }
            */

            for (int i = 0; i < n1; i++)
            {
                for (int j = 0; j < m1 / 2; j++)
                {
                    mulH[i] = mulH[i] + matr1[i][j * 2] * matr1[i][j * 2 + 1];
                }
            }

            for (int i = 0; i < m2; i++)
            {
                for (int j = 0; j < n2 / 2; j++)
                {
                    mulV[i] = mulV[i] + matr2[j * 2][i] * matr2[j * 2 + 1][i];
                }
            }


            int rowsForThread = n1 / nThreads;
            int n1St = 0;
            for (int i = 0; i < nThreads; i++)
            {
                int n1End = n1St + rowsForThread;
                if (i == nThreads - 1)
                    n1End = n1;
                AllParameters p = new AllParameters(res, matr1, matr2, mulV, mulH, n1St, n1End, m2, m1);

                t[i] = new Thread(MainCycleOptimize);
                t[i].Start(p);

                n1St = n1End;
            }
            foreach (Thread thread in t)
            {
                thread.Join();
            }


            if (m1 % 2 == 1)
            {
                for (int i = 0; i < n1; i++)
                {
                    for (int j = 0; j < m2; j++)
                    {
                        res[i][j] = res[i][j] + matr1[i][m1 - 1] * matr2[m1 - 1][j];
                    }
                }
            }

            return res;
        }

        public static void CountMulH(object obj)
        {
            Parameters p = (Parameters)obj;
            for (int i = p.St; i < p.End; i++)
            {
                for (int j = 0; j < p.End2 / 2; j++)
                {
                    p.mul[i] = p.mul[i] + p.matr[i][j * 2] * p.matr[i][j * 2 + 1];
                }
            }
        }

        public static void CountMulV(object obj)
        {
            Parameters p = (Parameters)obj;
            for (int i = p.St; i < p.End; i++)
            {
                for (int j = 0; j < p.End2 / 2; j++)
                {
                    p.mul[i] = p.mul[i] + p.matr[j * 2][i] * p.matr[j * 2 + 1][i];
                }
            }

        }
    }


        class Parameters
    {
        public int[][] matr;
        public int[] mul;
        public int St, End, End2;

        public Parameters(int[][] matr, int[] mul, int St, int End, int End2)
        {
            this.matr = matr;
            this.mul = mul;
            this.St = St;
            this.End = End;
            this.End2 = End2;
        }
    }

    class AllParameters
    {
        public int[][] res, matr1, matr2;
        public int[] mulV, mulH;
        public int St, End, End2, End3;

        public AllParameters(int[][] res, int[][] matr1, int[][] matr2, int[] mulV, int[] mulH, int St, int End, int End2, int End3)
        {
            this.res = res;
            this.matr1 = matr1;
            this.matr2 = matr2;
            this.mulV = mulV;
            this.mulH = mulH;
            this.St = St;
            this.End = End;
            this.End2 = End2;
            this.End3 = End3;
        }
    }
}

