using System;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;
using ASE_Calendar.ConsoleUI.ConsoleOptions;

namespace ASE_Calendar.ConsoleUI
{
    class Program
    {
        static void Main()
        {
            var currentTime = new DateTime();
            currentTime = DateTime.Now;
            var calendar = new Calendar(currentTime);
            UserEntity currentUser = null;
            var auth = new Authentification();

            Console.WriteLine("Do you already have an account? Y/N");

            switch (Console.ReadLine())
            {
                case "n":
                case "N":
                    auth.StartRegistration();
                    Console.Clear();
                    currentUser = auth.StartLogin();
                    break;
                case "y":
                case "Y":
                    currentUser = auth.StartLogin();
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Wrong input!");
                    break;
            }

            while (currentUser != null)
            {
                Console.Clear();
                calendar.CreateCalendarCurrentMonth();
                Console.WriteLine("Previous month: left arrow | Next month: right arrow | Book an appointment: F1 | Close application: F2");
                var input = Console.ReadKey();
                Console.Clear();
               
                
                switch (input.Key) 
                {
                    case ConsoleKey.LeftArrow:
                        Console.Clear();
                        calendar.CreateCalendarPrevMonth();
                        //PreviousMonth.Show();
                        break;
                    case ConsoleKey.RightArrow:
                        Console.Clear();
                        calendar.CreateCalendarNextMonth();
                        //NextMonth.Show();
                        break;
                    case ConsoleKey.F1:
                        AddAppointment addAppointment = new AddAppointment();
                        addAppointment.Create();
                        break;
                    case ConsoleKey.F2:
                        Console.Clear();
                        currentUser = null;
                        break;
                }
            }
        }
    }
}
