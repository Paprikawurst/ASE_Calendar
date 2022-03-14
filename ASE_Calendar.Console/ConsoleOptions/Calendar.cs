using System;
using ASE_Calendar.Application.Services;
namespace ASE_Calendar.ConsoleUI.ConsoleOptions
{
    public class Calendar
    {
        private DateTime _currentTime;
        private int _currentMonth;
        public CalendarHelperService CalendarHelperService = new();

        public Calendar(DateTime currentTime)
        {
            _currentTime = currentTime;
            _currentMonth = currentTime.Month;
        }

        public void CreateCalendarCurrentMonth()
        {
            Console.WriteLine(_currentTime + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthdayString(_currentTime.Month) + "\n");
            Console.WriteLine(CalendarHelperService.CalendarBuilderDays(_currentTime.Month, _currentTime.Year));
        }

        public void CreateCalendarNextMonth()
        {
            _currentMonth += 1;
            Console.WriteLine(_currentTime + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthdayString(_currentMonth) + "\n");
            Console.WriteLine(CalendarHelperService.CalendarBuilderDays(_currentMonth, _currentTime.Year));

        }

        public void CreateCalendarPrevMonth()
        {
            _currentMonth -= 1;
            Console.WriteLine(_currentTime + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthdayString(_currentMonth) + "\n");
            Console.WriteLine(CalendarHelperService.CalendarBuilderDays(_currentMonth, _currentTime.Year));
        }
    }
}