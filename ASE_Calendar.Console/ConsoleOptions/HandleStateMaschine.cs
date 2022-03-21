﻿using System;
using System.IO;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;
using ASE_Calendar.ConsoleUI.ConsoleOptions;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions
{
    public class HandleStateMaschine
    {
        private static AppointmentManager _appointmentManager;
        private enum State
        {
            RegisteredCheck,
            Register,
            Login,
            CalendarViewer,
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

            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json"))
            {
                var fileStream = File.Create(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json", 40000);
                fileStream.Close();
            }

            switch (_state)
            {
                case State.RegisteredCheck:
                    
                    Console.WriteLine("Do you already have an account? Y/N");
                    var userInput = Console.ReadLine();
                   
                    if (userInput is "y" or "Y")
                    {
                        goto case State.Login;
                    }
                    else if (userInput is "n" or "N")
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
                    goto case State.CalendarViewer;

                case State.CalendarViewer:

                    var calendar = new Calendar(currentTime, currentUser);
                    Console.Clear();
                    calendar.CreateCalendarCurrentMonth();

                    
                    Console.WriteLine("Previous month: left arrow | Next month: right arrow");
                    Console.WriteLine("Book an appointment: F1 | Show my appointments: F2");
                    Console.WriteLine("Delete an appointment: F3 | Show all appointments: F4");
                    Console.WriteLine("Change Description of an appointment: F5 | Change date of an appointments: F6");
                    Console.WriteLine("Logout: l/L | Exit application: e/E");
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
                        _appointmentManager = new AppointmentManager(currentUser, currentTime);
                        _appointmentManager.CreateAppointment();
                    }
                    if (input.Key == ConsoleKey.F2)
                    {
                        _appointmentManager = new AppointmentManager(currentUser, currentTime);
                        _appointmentManager.LoadAppointments();
                    }
                    if (input.Key == ConsoleKey.F3)
                    {
                        _appointmentManager = new AppointmentManager(currentUser, currentTime);
                        _appointmentManager.DeleteAnAppointment();
                    }
                    if (input.Key == ConsoleKey.F4)
                    {
                        _appointmentManager = new AppointmentManager(currentUser, currentTime);
                        _appointmentManager.LoadAllAppointments();
                    }
                    if (input.Key == ConsoleKey.F5)
                    {
                        _appointmentManager = new AppointmentManager(currentUser, currentTime);
                        _appointmentManager.ChangeDescriptionOfAnAppointment();
                    }
                    if (input.Key == ConsoleKey.F6)
                    {
                        _appointmentManager = new AppointmentManager(currentUser, currentTime);
                        _appointmentManager.ChangeDateOfAnAppointment();
                    }
                    if (input.Key == ConsoleKey.L)
                    {
                        goto case State.Logout;
                    }
                    if (input.Key == ConsoleKey.E)
                    {
                        goto case State.Exit;
                    }
                    
                    Console.Clear();
                    goto case State.CalendarViewer;

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
