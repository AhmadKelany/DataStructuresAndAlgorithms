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
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(DisplayArray(array));
            Screen.WriteLine($"The array now is: {DisplayArray(array)}" , ConsoleColor.Green);
            for (int i = 0; i < array.Length - 1; i++)
            {

                Screen.WriteLine($"Started outer loop {i + 1} iteration" , ConsoleColor.Yellow);
                Screen.WriteLine($"    i = {i}");
                int minIndex = i;
                Screen.WriteLine($"    min index = {minIndex}");
                T minValue = array[i];
                Screen.WriteLine($"    min value = {minValue}");
                Screen.WriteLine($"    array[i] = {array[i]}");
                int innerIterationCounter = 1;
                for (int j = i + 1; j < array.Length; j++) 
                {
                    Screen.WriteLine($"        starting inner loop {innerIterationCounter} iteration" , ConsoleColor.Magenta);
                    Screen.WriteLine($"            j = {j}");
                    Screen.WriteLine($"            array[j] = {array[j]}");
                    Screen.WriteLine($"            compare array[j] ({array[j]}) to min value ({minValue}) : {(array[j].CompareTo(minValue) < 0 ? "array[j] is less than min value" : "array[j] is greater than or equal to min value")}");
                    if (array[j].CompareTo(minValue) < 0)
                    {
                        Screen.WriteLine($"            because array[j] is less than min value, we assign min index to j and min value to array[j]");
                        minIndex = j;
                        minValue = array[j];
                    }
                    Screen.WriteLine($"            now min index = {minIndex}");
                    Screen.WriteLine($"            and min value = {minValue}");
                    Screen.WriteLine($"        ending inner loop {innerIterationCounter++} iteration" , ConsoleColor.Magenta);
                    Console.WriteLine();
                }
                Screen.WriteLine($"        inner loop is finished");
                Screen.WriteLine($"    now we swab the array element at i:({i}) and min index:({minIndex}), their values are {array[i]} and {array[minIndex]}");
                Swap(array, i, minIndex);
                sb.AppendLine(DisplayArray(array));
                Screen.WriteLine($"ended outer loop {i + 1} iteration" , ConsoleColor.Yellow);
                Console.WriteLine();
                Screen.WriteLine($"The array now is: {DisplayArray(array)}" , ConsoleColor.Green);
            }
            Screen.WriteLine($"=========================================");
            Screen.WriteLine($"after outer loop finished");
            Screen.WriteLine($"All the operations:");
            Screen.WriteLine(sb.ToString());
        }
        private static string DisplayArray<T>(T[] array)
        {
            StringBuilder sb = new StringBuilder();
            foreach (T item in array)
            {
                sb.Append(string.Format("{0,4}", $"{item}"));
            }
            return sb.ToString();
        }
        private static void Swap<T>(T[] array, int first, int second)
        {
            T temp = array[first];
            array[first] = array[second];
            array[second] = temp;
        }
    }
}
