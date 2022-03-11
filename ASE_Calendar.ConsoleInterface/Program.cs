using System;
using ASE_Calendar.Application.Services;

namespace ASE_Calendar.ConsoleInterface
{
    class Program
    {
        static void Main()
        {
            DateTime TimeNow = new DateTime();
            TimeNow = DateTime.Now;
            Calendar Calendar = new Calendar(TimeNow);
            AuthentificationService Auth = new AuthentificationService();
            Auth.ResetUserId();
            Auth.StartAuthentification();
            Console.Clear();
            Calendar.CreateCalendarThisMonth();

            var startProgram = true;
            while (startProgram)
            {
                Console.WriteLine("Previous month: 1 | Next month: 2 | Make an Appointment: 3 | Logout: 4 | Close application: 5");
                var input = Console.ReadLine();
                Console.Clear();

                switch (input) 
                {
                    case "1":
                        Console.Clear();
                        Calendar.CreateCalendarPrevMonth();
                        break;
                    case "2":
                        Console.Clear();
                        Calendar.CreateCalendarNextMonth();
                        break;
                    case "3":
                        
                        break;
                    case "4":
                        
                        break;
                    case "5":
                        startProgram = false;
                        break;
                }
            }
        }
    }
}
