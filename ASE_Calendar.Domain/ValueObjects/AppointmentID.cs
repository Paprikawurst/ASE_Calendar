using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Calendar.Domain.ValueObjects
{
    public class AppointmentId
    {
        public Guid Value { get; init; }

        public AppointmentId(Guid value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is AppointmentId iD &&
                   Value.Equals(iD.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}
