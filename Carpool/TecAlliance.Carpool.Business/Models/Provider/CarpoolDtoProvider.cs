using Microsoft.EntityFrameworkCore.InMemory.Diagnostics.Internal;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpools.Business.Models.Provider
{
    public class CarpoolDtoProvider : IExamplesProvider<CarpoolDto>
    {
        public CarpoolDto GetExamples()
        {
            return new CarpoolDto(1, new DriverDto(0, "Angelo Merte", "WKH" ), "MGH", "WKH", "15:50", 3);
        }
    }
    //public class ListCarpoolDtoProvider : IExamplesProvider<List<CarpoolDto>>
    //{
    //    public List<CarpoolDto> GetExamples()
    //    {
    //        return new List<CarpoolDto> { new CarpoolDto(21, new DriverDto(55, "Angelo Merte", "WKH"), "MGH", "WKH", "15:50", 3), new CarpoolDto(15, new DriverDto(68, "Max Mustermann", "Bad Mergentheim"), "Bad Mergentheim", "Weikersheim", "20:30", 2) };
    //    }
    //}

}