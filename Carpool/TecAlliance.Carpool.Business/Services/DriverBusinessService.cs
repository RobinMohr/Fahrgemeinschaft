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
    public class DriverBusinessService : IDriverBusinessService
    {
        private IDriverDataService _driverDataService;

        private List<Driver> allDrivers = new List<Driver>();

        private ICarpoolDataService _carpoolDataService;
        private List<Carpool> allCarpools = new List<Carpool>();

        public DriverBusinessService(IDriverDataService driverDataService, ICarpoolDataService carpoolDataService)
        {
            _driverDataService = driverDataService;
            _carpoolDataService = carpoolDataService;
        }

        public DriverDto GetDriverById(int id)
        {
            return ConvertDriverToDto(_driverDataService.GetDriverByID(id));
        }
        public List<DriverDto> GetAllDriver()
        {
            allDrivers = _driverDataService.ReadNonDriverData();
            List<DriverDto> allDriver = new List<DriverDto>();
            foreach (var driver in allDrivers)
            {
                if (!driver.Deleted)
                {
                    allDriver.Add(ConvertDriverToDto(driver));
                }            
            }
            return allDriver;
        }
        public DriverDto ConvertDriverToDto(Driver driver)
        {
            if (driver == null)
            {
                return null;
            }
            if (!driver.Deleted)
            {
                DriverDto driverDto = new DriverDto
                {
                    Name = driver.Name,
                    City = driver.City,
                    ID = driver.ID,
                };
                return driverDto;
            }
            return null;
        }
        public DriverDto ChangeDriverDataByID(int id, string pw, string name, string city)
        {
            allDrivers = _driverDataService.ReadNonDriverData();
            foreach (var driver in allDrivers)
            {
                if (driver.ID == id && driver.Password == pw)
                {
                    if (driver.Deleted)
                    {
                        return null;
                    }
                    allDrivers[id].City = city;
                    allDrivers[id].Name = name;
                    _driverDataService.PrintUserData(allDrivers);
                    return ConvertDriverToDto(new Driver { Password = pw, Name = name, City = city, ID = id });
                }
            }
            return null;
        }
        public DriverDto CreateNewDriver(string password, string name, string city)
        {
            allDrivers = _driverDataService.ReadNonDriverData();
            Driver newestDriver = new Driver { Name = name, City = city, ID = allDrivers.Count, Password = password , Deleted = false};
            allDrivers.Add(newestDriver);
            _driverDataService.PrintUserData(allDrivers);
            return ConvertDriverToDto(newestDriver);
        }
        public DriverDto DelDriverByID(int id, string password)
        {
            allDrivers = _driverDataService.ReadNonDriverData();
            foreach (var driver in allDrivers)
            {
                if (driver.ID == id && driver.Password == password)
                {
                    if (driver.Deleted)
                    {
                        return null;
                    }
                    driver.Deleted = true;
                    _driverDataService.PrintUserData(allDrivers);
                    return ConvertDriverToDto(driver);
                }
            }
            return null;
        }
        public List<Carpool> GetCarpoolsForUserID(int userID)
        {
            allCarpools = _carpoolDataService.ReadCarpoolData();
            List<Carpool> carpools = new List<Carpool>();
            foreach (var Carpool in allCarpools)
            {
                if (Carpool.Owner.ID == userID && !Carpool.Deleted)
                {
                    carpools.Add(Carpool);
                }
            }
            return carpools;
        }
    }
}