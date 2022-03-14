using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions
{
    public class AddAppointment
    {
        public DateTime Date { get; set; }
        public AddAppointment()
        {
            
        }

        public bool Create()
        {

            Console.WriteLine("Bitte Geben sie ein Datum und einen Zeitsolt ein.");


            return true;
        }
    }
}
