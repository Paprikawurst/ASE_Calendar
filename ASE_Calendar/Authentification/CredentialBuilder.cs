using ASE_Calendar.Entities;

namespace ASE_Calendar.Authentification
{
    class CredentialBuilder
    {
        public Customer Customer { get; set; }
        public Admin Admin { get; set; }
        public CarDealer CarDealer { get; set; }
        public Employee Employee { get; set; }

        public CredentialBuilder()
        {
        }

        public CredentialBuilder(Customer Customer)
        {
            this.Customer = Customer;
        }

        public CredentialBuilder(Admin Admin)
        {
            this.Admin = Admin;
        }

        public CredentialBuilder(CarDealer CarDealer)
        {
            this.CarDealer = CarDealer;
        }

        public CredentialBuilder(Employee Employee)
        {
            this.Employee = Employee;
        }

        public void CheckForNull()
        {
            if (this.Customer == null)
            {
                
            }
        }
    }
}
