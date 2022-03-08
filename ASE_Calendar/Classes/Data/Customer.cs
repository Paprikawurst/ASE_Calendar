namespace ASE_Calendar.Classes.Data
{
    public class Customer
    {
        private static int instancesCreated = 0;
        public int customerNumber { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        

        public Customer(string username, string password)
        {
            this.username = username;
            this.password = password;
            instancesCreated++;
            customerNumber = instancesCreated;
        }
    }
}