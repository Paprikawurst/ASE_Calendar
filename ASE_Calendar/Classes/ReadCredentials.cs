using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ASE_Calendar.Classes
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
            List<string> _data = new List<string>();
            string text = File.ReadAllText(@"C:\Users\" + systemUserName + @"\Source\Repos\ASE_Calendar\ASE_Calendar\temp\Users.json");
            _data = JsonSerializer.Deserialize<List<string>>(text);
            
            Console.WriteLine(_data);

        }

        public void test()
        {
            ReadFromJsonFile();
           
        }
    }
}
