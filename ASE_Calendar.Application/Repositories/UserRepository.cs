using System;
using System.IO;
using ASE_Calendar.Application.Shared;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Application.Repositories
{
    /// <summary>
    ///     This repository manages the CRUD operations for the user entity.
    /// </summary>
    public class UserRepository
    {
        private readonly CustomJsonConverter<UserEntity> _customJsonConverter = new();
        private readonly UserEntity _userEntity;

        public UserRepository(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public UserRepository(string username)
        {
            Username = username;
        }

        public UserRepository(UserEntity userEntity)
        {
            _userEntity = userEntity;
            CreateUser();
        }

        public string Username { get; set; }
        public string Password { get; set; }

        /// <summary>
        ///     Serializes a user entity to a json format and appends it to the ASECalendarUsers.json
        /// </summary>
        private void CreateUser()
        {
            var json = _customJsonConverter.SerializeObject(_userEntity);
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json", json + "\n");
        }

        /// <summary>
        ///     Reads current user from ASECalendarUsers.json
        /// </summary>
        /// <returns>
        ///     A user entity based on previously determined name and password or null.
        /// </returns>
        public UserEntity ReturnUserEntity()
        {
            var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json");
            var jsonSplit = json.Split("\n");

            foreach (var subString in jsonSplit)
            {
                var userEntity = _customJsonConverter.DeserializeObject(subString);

                if (userEntity != null)
                {
                    if (userEntity.UserDataRegistered.Username == Username &&
                        userEntity.UserDataRegistered.Password == Password)
                    {
                        return userEntity;
                    }
                }
            }

            return null;
        }

        /// <summary>
        ///     Reads users from ASECalendarUsers.json and returns whether current username exists or not.
        /// </summary>
        /// <returns>
        ///     A boolean.
        /// </returns>
        public bool ReadFromJsonFileReturnTrueIfUsernameExists()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json"))
            {
                var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json");
                var jsonSplit = json.Split("\n");

                foreach (var subString in jsonSplit)
                {
                    var userEntity = _customJsonConverter.DeserializeObject(subString);

                    if (userEntity != null)
                    {
                        if (userEntity.UserDataRegistered.Username == Username)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}