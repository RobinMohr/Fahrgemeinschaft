using Microsoft.EntityFrameworkCore.InMemory.Diagnostics.Internal;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpools.Business.Models
{
    public class CarpoolDtoProvider:IExamplesProvider<CarpoolDto>
    {
        public CarpoolDto GetExamples()
        {
            return new CarpoolDto() { CarpoolId = 0, EndingPoint = "MGH", StartingPoint = "WKH", FreeSpaces = 3, Owner = new DriverDto() { ID = 0, City = "WKH", Name = "Robin Mohr" }, Time = "15:50" };
        }
    }
}
