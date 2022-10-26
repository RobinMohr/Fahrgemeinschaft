using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Business.Services
{
    public interface IDriverBusinessService
    {
        DriverDto CreateNewDriver(string password, string name, string city);
        DriverDto DelDriver(int id, string password);
        DriverDto DriverToDTO(Driver d);
        List<DriverDto> GetAllDriver();
        DriverDto GetDriver(int id);
        DriverDto UpdateDriver(int id, string pw, string name, string city);
        List<Carpool> ViewCurrentCarpools(int userID);
    }
}