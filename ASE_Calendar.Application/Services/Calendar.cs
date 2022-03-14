using System;

namespace ASE_Calendar.Application.Services
{
    public class Calendar
    {
        private DateTime _currentTime;
        private int _currentMonth;
        public CalendarHelper CalendarHelper = new();

        public Calendar(DateTime currentTime)
        {
            _currentTime = currentTime;
            _currentMonth = currentTime.Month;
        }

        public void CreateCalendarThisMonth()
        {
            Console.WriteLine(_currentTime + "\n");
            Console.WriteLine(CalendarHelper.GetMonthdayString(_currentTime.Month) + "\n");
            Console.WriteLine(CalendarHelper.CalendarBuilderDays(_currentTime.Month, _currentTime.Year));
        }

        public void CreateCalendarNextMonth()
        {
            _currentMonth += 1;
            Console.WriteLine(_currentTime + "\n");
            Console.WriteLine(CalendarHelper.GetMonthdayString(_currentMonth) + "\n");
            Console.WriteLine(CalendarHelper.CalendarBuilderDays(_currentMonth, _currentTime.Year));

        }

        public void CreateCalendarPrevMonth()
        {
            _currentMonth -= 1;
            Console.WriteLine(_currentTime + "\n");
            Console.WriteLine(CalendarHelper.GetMonthdayString(_currentMonth) + "\n");
            Console.WriteLine(CalendarHelper.CalendarBuilderDays(_currentMonth, _currentTime.Year));
        }
    }
}