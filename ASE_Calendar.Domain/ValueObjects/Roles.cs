using System;

namespace ASE_Calendar.Domain.ValueObjects
{
    sealed class Roles
    {
        public readonly int roleId;
        private string _admin = "Admin";
        private string _carDealer = "CarDealer";
        private string _employee = "Employee";
        private string _customer = "Customer";

        public Roles(int roleId)
        {
            try
            {
                this.roleId = roleId;
                if (this.roleId < 0 || this.roleId > 3)
                {
                    throw new ArgumentOutOfRangeException(nameof(roleId), $"RoleId must be between 0 and 3");
                }
            }
            catch (ArgumentOutOfRangeException argumentOutOfRangeExeption)
            {
                Console.WriteLine($"Error: {argumentOutOfRangeExeption.Message}");
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Roles roles &&
                   roleId == roles.roleId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(roleId);
        }

        public string GetRoleString(){

            return roleId switch
            {
                0 => _admin,
                1 => _carDealer,
                2 => _employee,
                3 => _customer
            };
        }
    }
    
}