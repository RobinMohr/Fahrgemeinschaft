using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Business.Services.Interfaces
{
    public interface ICarpoolBusinessService
    {
        /// <summary>
        /// Converts a Carpool to a CarpoolDto
        /// </summary>
        /// <returns>CarpoolDto</returns>
        CarpoolDto ConvertCarpoolToDto(Carpool carpool);

        /// <summary>
        /// creates a new Carpool and adds that to the csv File
        /// </summary>
        /// <returns>new Carpool</returns>
        CarpoolDto CreateCarpool(int userID, string startingpoint, string endpoint, int freespaces, string time);

        /// <summary>
        /// Deletes a Carpool by the id and the id from the owner
        /// </summary>
        /// <returns>deleted Carpool</returns>
        CarpoolDto DelCarpoolByID(int id, int ownerID);

        /// <summary>
        /// Searches for a Carpool with the given id in the csv File
        /// </summary>
        /// <returns>carpool with given ID</returns>
        CarpoolDto GetCarpoolByID(int id);

        /// <summary>
        /// reads all Carpools from the csv files and returns them all
        /// </summary>
        /// <returns>List of Carpools</returns>
        List<CarpoolDto> GetAllCarpools();

        /// <summary>
        /// adding your id to the passenger list of a Carpool
        /// </summary>
        /// <returns>Carpool that you joined</returns>
        CarpoolDto JoinCarpool(int carpoolID, int userID);

        /// <summary>
        /// changes the Data of a Carpool with the given ID
        /// </summary>
        /// <returns>changed carpool</returns>
        CarpoolDto ChangeCarpoolDataByID(int id, string startingPoint, string endingPoint, int freeSpaces, string time);
    }
}