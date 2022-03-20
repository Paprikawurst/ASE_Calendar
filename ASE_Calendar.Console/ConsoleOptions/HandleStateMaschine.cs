using System;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;
using ASE_Calendar.ConsoleUI.ConsoleOptions;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions
{
    public class HandleStateMaschine
    {
        private static AppointmentManager appointmentManager;
        private enum State
        {
            RegisteredCheck,
            Register,
            Login,
            Calendarviewer,
            Logout,
            Exit
        }

        private readonly State _state = State.RegisteredCheck;
        public HandleStateMaschine()
        {

        }

        public void StartStateMaschine()
        {
            DateTime currentTime = DateTime.Now;
            UserEntity currentUser = null;
            var auth = new Authentification();
            ConsoleColorHelper colorHelper = new(); 
            

            switch (_state)
            {
                case State.RegisteredCheck:
                    
                    Console.WriteLine("Do you already have an account? Y/N");
                    var userInput = Console.ReadLine();
                   
                    if (userInput == "y" || userInput == "Y")
                    {
                        goto case State.Login;
                    }
                    else if (userInput == "n" || userInput == "N")
                    {
                        goto case State.Register;
                    }
                    Console.Clear();
                    goto case State.RegisteredCheck;

                case State.Register:
                    auth.StartRegistration();
                    Console.Clear();
                    goto case State.Login;

                case State.Login:
                    currentUser = auth.StartLogin();
                    Console.Clear();
                    goto case State.Calendarviewer;

                case State.Calendarviewer:

                    var calendar = new Calendar(currentTime, currentUser);
                    Console.Clear();
                    calendar.CreateCalendarCurrentMonth();

                    
                    Console.WriteLine("Previous month: left arrow | Next month: right arrow | Book an appointment: F1 | Show my appointments: F2 | Logout: F3 | Exit application: F4");
                    var input = Console.ReadKey();

                    if (input.Key == ConsoleKey.LeftArrow)
                    {
                        Console.Clear();
                        currentTime = calendar.CreateCalendarPrevMonth(currentTime);
                    }
                    if (input.Key == ConsoleKey.RightArrow)
                    {
                        Console.Clear();
                        currentTime = calendar.CreateCalendarNextMonth(currentTime);
                    }
                    if (input.Key == ConsoleKey.F1)
                    {
                        appointmentManager = new AppointmentManager(currentUser, currentTime);
                        appointmentManager.CreateAppointment();
                    }
                    if (input.Key == ConsoleKey.F2)
                    {
                        appointmentManager = new AppointmentManager(currentUser, currentTime);
                        appointmentManager.LoadAppointments();
                    }
                    if (input.Key == ConsoleKey.F3)
                    {
                        goto case State.Logout;
                    }
                    if (input.Key == ConsoleKey.F4)
                    {
                        goto case State.Exit;
                    }
                    
                    Console.Clear();
                    goto case State.Calendarviewer;

                case State.Logout:
                    
                    Console.Clear();
                    goto case State.RegisteredCheck;
                    
                case State.Exit:
                    Console.Clear();
                    break;


            }
        }
    }
}
