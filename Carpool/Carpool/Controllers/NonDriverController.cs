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
    public class NonDriverController : ControllerBase
    {
        private NonDriverBusinessService _nonDriverBusinessService = new NonDriverBusinessService();
        private readonly ILogger<DriverController> _logger;


        public NonDriverController(ILogger<DriverController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
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
        public async Task<ActionResult<NonDriverDto>> Get(int id)
        {
            var foo = _nonDriverBusinessService.GetNonDriver(id);
            if (foo == null)
            {
                return NotFound();
            }
            return foo;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<NonDriverDto>> UpdateNonDriver(int id, string pw, string name, string city)
        {
            if (id == 0 || pw == null || name == null || city == null)
            {
                return BadRequest();
            }
            return _nonDriverBusinessService.UpdateNonDriver(id, pw, name, city);
        }

        [HttpPost]
        public async Task<ActionResult<NonDriverDto>> CreateNewNonDriver(string password, string name, string city)
        {
            if (password == null || name == null || city == null)
            {
                return BadRequest();
            }
            return _nonDriverBusinessService.CreateNewNonDriver(password, name, city);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<NonDriverDto>> DeleteNonDriver(int id, string password)
        {
            NonDriverDto deletedNonDriver = _nonDriverBusinessService.DelNonDriver(id, password);
            if (deletedNonDriver == null)
            {
                return BadRequest();
            }
            return deletedNonDriver;
        }

        [HttpGet("yourCarpools/{userID}")]
        public async Task<ActionResult<List<CarpoolDto>>> ViewCurrentCarpools(int userID)
        {
            List<CarpoolDto> allCurrentCarpools = _nonDriverBusinessService.ViewCurrentCarpools(userID);
            if (allCurrentCarpools == null)
            {
                return NotFound();
            }
            return allCurrentCarpools;
        }
    }
}