using DataStructuresAndAlgorithms.Algorithms.Sort;
using System;

namespace DataStructuresAndAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new[] { 11, 2, 3 , 0 , -5 , -15 , 0 , 45 , -100};
            SelectionSort.Sort(array);
            Console.ReadLine();
        }
    }
}
