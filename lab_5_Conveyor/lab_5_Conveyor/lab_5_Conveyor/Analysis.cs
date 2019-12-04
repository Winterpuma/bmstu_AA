using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_5_Conveyor
{
    static class Analysis
    {
        public static void Analyse(string msg, List<Args> data)
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

    }
}
