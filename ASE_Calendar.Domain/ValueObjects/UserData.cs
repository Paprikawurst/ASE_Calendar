namespace ASE_Calendar.Domain.ValueObjects
{
    public class UserData
    {
        public UserData(string username, string password, int roleId)
        {
            Username = username;
            Password = password;
            RoleId = roleId;
        }

        public string Username { get; init; }
        public string Password { get; init; }
        public int RoleId { get; init; }

    }
}