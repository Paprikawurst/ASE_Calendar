using System;
using System.IO;
using ASE_Calendar.Application.Services;
using Newtonsoft.Json;
using ASE_Calendar;

namespace ASE_Calendar.Application.Repositories
{
    class SaveCredentials
    {
        private CredentialBuilderService Credentials;
        public SaveCredentials(CredentialBuilderService credentials)
        {
            this.Credentials = credentials;
            CredentialsToJson();
        }

        private void CredentialsToJson()
        {
            var json = JsonConvert.SerializeObject(Credentials);
           
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json", json + "\n");
            
        }
    }
}
