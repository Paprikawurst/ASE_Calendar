using System;
using System.Text.RegularExpressions;
using ASE_Calendar.Application.Repositories;
using ASE_Calendar.Application.Services;
using ASE_Calendar.ConsoleUI.Enums;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions
{
    /// <summary>
    /// A class that starts the progress of the authentification.
    /// </summary>
    public class Authentification
    {
        private readonly ConsoleColorHelper _colorHelper = new();
        private LoginState _loginState;

        private RegistrationState _registrationState;
        public AuthentificationService AuthService = new();

        /// <summary>
        /// Starts the progress of the registration on the ui.
        /// </summary>
        public void StartRegistration()
        {
            _registrationState = RegistrationState.UserInputUsername;
            var inputUsername = "";
            var inputPassword = "";
            var inputUserRole = "";

            switch (_registrationState)
            {
                case RegistrationState.UserInputUsername:

                    Console.WriteLine("Register");
                    Console.WriteLine("__________________________");
                    Console.WriteLine("Enter username:");
                    inputUsername = Console.ReadLine();

                    goto case RegistrationState.CheckUsername;

                case RegistrationState.UserInputPassword:
                    Console.WriteLine("Enter password:");
                    inputPassword = Console.ReadLine();

                    goto case RegistrationState.CheckPassword;

                case RegistrationState.UserInputRole:

                    Console.WriteLine("Choose role:");
                    Console.WriteLine("0: Admin | 1: CarDealer | 2: Customer");
                    inputUserRole = Console.ReadLine();
                    goto case RegistrationState.CheckRole;

                case RegistrationState.CheckUsername:
                    var credentialsRepository = new UserRepository(inputUsername);
                    if (credentialsRepository.ReadFromJsonFileReturnTrueIfUsernameExists())
                    {
                        Console.Clear();
                        _colorHelper.WriteLineRed("Username already exists!" + "\n");
                        goto case RegistrationState.UserInputUsername;
                    }

                    goto case RegistrationState.UserInputPassword;

                case RegistrationState.CheckPassword:

                    if (inputPassword.Length < 5 || inputPassword == "")
                    {
                        Console.Clear();
                        _colorHelper.WriteLineRed("Password must contain at least 5 symbols!" + "\n");
                        goto case RegistrationState.UserInputPassword;
                    }

                    goto case RegistrationState.UserInputRole;

                case RegistrationState.CheckRole:

                    var isNumber = Regex.IsMatch(inputUserRole, @"^[0-9]*$");

                    if (!isNumber || short.Parse(inputUserRole) < 0 || inputUserRole == "" ||
                        short.Parse(inputUserRole) > 2)
                    {
                        Console.Clear();
                        _colorHelper.WriteLineRed("Please select a role as shown!" + "\n");
                        goto case RegistrationState.UserInputRole;
                    }

                    break;
            }

            AuthentificationService.StartRegistration(inputUsername, inputPassword, inputUserRole);
        }

        /// <summary>
        /// Starts the progress of the login on the ui.
        /// </summary>
        public UserEntity StartLogin()
        {
            _loginState = LoginState.UserInput;
            var inputUsername = "";
            var inputPassword = "";

            switch (_loginState)
            {
                case LoginState.UserInput:

                    Console.WriteLine("Login");
                    Console.WriteLine("__________________________");
                    Console.WriteLine("Enter username:");
                    inputUsername = Console.ReadLine();
                    Console.WriteLine("Enter password:");
                    inputPassword = Console.ReadLine();
                    goto case LoginState.CheckInput;

                case LoginState.CheckInput:
                    var successfulLogin = AuthentificationService.StartLogin(inputUsername, inputPassword);

                    if (successfulLogin == null)
                    {
                        Console.Clear();
                        _colorHelper.WriteLineRed("Please use valid credentials!" + "\n");
                        goto case LoginState.UserInput;
                    }

                    break;
            }

            return AuthentificationService.StartLogin(inputUsername, inputPassword);
        }
    }
}