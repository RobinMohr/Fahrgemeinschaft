using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpools.Data.Models;
using TecAlliance.Carpools.Data.Services.Interfaces;

namespace TecAlliance.Carpools.Data.Services
{
    public class NonDriverDataService : INonDriverDataService
    {
        private string pathNonDriver = @$"{Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\TecAlliance.Carpool.Data\NonDriverInformation.csv"))}";

        public string PathNonDriver
        {
            get
            {
                return this.pathNonDriver;
            }
            set
            {
                this.pathNonDriver = value;
            }
        }

        public NonDriverDataService(string pathNonDriver)
        {
            PathNonDriver = pathNonDriver;
        }

        private List<NonDriver> allNonDrivers = new List<NonDriver>();

        public List<NonDriver> ReadNonDriverData()
        {
            List<NonDriver> data = new List<NonDriver>();

            using (StreamReader sr = new StreamReader(PathNonDriver))
            {
                while (!sr.EndOfStream)
                {
                    NonDriver driver = new NonDriver();
                    string? LineFromCsv = sr.ReadLine();
                    if (LineFromCsv != null)
                    {
                        string[] nonDriverFromCsv = LineFromCsv.Split(';');

                        driver.ID = Convert.ToInt32(nonDriverFromCsv[0]);
                        driver.Password = nonDriverFromCsv[1];
                        driver.Name = nonDriverFromCsv[2];
                        driver.City = nonDriverFromCsv[3];
                        driver.Deleted = Convert.ToBoolean(nonDriverFromCsv[4]);
                        data.Add(driver);
                    }
                }
            }
            return data;
        }
        public NonDriver GetNonDriverByID(int userId)
        {
            allNonDrivers = ReadNonDriverData();
            for (int i = 0; i < allNonDrivers.Count; i++)
            {
                if (allNonDrivers[i].ID == userId)
                {
                    return new NonDriver()
                    {
                        ID = Convert.ToInt32(allNonDrivers[i].ID),
                        Password = allNonDrivers[i].Password,
                        Name = allNonDrivers[i].Name,
                        City = allNonDrivers[i].City,
                    };                    
                }
            }
            return null;
        }

        public void PrintUserData(List<NonDriver> nonDrivers)
        {
            using (StreamWriter streamWriter = new StreamWriter(PathNonDriver))
            {
                List<string[]> allNonDriver = new List<string[]>();
                for (int p = 0; p < nonDrivers.Count; p++)
                {
                    streamWriter.WriteLine(String.Join(";", new string[] { Convert.ToString(nonDrivers[p].ID), nonDrivers[p].Password, nonDrivers[p].Name, nonDrivers[p].City, Convert.ToString(nonDrivers[p].Deleted) }));
                }
            }
        }
    }
}
