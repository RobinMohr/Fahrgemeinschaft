using System;
using System.Buffers;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Cache;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Data.Models;
using TecAlliance.Carpools.Data.Services;


namespace TecAlliance.Carpools.Business.Services
{
    public class CarpoolBusinessService : ICarpoolBusinessService
    {
        private ICarpoolDataService _carpoolDataService;

        List<Carpool> allCarpools = new List<Carpool>();

        private INonDriverDataService _nonDriverDataService;

        private IDriverBusinessService _driverBusinessService;

        private IDriverDataService _driverDataService;

        public CarpoolBusinessService(ICarpoolDataService carpoolDataService, INonDriverDataService nonDriverDataService, IDriverBusinessService driverBusinessService, IDriverDataService driverDataService)
        {
            _carpoolDataService = carpoolDataService;
            _nonDriverDataService = nonDriverDataService;
            _driverBusinessService = driverBusinessService;
            _driverDataService = driverDataService;
        }

        public CarpoolDto Get(int id)
        {
            allCarpools = _carpoolDataService.ReadCarpoolData();
            if (allCarpools == null)
            {
                return null;
            }
            return CarpoolToDto(_carpoolDataService.GetCarpoolData(id));
        }
        public List<CarpoolDto> GetAll()
        {
            allCarpools = _carpoolDataService.ReadCarpoolData();
            List<CarpoolDto> foo = new List<CarpoolDto>();
            if (allCarpools == null)
            {
                return null;
            }
            for (int i = 0; i < allCarpools.Count; i++)
            {
                foo.Add(CarpoolToDto(allCarpools[i]));
            }
            return foo;
        }
        public CarpoolDto CarpoolToDto(Carpool carpool)
        {
            CarpoolDto carpoolDto = new CarpoolDto
            {
                CarpoolId = carpool.CarpoolId,
                Owner = _driverBusinessService.DriverToDTO(carpool.Owner),
                StartingPoint = carpool.StartingPoint,
                EndingPoint = carpool.EndingPoint,
                Time = carpool.Time,
                FreeSpaces = carpool.FreeSpaces,
            };
            return carpoolDto;
        }
        public CarpoolDto UpdateCarpool(int id, string startingPoint, string endingPoint, int freeSpaces, string time)
        {
            allCarpools = _carpoolDataService.ReadCarpoolData();
            Carpool chosenCarpool = _carpoolDataService.GetCarpoolData(id);
            if (chosenCarpool == null)
            {
                return null;
            }
            chosenCarpool.StartingPoint = startingPoint;
            chosenCarpool.EndingPoint = endingPoint;
            chosenCarpool.FreeSpaces = freeSpaces;
            chosenCarpool.Time = time;

            _carpoolDataService.PrintCarpools(allCarpools);
            return new CarpoolDto
            {
                CarpoolId = id,
                Owner = _driverBusinessService.DriverToDTO(chosenCarpool.Owner),
                StartingPoint = startingPoint,
                EndingPoint = endingPoint,
                FreeSpaces = freeSpaces,
                Time = time
            };
        }
        public CarpoolDto JoinCarpool(int carpoolID, int userID)
        {
            allCarpools = _carpoolDataService.ReadCarpoolData();
            Carpool carpool = _carpoolDataService.GetCarpoolData(carpoolID);

            if (carpool.FreeSpaces <= 0 || allCarpools == null)
            {
                return null;
            }
            else
            {
                var passenger = _nonDriverDataService.GetNonDriver(userID);

                carpool.Passengers.Add(passenger);
                carpool.FreeSpaces--;

                _carpoolDataService.PrintCarpools(allCarpools);
                return CarpoolToDto(carpool);
            }
        }
        public CarpoolDto CreateCarpool(int userID, string startingpoint, string endpoint, int freespaces, string time)
        {
            allCarpools = _carpoolDataService.ReadCarpoolData();

            Carpool newCarpool = new Carpool
            {
                CarpoolId = allCarpools.Count,
                Owner = _driverDataService.GetDriver(userID),
                StartingPoint = startingpoint,
                EndingPoint = endpoint,
                FreeSpaces = freespaces,
                Passengers = new List<NonDriver>(),
                Time = time,
                Deleted = false,
            };

            if (newCarpool != null)
            {
                allCarpools.Add(newCarpool);
                _carpoolDataService.PrintCarpools(allCarpools);
                return CarpoolToDto(newCarpool);
            }
            return null;
        }
        public CarpoolDto DelCarpool(int id, int ownerID)
        {
            allCarpools = _carpoolDataService.ReadCarpoolData();

            if (allCarpools == null)
            {
                return null;
            }
            foreach (var carpool in allCarpools)
            {
                if (carpool.CarpoolId == id && ownerID == carpool.Owner.ID)
                {
                    carpool.Deleted = true;
                    _carpoolDataService.PrintCarpools(allCarpools);
                    return CarpoolToDto(carpool);
                }
            }
            return null;
        }
    }
}
