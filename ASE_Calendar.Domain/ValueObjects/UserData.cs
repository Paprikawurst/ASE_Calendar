using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Calendar.Domain.ValueObjects
{
    public class UserData
    {
        public string Username { get; init; }
        public string Password { get; init; }
        public int RoleId { get; init; }

        public UserData(string username, string password, int roleId)
        {
            this.Username = username;
            this.Password = password;
            this.RoleId = roleId;
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
