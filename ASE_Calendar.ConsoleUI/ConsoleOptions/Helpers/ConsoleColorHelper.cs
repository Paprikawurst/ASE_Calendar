using System;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers
{
    /// <summary>
    ///     Contains methods to output colored text to the console.
    /// </summary>
    public class ConsoleColorHelper
    {
        public void WriteLineGreen(string input)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(input);
            Console.ResetColor();
        }

        public void WriteLineRed(string input)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(input);
            Console.ResetColor();
        }

        public void WriteGreen(string input)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(input);
            Console.ResetColor();
        }

        public void WriteRed(string input)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(input);
            Console.ResetColor();
        }
    }
}