using System;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions
{
    public class Calendar
    {
        private DateTime _selectedTime;
        
        public CalendarHelperService CalendarHelperService = new();
        private CheckDateService CheckDate;
        readonly UserEntity currentUser;

        public Calendar(DateTime currentTime, UserEntity currentUser)
        {
            _selectedTime = currentTime;
            this.currentUser = currentUser;
        }

        public void CreateCalendarCurrentMonth()
        {
            Console.WriteLine(_selectedTime + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthdayString(_selectedTime.Month) + "\n");
            Console.WriteLine(CalendarHelperService.CalendarBuilderDays(_selectedTime, currentUser));
            
        }

        public DateTime CreateCalendarNextMonth(DateTime test)
        {
            CheckDate = new CheckDateService(test.Year, test.Month + 1);
            _selectedTime = CheckDate.Check();

            Console.WriteLine(_selectedTime + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthdayString(_selectedTime.Month) + "\n");
            Console.WriteLine(CalendarHelperService.CalendarBuilderDays(_selectedTime, currentUser));
            

            return _selectedTime;
        }

        public DateTime CreateCalendarPrevMonth(DateTime test)
        {

            CheckDate = new CheckDateService(test.Year, test.Month - 1);
            _selectedTime = CheckDate.Check();

            Console.WriteLine(_selectedTime + "\n");
            Console.WriteLine(CalendarHelperService.GetMonthdayString(_selectedTime.Month) + "\n");
            Console.WriteLine(CalendarHelperService.CalendarBuilderDays(_selectedTime, currentUser));


            return _selectedTime;
        }
    }
}