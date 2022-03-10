namespace ASE_Calendar.Entities
{
    public class User
    {
        private static int _instancesCreated = 0;
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }


        public User(string username, string password, string role)
        {
            Username = username;
            password = password;
            role = role;
            _instancesCreated++;
            UserId = _instancesCreated.ToString();
        }
    }
}