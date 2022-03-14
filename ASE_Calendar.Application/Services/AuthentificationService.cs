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

        public void StartRegistration(string username, string password, string roleId)
        {
            
            UserEntity User = new UserEntity(username, password, Int16.Parse(roleId));
            CredentialBuilderService Credentials = new CredentialBuilderService(User);
            SaveCredentials SavedCredentials = new SaveCredentials(Credentials);
            
        }

        public UserEntity StartLogin(string username, string password)
        {
            ReadCredentials ReadCredentials = new ReadCredentials(username,password);
            UserEntity LoggedInUser = ReadCredentials.ReadFromJsonFile();

            if (LoggedInUser.UserDataRegistered.username == username && LoggedInUser.UserDataRegistered.password == password)
            {
                return LoggedInUser;
            }
            return null;
        }
    }
}