using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Data.Services
{
    public class CarpoolDataService
    {
        //assembly get current path
        //var test = Assembly.GetAssembly(typeof(CarpoolDataService)).Location;

        //string path = AssemblyDirectory;

        private readonly string PathCarpoolData = GetPath();
        DriverDataService _driverDataService = new DriverDataService();
        NonDriverDataService _nonDriverDataService = new NonDriverDataService();

        private List<Carpool> allCarpools = new List<Carpool>();

        public List<Carpool> ReadCarpoolData()
        {
            List<Carpool> carpools = new List<Carpool>();
            using (StreamReader sr = new StreamReader(PathCarpoolData))
            {
                while (!sr.EndOfStream)
                {
                    Carpool carpool = new Carpool();
                    var car = sr.ReadLine();
                    if (car == null)
                    {
                    }
                    else
                    {
                        List<NonDriver> passengers = new List<NonDriver>();

                        string[] oldCarpool = car.Split(';');

                        Driver ownerDriver = _driverDataService.GetDriver(Convert.ToInt32(oldCarpool[1]));

                        string[] PassengerIDs = oldCarpool[4].Split(',');
                        foreach (string PassengerID in PassengerIDs)
                        {
                            if (PassengerID == null || PassengerID == "")
                            {
                                break;
                            }
                            passengers.Add(_nonDriverDataService.GetNonDriver(Convert.ToInt32(PassengerID)));
                        }
                        carpool.CarpoolId = Convert.ToInt32(oldCarpool[0]);
                        carpool.Owner = ownerDriver;
                        carpool.StartingPoint = oldCarpool[2];
                        carpool.EndingPoint = oldCarpool[3];
                        carpool.FreeSpaces = Convert.ToInt32(oldCarpool[5]);
                        carpool.Passengers = passengers;
                        carpool.Time = oldCarpool[6];

                        carpools.Add(carpool);
                    }
                }
            }
            return carpools;
        }
        public void PrintCarpools(List<Carpool> carpools)
        {
            using (StreamWriter streamWriter = new StreamWriter(PathCarpoolData))
            {
                foreach (Carpool carpool in carpools)
                {
                    List<int> passengers = new List<int>();
                    foreach (var x in carpool.Passengers)
                    {
                        passengers.Add(x.ID);
                    }
                    string passengerID = string.Join(',', passengers);
                    streamWriter.WriteLine(String.Join(';', new string[] { Convert.ToString(carpool.CarpoolId), Convert.ToString(carpool.Owner.ID), carpool.StartingPoint, carpool.EndingPoint, passengerID, Convert.ToString(carpool.FreeSpaces), carpool.Time }));
                }
            }
        }
        public Carpool GetCarpoolData(int carpoolID)
        {
            allCarpools = ReadCarpoolData();
            int carpoolPos = 0;
            if (allCarpools == null)
            {
                return null;
            }
            foreach (Carpool carpool in allCarpools)
            {
                if (carpool.CarpoolId == carpoolID)
                {
                    break;
                }
                carpoolPos++;
            }
            return new Carpool()
            {
                CarpoolId = allCarpools[carpoolPos].CarpoolId,
                Owner = allCarpools[carpoolPos].Owner,
                StartingPoint = allCarpools[carpoolPos].StartingPoint,
                EndingPoint = allCarpools[carpoolPos].EndingPoint,
                FreeSpaces = allCarpools[carpoolPos].FreeSpaces,
                Passengers = allCarpools[carpoolPos].Passengers,
                Time = allCarpools[carpoolPos].Time,
            };
        }
        public static string GetPath()
        {
            string path = @$"{Environment.CurrentDirectory}";
            return @$"{Path.GetFullPath(Path.Combine(path, @"..\"))}TecAlliance.Carpool.Data\Carpools.csv";
        }
    }
}
