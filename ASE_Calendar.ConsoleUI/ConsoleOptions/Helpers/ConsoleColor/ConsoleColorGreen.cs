using System;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers.ConsoleColor
{
    /// <summary>
    ///     Contains methods to output green text to the console.
    /// </summary>
    public class ConsoleColorGreen : ConsoleColorWriter
    {
        public override void Write(string input)
        {
            Console.ForegroundColor = System.ConsoleColor.Green;
            Console.Write(input);
            Console.ResetColor();
        }

        public override void WriteLine(string input)
        {
            Console.ForegroundColor = System.ConsoleColor.Green;
            Console.WriteLine(input);
            Console.ResetColor();
        }
    }
}