using Microsoft.EntityFrameworkCore.InMemory.Diagnostics.Internal;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpools.Business.Models.Provider
{
    public class DriverDtoProvider : IExamplesProvider<DriverDto>
    {
        public DriverDto GetExamples()
        {
            return new DriverDto { Name = "Lucifer", City = "Hölle", ID = 666 };
        }
    }
}
