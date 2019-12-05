using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace lab_5_Conveyor
{
    static class Analysis
    {
        public static void AnalyseTime(string msg, List<Args> data)
        {
            int nLines = data[0].ts.Length;
            double allTime = data[data.Count - 2].te[nLines - 1] - data[0].ts[0];

            double downTime = 0;
            for (int i = 0; i < data.Count - 1; i++)
            {
                for (int j = nLines - 1; j > 1; j--)
                {
                    downTime += data[i].ts[j] - data[i].te[j - 1];
                }
            }

            Console.WriteLine(msg + "total time: " + allTime.ToString() + " downtime: " + downTime.ToString());
        }

        public static void AnalyseDelta(int nLines, int nData)
        {
            Stopwatch stopWatch = new Stopwatch();

            List<Args> allDataParallel = new List<Args>();
            List<Args> allDataLinear = new List<Args>();
            List<Args> allDataFullLinear = new List<Args>();
            Program.GenData(nLines, nData, allDataParallel);
            Program.GenData(nLines, nData, allDataLinear);
            Program.GenData(nLines, nData, allDataFullLinear);
            
            int minDelay = 500;
            int maxDelay = 1000;
            int step = 100;
            int nIter = 1;

            AnalyseDeltaFunc(nIter, nLines, nData, minDelay, maxDelay, step, Program.Parallel);
            AnalyseDeltaFunc(nIter, nLines, nData, minDelay, maxDelay, step, Program.Linear);
            AnalyseDeltaFunc(nIter, nLines, nData, minDelay, maxDelay, step, Program.CompletelyLinear);
        }

        public static void AnalyseDeltaFunc(int nIter, int nLines, int nData, int minDelay, int maxDelay, int step, Action<List<Args>, int, int, int, Action<int, Args, int>> func)
        {
            Stopwatch stopWatch = new Stopwatch();
            List<Args> allData = new List<Args>();
            Program.GenData(nLines, nData, allData);

            for (int currDelay = minDelay; currDelay <= maxDelay; currDelay += step)
            {
                long ts = 0;
                Console.Write(currDelay + " ");
                for (int i = 0; i < nIter; i++)
                {
                    stopWatch.Start();
                    func(allData, nLines, minDelay, maxDelay, Program.Act2);
                    stopWatch.Stop();

                    ts += stopWatch.Elapsed.Ticks;
                }
                Console.WriteLine((long)(ts / nIter));
            }
            
        }

    }
}
