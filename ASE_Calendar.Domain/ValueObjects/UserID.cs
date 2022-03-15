using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Calendar.Domain.ValueObjects
{
    public class UserID
    {
        public Guid value { get; init; }

        public UserID(Guid value)
        {
            this.value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is UserID iD &&
                   value.Equals(iD.value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(value);
        }
    }
}
