using ASE_Calendar.Application.Repositories;
using ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers.ConsoleColor;
using ASE_Calendar.ConsoleUI.Enums;
using ASE_Calendar.Domain.Entities;
using System;
using System.Text.RegularExpressions;
using AppointmentService = ASE_Calendar.ConsoleUI.Services.AppointmentService;
using CalendarHelperService = ASE_Calendar.ConsoleUI.Services.CalendarHelperService;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Managers.Appointment
{
    /// <summary>
    ///     A class which starts the process on the ui to create an appointment.
    /// </summary>
    public class CreateAppointment
    {
        private readonly ConsoleColorRed _consoleColorRed = new();
        public UserEntity CurrentUser;
        public DateTime DateSelected;

        public CreateAppointment(UserEntity currentUser, DateTime dateSelected)
        {
            CurrentUser = currentUser;
            DateSelected = dateSelected;

            var day = "";
            var timeSlot = "";
            var description = "";

            var appointmentState = CreateAppointmentState.UserInputDay;

            switch (appointmentState)
            {
                case CreateAppointmentState.UserInputDay:

                    Console.WriteLine("\nPlease enter a Day of the current month");
                    day = Console.ReadLine();
                    goto case CreateAppointmentState.CheckInputDay;

                case CreateAppointmentState.UserInputTimeSlot:

                    Console.WriteLine(
                        "Please select a timeslot which is free on the selected day:\n08:00 - 09:00 = 1\n09:00 - 10:00 = 2\n10:00 - 11:00 = 3\n11:00 - 12:00 = 4\n13:00 - 14:00 = 5\n14:00 - 15:00 = 6\n15:00 - 16:00 = 7\n16:00 - 17:00 = 8\n");
                    timeSlot = Console.ReadLine();
                    goto case CreateAppointmentState.CheckInputTimeSlot;

                case CreateAppointmentState.CheckInputDay:

                    var isNumber = Regex.IsMatch(day, @"^[0-9]*$");
                    var maxDays = CalendarHelperService.GetMaxMonthDayInt(DateSelected.Month, DateSelected.Year);

                    if (!isNumber || day == "" || short.Parse(day) <= 0 || short.Parse(day) > maxDays)
                    {
                        _consoleColorRed.WriteLine("\n" + "Please enter the correct day!" + "\n");
                        goto case CreateAppointmentState.UserInputDay;
                    }

                    goto case CreateAppointmentState.UserInputTimeSlot;

                case CreateAppointmentState.CheckInputTimeSlot:

                    isNumber = Regex.IsMatch(timeSlot, @"[1-8]");


                    if (!isNumber || timeSlot == "" ||
                        !AppointmentService.CheckIfTimeSlotIsFree(DateSelected, short.Parse(timeSlot),
                            short.Parse(day)))
                    {
                        _consoleColorRed.WriteLine("\n" + "Please enter a correct time slot!" + "\n");
                        goto case CreateAppointmentState.UserInputTimeSlot;
                    }

                    goto case CreateAppointmentState.UserInputDescription;

                case CreateAppointmentState.UserInputDescription:

                    Console.WriteLine("Please enter a description of your appointment!. (Max 25 tokens)");
                    description = Console.ReadLine();

                    goto case CreateAppointmentState.CheckInputDescription;

                case CreateAppointmentState.CheckInputDescription:

                    if (description.Length > 25 || description == "" || string.IsNullOrWhiteSpace(description))
                    {
                        _consoleColorRed.WriteLine("Wrong input! Enter more than 0 and less than 25 signs!");
                        goto case CreateAppointmentState.UserInputDescription;
                    }

                    break;
            }

            var date = new DateTime(DateSelected.Year, DateSelected.Month, int.Parse(day));

            AppointmentEntity appointment =
                new(date, int.Parse(timeSlot), CurrentUser.UserId, Guid.NewGuid(), description);

            var appointmentRepository = new AppointmentRepository();
            appointmentRepository.CreateAppointment(appointment);
        }
    }
}