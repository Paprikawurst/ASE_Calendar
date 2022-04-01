using System;
using System.Text.RegularExpressions;
using ASE_Calendar.Application.Repositories;
using ASE_Calendar.Application.Services;
using ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers;
using ASE_Calendar.ConsoleUI.Enums;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Managers.Appointment
{
    /// <summary>
    /// A class which starts the process on the ui to change the date of an appointment.
    /// </summary>
    public class ChangeAppointmentDate
    {
        private readonly ConsoleColorHelper _colorHelper = new();

        public ChangeAppointmentDate(DateTime selectedTime)
        {
            ChangeAppointmentDateState changeDateAppointmentSate = ChangeAppointmentDateState.CheckForAppointments;
            Guid appointmentGuid = Guid.Empty;
            DateTime newDateTime = new DateTime();
            var inputGuidString = "";
            var appointmentDayString = "";
            var appointmentMonthString = "";
            var appointmentYearString = "";
            var appointmentTimeSlotString = "";
            short appointmentDayInt = 0;
            short appointmentMonthInt = 0;
            short appointmentYearInt = 0;

            switch (changeDateAppointmentSate)
            {
                case ChangeAppointmentDateState.CheckForAppointments:

                    AppointmentRepository appointmentRepository = new AppointmentRepository();

                    if (appointmentRepository.ReturnAllAppointmentsString() == null)
                    {
                        _colorHelper.WriteLineRed("\nThere are no appointments at the moment!");
                        _colorHelper.WriteLineRed("Any key to continue.");
                        Console.ReadLine();
                        break;
                    }
                    goto case ChangeAppointmentDateState.UserInputId;

                case ChangeAppointmentDateState.UserInputId:

                    ShowAppointmentsOnConsole();
                    Console.WriteLine("\nEnter the appointment ID you want to change the date:");
                    inputGuidString = Console.ReadLine();
                    goto case ChangeAppointmentDateState.CheckInputId;

                case ChangeAppointmentDateState.UserInputDay:

                    Console.WriteLine("Enter the new day:");
                    appointmentDayString = Console.ReadLine();

                    goto case ChangeAppointmentDateState.CheckInputDay;

                case ChangeAppointmentDateState.UserInputMonth:

                    Console.WriteLine("Enter the new month:");
                    appointmentMonthString = Console.ReadLine();

                    goto case ChangeAppointmentDateState.CheckInputMonth;

                case ChangeAppointmentDateState.UserInputYear:

                    Console.WriteLine("Enter the new year:");
                    appointmentYearString = Console.ReadLine();

                    goto case ChangeAppointmentDateState.CheckInputYear;

                case ChangeAppointmentDateState.UserInputTimeSlot:

                    Console.WriteLine(
                        "Please select a timeslot which is free on the selected day:\n08:00 - 09:00 = 1\n09:00 - 10:00 = 2\n10:00 - 11:00 = 3\n11:00 - 12:00 = 4\n13:00 - 14:00 = 5\n14:00 - 15:00 = 6\n15:00 - 16:00 = 7\n16:00 - 17:00 = 8\n");
                    appointmentTimeSlotString = Console.ReadLine();

                    goto case ChangeAppointmentDateState.CheckInputTimeSlot;

                case ChangeAppointmentDateState.CheckInputId:

                    bool isValid = Guid.TryParse(inputGuidString, out appointmentGuid);

                    if (isValid)
                    {
                        goto case ChangeAppointmentDateState.UserInputDay;
                    }

                    _colorHelper.WriteLineRed("Please enter a valid guid!");

                    goto case ChangeAppointmentDateState.UserInputId;

                case ChangeAppointmentDateState.CheckInputDay:

                    bool isValidDay = short.TryParse(appointmentDayString, out appointmentDayInt);

                    if (!isValidDay || appointmentDayInt > 31 || appointmentDayInt <= 0)
                    {
                        _colorHelper.WriteLineRed("Please enter a valid day!");
                        goto case ChangeAppointmentDateState.UserInputDay;
                    }

                    goto case ChangeAppointmentDateState.UserInputMonth;

                case ChangeAppointmentDateState.CheckInputMonth:

                    bool isValidMonth = short.TryParse(appointmentMonthString, out appointmentMonthInt);

                    if (!isValidMonth || appointmentMonthInt > 12 || appointmentMonthInt <= 0)
                    {
                        _colorHelper.WriteLineRed("Please enter a valid month!");
                        goto case ChangeAppointmentDateState.UserInputMonth;
                    }

                    goto case ChangeAppointmentDateState.UserInputYear;

                case ChangeAppointmentDateState.CheckInputYear:

                    bool isValidYear = short.TryParse(appointmentYearString, out appointmentYearInt);

                    if (!isValidYear || appointmentYearInt < selectedTime.Year)
                    {
                        _colorHelper.WriteLineRed("Please enter a valid year!");
                        goto case ChangeAppointmentDateState.UserInputYear;
                    }

                    goto case ChangeAppointmentDateState.CheckInputDate;

                case ChangeAppointmentDateState.CheckInputTimeSlot:

                    bool isNumber = Regex.IsMatch(appointmentTimeSlotString, @"[1-8]");

                    if (!isNumber || appointmentTimeSlotString == "")
                    {
                        _colorHelper.WriteLineRed("\n" + "Please enter a correct time slot!" + "\n");
                        goto case ChangeAppointmentDateState.UserInputTimeSlot;
                    }

                    if (!AppointmentService.CheckIfTimeSlotIsFree(newDateTime, short.Parse(appointmentTimeSlotString), appointmentDayInt))
                    {
                        _colorHelper.WriteLineRed("\n" + "Time slot is occupied!" + "\n");
                        goto case ChangeAppointmentDateState.UserInputTimeSlot;
                    }

                    goto case ChangeAppointmentDateState.ChangeDate;


                case ChangeAppointmentDateState.CheckInputDate:

                    if (CalendarHelperService.GetMaxMonthDayInt(appointmentMonthInt, appointmentYearInt) < appointmentDayInt)
                    {
                        _colorHelper.WriteLineRed("Please enter a valid date!");
                        goto case ChangeAppointmentDateState.UserInputDay;
                    }

                    newDateTime = new DateTime(appointmentYearInt, appointmentMonthInt, appointmentDayInt);

                    goto case ChangeAppointmentDateState.UserInputTimeSlot;

                case ChangeAppointmentDateState.ChangeDate:

                    var successfulChangeDate = AppointmentService.ChangeDate(appointmentGuid, newDateTime);

                    if (successfulChangeDate)
                    {
                        _colorHelper.WriteLineGreen("The appointment has been edited!");
                        _colorHelper.WriteLineGreen("Any key to continue!");
                        Console.ReadLine();
                    }
                    else
                    {
                        _colorHelper.WriteLineRed("There are no appointments booked at the moment!");
                        _colorHelper.WriteLineRed("Any key to continue!");
                        Console.ReadLine();
                    }
                    break;
            }
        }
        /// <summary>
        /// gets a list of appointment entities from the repository and outputs it to the console
        /// </summary>
        public void ShowAppointmentsOnConsole()
        {
            var appointmentRepository = new AppointmentRepository();
            var allAppointments = appointmentRepository.ReturnAllAppointmentsString();

            Console.WriteLine("\n\n" + allAppointments + "\n");
        }
    }
}
