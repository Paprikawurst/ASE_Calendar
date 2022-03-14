using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Calendar.Application.Services
{
    public class CheckDateService
    {
        private int year;
        private int month;
        
        public CheckDateService(int year, int month)
        {
            this.year = year;
            this.month = month;
        }

        public DateTime Check()
        {
            if (month > 12)
            {
                year += 1;
                month = 1;
            }

            if (month <= 0)
            {
                year -= 1;
                month = 12;
            }

            return new DateTime(year, month, 1);
        }
    }
}
