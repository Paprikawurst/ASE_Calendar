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
            Domain.Entities.UserEntity currentUser;


            ConsoleOptions.Authentification Auth = new ConsoleOptions.Authentification();

            System.Console.WriteLine("Haben Sie bereits ein Benutzerkonto? Y/N");
            string selection = System.Console.ReadLine();

            if (selection == "n" || selection == "N")
            {
                Auth.StartRegistration();
                System.Console.Clear();
                currentUser = Auth.StartLogin();
            }

            if (selection == "y" || selection == "Y")
            {
                currentUser = Auth.StartLogin();
                System.Console.Clear();
            }
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
