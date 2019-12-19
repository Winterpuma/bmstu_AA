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
            BruteForce.GetShortestPath(m);
        }
    }
}
