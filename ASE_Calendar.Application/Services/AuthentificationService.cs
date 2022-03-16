using ASE_Calendar.Application.Repositories;
using ASE_Calendar.Domain.Entities;
using System;

namespace ASE_Calendar.Application.Services
{
    public class AuthentificationService
    {
        
        public AuthentificationService()
        {
        }

        public static void StartRegistration(string username, string password, string roleId)
        {
            UserEntity User = new(username, password, Int16.Parse(roleId), Guid.NewGuid());
            CredentialBuilderService Credentials = new(User);
            CredentialsRepository credentialsToJson = new(Credentials);      
        }

        public static UserEntity StartLogin(string username, string password)
        {
            CredentialsRepository credentialsRepository = new(username, password);
            UserEntity LoggedInUser = credentialsRepository.ReadFromJsonFileReturnUser();

            if (LoggedInUser == null)
            {
                return null;
            }

            if (LoggedInUser.UserDataRegistered.Username == username && LoggedInUser.UserDataRegistered.Password == password)
            {
                return LoggedInUser;
            }
            return null;
        }
    }
}