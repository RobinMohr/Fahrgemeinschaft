using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata;
using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Business.Services;
using TecAlliance.Carpools.Data.Models;

namespace Carpools.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : ControllerBase
    {
        private DriverBusinessService driverBusinessService = new DriverBusinessService();
        private readonly ILogger<DriverController> _logger;

        public DriverController(ILogger<DriverController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<List<DriverDto>>> GetAll()
        {
            var foo = driverBusinessService.GetAllDriver();
            if (foo == null)
            {
                return BadRequest();
            }
            return foo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DriverDto>> Get(int id)
        {
            var foo = driverBusinessService.GetDriver(id);
            if (foo == null)
            {
                return NotFound();
            }
            return foo;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DriverDto>> Updatedriver(int id, string pw, string name, string city)
        {
            return driverBusinessService.UpdateDriver(id, pw, name, city);
        }

        [HttpPost]
        public async Task<ActionResult<DriverDto>> CreateNewDriver(string password, string name, string city)
        {
            if (password == "" || name == "" || city == "")
            {
                return BadRequest();
            }
            return driverBusinessService.CreateNewDriver(password, name, city);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DriverDto>> DeleteDriver(int id, string password)
        {
            DriverDto deletedDriver = driverBusinessService.DelDriver(id, password);
            if (deletedDriver == null)
            {
                return BadRequest();
            }
            return deletedDriver;
        }

        [HttpGet("yourCarpools/{userID}")]
        public async Task<ActionResult<List<Carpool>>> ViewCurrentCarpools(int userID)
        {
            List<Carpool> allCurrentCarpools = driverBusinessService.ViewCurrentCarpools(userID);
            if (allCurrentCarpools == null)
            {
                return NotFound();
            }
            return allCurrentCarpools;
        }
    }
}