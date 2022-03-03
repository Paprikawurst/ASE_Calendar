using System;

namespace ASE_Calendar.Classes.Calendar
{
    public class Calendar
    {
        public void CreateCalendarThisMonth()
        {
            CalendarHelper HelperCalendar = new CalendarHelper();

            DateTime TimeNow = new DateTime();
            TimeNow = DateTime.Now;
            Console.WriteLine(TimeNow + "\n");

            Console.WriteLine(HelperCalendar.GetMonthdayString(TimeNow.Month) + "\n");



            Console.WriteLine(HelperCalendar.CalendarBuilderDays(TimeNow.Month,TimeNow.Year));
        }
    }


}