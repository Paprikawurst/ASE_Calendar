using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Calendar.Domain.ValueObjects
{
    public class AppointmentData
    {
        public DateTime Date { get; init; }
        public int TimeSlot { get; init; }

        public AppointmentData(DateTime Date, int timeSlot)
        {
            this.Date = Date;
            this.TimeSlot = timeSlot;
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
