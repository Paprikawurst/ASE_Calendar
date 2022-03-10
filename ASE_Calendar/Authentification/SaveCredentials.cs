using System;
using System.IO;
using Newtonsoft.Json;

namespace ASE_Calendar.Authentification
{
    class SaveCredentials
    {
        private CredentialBuilder Credentials;
        public SaveCredentials(CredentialBuilder credentials)
        {
            Credentials = credentials;
            CredentialsToJson();
        }

        private void CredentialsToJson()
        {
            var json = JsonConvert.SerializeObject(Credentials);
         
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json", json + "\n");
        }
    }
}
