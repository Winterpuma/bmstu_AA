using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_3_Sort
{
    class Sorting
    {
        static Random rand = new Random();

        public static void BubbleSort(int[] arr)
        {
            int i = 0;
            bool swap_cnt = false;

            if (arr.Length == 0)
                return;

            while (i < arr.Length)
            {
                if (i + 1 != arr.Length && arr[i] > arr[i + 1])
                {
                    Swap(ref arr[i], ref arr[i + 1]);
                    swap_cnt = true;
                }
                i++;
                if (i == arr.Length && swap_cnt)
                {
                    swap_cnt = false;
                    i = 0;
                }
            }
        }

        public static void InserionSort(int[] arr)
        {
            int x, j;
            for (int i = 2; i <= arr.Length; i++)
            {
                x = arr[i];
                j = i;

                while (j > 1 && arr[j - 1] > x)
                {
                    arr[j] = arr[j - 1];
                    j--;
                }

                arr[j] = x;
             }
        }

        public static void QuickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int p = Partition(arr, low, high);
                QuickSort(arr, low, p);
                QuickSort(arr, p + 1, high);
            }
        }

        public static int Partition(int[] arr, int low , int high)
        {
            int pivot = arr[(low + high) / 2];
            int i = low - 1, j = high + 1;

            while (true)
            {
                do
                {
                    i++;
                }
                while (arr[i] < pivot);

                do
                {
                    j--;
                }
                while (arr[j] > pivot);

                if (i >= j)
                    return j;

                Swap(ref arr[i], ref arr[j]);
            }
        }

        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public static int[] GenerateRand(int n)
        {
            int[] arr = new int[n];
            
            for (int i = 0; i < n; i++)
            {
                arr[i] = rand.Next(1000);
            }

            return arr;
        }

        public static int[] GenerateDec(int n)
        {
            int[] arr = new int[n];

            for (int i = 0; i < n; i++)
            {
                arr[i] = n - i;
            }

            return arr;
        }

        public static int[] GenerateAsc(int n)
        {
            int[] arr = new int[n];

            for (int i = 0; i < n; i++)
            {
                arr[i] = i;
            }

            return arr;
        }
    }
}
