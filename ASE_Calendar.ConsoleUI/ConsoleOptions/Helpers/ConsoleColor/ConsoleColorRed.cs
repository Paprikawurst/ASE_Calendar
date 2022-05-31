using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers
{
    public class ConsoleColorRed : ConsoleColorWriter
    {
        public override void Write(String input)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(input);
            Console.ResetColor();
        }

        public override void WriteLine(String input)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(input);
            Console.ResetColor();
        }
    }
}
