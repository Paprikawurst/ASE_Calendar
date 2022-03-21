using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Calendar.Application.Services
{
    public class CheckDateService
    {
        private int _year;
        private int _month;

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