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
            int[] prefix = PrefixFunction(substr);
            int last_prefix = 0;
            for (int i = 0; i < str.Length; i++)
            {
                while (last_prefix > 0 && substr[last_prefix] != str[i])
                    last_prefix = prefix[last_prefix - 1];

                if (substr[last_prefix] == str[i])
                    last_prefix++;

                if (last_prefix == substr.Length)
                {
                    return i + 1 - substr.Length;
                }
            }
            return -1;
        }

        static int[] PrefixFunction(string substr)
        {
            int[] prefix = new int[substr.Length];

            int lastPrefix = prefix[0] = 0;
            for (int i = 1; i < substr.Length; i++)
            {
                while (lastPrefix > 0 && substr[lastPrefix] != substr[i])
                    lastPrefix = prefix[lastPrefix - 1];

                if (substr[lastPrefix] == substr[i])
                    lastPrefix++;

                prefix[i] = lastPrefix;
            }
            return prefix;
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
        static int[] GetSuffix(string substr)
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

            int[] suffix = GetSuffix(substr);

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
                var a = letters.ContainsKey(str[i]) ? letters[str[i]] : substr.Length;
                var b = suffix[substr.Length - 1 - j];
                i += Math.Max(a, b);
            }
            return -1;
        }

    }
}
