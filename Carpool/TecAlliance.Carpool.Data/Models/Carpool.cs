using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TecAlliance.Carpools.Data.Models
{
    public class Carpool
    {
        public int CarpoolId { get; set; }
        public Driver Owner { get; set; }
        public string StartingPoint { get; set; }
        public string EndingPoint { get; set; }
        public int FreeSpaces { get; set; }
        public List<NonDriver> Passengers { get; set; }
        public string Time { get; set; }

        public Carpool(int carpoolId, Driver owner, string startingPoint, string endingPoint, int freeSpaces, List<NonDriver> passengers, string time)
        {
            CarpoolId = carpoolId;
            Owner = owner;
            StartingPoint = startingPoint;
            EndingPoint = endingPoint;
            FreeSpaces = freeSpaces;
            Passengers = passengers;
            Time = time;
        }

        public Carpool()
        {

        }
    }
}