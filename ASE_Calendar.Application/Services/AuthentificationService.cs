using System;
using ASE_Calendar.Application.Repositories;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Application.Services
{
    /// <summary>
    ///     A service that manages registration and login.
    /// </summary>
    public class AuthentificationService
    {
        /// <summary>
        ///     Starts registration process based on given username, password and role.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="roleId"></param>
        public static void StartRegistration(string username, string password, string roleId)
        {
            UserEntity user = new(username, password, short.Parse(roleId), Guid.NewGuid());
            UserRepository userRepository = new(user);
        }

        /// <summary>
        ///     Start login process based on given username and password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>
        ///     A user entity or null.
        /// </returns>
        public static UserEntity StartLogin(string username, string password)
        {
            UserRepository userRepository = new(username, password);
            var userEntity = userRepository.ReturnUserEntity();

            if (userEntity == null)
            {
                return null;
            }

            if (userEntity.UserDataRegistered.Username == username &&
                userEntity.UserDataRegistered.Password == password)
            {
                return userEntity;
            }

            return null;
        }
    }
}