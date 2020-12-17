using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.Algorithms.Sort
{
    public static class SelectionSort
    {
        public static void Sort<T>(T[] array) where T : IComparable
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                Console.WriteLine($"i = {i}");
                int minIndex = i;
                Console.WriteLine($"min index = {minIndex}");
                T minValue = array[i];
                Console.WriteLine($"min value = {minValue}");
                for (int j = i + 1; j < array.Length; j++) 
                {
                    if (array[j].CompareTo(minValue) < 0)
                    {
                        minIndex = j; minValue = array[j];
                    } 
                }
                Swap(array, i, minIndex);
            }
        }

        private static void Swap<T>(T[] array, int first, int second)
        {
            T temp = array[first];
            array[first] = array[second];
            array[second] = temp;
        }
    }
}
