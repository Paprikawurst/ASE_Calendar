using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ASE_Calendar.Classes
{
    class SaveCredentials
    {
        private CredentialBuilder Credentials;
        public SaveCredentials(CredentialBuilder Credentials)
        {
            this.Credentials = Credentials;
        }

        public string CredentialsToJson()
        {
            
            string jsonString = JsonSerializer.Serialize(Credentials);
            Console.WriteLine(jsonString);

            List<string> _data = new List<string>();

            _data.Add(jsonString);
            string json = JsonSerializer.Serialize(_data);
            string systemUserName = Environment.UserName;
            File.WriteAllText(@"C:\Users\" + systemUserName + @"\Source\Repos\ASE_Calendar\ASE_Calendar\temp\Users.json", json);
            return jsonString;
        }
    }
}
