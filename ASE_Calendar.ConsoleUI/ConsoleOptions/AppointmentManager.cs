using System;
using System.Text.RegularExpressions;
using ASE_Calendar.Application.Services;
using ASE_Calendar.ConsoleUI.Enums;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions
{
    public class AppointmentManager
    {
        private AppointmentState _appointmentState;
        private readonly ConsoleColorHelper colorHelper = new();
        public DateTime CurrentTime = DateTime.Now;
        public UserEntity CurrentUser;
        public DateTime DateSelected;

        public AppointmentManager(UserEntity currentUser, DateTime dateSelected)
        {
            CurrentUser = currentUser;
            DateSelected = dateSelected;
        }

        public DateTime Date { get; set; }

        public void CreateAppointment()
        {
            var day = "";
            var timeSlot = "";
            var description = "";

            _appointmentState = AppointmentState.UserInputDay;

            switch (_appointmentState)
            {
                case AppointmentState.UserInputDay:

                    Console.WriteLine("Please enter a Day of the current month");
                    day = Console.ReadLine();
                    goto case AppointmentState.CheckInputDay;

                case AppointmentState.UserInputTimeSlot:

                    Console.WriteLine(
                        "Please select a timeslot wich is free on the selected day:\n08:00 - 09:00 = 1\n09:00 - 10:00 = 2\n10:00 - 11:00 = 3\n11:00 - 12:00 = 4\n13:00 - 14:00 = 5\n14:00 - 15:00 = 6\n15:00 - 16:00 = 7\n16:00 - 17:00 = 8\n");
                    timeSlot = Console.ReadLine();
                    goto case AppointmentState.CheckInputTimeSlot;

                case AppointmentState.CheckInputDay:

                    var isNumber = Regex.IsMatch(day, @"^[0-9]*$");
                    var maxDays = CalendarHelperService.GetMaxMonthDayInt(DateSelected.Month, DateSelected.Year);
                    
                    if (!isNumber || day == "" || short.Parse(day) <= 0 || short.Parse(day) > maxDays)
                    {
                        colorHelper.WriteLineRed("\n" + "Please enter the correct day!" + "\n");
                        goto case AppointmentState.UserInputDay;
                    }

                    goto case AppointmentState.UserInputTimeSlot;

                case AppointmentState.CheckInputTimeSlot:

                    isNumber = Regex.IsMatch(timeSlot, @"[1-8]");
                    

                    if (!isNumber || timeSlot == "" ||
                        !AppointmentService.CheckIfTimeSlotIsFree(DateSelected, short.Parse(timeSlot), short.Parse(day)))
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

            Date = new DateTime(DateSelected.Year, DateSelected.Month, int.Parse(day));

            AppointmentEntity appointment =
                new(Date, int.Parse(timeSlot), CurrentUser.UserId, Guid.NewGuid(), description);
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
                colorHelper.WriteLineRed("You don't have any appointments at the moment!");
                colorHelper.WriteLineRed("Any key to continue!");
                Console.ReadLine();
            }
        }

        public void LoadAllAppointments()
        {
            Console.WriteLine("All Appointments:\n");
            var appointmentData = AppointmentService.LoadAllAppointments();

            if (appointmentData != null)
            {
                Console.WriteLine(appointmentData);
                Console.ReadLine();
            }
            else
            {
                colorHelper.WriteLineRed("There are no appointments booked at the moment!");
                colorHelper.WriteLineRed("Any key to continue!");
                Console.ReadLine();
            }
        }

        public void DeleteAnAppointment()
        {
            Console.WriteLine("Enter the appointment Id you want to delete:");
            var appointmentIdString = Console.ReadLine();
            var appointmentIdGuid = Guid.Parse(appointmentIdString);
            var appointmentData = AppointmentService.DeleteAnAppointment(appointmentIdGuid);

            if (appointmentData != null)
            {
                colorHelper.WriteLineGreen("The appointment has been deleted!");
                colorHelper.WriteLineGreen("Any key to continue!");
                Console.ReadLine();
            }
            else
            {
                colorHelper.WriteLineRed("There are no appointments booked at the moment!");
                colorHelper.WriteLineRed("Any key to continue!");
                Console.ReadLine();
            }
        }

        public void ChangeDescriptionOfAnAppointment()
        {
            Console.WriteLine("Enter the appointment Id you want to change:");
            var appointmentIdString = Console.ReadLine();
            Console.WriteLine("Enter the new description:");
            var appointmentDescription = Console.ReadLine();
            var appointmentIdGuid = Guid.Parse(appointmentIdString);
            var appointmentData = AppointmentService.ChangeDescription(appointmentIdGuid, appointmentDescription);

            if (appointmentData != null)
            {
                colorHelper.WriteLineGreen("The appointment has been edited!");
                colorHelper.WriteLineGreen("Any key to continue!");
                Console.ReadLine();
            }
            else
            {
                colorHelper.WriteLineRed("There are no appointments booked at the moment!");
                colorHelper.WriteLineRed("Any key to continue!");
                Console.ReadLine();
            }
        }

        public void ChangeDateOfAnAppointment()
        {
            Console.WriteLine("Enter the appointment Id you want to change:");
            var appointmentIdString = Console.ReadLine();
            Console.WriteLine("Enter the new day:");
            var appointmentDay = Console.ReadLine();
            Console.WriteLine("Enter the new month:");
            var appointmentMonth = Console.ReadLine();
            Console.WriteLine("Enter the new year:");
            var appointmentYear = Console.ReadLine();

            var newDate = new DateTime(short.Parse(appointmentYear), short.Parse(appointmentMonth),
                short.Parse(appointmentDay));
            var appointmentIdGuid = Guid.Parse(appointmentIdString);
            var appointmentData = AppointmentService.ChangeDate(appointmentIdGuid, newDate);

            if (appointmentData != null)
            {
                colorHelper.WriteLineGreen("The appointment has been edited!");
                colorHelper.WriteLineGreen("Any key to continue!");
                Console.ReadLine();
            }
            else
            {
                colorHelper.WriteLineRed("There are no appointments booked at the moment!");
                colorHelper.WriteLineRed("Any key to continue!");
                Console.ReadLine();
            }
        }
    }
}