using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Data.Models;
using TecAlliance.Carpools.Data.Services;


namespace TecAlliance.Carpools.Business.Services
{
    public class DriverBusinessService
    {
        private DriverDataService _driverDataService = new DriverDataService();

        private List<Driver> allDrivers = new List<Driver>();

        private CarpoolDataService _carpoolDataService = new CarpoolDataService();
        private List<Carpool> allCarpools = new List<Carpool>();

        public DriverDto GetDriver(int id)
        {
            return DriverToDTO(_driverDataService.GetDriver(id));
        }
        public List<DriverDto> GetAllDriver()
        {
            allDrivers = _driverDataService.ReadNonDriverData();
            List<DriverDto> allDriver = new List<DriverDto>();
            foreach (var driver in allDrivers)
            {
                allDriver.Add(DriverToDTO(driver));
            }
            return allDriver;
        }
        public DriverDto DriverToDTO(Driver d)
        {
            if (d == null)
            {
                return null;
            }
            DriverDto driver = new DriverDto
            {
                Name = d.Name,
                City = d.City,
                ID = d.ID,
            };
            return driver;
        }
        public DriverDto UpdateDriver(int id, string pw, string name, string city)
        {
            allDrivers = _driverDataService.ReadNonDriverData();
            foreach (var driver in allDrivers)
            {
                if (driver.ID == id && driver.Password == pw)
                {
                    allDrivers[id].City = city;
                    allDrivers[id].Name = name;
                    _driverDataService.PrintUserData(allDrivers);
                    return DriverToDTO(new Driver { Password = pw, Name = name, City = city, ID = id });
                }
            }
            return null;
        }
        public DriverDto CreateNewDriver(string password, string name, string city)
        {
            allDrivers = _driverDataService.ReadNonDriverData();           
            Driver newestDriver = new Driver { Name = name, City = city, ID = allDrivers.Count, Password = password };
            allDrivers.Add(newestDriver);
            _driverDataService.PrintUserData(allDrivers);
            return DriverToDTO(newestDriver);
        }
        public DriverDto DelDriver(int id, string password)
        {
            allDrivers = _driverDataService.ReadNonDriverData();
            foreach (var driver in allDrivers)
            {
                if (driver.ID == id && driver.Password == password)
                {
                    driver.Deleted = true;
                    _driverDataService.PrintUserData(allDrivers);
                    return DriverToDTO(driver);
                }
            }
            return null;
        }
        public List<Carpool> ViewCurrentCarpools(int userID)
        {
            allCarpools = _carpoolDataService.ReadCarpoolData();
            List<Carpool> carpools = new List<Carpool>();
            foreach (var Carpool in allCarpools)
            {
                if (Carpool.Owner.ID == userID)
                {
                    carpools.Add(Carpool);
                }
            }
            return carpools;
        }
    }
}