using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASE_Calendar.Domain.Entities;
using ASE_Calendar.Application.Services;
using System.Text.RegularExpressions;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions
{
    public class AppointmentManager
    {
        public DateTime Date { get; set; }
        public DateTime CurrentTime = DateTime.Now;
        public UserEntity CurrentUser;
        public DateTime DateSelected;

        private enum AppointmentState
        {
            UserInputDay,
            UserInputTimeSlot,
            UserInputDescription,
            CheckInputDay,
            CheckInputTimeSlot,
            CheckInputDescription
        };

        private AppointmentState _appointmentState;

        public AppointmentManager(UserEntity currentUser, DateTime dateSelected)
        {
            CurrentUser = currentUser;
            DateSelected = dateSelected;
        }

        public void CreateAppointment()
        {
            string day = "";
            var timeSlot = "";
            string description = "";
            ConsoleColorHelper colorHelper = new();

            _appointmentState = AppointmentState.UserInputDay;

            switch (_appointmentState)
            {
                case AppointmentState.UserInputDay:

                    Console.WriteLine("Please enter a Day of the current month");
                    day = Console.ReadLine();
                    goto case AppointmentState.CheckInputDay;

                case AppointmentState.UserInputTimeSlot:

                    Console.WriteLine(
                        "Please select a timeslot: 8-9 = 1, 9-10 = 2, 10-11 = 3, 11-12 = 4\n13-14 = 5, 14-15 = 6, 15-16 = 7, 16-17 = 8");
                    timeSlot = Console.ReadLine();
                    goto case AppointmentState.CheckInputTimeSlot;

                case AppointmentState.CheckInputDay:

                    var isNumber = Regex.IsMatch(day, @"^[0-9]*$");
                    var maxDays = CalendarHelperService.GetMaxMonthDayInt(DateSelected.Month, DateSelected.Year);

                    if (!isNumber || Int16.Parse(day) > maxDays || Int16.Parse(day) <= 0 || day == "")
                    {
                        colorHelper.WriteLineRed("\n" + "Please enter the correct day!" + "\n");
                        goto case AppointmentState.UserInputDay;
                    }

                    goto case AppointmentState.UserInputTimeSlot;

                case AppointmentState.CheckInputTimeSlot:

                    isNumber = Regex.IsMatch(timeSlot, @"[1-8]");

                    if (!isNumber || timeSlot == "")
                    {
                        colorHelper.WriteLineRed("\n" + "Please enter a correct time slot!" + "\n");
                        goto case AppointmentState.UserInputTimeSlot;
                    }

                    goto case AppointmentState.UserInputDescription;

                case AppointmentState.UserInputDescription:

                    Console.WriteLine("Please enter a description of your appointment!. (Max 25 tokens)");
                    description = Console.ReadLine();

                    goto case AppointmentState.CheckInputDescription;

                case AppointmentState.CheckInputDescription:

                    if (description.Length > 25 || description == "")
                    {
                        colorHelper.WriteLineRed("Wrong input! Enter more than 0 and less than 25 signs!");
                        goto case AppointmentState.UserInputDescription;
                    }

                    break;
            }

            Date = new DateTime(DateSelected.Year, DateSelected.Month, Int32.Parse(day));

            AppointmentEntity appointment =
                new(Date, Int32.Parse(timeSlot), CurrentUser.UserId, Guid.NewGuid(), description);
            AppointmentService.CreateAppointment(appointment);
        }

        public void LoadAppointments()
        {
            Console.WriteLine("Your Appointments:\n");
            var appointmentData = AppointmentService.LoadAppointments(CurrentUser);

            if (appointmentData != null)
            {
                Console.WriteLine(appointmentData);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("You don't have any appointments at the moment!");
                Console.WriteLine("Any key to continue!");
                Console.ReadLine();
            }
        }

        public void LoadAllAppointments()
        {

        }

        public void DeleteAnAppointment()
        {

        }

        public void ChangeDescriptionOfAnAppointment()
        {

        }

        public void ChangeDateOfAnAppointment()
        {

        }


    }
}