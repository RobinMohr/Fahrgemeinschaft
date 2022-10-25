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
    public class NonDriverBusinessService
    {
        private NonDriverDataService _nonDriverDataService = new NonDriverDataService();
        private List<NonDriver> allNonDrivers = new List<NonDriver>();

        private CarpoolBusinessService _carpoolBusinessService = new CarpoolBusinessService();
        private CarpoolDataService _carpoolDataService = new CarpoolDataService();
        private List<Carpool> allCarpools = new List<Carpool>();
        public NonDriverDto GetNonDriver(int id)
        {
            return NonDriverToDto(_nonDriverDataService.GetNonDriver(id));
        }
        public List<NonDriverDto> GetAllNonDriver()
        {
            allNonDrivers = _nonDriverDataService.ReadUserData();
            List<NonDriverDto> foo = new List<NonDriverDto>();
            foreach (var NonDriver in allNonDrivers)
            {
                foo.Add(NonDriverToDto(NonDriver));
            }
            return foo;
        }
        public NonDriverDto NonDriverToDto(NonDriver d)
        {
            NonDriverDto NonDriver = new NonDriverDto
            {
                Name = d.Name,
                City = d.City,
                ID = d.ID
            };
            return NonDriver;
        }
        public NonDriverDto UpdateNonDriver(int id,string pw, string name,string city)
        {
            allNonDrivers = _nonDriverDataService.ReadUserData();
            int count = 0;
            foreach (var NonDriver in allNonDrivers)
            {
                if (NonDriver.ID == id && NonDriver.Password == pw)
                {
                    allNonDrivers[id].City = city;
                    allNonDrivers[id].Name = name;
                    _nonDriverDataService.PrintUserData(allNonDrivers);
                    count++;
                    break;
                }
            }
            if (count == 0)
            {
                throw new Exception("nonono, id oder pw falsch");
            }
                return NonDriverToDto(new NonDriver { Name = name, City = city, Password = pw, ID = id });            
        }
        public NonDriverDto CreateNewNonDriver(string password, string name, string city)
        {
            allNonDrivers = _nonDriverDataService.ReadUserData();
            NonDriver newNonDriver = new NonDriver { Name = name, City = city, ID = allNonDrivers.Count, Password = password };
            allNonDrivers.Add(newNonDriver);
            _nonDriverDataService.PrintUserData(allNonDrivers);
            return NonDriverToDto(newNonDriver);
        }
        public NonDriverDto DelNonDriver(int id, string password)
        {
            allNonDrivers = _nonDriverDataService.ReadUserData();           
            foreach (var NonDriver in allNonDrivers)
            {
                if (NonDriver.ID == id && NonDriver.Password == password)
                {
                    NonDriver.Deleted = true;
                    _nonDriverDataService.PrintUserData(allNonDrivers);
                    return NonDriverToDto(NonDriver);
                }
            }
            return null;
        }

        public List<CarpoolDto> ViewCurrentCarpools(int userID)
        {
            allCarpools = _carpoolDataService.ReadCarpoolData();
            List<CarpoolDto> carpools = new List<CarpoolDto>();
            foreach (var Carpool in allCarpools)
            {
               foreach (NonDriver passenger in Carpool.Passengers)
               {
                    if (passenger.ID == userID)
                    {
                        carpools.Add(_carpoolBusinessService.CarpoolToDto(Carpool));
                    }
               }
            }
            return carpools;
        }
    }
}
