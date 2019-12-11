using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_7_Substr
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = StrMatching.Standard("aaabaa", "aba");
            i = StrMatching.KMP("aaabaa", "aba");
            i = StrMatching.BM("aaabaa", "aba");
        }
    }
}
