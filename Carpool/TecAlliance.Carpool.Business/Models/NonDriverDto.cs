using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Business.Models
{
    public class NonDriverDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public NonDriverDto(int iD, string name, string city)
        {
            ID = iD;
            Name = name;
            City = city;
        }
        public NonDriverDto()
        {

        }
    }
}
