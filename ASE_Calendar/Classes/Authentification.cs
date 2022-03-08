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
        }

        public void StartUserRegistration()
        {
            Console.WriteLine("Bitte geben sie Ihren Usernamen ein:");
            string inputUsernameRegistration = Console.ReadLine();
            Console.WriteLine("Bitte geben sie Ihr Passwort ein (Mindestens 5 Zeichen):");
            string inputPasswordRegistration = Console.ReadLine();
            inputPasswordRegistration = CheckPassword(inputPasswordRegistration);

            CredentialBuilder Credentials = new CredentialBuilder(new Data.Customer(inputUsernameRegistration,inputPasswordRegistration));
            SaveCredentials SavedCredentials = new SaveCredentials(Credentials);
            SavedCredentials.CredentialsToJson();
        }

        public void StartLogin()
        {

        }

        private string CheckPassword(string password)
        {
            bool passwordIncorectLoop = true;
            string inputPasswordRegistration = "";

            if( password.Length < 5) 
            { 
                while (passwordIncorectLoop)
                {
                    Console.WriteLine("Das Passwort muss mindestens 5 Zeichen lang sein!");
                    inputPasswordRegistration = Console.ReadLine();

                    if(inputPasswordRegistration.Length >= 5)
                    {
                        passwordIncorectLoop = false;
                    }
                }
            }
            return inputPasswordRegistration;
        }

    }
}