using ASE_Calendar.Classes;
using ASE_Calendar.Classes.Calendar;
using System;

namespace ASE_Calendar
{
    class Program
    {
        static void Main()
        {
            DateTime TimeNow = new DateTime();
            TimeNow = DateTime.Now;
            Calendar Calendar = new Calendar(TimeNow);
            Authentification Auth = new Authentification();
            var startProgramm = true;

        
         
            while (startProgramm)
            {
                Auth.StartAuthentification();
                Calendar.CreateCalendarThisMonth();
                Console.WriteLine("Für vorherigen Monat 1 eingeben, für nächsten Monat 2 und für Abbruch 3.");
                var input = Console.ReadLine();
                   
                switch (input) 
                {
       
                    case "1":
                        Calendar.ClearScreen();
                        Calendar.CreateCalendarPrevMonth();
                        break;
                    case "2":
                        Calendar.ClearScreen();
                        Calendar.CreateCalendarNextMonth();
                        break;
                    case "3":
                        startProgramm = false;
                        break;
                }

            }
        }
    }
}
