using System;
using System.IO;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;
using Newtonsoft.Json;

namespace ASE_Calendar.Application.Repositories
{
    class CredentialsRepository
    {
        private readonly CredentialBuilderService _credentialBuilder;
        public string Username { get; set; }
        public string Password { get; set; }

        public CredentialsRepository(CredentialBuilderService credentialBuilder)
        {
            _credentialBuilder = credentialBuilder;
            CredentialsToJson();
        }

        public CredentialsRepository(string username, string password)
        {
            this.Username = username;
            this.Password = password;

        }

        private void CredentialsToJson()
        {
            var json = JsonConvert.SerializeObject(_credentialBuilder);

            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json", json + "\n");

        }

        public UserEntity ReadFromJsonFile()
        {
            string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json");
            string[] jsonSplit = json.Split("\n");

            foreach (var subString in jsonSplit)
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
