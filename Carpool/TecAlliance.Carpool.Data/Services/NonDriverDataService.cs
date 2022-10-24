using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Data.Services
{
    public class NonDriverDataService
    {
        public readonly string PathNonDriver = @"C:\010Pojects\020Fahrgemeinschaft\NonDriverInformation.csv";
        private List<NonDriver> allNonDrivers = new List<NonDriver>();
        public List<NonDriver> ReadUserData()
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
                        data.Add(driver);
                    }
                }
            }
            return data;
        }
        public NonDriver GetNonDriver(int userId)
        {
            int count = 0;
            allNonDrivers = ReadUserData();
            if (allNonDrivers.Count == 0)
            {
                return null;
            }
            int i = 0;
            for (int j =0; i < allNonDrivers.Count; i++)
            {
                if (allNonDrivers[i].ID == userId)
                {
                    count++;
                    break;
                }
            }
            if (count != 0)
            {
                NonDriver nonDriver = new NonDriver()
                {
                    ID = Convert.ToInt32(allNonDrivers[i].ID),
                    Password = allNonDrivers[i].Password,
                    Name = allNonDrivers[i].Name,
                    City = allNonDrivers[i].City,
                };
                return nonDriver;
            }         
            else
            {
                throw new Exception("no user with that id could be found");
            }
        }
       
        public void PrintUserData(List<NonDriver> nonDrivers)
        {
            using (StreamWriter sw = new StreamWriter(PathNonDriver))
            {
                List<string[]> sa = new List<string[]>();

                for (int p = 0; p < nonDrivers.Count; p++)
                {
                    string[] s = new string[] { Convert.ToString(nonDrivers[p].ID), nonDrivers[p].Password, nonDrivers[p].Name, nonDrivers[p].City };
                    sa.Add(s);
                }
                for (int k = 0; k < nonDrivers.Count; k++)
                {
                    sw.WriteLine(String.Join(";", sa[k]));
                }
            }
        }
    }
}
