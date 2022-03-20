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
        public DateTime currentTime = DateTime.Now;
        public UserEntity currentUser;
        public DateTime dateSelected;

        private enum AppointmentState
        {
            userInputDay,
            userInputTimeSlot,
            userInputDescription,
            checkInputDay,
            checkInputTimeSlot,
            checkInputDescription
        };

        private AppointmentState _appointmentState;

        public AppointmentManager(UserEntity currentUser, DateTime dateSelected)
        {
            this.currentUser = currentUser;
            this.dateSelected = dateSelected;
        }

        public void CreateAppointment()
        {
            string day = "";
            var timeSlot = "";
            string description = "";
            ConsoleColorHelper colorHelper = new();

            _appointmentState = AppointmentState.userInputDay;

            switch (_appointmentState)
            {
                case AppointmentState.userInputDay:

                    Console.WriteLine("Please enter a Day of the current month");
                    day = Console.ReadLine();
                    goto case AppointmentState.checkInputDay;

                case AppointmentState.userInputTimeSlot:

                    Console.WriteLine("Please select a timeslot: 8-9 = 1, 9-10 = 2, 10-11 = 3, 11-12 = 4\n13-14 = 5, 14-15 = 6, 15-16 = 7, 16-17 = 8");
                    timeSlot = Console.ReadLine();
                    goto case AppointmentState.checkInputTimeSlot;

                case AppointmentState.checkInputDay:

                    var isNumber = Regex.IsMatch(day, @"^[0-9]*$");
                    var maxDays = CalendarHelperService.GetMaxMonthDayInt(dateSelected.Month, dateSelected.Year);

                    if (!isNumber || Int16.Parse(day) > maxDays || Int16.Parse(day) <= 0 || day == "")
                    {
                        colorHelper.WriteLineRed("\n" + "Please enter the correct day!" + "\n");
                        goto case AppointmentState.userInputDay;
                    }

                    goto case AppointmentState.userInputTimeSlot;

                case AppointmentState.checkInputTimeSlot:

                    isNumber = Regex.IsMatch(timeSlot, @"[1-8]");

                    if (!isNumber || timeSlot == "")
                    {
                        colorHelper.WriteLineRed("\n" + "Please enter a correct time slot!" + "\n");
                        goto case AppointmentState.userInputTimeSlot;
                    }

                    goto case AppointmentState.userInputDescription;

                case AppointmentState.userInputDescription:

                    Console.WriteLine("Please enter a description of your appointment!. (Max 25 tokens)");
                    description = Console.ReadLine();

                    goto case AppointmentState.checkInputDescription;

                case AppointmentState.checkInputDescription:

                    if (description.Length > 25 || description == "")
                    {
                        colorHelper.WriteLineRed("Wrong input! Enter more than 0 and less than 25 signs!");
                        goto case AppointmentState.userInputDescription;
                    }
                    break;
            }

            Date = new DateTime(dateSelected.Year, dateSelected.Month, Int32.Parse(day));
           
            AppointmentEntity Appointment = new(Date, Int32.Parse(timeSlot), currentUser.userId, Guid.NewGuid(), description);
            AppointmentService.CreateAppointment(Appointment);
        }

        public void LoadAppointments()
        {
            Console.WriteLine("Your Appointments:\n");
            var appointmentData = AppointmentService.LoadAppointments(currentUser);

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
    }
}
