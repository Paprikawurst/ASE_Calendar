using System;
using ASE_Calendar.Application.Services;

namespace ASE_Calendar.Console
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
            System.Console.Clear();
            Calendar.CreateCalendarThisMonth();

            var startProgram = true;
            while (startProgram)
            {
                System.Console.WriteLine("Previous month: Left Arrow | Next month: Right Arrow | Make an Appointment: F1 | Logout: F2 | Close application: F4");
                var input = System.Console.ReadKey();
                System.Console.Clear();
               
                
                switch (input.Key) 
                {
                    case ConsoleKey.LeftArrow:
                        System.Console.Clear();
                        Calendar.CreateCalendarPrevMonth();
                        //PreviousMonth.Show();
                        break;
                    case ConsoleKey.RightArrow:
                        System.Console.Clear();
                        Calendar.CreateCalendarNextMonth();
                        //NextMonth.Show();
                        break;
                    case ConsoleKey.F1:
                        //AddAppointment.Create();
                        break;
                    case ConsoleKey.F2:
                        //Logout();
                        break;
                    case ConsoleKey.F4:
                        startProgram = false;
                        break;
                }
            }
        }
    }
}
