using System;
using System.IO;
using ASE_Calendar.ConsoleUI.Enums;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions
{
    public class HandleStateMachine
    {
        private static AppointmentManager _appointmentManager;

        private readonly State _state = State.RegisteredCheck;

        public void StartStateMachine()
        {
            var selectedTime = DateTime.Now;
            UserEntity currentUser = null;
            var auth = new Authentication();
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

                    var calendar = new Calendar(selectedTime, currentUser);
                    Console.Clear();
                    calendar.CreateCalendar();

                    Console.WriteLine("\nCurrent month: arrow up");
                    Console.WriteLine("Previous month: left arrow | Next month: right arrow");
                    if (currentUser.UserDataRegistered.RoleId == 2)
                    {
                        Console.WriteLine("Book an appointment: 1 | Show my appointments: 2");
                    }

                    if (currentUser.UserDataRegistered.RoleId == 1)
                    {
                        Console.WriteLine(
                            "Change Description of an appointment: 5 | Change date of an appointments: 6");
                    }

                    if (currentUser.UserDataRegistered.RoleId == 0)
                    {
                        Console.WriteLine("Book an appointment: 1 | Show my appointments: 2");
                        Console.WriteLine("Delete an appointment: 3 | Show all appointments: 4");
                        Console.WriteLine(
                            "Change Description of an appointment: 5 | Change date of an appointments: 6");
                    }

                    Console.WriteLine("Logout: l/L | Exit application: e/E | Information: i/I");

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

                    if (input.Key == ConsoleKey.D1 && currentUser.UserDataRegistered.RoleId is 2 or 0)
                    {
                        _appointmentManager = new AppointmentManager(currentUser, selectedTime);
                        _appointmentManager.CreateAppointment();
                    }

                    if (input.Key == ConsoleKey.D2 && currentUser.UserDataRegistered.RoleId is 2 or 0)
                    {
                        _appointmentManager = new AppointmentManager(currentUser, selectedTime);
                        _appointmentManager.LoadAppointments();
                    }

                    if (input.Key == ConsoleKey.D3 && currentUser.UserDataRegistered.RoleId == 0)
                    {
                        _appointmentManager = new AppointmentManager(currentUser, selectedTime);
                        _appointmentManager.DeleteAnAppointment(selectedTime);
                    }

                    if (input.Key == ConsoleKey.D4 && currentUser.UserDataRegistered.RoleId == 0)
                    {
                        _appointmentManager = new AppointmentManager(currentUser, selectedTime);
                        _appointmentManager.LoadAllAppointments();
                    }

                    if (input.Key == ConsoleKey.D5 && currentUser.UserDataRegistered.RoleId is 1 or 0)
                    {
                        _appointmentManager = new AppointmentManager(currentUser, selectedTime);
                        _appointmentManager.ChangeDescriptionOfAnAppointment();
                    }

                    if (input.Key == ConsoleKey.D6 && currentUser.UserDataRegistered.RoleId is 1 or 0)
                    {
                        _appointmentManager = new AppointmentManager(currentUser, selectedTime);
                        _appointmentManager.ChangeDateOfAnAppointment();
                    }

                    if (input.Key == ConsoleKey.I)
                    {
                        goto case State.Info;
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

                case State.Info:
                    //TODO: Infotext schreiben
                    var infoHelper = new InfoHelper();
                    infoHelper.ShowInfo(currentUser);
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