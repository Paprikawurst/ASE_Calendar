using System;
using System.Text.RegularExpressions;
using ASE_Calendar.Application.Repositories;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions
{
    public class Authentification
    {
        public AuthentificationService AuthService = new();
        private enum RegistrationState 
        { 
            userInputUsername,
            userInputPassword,
            userInputRole,
            checkUsername,
            checkPassword,
            checkRole,
        };

        private enum LoginState
        {
            userInput,
            checkInput
        };

        private RegistrationState _registrationState;
        private LoginState _loginState;

        public Authentification()
        {
           
        }

        public void StartRegistration()
        {
            _registrationState = RegistrationState.userInputUsername;
            string inputUsername = "";
            string inputPassword = "";
            string inputUserRole = "";

            switch (_registrationState)
            {
                case RegistrationState.userInputUsername:

                    Console.WriteLine("Register");
                    Console.WriteLine("__________________________");
                    Console.WriteLine("Enter username:");
                    inputUsername = Console.ReadLine();

                    goto case RegistrationState.checkUsername;

                case RegistrationState.userInputPassword:
                    Console.WriteLine("Enter password:");
                    inputPassword = Console.ReadLine();

                    goto case RegistrationState.checkPassword;

                case RegistrationState.userInputRole:

                    Console.WriteLine("Choose role:");
                    Console.WriteLine("0: Admin | 1: CarDealer | 2: Customer");
                    inputUserRole = Console.ReadLine();
                    goto case RegistrationState.checkRole;

                case RegistrationState.checkUsername:
                    CredentialsRepository credentialsRepository = new CredentialsRepository(inputUsername);
                    if (credentialsRepository.ReadFromJsonFileReturnTrueIfUsernameExists())
                    {
                        Console.Clear();
                        Console.WriteLine("Username already exists!" + "\n");
                        goto case RegistrationState.userInputUsername;
                    }
                    goto case RegistrationState.userInputPassword;

                case RegistrationState.checkPassword:

                    if (inputPassword.Length < 5 || inputPassword == "")
                    {
                        Console.Clear();
                        Console.WriteLine("Password must contain at least 5 symbols!" + "\n");
                        goto case RegistrationState.userInputPassword;
                    }

                    goto case RegistrationState.userInputRole;
                    
                case RegistrationState.checkRole:

                    var isNumber = Regex.IsMatch(inputUserRole, @"^[0-9]*$");

                    if (!isNumber || Int16.Parse(inputUserRole) < 0 || inputUserRole == "" || Int16.Parse(inputUserRole) > 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Please select a role as shown!" + "\n");
                        goto case RegistrationState.userInputRole;
                    }

                    break;
            }

            AuthentificationService.StartRegistration(inputUsername, inputPassword, inputUserRole);
        }

        public UserEntity StartLogin()
        {
            _loginState = LoginState.checkInput;
            string inputUsername = "";
            string inputPassword = "";

            switch (_loginState)
            {
                case LoginState.userInput:

                    Console.WriteLine("Login");
                    Console.WriteLine("__________________________");
                    Console.WriteLine("Enter username:");
                    inputUsername = Console.ReadLine();
                    Console.WriteLine("Enter password:");
                    inputPassword = Console.ReadLine();
                    goto case LoginState.checkInput;

                case LoginState.checkInput:
                    var succsfullLogin = AuthentificationService.StartLogin(inputUsername, inputPassword);

                    if (succsfullLogin == null)
                    {
                        Console.Clear();
                        Console.WriteLine("Please use valid credentials!" + "\n");
                        goto case LoginState.userInput;
                    }
                    break;

            }

            return AuthentificationService.StartLogin(inputUsername, inputPassword);
        }
    }
}
