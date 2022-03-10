using System;
using ASE_Calendar.Entities;

namespace ASE_Calendar.Authentification
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
                StartRegistration();
            }

            if (selection == "y" || selection == "Y")
            {
                StartLogin();
            }
        }

        public void StartRegistration()
        {
            Console.WriteLine("Registrieren");
            Console.WriteLine("__________________________");
            Console.WriteLine("Bitte geben sie Ihren Usernamen ein:");
            string inputUsernameRegistration = Console.ReadLine();
            Console.WriteLine("Bitte geben sie ein Passwort ein (Mindestens 5 Zeichen):");
            string inputPasswordRegistration = Console.ReadLine();
            inputPasswordRegistration = CheckPassword(inputPasswordRegistration); 
            User User = new User(inputUsernameRegistration, inputPasswordRegistration, "customer");

            if (User.UserId == "1")
            {
                User.Role = "admin";
            }

            CredentialBuilder Credentials = new CredentialBuilder(User);
            SaveCredentials SavedCredentials = new SaveCredentials(Credentials);
            
        }

        public void StartLogin()
        {
            Console.WriteLine("Login");
            Console.WriteLine("__________________________");
            Console.WriteLine("Bitte geben sie Ihren Usernamen ein:");
            string inputUsernameLogin = Console.ReadLine();
            Console.WriteLine("Bitte geben sie ihr Passwort ein (Mindestens 5 Zeichen):");
            string inputPasswordLogin = Console.ReadLine();

            ReadCredentials ReadCredentials = new ReadCredentials(inputUsernameLogin,inputPasswordLogin);
            ReadCredentials.test();
        }

        private string CheckPassword(string password)
        {
            bool passwordIncorectLoop = true;
            string inputPasswordRegistration = password;

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