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
            int[] a = Sorting.GenerateDec(5);
            Sorting.BubbleSort(a);
        }

    }
}
