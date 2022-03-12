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
                System.Console.WriteLine("Previous month: 1 | Next month: 2 | Make an Appointment: 3 | Logout: 4 | Close application: 5");
                var input = System.Console.ReadLine();
                System.Console.Clear();

                switch (input) 
                {
                    case "1":
                        System.Console.Clear();
                        Calendar.CreateCalendarPrevMonth();
                        //PreviousMonth.Show();
                        break;
                    case "2":
                        System.Console.Clear();
                        Calendar.CreateCalendarNextMonth();
                        //NextMonth.Show();
                        break;
                    case "3":
                        //AddAppointment.Create();
                        break;
                    case "4":
                        //Logout();
                        break;
                    case "5":
                        startProgram = false;
                        break;
                }
            }
        }
    }
}
