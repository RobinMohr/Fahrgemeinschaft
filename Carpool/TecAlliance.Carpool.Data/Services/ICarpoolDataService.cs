using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Data.Services
{
    public interface ICarpoolDataService
    {
        Carpool GetCarpoolData(int carpoolID);
        void PrintCarpools(List<Carpool> carpools);
        List<Carpool> ReadCarpoolData();
    }
}