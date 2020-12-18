using System;
using System.Text;

namespace DataStructuresAndAlgorithms.Algorithms.Sort
{
    public static class ArrayHelper
    {
        public static void Swap<T>(T[] array, int first, int second) where T : IComparable
        {
            T temp = array[first];
            array[first] = array[second];
            array[second] = temp;
        }
        public static string DisplayArray<T>(T[] array)
        {
            StringBuilder sb = new StringBuilder();
            foreach (T item in array)
            {
                sb.Append(string.Format("{0,4}", $"{item}"));
            }
            return sb.ToString();
        }

    }
}
