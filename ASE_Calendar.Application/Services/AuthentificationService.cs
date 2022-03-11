using System;
using System.IO;
using ASE_Calendar.Application.Repositories;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Application.Services
{
    public class AuthentificationService
    {
        public AuthentificationService()
        {
        }

        public void StartAuthentification()
        {
            Console.WriteLine("Haben Sie bereits ein Benutzerkonto? Y/N");
            string selection = Console.ReadLine();

            if (selection == "n" || selection == "N")
            {
                StartRegistration();
                Console.Clear();
            }

            if (selection == "y" || selection == "Y")
            {
                StartLogin();
                Console.Clear();
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
            UserEntity User = new UserEntity(inputUsernameRegistration, inputPasswordRegistration, "customer");

            if (User.userId == "1")
            {
                User.role = "admin";
            }

            CredentialBuilderService Credentials = new CredentialBuilderService(User);
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
            UserEntity LogedInUser = ReadCredentials.ReadFromJsonFile();
        }

        private string CheckPassword(string password)
        {
            bool passwordIncorrectLoop = true;
            string inputPasswordRegistration = password;

            if( password.Length < 5) 
            { 
                while (passwordIncorrectLoop)
                {
                    Console.WriteLine("Das Passwort muss mindestens 5 Zeichen lang sein!");
                    inputPasswordRegistration = Console.ReadLine();

                    if(inputPasswordRegistration.Length >= 5)
                    {
                        passwordIncorrectLoop = false;
                    }
                }
            }
            return inputPasswordRegistration;
        }

        public void ResetUserId()
        {
            if(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUserIds.txt"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUserIds.txt");
                File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUserIds.txt", "1");
            }
            else
            {
                File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUserIds.txt", "1");
            }
        }
    }
}