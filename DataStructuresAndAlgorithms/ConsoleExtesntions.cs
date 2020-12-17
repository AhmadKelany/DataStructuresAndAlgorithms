using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    public static class Screen
    {
        public static void Write(string text , ConsoleColor consoleColor = ConsoleColor.White)
        {
            Console.ForegroundColor = consoleColor;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void WriteLine(string text , ConsoleColor consoleColor = ConsoleColor.White)
        {
            Write($"{text}\n", consoleColor);
        }
    }
}
