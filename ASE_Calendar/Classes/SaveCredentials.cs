using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ASE_Calendar.Classes
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
            File.AppendAllText(@"C:\Users\" + systemUserName + @"\Source\Repos\ASE_Calendar\ASE_Calendar\temp\Users.json", json + "\n");
            
        }
    }
}
