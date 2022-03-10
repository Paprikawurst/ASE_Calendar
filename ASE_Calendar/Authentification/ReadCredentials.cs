using System;
using System.IO;
using ASE_Calendar.Entities;

namespace ASE_Calendar.Authentification
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

       

        public User ReadFromJsonFile()
        {
            string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json");
            string[] jsonSplit = json.Split("\n");

            foreach(var subString in jsonSplit)
            {
                var credentialBuilder = Newtonsoft.Json.JsonConvert.DeserializeObject<CredentialBuilder>(subString);

                if (credentialBuilder != null)
                {
                    if (credentialBuilder.User.username == this.username &&
                        credentialBuilder.User.password == this.password)
                    {
                        Console.WriteLine("Login erfolgreich");
                        return credentialBuilder.User;
                    }
                }

                return credentialBuilder.User;
            }

            return null;

        }
    }
}
