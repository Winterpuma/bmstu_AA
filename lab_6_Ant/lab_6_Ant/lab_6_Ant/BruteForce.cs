using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_6_Ant
{
    class BruteForce
    {
        
        public static List<List<T>> GetAllCombinations<T>(IList<T> arr, List<List<T>> res = null, List<T> current = null)
        {
            if (res == null)
                res = new List<List<T>>();
            if (arr.Count == 0) //если все элементы использованы
            {
                res.Add(current);
                return res;
            }
            for (int i = 0; i < arr.Count; i++)
            {
                //в цикле для каждого элемента прибавляем его к итоговому массиву, 
                //создаем новый список из оставшихся элементов, и вызываем эту же функцию рекурсивно с новыми параметрами
                List<T> lst = new List<T>(arr);
                lst.RemoveAt(i);
                List<T> next;
                if (current == null)
                    next = new List<T>();
                else
                    next = new List<T>(current);
                next.Add(arr[i]);
                GetAllCombinations(lst, res, next);
            }
            return res;
        }
    }
}
