using System;
using System.IO;
using ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers;
using ASE_Calendar.ConsoleUI.ConsoleOptions.Managers;
using ASE_Calendar.ConsoleUI.ConsoleOptions.Managers.Appointment;
using ASE_Calendar.ConsoleUI.Enums;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions
{
    public class HandleStateMachine
    {
        private readonly State _state = State.RegisteredCheck;

        public void StartStateMachine()
        {
            var selectedTime = DateTime.Now;
            UserEntity currentUser = null;
            var auth = new AuthentificationManager();
            var infoHelper = new InfoHelper();

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

                    var calendar = new CalendarManager(selectedTime, currentUser);
                    Console.Clear();
                    calendar.CreateCalendar();

                    Console.WriteLine("\nCurrent month: arrow up");
                    Console.WriteLine("Previous month: left arrow | Next month: right arrow\n");

                    if (currentUser.UserDataRegistered.RoleId == 2)
                    {
                        Console.WriteLine("Book an appointment: 1 | Show my appointments: 2");
                    }

                    if (currentUser.UserDataRegistered.RoleId == 1)
                    {
                        Console.WriteLine("Delete an appointment: 3 | Show all appointments: 4");
                        Console.WriteLine("Change Description of an appointment: 5 | Change date of an appointment: 6");
                    }

                    if (currentUser.UserDataRegistered.RoleId == 0)
                    {
                        Console.WriteLine("Book an appointment: 1 | Show my appointments: 2");
                        Console.WriteLine("Delete an appointment: 3 | Show all appointments: 4");
                        Console.WriteLine("Change Description of an appointment: 5 | Change date of an appointment: 6");
                    }

                    Console.WriteLine("\nLogout: l/L | Exit application: e/E | Information: i/I");

                    var input = Console.ReadKey();

                    if (input.Key == ConsoleKey.UpArrow)
                    {
                        Console.Clear();
                        selectedTime = calendar.CreateCalendarCurrentMonth();
                    }

                    if (input.Key == ConsoleKey.LeftArrow)
                    {
                        Console.Clear();
                        selectedTime = calendar.CreateCalendarPrevMonth(selectedTime);
                    }

                    if (input.Key == ConsoleKey.RightArrow)
                    {
                        Console.Clear();
                        selectedTime = calendar.CreateCalendarNextMonth(selectedTime);
                    }

                    //Create appointment
                    if (input.Key == ConsoleKey.D1 && currentUser.UserDataRegistered.RoleId is 2 or 0)
                    {
                        new CreateAppointment(currentUser, selectedTime);
                    }

                    //Read appointments from specific user
                    if (input.Key == ConsoleKey.D2 && currentUser.UserDataRegistered.RoleId is 2 or 0)
                    {
                        LoadAppointment getAppointments = new LoadAppointment(currentUser);
                        getAppointments.LoadAppointments();
                    }

                    //Delete appointment
                    if (input.Key == ConsoleKey.D3 && currentUser.UserDataRegistered.RoleId is 1 or 0)
                    {
                        new DeleteAppointment();
                    }

                    //Read appointments from all users
                    if (input.Key == ConsoleKey.D4 && currentUser.UserDataRegistered.RoleId is 1 or 0)
                    {
                        LoadAppointment getAppointments = new LoadAppointment(currentUser);
                        getAppointments.LoadAllAppointments();
                    }

                    //Change description of one of all appointments
                    if (input.Key == ConsoleKey.D5 && currentUser.UserDataRegistered.RoleId is 1 or 0)
                    {
                        new ChangeAppointmentDescription();
                    }

                    //Change date of one of all appointments
                    if (input.Key == ConsoleKey.D6 && currentUser.UserDataRegistered.RoleId is 1 or 0)
                    {
                        new ChangeAppointmentDate(selectedTime);
                    }

                    //Show information
                    if (input.Key == ConsoleKey.I)
                    {
                        goto case State.Info;
                    }

                    //Logout current user
                    if (input.Key == ConsoleKey.L)
                    {
                        goto case State.Logout;
                    }

                    //Exit application
                    if (input.Key == ConsoleKey.E)
                    {
                        goto case State.Exit;
                    }

                    Console.Clear();
                    goto case State.CalendarViewer;

                case State.Info:
                    infoHelper.ShowInfo(currentUser);
                    goto case State.CalendarViewer;

                case State.Logout:
                    Console.Clear();
                    currentUser = null;
                    goto case State.RegisteredCheck;

                case State.Exit:
                    Console.Clear();
                    break;
            }
        }
    }
}