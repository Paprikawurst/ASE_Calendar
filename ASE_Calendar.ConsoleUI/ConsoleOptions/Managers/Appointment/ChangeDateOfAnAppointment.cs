using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ASE_Calendar.Application.Repositories;
using ASE_Calendar.Application.Services;
using ASE_Calendar.ConsoleUI.Enums;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Managers.Appointment
{
    public class ChangeDateOfAnAppointment
    {
        private readonly ConsoleColorHelper _colorHelper = new();
        public ChangeDateOfAnAppointment(DateTime selectedTime)
        {
            ChangeDateAppointmentSate changeDateAppointmentSate = ChangeDateAppointmentSate.UserInputId;
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
                case ChangeDateAppointmentSate.UserInputId:

                    ShowAppointmentsOnConsole();
                    Console.WriteLine("Enter the appointment ID you want to change the date:");
                    inputGuidString = Console.ReadLine();
                    goto case ChangeDateAppointmentSate.CheckInputId;

                case ChangeDateAppointmentSate.UserInputDay:

                    Console.WriteLine("Enter the new day:");
                    appointmentDayString = Console.ReadLine();

                    goto case ChangeDateAppointmentSate.CheckInputDay;

                case ChangeDateAppointmentSate.UserInputMonth:

                    Console.WriteLine("Enter the new month:");
                    appointmentMonthString = Console.ReadLine();

                    goto case ChangeDateAppointmentSate.CheckInputMonth;

                case ChangeDateAppointmentSate.UserInputYear:

                    Console.WriteLine("Enter the new year:");
                    appointmentYearString = Console.ReadLine();

                    goto case ChangeDateAppointmentSate.CheckInputYear;

                case ChangeDateAppointmentSate.UserInputTimeSlot:

                    Console.WriteLine(
                        "Please select a timeslot which is free on the selected day:\n08:00 - 09:00 = 1\n09:00 - 10:00 = 2\n10:00 - 11:00 = 3\n11:00 - 12:00 = 4\n13:00 - 14:00 = 5\n14:00 - 15:00 = 6\n15:00 - 16:00 = 7\n16:00 - 17:00 = 8\n");
                    appointmentTimeSlotString = Console.ReadLine();

                    goto case ChangeDateAppointmentSate.CheckInputTimeSlot;

                case ChangeDateAppointmentSate.CheckInputId:

                    bool isValid = Guid.TryParse(inputGuidString, out appointmentGuid);

                    if (isValid)
                    {
                        goto case ChangeDateAppointmentSate.UserInputDay;
                    }

                    _colorHelper.WriteLineRed("Please enter a valid guid!");

                    goto case ChangeDateAppointmentSate.UserInputId;

                case ChangeDateAppointmentSate.CheckInputDay:

                    bool isValidDay = short.TryParse(appointmentDayString, out appointmentDayInt);

                    if (!isValidDay || appointmentDayInt > 31 || appointmentDayInt <= 0)
                    {
                        _colorHelper.WriteLineRed("Please enter a valid day!");
                        goto case ChangeDateAppointmentSate.UserInputDay;
                    }

                    goto case ChangeDateAppointmentSate.UserInputMonth;

                case ChangeDateAppointmentSate.CheckInputMonth:

                    bool isValidMonth = short.TryParse(appointmentMonthString, out appointmentMonthInt);

                    if (!isValidMonth || appointmentMonthInt > 12 || appointmentMonthInt <= 0)
                    {
                        _colorHelper.WriteLineRed("Please enter a valid month!");
                        goto case ChangeDateAppointmentSate.UserInputMonth;
                    }

                    goto case ChangeDateAppointmentSate.UserInputYear;

                case ChangeDateAppointmentSate.CheckInputYear:

                    bool isValidYear = short.TryParse(appointmentYearString, out appointmentYearInt);

                    if (!isValidYear || appointmentYearInt < selectedTime.Year)
                    {
                        _colorHelper.WriteLineRed("Please enter a valid year!");
                        goto case ChangeDateAppointmentSate.UserInputYear;
                    }

                    goto case ChangeDateAppointmentSate.CheckInputDate;

                case ChangeDateAppointmentSate.CheckInputTimeSlot:

                    bool isNumber = Regex.IsMatch(appointmentTimeSlotString, @"[1-8]");

                    if (!isNumber || appointmentTimeSlotString == "")
                    {
                        _colorHelper.WriteLineRed("\n" + "Please enter a correct time slot!" + "\n");
                        goto case ChangeDateAppointmentSate.UserInputTimeSlot;
                    }

                    if (!AppointmentService.CheckIfTimeSlotIsFree(newDateTime, short.Parse(appointmentTimeSlotString), appointmentDayInt))
                    {
                        _colorHelper.WriteLineRed("\n" + "Time slot is occupied!" + "\n");
                        goto case ChangeDateAppointmentSate.UserInputTimeSlot;
                    }

                    goto case ChangeDateAppointmentSate.ChangeDate;


                case ChangeDateAppointmentSate.CheckInputDate:

                    if (CalendarHelperService.GetMaxMonthDayInt(appointmentMonthInt, appointmentYearInt) < appointmentDayInt)
                    {
                        _colorHelper.WriteLineRed("Please enter a valid date!");
                        goto case ChangeDateAppointmentSate.UserInputDay;
                    }

                    newDateTime = new DateTime(appointmentYearInt, appointmentMonthInt, appointmentDayInt);

                    goto case ChangeDateAppointmentSate.UserInputTimeSlot;

                case ChangeDateAppointmentSate.ChangeDate:

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

        public void ShowAppointmentsOnConsole()
        {
            var appointmentRepository = new AppointmentRepository();
            var allAppointments = appointmentRepository.ReturnAllAppointmentsString();

            Console.WriteLine("\n\n" + allAppointments + "\n");
        }
    }
}
