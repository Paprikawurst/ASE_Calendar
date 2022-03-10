using System;
using System.IO;

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

       

        public void ReadFromJsonFile()
        {
            string systemUserName = Environment.UserName;
            string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json");
            string[] jsonSplit = json.Split("\n");

            foreach(var subString in jsonSplit)
            {
                var credentialBuilder = Newtonsoft.Json.JsonConvert.DeserializeObject<CredentialBuilder>(subString);

                if (credentialBuilder != null)
                {
                    if(credentialBuilder.user != null)
                    {
                        if (credentialBuilder.user.username == this.username && credentialBuilder.user.password == this.password)
                        {
                            Console.WriteLine("Login erfolgreich");
                        } 
                    }
                }
                
            }
            
        }

        public void test()
        {
            ReadFromJsonFile();
           
        }
    }
}
