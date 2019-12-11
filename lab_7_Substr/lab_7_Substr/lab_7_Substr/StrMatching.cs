using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_7_Substr
{
    public static class StrMatching
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
                return textloc - substr.Length;
            return -1;
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
        

        static bool IsPrefix(string x, int p)
        {
            int j = 0;
            for (int i = p; i < x.Length; i++)
            {
                if (x[i] != x[j])
                    return false;
                j++;
            }
            return true;
        }

        /// <summary>
        /// Возвращает для позиции p длину максимальной подстроки
        /// которая является суффиксом шаблона x
        /// </summary>
        /// <param name="substr"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        static int SuffixLength(string substr, int p)
        {
            int len = 0;
            int i = p, j = substr.Length - 1;
            while (i >= 0 && substr[i] == substr[j])
            {
                len++;
                i--;
                j--;
            }
            return len;
        }

        /// <summary>
        /// Функция для вычисления сдвигов хороших суффиксов
        /// </summary>
        static int[] PreBmGs(string substr)
        {
            int[] table = new int[substr.Length];
            int lastPrefixPosition = substr.Length;

            for (int i = substr.Length - 1; i >= 0; i--)
            {
                if (IsPrefix(substr, i + 1))
                    lastPrefixPosition = i + 1;
                table[substr.Length - 1 - i] = lastPrefixPosition - i + substr.Length - 1;
            }

            for (int i = 0; i < substr.Length - 1; i++)
            {
                int slen = SuffixLength(substr, i);
                table[slen] = substr.Length - 1 - i + slen;
            }

            return table;
        }

        public static int BM(string str, string substr)
        {
            if (substr.Length == 0)
                return -1;
            
            Dictionary<char, int> letters = new Dictionary<char, int>();
            for (int i = 0; i < substr.Length; i++)
                if (letters.ContainsKey(substr[i]))
                    letters[substr[i]] = substr.Length - 1 - i;
                else
                    letters.Add(substr[i], substr.Length - 1 - i);

            int[] bmGs = PreBmGs(substr);

            for (int i = substr.Length - 1; i < str.Length;)
            {
                int j = substr.Length - 1;
                while (substr[j] == str[i])
                {
                    if (j == 0)
                        return i;
                    i--;
                    j--;
                }
                var a = letters.ContainsKey(str[i]) ? letters[str[i]] : 1;
                var b = bmGs[substr.Length - 1 - j];
                i += Math.Max(a, b);
            }
            return -1;
        }

    }
}
