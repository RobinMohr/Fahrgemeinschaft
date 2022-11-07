using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata;
using System.Threading.Channels;
using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Business.Services.Interfaces;
using TecAlliance.Carpools.Data.Models;

namespace Carpools.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NonDriverController : ControllerBase
    {
        private INonDriverBusinessService _nonDriverBusinessService;
        private readonly ILogger<DriverController> _logger;
        public NonDriverController(ILogger<DriverController> logger, INonDriverBusinessService nonDriverBusinessService)
        {
            _logger = logger;
            _nonDriverBusinessService = nonDriverBusinessService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<NonDriverDto>>> GetAll()
        {
            var foo = _nonDriverBusinessService.GetAllNonDriver();
            if (foo == null)
            {
                return NotFound();
            }
            return foo;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NonDriverDto>> Get(int id)
        {
            var foo = _nonDriverBusinessService.GetNonDriverByID(id);
            if (foo == null)
            {
                return NotFound();
            }
            return foo;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NonDriverDto>> UpdateNonDriver(int id, string pw, string name, string city)
        {
            if (id == 0 || pw == null || name == null || city == null)
            {
                return BadRequest();
            }
            return _nonDriverBusinessService.ChangeNonDriverDataByID(id, pw, name, city);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NonDriverDto>> CreateNewNonDriver(string password, string name, string city)
        {
            NonDriverDto createdNonDriver = _nonDriverBusinessService.CreateNewNonDriver(password, name, city);
            if (createdNonDriver == null)
            {
                return BadRequest();
            }
            return createdNonDriver;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NonDriverDto>> DeleteNonDriver(int id, string password)
        {
            NonDriverDto deletedNonDriver = _nonDriverBusinessService.DelNonDriverByID(id, password);
            if (deletedNonDriver == null)
            {
                return BadRequest();
            }
            return deletedNonDriver;
        }

        [HttpGet("yourCarpools/{userID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CarpoolDto>>> ViewCurrentCarpools(int userID)
        {
            List<CarpoolDto> allCurrentCarpools = _nonDriverBusinessService.GetCarpoolsForUserID(userID);
            if (allCurrentCarpools == null)
            {
                return NotFound($"ID:{userID} was not found");
            }
            return allCurrentCarpools;
        }
    }
}