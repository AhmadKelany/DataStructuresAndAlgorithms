using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.Algorithms.Sort
{
    public static class InsertionSort
    {
        public static void Sort<T>(T[] array) where T : IComparable
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(ArrayHelper.DisplayArray(array));
            Screen.WriteLine($"The array now: {ArrayHelper.DisplayArray(array)}");
            for (int i = 1; i < array.Length; i++)
            {
                Screen.WriteLine($"Started outer loop {i} iteration", ConsoleColor.Green);
                Screen.WriteLine($"The array now: {ArrayHelper.DisplayArray(array)}");

                int innerLoopCounter = 1;
                int j = i;
                Screen.WriteLine($"j={j}, array[j]={array[j]}, array[j-1]={array[j - 1]}");
                while (j > 0 && array[j].CompareTo(array[j - 1]) < 0) 
                {
                    Screen.WriteLine($"    Started inner loop {innerLoopCounter} iteration" , ConsoleColor.Cyan);
                    Screen.WriteLine($"    Swab current({array[j]}) and previous({array[j-1]}) items");
                    ArrayHelper.Swap(array, j, j - 1);
                    Screen.WriteLine($"    The array now: {ArrayHelper.DisplayArray(array)}");
                    sb.AppendLine(ArrayHelper.DisplayArray(array));
                    Screen.WriteLine($"    j now ={j}");
                    j--;
                    Screen.WriteLine($"    ended inner loop {innerLoopCounter++} iteration", ConsoleColor.Cyan);
                    Screen.WriteLine($"    =======================================");

                }
                Screen.WriteLine($"ended outer loop {i} iteration", ConsoleColor.Green);
                Screen.WriteLine($"==============================");

            }
            Screen.WriteLine("===========================================", ConsoleColor.DarkMagenta);
            Screen.WriteLine($"All operations:\n{sb.ToString()}" , ConsoleColor.Yellow);
            
        }

    }
}
