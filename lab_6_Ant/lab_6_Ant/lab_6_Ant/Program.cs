using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_6_Ant
{
    class Program
    {
        static void Main(string[] args)
        {
            Map m = new Map(4);
            var a = BruteForce.GetShortestPath(m);
            var b = AntAlgorithm.GetShortestPath(m, 30, 0.7, 0.3, 4, 0.1);
        }

        public static T[][] InitMatr<T>(int n, T val)
        {
            T[][] matr = new T[n][]; // feromon
            for (int i = 0; i < n; i++)
            {
                matr[i] = new T[n];
                for (int j = 0; j < n; j++)
                    matr[i][j] = val;
            }
            return matr;
        }
    }
}
