using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Calendar.Classes
{
    class CredentialBuilder
    {
        private static int instancesCreated = 0;
        public int customerNumber { get; set; }
        public string username { get; set; }
        public string password { get; set; }
      
        public CredentialBuilder(string username, string password)
        {
            this.username = username;
            this.password = password;
            instancesCreated++;
            customerNumber = instancesCreated;

        }
    }
}
