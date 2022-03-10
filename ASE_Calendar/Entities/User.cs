namespace ASE_Calendar.Entities
{
    public class User
    {
        private static int instancesCreated = 0;
        public string userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }


        public User(string username, string password, string role)
        {
            this.username = username;
            this.password = password;
            this.role = role;
            instancesCreated++;
            userId = instancesCreated.ToString();
        }
    }
}