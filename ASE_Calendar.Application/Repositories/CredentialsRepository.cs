using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;
using Newtonsoft.Json;

namespace ASE_Calendar.Application.Repositories
{
    public class CredentialsRepository
    {
        public string Username { get; set; }
        public string Password { get; set; }

        private CredentialBuilderService Credentials;

        public CredentialsRepository(string username, string password)
        {
            this.Username = username;
            this.Password = password;

        }

        public CredentialsRepository(string username)
        {
            this.Username = username;

        }

        public CredentialsRepository(CredentialBuilderService credentials)
        {
            this.Credentials = credentials;
            CreateNewCredentials();
        }

        public UserEntity ReadFromJsonFileReturnUser()
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

        public bool ReadFromJsonFileReturnTrueIfUsernameExists()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json"))
            {
                string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json");
                string[] jsonSplit = json.Split("\n");

                foreach (var subString in jsonSplit)
                {
                    var credentialBuilder = JsonConvert.DeserializeObject<CredentialBuilderService>(subString);

                    if (credentialBuilder != null)
                    {
                        if (credentialBuilder.UserEntity.UserDataRegistered.Username == Username)
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
            var json = JsonConvert.SerializeObject(Credentials);

            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json", json + "\n");

        }
    }
}
