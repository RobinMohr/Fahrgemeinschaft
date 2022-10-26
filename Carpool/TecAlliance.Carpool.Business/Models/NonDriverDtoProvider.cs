using Microsoft.EntityFrameworkCore.InMemory.Diagnostics.Internal;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpools.Business.Models
{
    public class NonDriverDtoProvider : IExamplesProvider<NonDriverDto>
    {
        public NonDriverDto GetExamples()
        {
            return new NonDriverDto { Name = "Pascalll Passagier", City = "Janz weet drousen", ID = 99};
        }
    }
}
