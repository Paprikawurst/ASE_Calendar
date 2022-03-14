using System;
using ASE_Calendar.Application.Services;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions
{
    public class Authentification
    {
        public AuthentificationService AuthService = new AuthentificationService();
        public Authentification()
        {
           
        }

        public void StartRegistration()
        {
            Console.WriteLine("Register");
            Console.WriteLine("__________________________");
            Console.WriteLine("Enter username:");
            var inputUsername = Console.ReadLine();
            Console.WriteLine("Enter password:");
            var inputPassword = Console.ReadLine();
            Console.WriteLine("Choose role:");
            Console.WriteLine("0: Admin | 1: CarDealer | 2: Employee | 3: Customer");
            var inputUserRole = Console.ReadLine();

            AuthService.StartRegistration(inputUsername, inputPassword, inputUserRole);
        }

        public Domain.Entities.UserEntity StartLogin()
        {
            Console.WriteLine("Login");
            Console.WriteLine("__________________________");
            Console.WriteLine("Enter username:");
            var inputUsername = Console.ReadLine();
            Console.WriteLine("Enter password:");
            var inputPassword = Console.ReadLine();

            return AuthService.StartLogin(inputUsername, inputPassword);
        }
    }
}
