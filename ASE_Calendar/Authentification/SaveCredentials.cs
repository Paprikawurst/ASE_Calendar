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
            var systemUserName = Environment.UserName;
            var filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            //bool fileExists = File.Exists(@"C:\Users\" + systemUserName + @"\Source\Repos\temp\ASECalendarUsers.json");
            var fileExists = File.Exists(filePath + @"\ASECalendarUsers.json");

            if (fileExists)
            {
                //File.AppendAllText(@"C:\Users\" + systemUserName + @"\Source\Repos\temp\ASECalendarUsers.json", json + "\n");
                File.AppendAllText(filePath + "ASECalendarUsers.json", json + "\n");
            }
            else if (!fileExists)
            {
                File.Create(filePath + "ASECalendarUsers.json");
                //TODO: created user has to be added after
            }
        }
    }
}
