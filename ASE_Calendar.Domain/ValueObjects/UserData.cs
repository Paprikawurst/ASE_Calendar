using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Calendar.Domain.ValueObjects
{
    public class UserData
    {
        public string username { get; init; }
        public string password { get; init; }
        public int roleId { get; init; }

        public UserData(string username, string password, int roleId)
        {
            this.username = username;
            this.password = password;
            this.roleId = roleId;
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
