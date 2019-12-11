using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_7_Substr
{
    class StrMatching
    {
        public static int Standard(string str, string substr)
        {
            for (int i = 0; i <= str.Length - substr.Length; i++)
            {
                bool correct = true; 
                for (int j = 0; j < substr.Length && correct; j++)
                {
                    if (str[i + j] != substr[j])
                        correct = false;
                }
                if (correct)
                    return i;
            }
            return -1;
        }
    }
}
