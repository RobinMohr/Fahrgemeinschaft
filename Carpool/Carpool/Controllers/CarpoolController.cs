using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.Design;
using System.Reflection.Metadata;
using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Business.Services;
using TecAlliance.Carpools.Data.Models;

namespace Carpools.Controllerss
{
    [ApiController] 
    [Route("[controller]")]
    public class CarpoolController : ControllerBase
    {
        private CarpoolBusinessService _carpoolBusinessService = new CarpoolBusinessService();
        private readonly ILogger<CarpoolController> _logger;

        public CarpoolController(ILogger<CarpoolController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarpoolDto>>> GetAll()
        {
            List<CarpoolDto> allCarpools = _carpoolBusinessService.GetAll();
            if (allCarpools == null)
            {
                return NotFound();
            }
            return allCarpools;
        }

        [HttpGet("{carpoolID}")]
        public async Task<ActionResult<CarpoolDto>> Get(int carpoolID)
        {
            CarpoolDto singleCarpool = _carpoolBusinessService.Get(carpoolID);
            if (singleCarpool == null)
            {
                return NotFound();
            }
            return singleCarpool;
        }

        [HttpPut("{carpoolID}")]
        public async Task<ActionResult<CarpoolDto>> ChangeCarpool(int carpoolID, string startingPoint, string endingPoint, int freeSpaces, string time)
        {
            CarpoolDto changedCarpool = _carpoolBusinessService.UpdateCarpool(carpoolID, startingPoint, endingPoint, freeSpaces, time);
            if (changedCarpool == null)
            {
                return BadRequest();
            }
            return changedCarpool;
        }

        [HttpPut("join/{JoinID}")]
        public async Task<ActionResult<CarpoolDto>> JoinCarpool(int JoinID, int userID)
        {
            CarpoolDto joinedCarpool = _carpoolBusinessService.JoinCarpool(JoinID, userID);
            if (joinedCarpool == null)
            {
                return BadRequest();
            }
            return joinedCarpool;
        }

        [HttpPost]
        public async Task<ActionResult<CarpoolDto>> CreateNewCarpool(int userID, string startingpoint, string endpoint, int freespaces, string time)
        {
            CarpoolDto addedCarpool = _carpoolBusinessService.CreateCarpool(userID, startingpoint, endpoint, freespaces, time);
            if (addedCarpool == null)
            {
                return NotFound();
            }
            return addedCarpool;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CarpoolDto>> DeleteDriver(int id, int ownerID)
        {
            CarpoolDto carpoolDTO = _carpoolBusinessService.DelCarpool(id, ownerID);

            if (carpoolDTO == null)
            {
                return BadRequest();
            }
            return carpoolDTO;
        }        
    }
}