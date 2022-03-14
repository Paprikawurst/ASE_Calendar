using System;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;
using ASE_Calendar.ConsoleUI.ConsoleOptions;

namespace ASE_Calendar.ConsoleUI
{
    class Program
    {
        private static AppointmentManager appointmentManager;

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

            Console.Clear();
            calendar.CreateCalendarCurrentMonth();

            while (currentUser != null)
            {
                
                Console.WriteLine("Previous month: left arrow | Next month: right arrow | Book an appointment: F1 | Show my appointments: F2 | exit applicatiopn: F3");
                var input = Console.ReadKey();
                
               
                
                switch (input.Key) 
                {
                    case ConsoleKey.LeftArrow:
                        Console.Clear();
                        currentTime = calendar.CreateCalendarPrevMonth(currentTime);
                        
                        break;
                    case ConsoleKey.RightArrow:
                        Console.Clear();
                        currentTime = calendar.CreateCalendarNextMonth(currentTime);
                        
                        break;
                    case ConsoleKey.F1:
                        appointmentManager = new AppointmentManager(currentUser, currentTime);
                        appointmentManager.CreateAppointment();
                        break;
                    case ConsoleKey.F2:
                        appointmentManager = new AppointmentManager(currentUser, currentTime);
                        appointmentManager.LoadAppointments();
                        break;
                    case ConsoleKey.F3:
                        Console.Clear();
                        currentUser = null;
                        break;
                }
            }
        }
    }
}
