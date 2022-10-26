using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Business.Services
{
    public interface INonDriverBusinessService
    {
        NonDriverDto CreateNewNonDriver(string password, string name, string city);
        NonDriverDto DelNonDriver(int id, string password);
        List<NonDriverDto> GetAllNonDriver();
        NonDriverDto GetNonDriver(int id);
        NonDriverDto NonDriverToDto(NonDriver nonDriver);
        NonDriverDto UpdateNonDriver(int id, string pw, string name, string city);
        List<CarpoolDto> ViewCurrentCarpools(int userID);
    }
}