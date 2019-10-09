using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_3_Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = Sorting.GenerateRand(5);
            Sorting.QuickSort(a, 0, a.Length - 1);
        }

    }
}
