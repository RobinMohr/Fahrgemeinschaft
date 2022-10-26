using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Data.Services
{
    public interface INonDriverDataService
    {
        NonDriver GetNonDriver(int userId);
        void PrintUserData(List<NonDriver> nonDrivers);
        List<NonDriver> ReadUserData();
    }
}