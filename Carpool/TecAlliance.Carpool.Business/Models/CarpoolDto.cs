using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Business.Models
{
    public class CarpoolDto
    {
        public int CarpoolId { get; set; }
        public DriverDto Owner { get; set; }
        public string StartingPoint { get; set; }
        public string EndingPoint { get; set; }
        public string Time { get; set; }
        public int FreeSpaces { get; set; }

        public CarpoolDto(int carpoolId, DriverDto owner, string startingPoint, string endingPoint, string time, int freeSpaces)
        {
            CarpoolId = carpoolId;
            Owner = owner;
            StartingPoint = startingPoint;
            EndingPoint = endingPoint; 
            Time = time;
            FreeSpaces = freeSpaces;
        }
        public CarpoolDto()
        {

        }
    }
}