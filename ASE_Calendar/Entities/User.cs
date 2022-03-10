using System;
using System.IO;

namespace ASE_Calendar.Entities
{
    public class User
    {
        public string userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }


        public User(string username, string password, string role)
        {
            this.username = username;
            this.password = password;
            this.role = role;
            userId = AddUserId();
        }

        private string AddUserId()
        {
            string userId = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUserIds.txt");

            int newUserId = Int16.Parse(userId) + 1;
      
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUserIds.txt", newUserId.ToString());

            return userId;

        }
    }
}