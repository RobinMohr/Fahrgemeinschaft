using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Data.Services
{
    public class DriverDataService
    {
        private readonly string PathDriverData= @"C:\010Pojects\020Fahrgemeinschaft\DriverInformation.csv";
        private List<Driver> allDrivers = new List<Driver>();
        
        public List<Driver> ReadNonDriverData()
        {
            List<Driver> data = new List<Driver>();

            using (StreamReader sr = new StreamReader(PathDriverData))
            {
                while (!sr.EndOfStream)
                {
                    Driver driver = new Driver();
                    string? LineFromCsv = sr.ReadLine();

                    if (LineFromCsv != null)
                    {
                        string[] driverFromCsv = LineFromCsv.Split(';');
                        driver.ID = Convert.ToInt32(driverFromCsv[0]);
                        driver.Password = driverFromCsv[1];
                        driver.Name = driverFromCsv[2];
                        driver.City = driverFromCsv[3];
                        data.Add(driver);
                    }                    
                }
            }
            return data;
        }
        public Driver GetDriver(int userId)
        {
            int count = 0;
            allDrivers = ReadNonDriverData();
            int i = 0;
            for (i = 0; i < allDrivers.Count; i++)
            {
                if (allDrivers[i].ID == userId)
                {
                    count++;
                    break;
                }
            }
            if (count != 0)
            {
                Driver driver = new Driver()
                {
                    ID = Convert.ToInt32(allDrivers[i].ID),
                    Password = allDrivers[i].Password,
                    Name = allDrivers[i].Name,
                    City = allDrivers[i].City,
                };
                return driver;
            }
            throw new Exception("User id was not in CSV File");
        }
       
        public void PrintUserData(List<Driver> drivers)
        {
            using (StreamWriter sw = new StreamWriter(PathDriverData))
            {
                List<string[]> sa = new List<string[]>();

                for (int p = 0; p < drivers.Count; p++)
                {
                    string[] s = new string[] { Convert.ToString(drivers[p].ID), drivers[p].Password, drivers[p].Name, drivers[p].City };
                    sa.Add(s);
                }
                for (int k = 0; k < drivers.Count; k++)
                {
                    sw.WriteLine(String.Join(";", sa[k]));
                }
            }
        }
    }
}
