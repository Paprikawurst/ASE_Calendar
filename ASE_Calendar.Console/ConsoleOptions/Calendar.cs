using System;
using ASE_Calendar.Application.Services;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions
{
    public class Calendar
    {
        private DateTime _selectedTime;
        private int _monthSelected;
        public CalendarHelperService CalendarHelperService = new();
        private CheckDateService CheckDate;

        public Calendar(DateTime currentTime)
        {
            _selectedTime = currentTime;
            _monthSelected = currentTime.Month;
        }

        public void CreateCalendarCurrentMonth()
        {
            Console.WriteLine(_selectedTime + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthdayString(_selectedTime.Month) + "\n");
            Console.WriteLine(CalendarHelperService.CalendarBuilderDays(_selectedTime));
            
        }

        public DateTime CreateCalendarNextMonth(DateTime test)
        {
            CheckDate = new CheckDateService(test.Year, test.Month + 1);
            _selectedTime = CheckDate.Check();

            Console.WriteLine(_selectedTime + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthdayString(_selectedTime.Month) + "\n");
            Console.WriteLine(CalendarHelperService.CalendarBuilderDays(_selectedTime));
            

            return _selectedTime;
        }

        public DateTime CreateCalendarPrevMonth(DateTime test)
        {

            CheckDate = new CheckDateService(test.Year, test.Month - 1);
            _selectedTime = CheckDate.Check();

            Console.WriteLine(_selectedTime + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthdayString(_selectedTime.Month) + "\n");
            Console.WriteLine(CalendarHelperService.CalendarBuilderDays(_selectedTime));


            return _selectedTime;
        }
    }
}