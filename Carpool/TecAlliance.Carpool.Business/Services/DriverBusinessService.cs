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
            List<DriverDto> foo = new List<DriverDto>();
            foreach (var driver in allDrivers)
            {
                foo.Add(DriverToDTO(driver));
            }
            return foo;
        }
        public DriverDto DriverToDTO(Driver d)
        {
            DriverDto driver = new DriverDto
            {
                Name = d.Name,
                City = d.City,
                ID = d.ID
            };
            return driver;
        }
        public DriverDto UpdateDriver(int id, string pw, string name, string city)
        {
            allDrivers = _driverDataService.ReadNonDriverData();
            int count = 0;
            foreach (var driver in allDrivers)
            {
                if (driver.ID == id && driver.Password == pw)
                {
                    allDrivers[id].City = city;
                    allDrivers[id].Name = name;
                    _driverDataService.PrintUserData(allDrivers);
                    count++;
                }
            }
            if (count == 0)
            {
                throw new Exception("nonono, id falsch");
            }
            else
            {
                return DriverToDTO(new Driver { Password = pw, Name = name, City = city, ID = id });
            }

        }
        public DriverDto CreateNewDriver(string password, string name, string city)
        {
            allDrivers = _driverDataService.ReadNonDriverData();
            int id = 0;
            if (allDrivers == null)
            {
                id = 0;
            }
            else
            {
                for (int i = 0; i <= allDrivers.Count; i++)
                {
                    int count = 0;
                    foreach (var carpool in allDrivers)
                    {
                        if (carpool.ID == i) { break; }
                        else
                        {
                            count++;
                        }
                    }
                    if (count == allDrivers.Count)
                    {
                        id = i;
                        break;
                    }
                }
            }
            Driver newestDriver = new Driver { Name = name, City = city, ID = id, Password = password };
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
                    allDrivers.Remove(driver);
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