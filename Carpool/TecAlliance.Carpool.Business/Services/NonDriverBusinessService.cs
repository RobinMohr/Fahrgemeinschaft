using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Business.Services.Interfaces;
using TecAlliance.Carpools.Data.Models;
using TecAlliance.Carpools.Data.Services.Interfaces;

namespace TecAlliance.Carpools.Business.Services
{
    public class NonDriverBusinessService : INonDriverBusinessService
    {
        private INonDriverDataService _nonDriverDataService;
        private List<NonDriver> allNonDrivers = new List<NonDriver>();

        private ICarpoolBusinessService _carpoolBusinessService;
        private ICarpoolDataService _carpoolDataService;
        private List<Carpool> allCarpools = new List<Carpool>();

        public NonDriverBusinessService(INonDriverDataService nonDriverDataService, ICarpoolBusinessService carpoolBusinessService, ICarpoolDataService carpoolDataService)
        {
            _nonDriverDataService = nonDriverDataService;
            _carpoolBusinessService = carpoolBusinessService;
            _carpoolDataService = carpoolDataService;
        }

        public NonDriverDto GetNonDriverByID(int id)
        {
            return ConvertNonDriverToDto(_nonDriverDataService.GetNonDriverByID(id));
        }
        public List<NonDriverDto> GetAllNonDriver()
        {
            allNonDrivers = _nonDriverDataService.ReadNonDriverData();
            List<NonDriverDto> allNonDriver = new List<NonDriverDto>();
            foreach (var NonDriver in allNonDrivers)
            {
                if (!NonDriver.Deleted)
                {
                    allNonDriver.Add(ConvertNonDriverToDto(NonDriver));
                }
            }
            return allNonDriver;
        }
        public NonDriverDto ConvertNonDriverToDto(NonDriver nonDriver)
        {
            if (nonDriver == null)
            {
                return null;
            }
            NonDriverDto nonDriverDto = new NonDriverDto
            {
                Name = nonDriver.Name,
                City = nonDriver.City,
                ID = nonDriver.ID
            };
            return nonDriverDto;
        }
        public NonDriverDto ChangeNonDriverDataByID(int id, string pw, string name, string city)
        {
            allNonDrivers = _nonDriverDataService.ReadNonDriverData();
            foreach (var NonDriver in allNonDrivers)
            {
                if (NonDriver.ID == id && NonDriver.Password == pw)
                {
                    allNonDrivers[id].City = city;
                    allNonDrivers[id].Name = name;
                    _nonDriverDataService.PrintUserData(allNonDrivers);
                    return ConvertNonDriverToDto(new NonDriver { Name = name, City = city, Password = pw, ID = id });
                }
            }
            return null;
        }
        public NonDriverDto CreateNewNonDriver(string password, string name, string city)
        {
            if (password == null || name == null || city == null)
            {
                return null;
            }
            allNonDrivers = _nonDriverDataService.ReadNonDriverData();
            NonDriver newNonDriver = new NonDriver { Name = name, City = city, ID = allNonDrivers.Count, Password = password, Deleted = false };
            allNonDrivers.Add(newNonDriver);
            _nonDriverDataService.PrintUserData(allNonDrivers);
            return ConvertNonDriverToDto(newNonDriver);
        }
        public NonDriverDto DelNonDriverByID(int id, string password)
        {
            allNonDrivers = _nonDriverDataService.ReadNonDriverData();
            foreach (var NonDriver in allNonDrivers)
            {
                if (NonDriver.ID == id && NonDriver.Password == password)
                {
                    NonDriver.Deleted = true;
                    _nonDriverDataService.PrintUserData(allNonDrivers);
                    return ConvertNonDriverToDto(NonDriver);
                }
            }
            return null;
        }
        public List<CarpoolDto> GetCarpoolsForUserID(int userID)
        {
            allCarpools = _carpoolDataService.ReadCarpoolData();
            List<CarpoolDto> carpools = new List<CarpoolDto>();
            foreach (var Carpool in allCarpools)
            {
                foreach (NonDriver passenger in Carpool.Passengers)
                {
                    if (passenger.ID == userID)
                    {
                        carpools.Add(_carpoolBusinessService.ConvertCarpoolToDto(Carpool));
                    }
                }
            }
            return carpools;
        }
    }
}
