using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Data.Services
{
    public interface IDriverDataService
    {
        Driver GetDriver(int userId);
        void PrintUserData(List<Driver> drivers);
        List<Driver> ReadNonDriverData();
    }
}