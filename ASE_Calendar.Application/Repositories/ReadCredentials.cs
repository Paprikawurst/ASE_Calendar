using System;
using System.IO;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;
using Newtonsoft.Json;

namespace ASE_Calendar.Application.Repositories
{
    class ReadCredentials
    {
        public string username { get; set; }
        public string password { get; set; }

        public ReadCredentials(string username, string password)
        {
            this.username = username;
            this.password = password;
            
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
                    if (credentialBuilder.UserEntity.UserDataRegistered.username == username && credentialBuilder.UserEntity.UserDataRegistered.password == password)
                    {
                        return credentialBuilder.UserEntity;
                    }
                }
            }

            return null;

        }
    }
}
