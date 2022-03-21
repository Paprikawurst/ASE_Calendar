using System;
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

        public Calendar(DateTime currentTime, UserEntity currentUser)
        {
            _selectedTime = currentTime;
            _currentUser = currentUser;
        }

        public void CreateCalendarCurrentMonth()
        {
            Console.WriteLine(_selectedTime + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthdayString(_selectedTime.Month) + "\n");
            Console.WriteLine(CalendarHelperService.CalendarBuilderDays(_selectedTime, _currentUser));
        }

        public DateTime CreateCalendarNextMonth(DateTime test)
        {
            _checkDate = new CheckDateService(test.Year, test.Month + 1);
            _selectedTime = _checkDate.Check();

            Console.WriteLine(_selectedTime + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthdayString(_selectedTime.Month) + "\n");
            Console.WriteLine(CalendarHelperService.CalendarBuilderDays(_selectedTime, _currentUser));


            return _selectedTime;
        }

        public DateTime CreateCalendarPrevMonth(DateTime test)
        {
            _checkDate = new CheckDateService(test.Year, test.Month - 1);
            _selectedTime = _checkDate.Check();

            Console.WriteLine(_selectedTime + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthdayString(_selectedTime.Month) + "\n");
            Console.WriteLine(CalendarHelperService.CalendarBuilderDays(_selectedTime, _currentUser));


            return _selectedTime;
        }
    }
}