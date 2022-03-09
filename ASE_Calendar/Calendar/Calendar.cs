using System;

namespace ASE_Calendar.Classes.Calendar
{
    public class Calendar
    {
        private DateTime timeNow;
        private int monthNow;
        CalendarHelper HelperCalendar = new CalendarHelper();

        public Calendar(DateTime timeNow)
        {
            this.timeNow = timeNow;
            this.monthNow = timeNow.Month;

        }

        public void CreateCalendarThisMonth()
        {
            Console.WriteLine(timeNow + "\n");
            Console.WriteLine(HelperCalendar.GetMonthdayString(timeNow.Month) + "\n");
            Console.WriteLine(HelperCalendar.CalendarBuilderDays(timeNow.Month, timeNow.Year));
        }

        public void CreateCalendarNextMonth()
        {
            monthNow += 1;
            Console.WriteLine(timeNow + "\n");
            Console.WriteLine(HelperCalendar.GetMonthdayString(monthNow) + "\n");
            Console.WriteLine(HelperCalendar.CalendarBuilderDays(monthNow, timeNow.Year));

        }

        public void CreateCalendarPrevMonth()
        {
            monthNow -= 1;
            Console.WriteLine(timeNow + "\n");
            Console.WriteLine(HelperCalendar.GetMonthdayString(monthNow) + "\n");
            Console.WriteLine(HelperCalendar.CalendarBuilderDays(monthNow, timeNow.Year));

        }

        public void ClearScreen()
        {
             Console.WriteLine("\n\n");
        }
    }
}