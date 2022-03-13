using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Calendar.Domain.ValueObjects
{
    public class UserID
    {
        public Guid userID { get; init; }

        public UserID()
        {
            userID = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            return obj is UserID iD &&
                   userID.Equals(iD.userID);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(userID);
        }
    }
}
