using System;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers.ConsoleColor
{
    public class ConsoleColorRed : ConsoleColorWriter
    {
        public override void Write(String input)
        {
            Console.ForegroundColor = System.ConsoleColor.Red;
            Console.Write(input);
            Console.ResetColor();
        }

        public override void WriteLine(String input)
        {
            Console.ForegroundColor = System.ConsoleColor.Red;
            Console.WriteLine(input);
            Console.ResetColor();
        }
    }
}
