using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace lab_7_Substr
{
    class Program
    {
        static Random rand = new Random();

        static void Main(string[] args)
        {
            //AnalyseAllByStr(1000, 1000000, 100, 999);
            //AnalyseAllBySub(1000000, 9999999, 100, 1000000);
            //AnalyseAll(10, 1000000000);
        }

        public static void AnalyseAllByStr(int minStr, int maxStr, int minSub, int maxSub, int nIter = 10)
        {
            AnalyseRangingStr("StandardStr.txt", StrMatching.Standard, minStr, maxStr, minSub, maxSub, nIter);
            AnalyseRangingStr("BMStr.txt", StrMatching.BM, minStr, maxStr, minSub, maxSub, nIter);
            AnalyseRangingStr("KMPStr.txt", StrMatching.KMP, minStr, maxStr, minSub, maxSub, nIter);
        }

        public static void AnalyseRangingStr(string filename, Func<string, string, int> f, int minStr, int maxStr, int minSub, int maxSub, int nIter = 10)
        {
            Stopwatch stopWatch = new Stopwatch();

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(filename))
            {
                for (int currStrLen = minStr; currStrLen <= maxStr; currStrLen *= 10)
                {
                    long ts = 0;
                    for (int i = 0; i < nIter; i++)
                    {
                        string s = rand.Next(currStrLen, currStrLen * 10).ToString();
                        string sub = rand.Next(minSub, maxSub).ToString();

                        stopWatch.Start();
                        f(s, sub);
                        stopWatch.Stop();

                        ts += stopWatch.Elapsed.Ticks;
                    }
                    file.WriteLine(currStrLen + " " + (long)(ts / nIter));
                }
            }
        }

        public static void AnalyseAllBySub(int minStr, int maxStr, int minSub, int maxSub, int nIter = 10)
        {
            AnalyseRangingSub("StandardSub.txt", StrMatching.Standard, minStr, maxStr, minSub, maxSub, nIter);
            AnalyseRangingSub("BMSub.txt", StrMatching.BM, minStr, maxStr, minSub, maxSub, nIter);
            AnalyseRangingSub("KMPSub.txt", StrMatching.KMP, minStr, maxStr, minSub, maxSub,  nIter);
        }

        public static void AnalyseRangingSub(string filename, Func<string, string, int> f, int minStr, int maxStr, int minSub, int maxSub, int nIter = 10)
        {
            Stopwatch stopWatch = new Stopwatch();

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(filename))
            {
                for (int currSubLen = minSub; currSubLen <= maxSub; currSubLen *= 10)
                {
                    long ts = 0;
                    for (int i = 0; i < nIter; i++)
                    {
                        string s = rand.Next(minStr, maxStr).ToString();
                        string sub = rand.Next(currSubLen, currSubLen * 10).ToString(); 

                        stopWatch.Start();
                        f(s, sub);
                        stopWatch.Stop();

                        ts += stopWatch.Elapsed.Ticks;
                    }
                    file.WriteLine(currSubLen + " " + (long)(ts / nIter));
                }
            }
        }

        public static void AnalyseAll(int min, int max, int nIter = 10)
        {
            AnalyseRanging("Standard.txt", StrMatching.Standard, min, max, nIter);
            AnalyseRanging("BM.txt", StrMatching.BM, min, max, nIter);
            AnalyseRanging("KMP.txt", StrMatching.KMP, min, max, nIter);
        }

        public static void AnalyseRanging(string filename, Func<string, string, int> f, int min, int max, int nIter = 10)
        {
            Stopwatch stopWatch = new Stopwatch();

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(filename))
            {
                for (int currLen = min; currLen <= max; currLen *= 10)
                {
                    long ts = 0;
                    for (int i = 0; i < nIter; i++)
                    {
                        string s = rand.Next(currLen, currLen * 10).ToString();
                        string sub = rand.Next(currLen, currLen * 10).ToString();

                        stopWatch.Start();
                        f(s, sub);
                        stopWatch.Stop();

                        ts += stopWatch.Elapsed.Ticks;
                    }
                    file.WriteLine(currLen + " " + (long)(ts / nIter));
                }
            }
        }
    }
}
