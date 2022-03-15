using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Calendar.Domain.ValueObjects
{
    public class AppointmentID
    {
        public Guid value { get; init; }

        public AppointmentID(Guid value)
        {
            this.value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is AppointmentID iD &&
                   value.Equals(iD.value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(value);
        }
    }
}
