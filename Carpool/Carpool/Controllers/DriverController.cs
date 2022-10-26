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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<DriverDto>>> GetAll()
        {
            var allDriver = driverBusinessService.GetAllDriver();
            if (allDriver == null)
            {
                return NotFound();
            }
            return allDriver;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DriverDto>> Get(int id)
        {
            var returnedDriver = driverBusinessService.GetDriver(id);
            if (returnedDriver == null)
            {
                return NotFound($"ID:{id} was not found.");
            }
            return returnedDriver;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DriverDto>> Updatedriver(int id, string pw, string name, string city)
        {
            var updatedDriver = driverBusinessService.UpdateDriver(id, pw, name, city);
            if (updatedDriver == null)
            {
                return NotFound($"ID:{id} was not found.");
            }
            return updatedDriver;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DriverDto>> CreateNewDriver(string password, string name, string city)
        {
            var createdDriver = driverBusinessService.CreateNewDriver(password, name, city);
            if (createdDriver == null)
            {
                return BadRequest($"Something went wrong");
            }

            return createdDriver;           
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Carpool>>> ViewCurrentCarpools(int userID)
        {
            List<Carpool> allCurrentCarpools = driverBusinessService.ViewCurrentCarpools(userID);
            if (allCurrentCarpools == null)
            {
                return NotFound($"No current Carpools can be found");
            }
            return allCurrentCarpools;
        }
    }
}