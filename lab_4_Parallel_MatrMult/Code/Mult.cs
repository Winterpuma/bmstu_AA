using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab_4_Parallel_MatrMult
{
    class Mult
    {
        public static int[][] MultVin(int[][] matr1, int[][] matr2, int nTh = 0)
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

            for (int i = 0; i < n1; i++)
            {
                for (int j = 0; j < m2; j++)
                {
                    res[i][j] = -mulH[i] - mulV[j];
                    for (int k = 0; k < m1 / 2; k++)
                    {
                        res[i][j] = res[i][j] + (matr1[i][2 * k + 1] + matr2[2 * k][j]) * (matr1[i][2 * k] + matr2[2 * k + 1][j]);
                    }
                }
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
        
    }
}
