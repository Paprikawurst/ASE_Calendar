using System;
using System.Text.Json;

namespace ASE_Calendar.Classes
{
    public class Authentification
    {
        private string inputUsername;
        private string inputPassword;
        public Authentification()
        {
        }

        public void StartAuthentification()
        {
            Console.WriteLine("Haben Sie bereits ein Benutzerkonto? Y/N");
            string selection = Console.ReadLine();

            if (selection == "n" || selection == "N")
            {
                StartUserRegistration();
            }

            Console.WriteLine("Bitte geben sie Ihren Usernamen ein:");
            inputUsername = Console.ReadLine();
            Console.WriteLine("Bitte geben sie Ihr Passwort ein:");
            inputPassword = Console.ReadLine();

        }

        public void StartUserRegistration()
        {
            Console.WriteLine("Bitte geben sie Ihren Usernamen ein:");
            string inputUsernameRegistration = Console.ReadLine();
            Console.WriteLine("Bitte geben sie Ihr Passwort ein (Mindestens 5 Zeichen):");
            string inputPasswordRegistration = Console.ReadLine();

            CredentialBuilder Credentials = new CredentialBuilder(inputUsernameRegistration, inputPasswordRegistration);
            SaveCredentials SavedCredentials = new SaveCredentials(Credentials);
            SavedCredentials.CredentialsToJson();
        }

        public void StartLogin()
        {

        }
    }
}