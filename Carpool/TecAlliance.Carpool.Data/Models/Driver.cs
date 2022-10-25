using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TecAlliance.Carpools.Data.Models
{
    public class Driver
    {
        public int ID { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public bool Deleted { get; set; }

        public Driver(int iD, string password, string name, string city)
        {
            ID = iD;
            Password = password;
            Name = name;
            City = city;
            Deleted = false;
        }
        public Driver()
        {

        }
    }
}
