using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Business.Services
{
    public interface ICarpoolBusinessService
    {
        CarpoolDto CarpoolToDto(Carpool carpool);
        CarpoolDto CreateCarpool(int userID, string startingpoint, string endpoint, int freespaces, string time);
        CarpoolDto DelCarpool(int id, int ownerID);
        CarpoolDto Get(int id);
        List<CarpoolDto> GetAll();
        CarpoolDto JoinCarpool(int carpoolID, int userID);
        CarpoolDto UpdateCarpool(int id, string startingPoint, string endingPoint, int freeSpaces, string time);
    }
}