using ASE_Calendar.Application.Repositories;
using ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers.ConsoleColor;
using ASE_Calendar.Domain.Entities;
using System;
using CalendarHelperService = ASE_Calendar.ConsoleUI.Services.CalendarHelperService;
using CheckDateService = ASE_Calendar.ConsoleUI.Services.CheckDateService;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Managers
{
    /// <summary>
    ///     Manages the calendar output on the console.
    /// </summary>
    public class CalendarManager
    {
        private readonly UserEntity _currentUser;
        private readonly ConsoleColorGreen _consoleColorGreen = new();
        private readonly ConsoleColorRed _consoleColorRed = new();
        private CheckDateService _checkDate;
        private DateTime _selectedTime;

        public CalendarHelperService CalendarHelperService = new();

        public CalendarManager(DateTime currentTime, UserEntity currentUser)
        {
            _selectedTime = currentTime;
            _selectedTime = new DateTime(currentTime.Year, currentTime.Month, 1);
            _currentUser = currentUser;
        }
        /// <summary>
        /// Initially creates the calendar for the current local date and time.
        /// </summary>
        public void CreateCalendar()
        {
            Console.WriteLine(_selectedTime.ToLongDateString() + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthDayString(_selectedTime.Month) + "\n");
            CreateCalendarHelper();
        }
        /// <summary>
        /// Creates the calendar for the current month.
        /// </summary>
        /// <returns>
        /// A DateTime object with the current local date and time.
        /// </returns>
        public DateTime CreateCalendarCurrentMonth()
        {
            _selectedTime = DateTime.Now;
            Console.WriteLine(_selectedTime.ToLongDateString() + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthDayString(_selectedTime.Month) + "\n");
            CreateCalendarHelper();

            return _selectedTime;
        }
        /// <summary>
        /// Uses the input DateTime to create a calendar for the next month.
        /// </summary>
        /// <param name="time"></param>
        /// <returns>
        /// The updated DateTime object
        /// </returns>
        public DateTime CreateCalendarNextMonth(DateTime time)
        {
            _checkDate = new CheckDateService(time.Year, time.Month + 1);
            _selectedTime = _checkDate.AdjustYearAndMonthReturnDateTime();

            Console.WriteLine(_selectedTime.ToLongDateString() + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthDayString(_selectedTime.Month) + "\n");
            CreateCalendarHelper();

            return _selectedTime;
        }

        /// <summary>
        /// Uses the input DateTime to create a calendar for the previous month.
        /// </summary>
        /// <param name="time"></param>
        /// <returns>
        /// The updated DateTime object
        /// </returns>
        public DateTime CreateCalendarPrevMonth(DateTime time)
        {
            _checkDate = new CheckDateService(time.Year, time.Month - 1);
            _selectedTime = _checkDate.AdjustYearAndMonthReturnDateTime();

            Console.WriteLine(_selectedTime.ToLongDateString() + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthDayString(_selectedTime.Month) + "\n");
            CreateCalendarHelper();

            return _selectedTime;
        }

        /// <summary>
        /// Manages the console output with all appointments for a calendar.
        /// </summary>
        private void CreateCalendarHelper()
        {
            AppointmentRepository appointmentRepository = new();
            var appointmentDict =
                appointmentRepository.ReturnAllAppointmentsDictSelectedMonth(_selectedTime);

            var first = true;

            for (var i = 1; i <= CalendarHelperService.GetMaxMonthDayInt(_selectedTime.Month, _selectedTime.Year); i++)
                if (appointmentDict.ContainsKey(i))
                {
                    for (var j = 1; j <= 8; j++)
                    {
                        if (appointmentDict[i].ContainsKey(j))
                        {
                            if (i == appointmentDict[i][j].AppointmentData.Date.Day
                                && appointmentDict[i][j].AppointmentData.Date.Month == _selectedTime.Month
                                && appointmentDict[i][j].AppointmentData.Date.Year == _selectedTime.Year)
                            {
                                if (appointmentDict[i].ContainsKey(j))
                                {
                                    if (first)
                                    {
                                        first = false;

                                        Console.Write(i + ": ");

                                        if (appointmentDict[i][j].UserId.Value == _currentUser.UserId.Value)
                                        {
                                            _consoleColorGreen.Write(
                                                CalendarHelperService.TimeSlotToTimeStamp(appointmentDict[i][j]
                                                    .AppointmentData.TimeSlot) +
                                                " " +
                                                appointmentDict[i][j].AppointmentData.Description);
                                        }
                                        else
                                        {
                                            _consoleColorRed.Write(
                                                CalendarHelperService.TimeSlotToTimeStamp(appointmentDict[i][j]
                                                    .AppointmentData.TimeSlot));
                                        }
                                    }
                                    else
                                    {
                                        Console.Write(" | ");
                                        if (appointmentDict[i][j].UserId.Value == _currentUser.UserId.Value)
                                        {
                                            _consoleColorGreen.Write(
                                                CalendarHelperService.TimeSlotToTimeStamp(appointmentDict[i][j]
                                                    .AppointmentData.TimeSlot) +
                                                " " +
                                                appointmentDict[i][j].AppointmentData.Description);
                                        }
                                        else
                                        {
                                            _consoleColorRed.Write(
                                                CalendarHelperService.TimeSlotToTimeStamp(appointmentDict[i][j]
                                                    .AppointmentData.TimeSlot));
                                        }
                                    }
                                }
                            }
                        }

                        if (j == 8)
                        {
                            Console.Write("\n");
                            first = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine(i + ":");
                }
        }
    }
}