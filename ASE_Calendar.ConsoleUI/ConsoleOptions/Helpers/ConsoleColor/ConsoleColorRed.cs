using System;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers.ConsoleColor
{
    /// <summary>
    ///     Contains methods to output red text to the console.
    /// </summary>
    public class ConsoleColorRed : ConsoleColorWriter
    {
        public override void Write(string input)
        {
            Console.ForegroundColor = System.ConsoleColor.Red;
            Console.Write(input);
            Console.ResetColor();
        }

        public override void WriteLine(string input)
        {
            Console.ForegroundColor = System.ConsoleColor.Red;
            Console.WriteLine(input);
            Console.ResetColor();
        }
    }
}
