using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpools.Data.Models;
using TecAlliance.Carpools.Data.Services.Interfaces;

namespace TecAlliance.Carpools.Data.Services
{
    public class CarpoolDataService : ICarpoolDataService
    {
        //public string driverDataPath = Carpools.Data.Properties.Resources.GeneralPath;
        private string pathCarpoolData = @$"{Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\"))}TecAlliance.Carpool.Data\Carpools.csv";
        public string PathCarpoolData
        {
            get
            {
                return this.pathCarpoolData;
            }
            set
            {
                this.pathCarpoolData = value;
            }
        }

        private IDriverDataService _driverDataService;
        private INonDriverDataService _nonDriverDataService;

        private List<Carpool> allCarpools = new List<Carpool>();

        public CarpoolDataService(IDriverDataService driverDataService, INonDriverDataService nonDriverDataService, string pathCarpoolData)
        {
            _driverDataService = driverDataService;
            _nonDriverDataService = nonDriverDataService;
            PathCarpoolData = pathCarpoolData;
        }
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

                        Driver ownerDriver = _driverDataService.GetDriverByID(Convert.ToInt32(oldCarpool[1]));

                        string[] PassengerIDs = oldCarpool[4].Split(',');
                        foreach (string PassengerID in PassengerIDs)
                        {
                            if (PassengerID == null || PassengerID == "")
                            {
                                break;
                            }
                            passengers.Add(_nonDriverDataService.GetNonDriverByID(Convert.ToInt32(PassengerID)));
                        }
                        carpool.CarpoolId = Convert.ToInt32(oldCarpool[0]);
                        carpool.Owner = ownerDriver;
                        carpool.StartingPoint = oldCarpool[2];
                        carpool.EndingPoint = oldCarpool[3];
                        carpool.FreeSpaces = Convert.ToInt32(oldCarpool[5]);
                        carpool.Passengers = passengers;
                        carpool.Time = oldCarpool[6];
                        carpool.Deleted = Convert.ToBoolean(oldCarpool[7]);

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
                    foreach (var passenger in carpool.Passengers)
                    {
                        passengers.Add(passenger.ID);
                    }
                    streamWriter.WriteLine(string.Join(';', new string[] { Convert.ToString(carpool.CarpoolId), Convert.ToString(carpool.Owner.ID), carpool.StartingPoint, carpool.EndingPoint, string.Join(',', passengers), Convert.ToString(carpool.FreeSpaces), carpool.Time ,Convert.ToString(carpool.Deleted)}));
                }
            }
        }
        public Carpool GetCarpoolDataByID(int carpoolID)
        {
            allCarpools = ReadCarpoolData();
            if (allCarpools == null)
            {
                return null;
            }
            foreach (Carpool carpool in allCarpools)
            {
                if (carpool.CarpoolId == carpoolID)
                {
                    return new Carpool()
                    {
                        CarpoolId = carpool.CarpoolId,
                        Owner = carpool.Owner,
                        StartingPoint = carpool.StartingPoint,
                        EndingPoint = carpool.EndingPoint,
                        FreeSpaces = carpool.FreeSpaces,
                        Passengers = carpool.Passengers,
                        Time = carpool.Time,
                        Deleted = carpool.Deleted,
                    };
                }
            }
            return null;
        }
    }
}
