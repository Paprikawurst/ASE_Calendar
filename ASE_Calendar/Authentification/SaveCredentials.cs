using System;
using System.IO;
using Newtonsoft.Json;

namespace ASE_Calendar.Authentification
{
    class SaveCredentials
    {
        private CredentialBuilder Credentials;
        public SaveCredentials(CredentialBuilder Credentials)
        {
            this.Credentials = Credentials;
            CredentialsToJson();
        }

        private void CredentialsToJson()
        {
            string json = JsonConvert.SerializeObject(Credentials);
            string systemUserName = Environment.UserName;
            //TODO: create filepath in case it does not exist
            bool fileExists = File.Exists(@"C:\Users\" + systemUserName + @"\Source\Repos\temp\ASECalendarUsers.json");
            //TODO: schauen ob wir was mit dem pfad machen können um die datei zu speichern
            string path = Directory.GetCurrentDirectory();
            if (fileExists)
            {
                File.AppendAllText(@"C:\Users\" + systemUserName + @"\Source\Repos\temp\ASECalendarUsers.json", json + "\n");
            }
            else if (!fileExists)
            {
                // TODO: file or complete path has to be created
            }
        }
    }
}
