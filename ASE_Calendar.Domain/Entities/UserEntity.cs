using ASE_Calendar.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.IO;

namespace ASE_Calendar.Domain.Entities
{
    public class UserEntity
    {
        public UserData UserDataRegistered { get; init; }
        public UserID userId { get; init; }
        
        public UserEntity(string username, string password, int roleId, Guid userIdGuid)
        {
            this.userId = new UserID(userIdGuid);
            UserDataRegistered = new UserData(username, password, roleId);

        }
       
        public override bool Equals(object obj)
        {
            return obj is UserEntity entity &&
                   EqualityComparer<UserData>.Default.Equals(UserDataRegistered, entity.UserDataRegistered);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserDataRegistered);
        }
    }
}