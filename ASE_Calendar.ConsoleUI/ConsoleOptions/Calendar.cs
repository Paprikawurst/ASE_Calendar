using System;
using ASE_Calendar.Application.Repositories;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions
{
    public class Calendar
    {
        private readonly UserEntity _currentUser;
        private CheckDateService _checkDate;
        private DateTime _selectedTime;

        public CalendarHelperService CalendarHelperService = new();
        private readonly ConsoleColorHelper colorHelper = new();

        public Calendar(DateTime currentTime, UserEntity currentUser)
        {
            _selectedTime = currentTime;
            _selectedTime = new DateTime(currentTime.Year, currentTime.Month, 1);
            _currentUser = currentUser;
        }

        public void CreateCalendar()
        {
            Console.WriteLine(_selectedTime.ToLongDateString() + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthdayString(_selectedTime.Month) + "\n");
            CreateCalendarHelper();
        }

        public DateTime CreateCalendarCurrentMonth()
        {
            _selectedTime = DateTime.Now;
            Console.WriteLine(_selectedTime.ToLongDateString() + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthdayString(_selectedTime.Month) + "\n");
            CreateCalendarHelper();

            return _selectedTime;
        }

        public DateTime CreateCalendarNextMonth(DateTime test)
        {
            _checkDate = new CheckDateService(test.Year, test.Month + 1);
            _selectedTime = _checkDate.Check();

            Console.WriteLine(_selectedTime.ToLongDateString() + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthdayString(_selectedTime.Month) + "\n");
            CreateCalendarHelper();

            return _selectedTime;
        }

        public DateTime CreateCalendarPrevMonth(DateTime test)
        {
            _checkDate = new CheckDateService(test.Year, test.Month - 1);
            _selectedTime = _checkDate.Check();

            Console.WriteLine(_selectedTime.ToLongDateString() + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthdayString(_selectedTime.Month) + "\n");
            CreateCalendarHelper();

            return _selectedTime;
        }


        private void CreateCalendarHelper()
        {
            AppointmentRepository appointmentRepository = new();
            var appointmentDict =
                appointmentRepository.ReturnAllAppointmentDict(_selectedTime);

            bool first = true;

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
                                            colorHelper.WriteGreen(
                                                CalendarHelperService.TimeSlotToTimeStamp(appointmentDict[i][j]
                                                    .AppointmentData.TimeSlot) +
                                                " " +
                                                appointmentDict[i][j].AppointmentData.Description);
                                        }
                                        else
                                        {
                                            colorHelper.WriteRed(
                                                CalendarHelperService.TimeSlotToTimeStamp(appointmentDict[i][j]
                                                    .AppointmentData.TimeSlot));
                                        }
                                    }
                                    else
                                    {
                                        Console.Write(" | ");
                                        if (appointmentDict[i][j].UserId.Value == _currentUser.UserId.Value)
                                        {
                                            colorHelper.WriteGreen(
                                                CalendarHelperService.TimeSlotToTimeStamp(appointmentDict[i][j]
                                                    .AppointmentData.TimeSlot) +
                                                " " +
                                                appointmentDict[i][j].AppointmentData.Description);
                                        }
                                        else
                                        {
                                            colorHelper.WriteRed(
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