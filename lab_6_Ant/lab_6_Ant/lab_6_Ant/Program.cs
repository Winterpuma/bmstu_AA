using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace lab_6_Ant
{
    class Program
    {
        static void Main(string[] args)
        {
            //Map m = new Map(4);
            //var a = BruteForce.GetShortestPath(m);
            //var b = AntAlgorithm.GetShortestPath(m, 30, 0.7, 0.3, 4, 0.1);
            //AnalyseTime(10, 2, 11);
            GetCoeffs();
        }

        public static T[][] InitMatr<T>(int n, T val)
        {
            T[][] matr = new T[n][];
            for (int i = 0; i < n; i++)
            {
                matr[i] = new T[n];
                for (int j = 0; j < n; j++)
                    matr[i][j] = val;
            }
            return matr;
        }

        public static void AnalyseTime(int nIter, int mapSizeFrom = 2, int mapSizeTo = 10)
        {
            Stopwatch stopWatch = new Stopwatch();
            List<string> lines = new List<string>();

            for (int mapSizeCurr = mapSizeFrom; mapSizeCurr <= mapSizeTo; mapSizeCurr++)
            {
                Map m = new Map(mapSizeCurr);
                long ts = 0;
                for (int i = 0; i < nIter; i++)
                {
                    stopWatch.Start();
                    //BruteForce.GetShortestPath(m);
                    AntAlgorithm.GetShortestPath(m, 30, 0.7, 0.3, 4, 0.1);
                    stopWatch.Stop();

                    ts += stopWatch.Elapsed.Ticks;
                }
                File.AppendAllText("Ant.txt", mapSizeCurr.ToString() + " " + ((long)(ts / nIter)).ToString() + "\n");
            }
        }

        public static void GetCoeffs()
        {
            for (int i = 3; i < 10; i++)
            {
                double sumab = 1;
                double alpha = 0;
                int alphaZeroErr = 0, alphaErr = 0;
                Map m = new Map(i);
                var shortestPath = BruteForce.GetShortestPath(m);

                for (alpha = 0; alpha < sumab; alpha+=0.1)
                {
                    var cv = AntAlgorithm.GetShortestPath(m, 30, alpha, sumab - alpha, 5, 1);

                    if (cv.distance != shortestPath.distance)
                    {
                        Console.WriteLine("- " + i + " " + cv.distance + " " + shortestPath.distance + " " + alpha);
                    }
                    else
                        Console.WriteLine("+ " + i + " " + cv.distance + " " + shortestPath.distance + " " + alpha);
                }
            }
            Console.ReadLine();
        }
                
    }
}
