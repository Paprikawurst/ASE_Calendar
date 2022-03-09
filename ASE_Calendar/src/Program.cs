using ASE_Calendar.Classes;
using ASE_Calendar.Classes.Calendar;
using System;

namespace ASE_Calendar
{
    class Program
    {
        static void Main()
        {
            //TODO: only works on Windows - comment out this line if a PlatformNotSupportedException appears
            Console.SetWindowSize(150, 50);

            DateTime TimeNow = new DateTime();
            TimeNow = DateTime.Now;
            Calendar Calendar = new Calendar(TimeNow);
            Authentification Auth = new Authentification();
            var startProgramm = true;

            Auth.StartAuthentification();
            Calendar.ClearScreen();
          

            while (startProgramm)
            {
                Calendar.CreateCalendarThisMonth();

                //Calendar.ClearScreen();
                Console.WriteLine("Previous month: 1 | Next month: 2 | Close application: 3");
                var input = Console.ReadLine();
                Console.Clear();

                switch (input) 
                {
       
                    case "1":
                        //Calendar.ClearScreen();
                        Console.Clear();
                        Calendar.CreateCalendarPrevMonth();
                        break;
                    case "2":
                        //Calendar.ClearScreen();
                        Console.Clear();
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
