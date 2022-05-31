using System;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers.ConsoleColor
{
    /// <summary>
    ///     Contains methods to output colored text to the console.
    /// </summary>
    public class ConsoleColorGreen : ConsoleColorWriter
    {
        public override void Write(String input)
        {
            Console.ForegroundColor = System.ConsoleColor.Green;
            Console.Write(input);
            Console.ResetColor();
        }

        public override void WriteLine(String input)
        {
            Console.ForegroundColor = System.ConsoleColor.Green;
            Console.WriteLine(input);
            Console.ResetColor();
        }
    }
}