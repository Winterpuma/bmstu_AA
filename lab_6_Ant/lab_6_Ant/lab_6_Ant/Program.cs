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
            //var m = new Map(4);
            var ho = new int[3];
            ho[0] = 2;
            ho[1] = 3;
            ho[2] = 4;
            
            var a = BruteForce.GetAllCombinations(ho);
        }
    }
}
