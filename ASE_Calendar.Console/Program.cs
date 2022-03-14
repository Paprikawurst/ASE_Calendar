using System;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Console
{
    class Program
    {
        static void Main()
        {
            var currentTime = new DateTime();
            currentTime = DateTime.Now;
            var calendar = new Calendar(currentTime);
            UserEntity currentUser = null;
            var auth = new ConsoleOptions.Authentification();

            System.Console.WriteLine("Do you already have an account? Y/N");

            switch (System.Console.ReadLine())
            {
                case "n":
                case "N":
                    auth.StartRegistration();
                    System.Console.Clear();
                    currentUser = auth.StartLogin();
                    break;
                case "y":
                case "Y":
                    currentUser = auth.StartLogin();
                    System.Console.Clear();
                    break;
                default:
                    System.Console.WriteLine("Wrong input!");
                    break;
            }

            while (currentUser != null)
            {
                System.Console.Clear();
                calendar.CreateCalendarCurrentMonth();
                System.Console.WriteLine("Previous month: left arrow | Next month: right arrow | Book an appointment: F1 | Close application: F2");
                var input = System.Console.ReadKey();
                System.Console.Clear();
               
                
                switch (input.Key) 
                {
                    case ConsoleKey.LeftArrow:
                        System.Console.Clear();
                        calendar.CreateCalendarPrevMonth();
                        //PreviousMonth.Show();
                        break;
                    case ConsoleKey.RightArrow:
                        System.Console.Clear();
                        calendar.CreateCalendarNextMonth();
                        //NextMonth.Show();
                        break;
                    case ConsoleKey.F1:
                        //AddAppointment.Create();
                        break;
                    case ConsoleKey.F2:
                        System.Console.Clear();
                        currentUser = null;
                        break;
                }
            }
        }
    }
}
