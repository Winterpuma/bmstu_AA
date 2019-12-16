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
           // AnalyseAll(4, 1000);
        }
        
        public static void AnalyseAll(int minLen, int maxLen, int nIter = 10)
        {
            AnalyseRanging("Standard.txt", StrMatching.Standard, minLen, maxLen, nIter);
            AnalyseRanging("BM.txt", StrMatching.BM, minLen, maxLen, nIter);
            AnalyseRanging("KMP.txt", StrMatching.KMP, minLen, maxLen, nIter);
        }

        public static void AnalyseRanging(string filename, Func<string, string, int> f, int minLen, int maxLen, int nIter = 20)
        {
            Stopwatch stopWatch = new Stopwatch();

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(filename))
            {
                for (int curLen = minLen; curLen < maxLen; curLen++)
                {
                    long ts = 0;
                    for (int i = 0; i < nIter; i++)
                    {
                        string s = GenStr(curLen);
                        string sub = GenStr(3);

                        stopWatch.Start();
                        f(s, sub);
                        stopWatch.Stop();

                        ts += stopWatch.Elapsed.Ticks;
                    }
                    file.WriteLine(curLen + " " + (long)(ts / nIter));
                    curLen++;
                }
            }
        }

        public static string GenStr(int len)
        {
            string s = "";
            for (int i = 0; i < len; i++)
            {
                s += rand.Next(0, 9).ToString();
            }
            return s;
        }
    }
}
