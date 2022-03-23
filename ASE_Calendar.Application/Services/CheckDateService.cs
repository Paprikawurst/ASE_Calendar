using System;

namespace ASE_Calendar.Application.Services
{
    public class CheckDateService
    {
        private int _month;
        private int _year;

        public CheckDateService(int year, int month)
        {
            _year = year;
            _month = month;
        }

        public DateTime Check()
        {
            if (_month > 12)
            {
                _year += 1;
                _month = 1;
            }

            if (_month <= 0)
            {
                _year -= 1;
                _month = 12;
            }

            return new DateTime(_year, _month, 1);
        }
    }
}