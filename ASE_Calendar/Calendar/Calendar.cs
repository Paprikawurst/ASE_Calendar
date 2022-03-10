using System;

namespace ASE_Calendar.Calendar
{
    public class Calendar
    {
        private DateTime _timeNow;
        private int _monthNow;
        CalendarHelper HelperCalendar = new CalendarHelper();

        public Calendar(DateTime timeNow)
        {
            _timeNow = timeNow;
            _monthNow = timeNow.Month;

        }

        public void CreateCalendarThisMonth()
        {
            Console.WriteLine(_timeNow + "\n");
            Console.WriteLine(HelperCalendar.GetMonthdayString(_timeNow.Month) + "\n");
            Console.WriteLine(HelperCalendar.CalendarBuilderDays(_timeNow.Month, _timeNow.Year));
        }

        public void CreateCalendarNextMonth()
        {
            _monthNow += 1;
            Console.WriteLine(_timeNow + "\n");
            Console.WriteLine(HelperCalendar.GetMonthdayString(_monthNow) + "\n");
            Console.WriteLine(HelperCalendar.CalendarBuilderDays(_monthNow, _timeNow.Year));

        }

        public void CreateCalendarPrevMonth()
        {
            _monthNow -= 1;
            Console.WriteLine(_timeNow + "\n");
            Console.WriteLine(HelperCalendar.GetMonthdayString(_monthNow) + "\n");
            Console.WriteLine(HelperCalendar.CalendarBuilderDays(_monthNow, _timeNow.Year));
        }
    }
}