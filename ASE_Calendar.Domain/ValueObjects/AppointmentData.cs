using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Calendar.Domain.ValueObjects
{
    public class AppointmentData
    {
        public DateTime DateAndTime { get; init; }
       
        public AppointmentData(DateTime DateAndTime)
        {
            this.DateAndTime = DateAndTime;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
