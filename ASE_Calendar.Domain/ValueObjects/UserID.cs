using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Calendar.Domain.ValueObjects
{
    public class UserID
    {
        public Guid Value { get; init; }

        public UserID(Guid value)
        {
            this.Value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is UserID iD &&
                   Value.Equals(iD.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}
