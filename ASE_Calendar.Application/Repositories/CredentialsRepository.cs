using System;
using System.IO;
using ASE_Calendar.Application.Shared;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Application.Repositories
{
    public class CredentialsRepository
    {
        private readonly UserEntity _userEntity;

        public CredentialsRepository(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public CredentialsRepository(string username)
        {
            Username = username;
        }

        public CredentialsRepository(UserEntity userEntity)
        {
            _userEntity = userEntity;
            CreateNewCredentials();
        }

        public string Username { get; set; }
        public string Password { get; set; }

        public UserEntity ReadFromJsonFileReturnUser()
        {
            var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json");
            var jsonSplit = json.Split("\n");

            foreach (var subString in jsonSplit)
            {
                var customJsonConverter = new CustomJsonConverter<UserEntity>();
                var userEntityDeserializeObject = customJsonConverter.DeserializeObject(subString);

                if (userEntityDeserializeObject != null)
                {
                    if (userEntityDeserializeObject.UserDataRegistered.Username == Username &&
                        userEntityDeserializeObject.UserDataRegistered.Password == Password)
                    {
                        return userEntityDeserializeObject;
                    }
                }
            }

            return null;
        }

        public bool ReadFromJsonFileReturnTrueIfUsernameExists()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json"))
            {
                var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json");
                var jsonSplit = json.Split("\n");

                foreach (var subString in jsonSplit)
                {
                    var customJsonConverter = new CustomJsonConverter<UserEntity>();
                    var userEntityDeserializeObject = customJsonConverter.DeserializeObject(subString);

                    if (userEntityDeserializeObject != null)
                    {
                        if (userEntityDeserializeObject.UserDataRegistered.Username == Username)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void CreateNewCredentials()
        {
            var customJsonConverter = new CustomJsonConverter<UserEntity>();
            var json = customJsonConverter.SerializeObject(_userEntity);

            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json", json + "\n");
        }
    }
}