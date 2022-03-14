using System;

namespace ASE_Calendar.Console.ConsoleOptions
{
    public class Authentification
    {
        public Application.Services.AuthentificationService AuthService = new Application.Services.AuthentificationService();
        public Authentification()
        {
           
        }

        public void StartRegistration()
        {
            System.Console.WriteLine("Registrieren");
            System.Console.WriteLine("__________________________");
            System.Console.WriteLine("Bitte geben sie Ihren Usernamen ein:");
            string inputUsername = System.Console.ReadLine();
            System.Console.WriteLine("Bitte geben sie ein Passwort ein (Mindestens 5 Zeichen):");
            string inputPassword = System.Console.ReadLine();
            System.Console.WriteLine("Bitte geben sie Ihre Rolle ein:");
            System.Console.WriteLine("0: Admin, 1: CarDealer, 2: Employee, 3: Customer");
            var inputUserRole = System.Console.ReadLine();

            AuthService.StartRegistration(inputUsername, inputPassword, inputUserRole);
        }

        public Domain.Entities.UserEntity StartLogin()
        {
            System.Console.WriteLine("Login");
            System.Console.WriteLine("__________________________");
            System.Console.WriteLine("Bitte geben sie Ihren Usernamen ein:");
            string inputUsername = System.Console.ReadLine();
            System.Console.WriteLine("Bitte geben sie ihr Passwort ein (Mindestens 5 Zeichen):");
            string inputPassword = System.Console.ReadLine();

            return AuthService.StartLogin(inputUsername, inputPassword);
        }
    }
}
