using System;
using System.IO;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;
using Newtonsoft.Json;
using ASE_Calendar;

namespace ASE_Calendar.Application.Repositories
{
    class ReadCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public ReadCredentials(string username, string password)
        {
            this.Username = username;
            this.Password = password;
            
        }

        public UserEntity ReadFromJsonFile()
        {
            string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json");
            string[] jsonSplit = json.Split("\n");
            
            foreach(var subString in jsonSplit)
            {
                var credentialBuilder = JsonConvert.DeserializeObject<CredentialBuilderService>(subString);

                if (credentialBuilder != null)
                {
                    if (credentialBuilder.UserEntity.UserDataRegistered.Username == Username && credentialBuilder.UserEntity.UserDataRegistered.Password == Password)
                    {
                        return credentialBuilder.UserEntity;
                    }
                }
            }

            return null;

        }
    }
}
