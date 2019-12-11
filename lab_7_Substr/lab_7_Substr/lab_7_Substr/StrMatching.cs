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

        public static int KMP(string str, string substr)
        {
            List<int> fail = CreateStateMachine(str);
            int subloc = 0, textloc = 0;
            int res = -1;
            while ((subloc < substr.Length) && (textloc < str.Length))
            {
                if ((subloc == 0) || (str[textloc] == substr[subloc]))
                {
                    textloc++;
                    subloc++;
                }
                else
                    subloc = fail[subloc];
            }
            if (subloc >= substr.Length)
                res = textloc - substr.Length;
            return res;
        }

        static List<int> CreateStateMachine(string s)
        {
            List<int> fail = new List<int>();
            for (int i = 0; i < s.Length; i++)
                fail.Add(0);
            for (int i = 2; i < s.Length; i++)
            {
                int tmp = fail[i - 1];
                while ((tmp > 0) && (s[tmp] != s[i - 1]))
                    tmp = fail[tmp];
                fail[i] = tmp + 1;
            }
            return fail;
        }

        static List<int> CreateArrShift(string sub)
        {
            List<int> shift = new List<int>();
            for (int i = 0; i < 256; i++)
                shift.Add(sub.Length);
            for (int i = 0; i < sub.Length; i++)
                shift[sub[i]] = i;
            return shift;
        }

        public static List<int> CreateArrJump(string substr)
        {
            List<int> jump = new List<int>(), link = new List<int>();
            for (int i = 0; i < substr.Length; i++)
            {
                jump.Add(2 * substr.Length - i - 1);
                link.Add(0);
            }
            link.Add(0);
            int test = substr.Length - 1;
            int target = substr.Length;
            while (test > 0)
            {
                link[test] = target;
                while ((target < substr.Length) && (substr[test] != substr[target]))
                {
                    jump[target] = Math.Min(jump[target], substr.Length - test - 1);
                    target = link[target];
                }
                test--;
                target--;
            }
            ;
            for (int i = 0; i < target; i++)
                jump[i] = Math.Min(jump[i], substr.Length + target - i - 1);

            int tmp = link[target];
            while (target < substr.Length)
            {
                while (target < tmp)
                {
                    jump[target] = Math.Min(jump[target], tmp - target + substr.Length + 1);
                    target++;
                }
                tmp = link[tmp];
            }
            return jump;
        }

        
        public static int BM(string str, string substr)
        {
            List<int> shift = CreateArrShift(substr);
            List<int> jump = CreateArrJump(substr);
            
            int textLoc = substr.Length - 1;
            int subLoc = substr.Length - 1;
            while ((textLoc <= str.Length) && (subLoc > 0))
            {
                if (str[textLoc] == substr[subLoc])
                {
                    textLoc--;
                    subLoc--;
                }
                else
                {
                    textLoc = textLoc + Math.Max(shift[str[textLoc]], jump[subLoc]);
                    subLoc = substr.Length - 1;
                }
            }
            if (subLoc == 0)
                return textLoc;
            return -1;
        }

    }
}
