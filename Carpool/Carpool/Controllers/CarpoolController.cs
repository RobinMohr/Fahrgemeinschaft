using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.Design;
using System.Reflection.Metadata;
using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Business.Services.Interfaces;
using TecAlliance.Carpools.Data.Models;

namespace Carpools.Controllerss
{
    [ApiController] 
    [Route("[controller]")]
    public class CarpoolController : ControllerBase
    {
        private ICarpoolBusinessService _carpoolBusinessService;
        private readonly ILogger<CarpoolController> _logger;

        public CarpoolController(ILogger<CarpoolController> logger, ICarpoolBusinessService carpoolBusinessService)
        {
            _carpoolBusinessService = carpoolBusinessService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CarpoolDto>>> GetAll()
        {
            List<CarpoolDto> allCarpools = _carpoolBusinessService.GetAllCarpools();
            if (allCarpools == null)
            {
                return NotFound();
            }
            return allCarpools;
        }

        [HttpGet("{carpoolID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CarpoolDto>> Get(int carpoolID)
        {
            CarpoolDto singleCarpool = _carpoolBusinessService.GetCarpoolByID(carpoolID);
            if (singleCarpool == null)
            {
                return NotFound();
            }
            return singleCarpool;
        }

        [HttpPut("{carpoolID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarpoolDto>> ChangeCarpool(int carpoolID, string startingPoint, string endingPoint, int freeSpaces, string time)
        {
            CarpoolDto changedCarpool = _carpoolBusinessService.ChangeCarpoolDataByID(carpoolID, startingPoint, endingPoint, freeSpaces, time);
            if (changedCarpool == null)
            {
                return BadRequest();
            }
            return changedCarpool;
        }

        [HttpPut("join/{JoinID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarpoolDto>> JoinCarpool(int JoinID, int userID)
        {
            CarpoolDto joinedCarpool = _carpoolBusinessService.JoinCarpool(JoinID, userID);
            if (joinedCarpool == null)
            {
                return NotFound();
            }
            return joinedCarpool;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CarpoolDto>> CreateNewCarpool(int userID, string startingpoint, string endpoint, int freespaces, string time)
        {
            CarpoolDto addedCarpool = _carpoolBusinessService.CreateCarpool(userID, startingpoint, endpoint, freespaces, time);
            if (addedCarpool == null)
            {
                return BadRequest();
            }
            return addedCarpool;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarpoolDto>> DeleteDriver(int id, int ownerID)
        {
            CarpoolDto carpoolDTO = _carpoolBusinessService.DelCarpoolByID(id, ownerID);

            if (carpoolDTO == null)
            {
                return NotFound();
            }
            return carpoolDTO;
        }        
    }
}