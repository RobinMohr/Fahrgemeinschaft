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
using TecAlliance.Carpools.Business.Services.Interfaces;
using TecAlliance.Carpools.Data.Models;
using TecAlliance.Carpools.Data.Services.Interfaces;

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

        public CarpoolDto GetCarpoolByID(int id)
        {
            allCarpools = _carpoolDataService.ReadCarpoolData(); 
            if (allCarpools == null)
            {
                return null;
            }
            return ConvertCarpoolToDto(_carpoolDataService.GetCarpoolDataByID(id));
        }
        public List<CarpoolDto> GetAllCarpools()
        {
            allCarpools = _carpoolDataService.ReadCarpoolData();
            List<CarpoolDto> foo = new List<CarpoolDto>();
            if (allCarpools == null)
            {
                return null;
            }
            for (int i = 0; i < allCarpools.Count; i++)
            {
                foo.Add(ConvertCarpoolToDto(allCarpools[i]));
            }
            return foo;
        }
        public CarpoolDto ConvertCarpoolToDto(Carpool carpool)
        {
            CarpoolDto carpoolDto = new CarpoolDto
            {
                CarpoolId = carpool.CarpoolId,
                Owner = _driverBusinessService.ConvertDriverToDto(carpool.Owner),
                StartingPoint = carpool.StartingPoint,
                EndingPoint = carpool.EndingPoint,
                Time = carpool.Time,
                FreeSpaces = carpool.FreeSpaces,
            };
            return carpoolDto;
        }
        public CarpoolDto ChangeCarpoolDataByID(int id, string startingPoint, string endingPoint, int freeSpaces, string time)
        {
            allCarpools = _carpoolDataService.ReadCarpoolData();
            Carpool chosenCarpool = _carpoolDataService.GetCarpoolDataByID(id);
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
                Owner = _driverBusinessService.ConvertDriverToDto(chosenCarpool.Owner),
                StartingPoint = startingPoint,
                EndingPoint = endingPoint,
                FreeSpaces = freeSpaces,
                Time = time
            };
        }
        public CarpoolDto JoinCarpool(int carpoolID, int userID)
        {
            allCarpools = _carpoolDataService.ReadCarpoolData();
            Carpool carpool = _carpoolDataService.GetCarpoolDataByID(carpoolID);

            if (carpool.FreeSpaces <= 0 || allCarpools == null)
            {
                return null;
            }
            else
            {
                var passenger = _nonDriverDataService.GetNonDriverByID(userID);

                carpool.Passengers.Add(passenger);
                carpool.FreeSpaces--;

                _carpoolDataService.PrintCarpools(allCarpools);
                return ConvertCarpoolToDto(carpool);
            }
        }
        public CarpoolDto CreateCarpool(int userID, string startingpoint, string endpoint, int freespaces, string time)
        {
            allCarpools = _carpoolDataService.ReadCarpoolData();

            Carpool newCarpool = new Carpool
            {
                CarpoolId = allCarpools.Count,
                Owner = _driverDataService.GetDriverByID(userID),
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
                return ConvertCarpoolToDto(newCarpool);
            }
            return null;
        }
        public CarpoolDto DelCarpoolByID(int id, int ownerID)
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
                    return ConvertCarpoolToDto(carpool);
                }
            }
            return null;
        }
    }
}
