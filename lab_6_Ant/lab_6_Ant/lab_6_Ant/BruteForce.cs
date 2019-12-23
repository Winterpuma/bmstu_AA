using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_6_Ant
{
    class BruteForce
    {
        public static Path GetShortestPath(Map m)
        {
            int[] routeIndexes = new int[m.n - 1];
            for (int i = 0; i < m.n - 1; i++)
            {
                routeIndexes[i] = i + 1;
            }
            // Нет смысла рассматривать маршруты, начинающиеся в 1, 2... городах, т.к они будут кольцевым сдвигом тех, что для 0 города
            var allRoutes = GetAllRoutes(routeIndexes); // Все комбинации маршрутов, начинающихся в 0 городе;

            Path shortestPath = new Path(int.MaxValue);
            foreach (List<int> path in allRoutes)
            {
                // Путь начинается и заканчивается с 0 города
                path.Insert(0, 0);
                path.Add(0);
                Path cur = new Path(m, path.ToArray());
                if (cur.distance < shortestPath.distance)
                    shortestPath = cur;
            }

            return shortestPath;
        }

        /// <summary>
        /// Получение всех возможных перестановок массива
        /// </summary>
        /// <typeparam name="T">Тип значений</typeparam>
        /// <param name="arr">Массив элементов для перестановки</param>
        /// <param name="res">Для результата: массив всех возможных перестановок</param>
        /// <param name="current">Массив значений текущей ветки</param>
        /// <returns></returns>
        public static List<List<T>> GetAllRoutes<T>(IList<T> arr, List<List<T>> res = null, List<T> current = null)
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
                GetAllRoutes(lst, res, next);
            }
            return res;
        }
    }
}
