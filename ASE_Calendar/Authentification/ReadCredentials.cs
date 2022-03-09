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
            string json = File.ReadAllText(@"C:\Users\" + systemUserName + @"\Source\Repos\ASE_Calendar\ASE_Calendar\temp\Users.json");
            string[] jsonSplit = json.Split("\n");

            foreach(var subString in jsonSplit)
            {
                var credentialBuilder = Newtonsoft.Json.JsonConvert.DeserializeObject<CredentialBuilder>(subString);

                if (credentialBuilder != null)
                {
                    if(credentialBuilder.Customer != null)
                    {
                        if (credentialBuilder.Customer.username == this.username && credentialBuilder.Customer.password == this.password)
                        {
                            Console.WriteLine("Login erfolgreich");
                        } 
                    }
                    if (credentialBuilder.CarDealer != null)
                    {

                    }
                    if (credentialBuilder.Admin != null)
                    {

                    }
                    if (credentialBuilder.Employee != null)
                    {

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
