using System;

namespace ASE_Calendar.Application.Services
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
            System.Console.WriteLine(_currentTime + "\n");
            System.Console.WriteLine(CalendarHelperService.GetMonthdayString(_currentTime.Month) + "\n");
            System.Console.WriteLine(CalendarHelperService.CalendarBuilderDays(_currentTime.Month, _currentTime.Year));
        }

        public void CreateCalendarNextMonth()
        {
            _currentMonth += 1;
            System.Console.WriteLine(_currentTime + "\n");
            System.Console.WriteLine(CalendarHelperService.GetMonthdayString(_currentMonth) + "\n");
            System.Console.WriteLine(CalendarHelperService.CalendarBuilderDays(_currentMonth, _currentTime.Year));

        }

        public void CreateCalendarPrevMonth()
        {
            _currentMonth -= 1;
            System.Console.WriteLine(_currentTime + "\n");
            System.Console.WriteLine(CalendarHelperService.GetMonthdayString(_currentMonth) + "\n");
            System.Console.WriteLine(CalendarHelperService.CalendarBuilderDays(_currentMonth, _currentTime.Year));
        }
    }
}