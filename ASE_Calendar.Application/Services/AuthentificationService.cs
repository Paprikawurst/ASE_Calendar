using System;
using ASE_Calendar.Application.Repositories;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Application.Services
{
    public class AuthentificationService
    {
        public static void StartRegistration(string username, string password, string roleId)
        {
            UserEntity user = new(username, password, short.Parse(roleId), Guid.NewGuid());
            UserRepository credentialsToJson = new(user);
        }

        public static UserEntity StartLogin(string username, string password)
        {
            UserRepository credentialsRepository = new(username, password);
            var loggedInUser = credentialsRepository.ReturnUserEntity();

            if (loggedInUser == null)
            {
                return null;
            }

            if (loggedInUser.UserDataRegistered.Username == username &&
                loggedInUser.UserDataRegistered.Password == password)
            {
                return loggedInUser;
            }

            return null;
        }
    }
}