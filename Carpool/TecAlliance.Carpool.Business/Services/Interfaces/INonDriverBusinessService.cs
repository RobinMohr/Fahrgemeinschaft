using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Business.Services.Interfaces
{
    public interface INonDriverBusinessService
    {
        /// <summary>
        /// Creates and prints the non driver in the csv file
        /// </summary>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <param name="city"></param>
        /// <returns>created non Driver</returns>
        NonDriverDto CreateNewNonDriver(string password, string name, string city);

        /// <summary>
        /// Sets the non driver with the Id and password to deleted, so he can not be used anymore
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns>delted driver</returns>
        NonDriverDto DelNonDriverByID(int id, string password);

        /// <summary>
        /// reads all non Drivers from the csv file and returns them
        /// </summary>
        /// <returns>List of all non drivers</returns>
        List<NonDriverDto> GetAllNonDriver();

        /// <summary>
        /// Gets a non driver by ID
        /// </summary>
        /// <returns>NonDriverDto with the wanted ID</returns>
        NonDriverDto GetNonDriverByID(int id);

        /// <summary>
        /// Covnerts a given non Driver to a nonDriverDto
        /// </summary>
        /// <returns>
        /// nonDriverDto
        /// </returns>
        NonDriverDto ConvertNonDriverToDto(NonDriver nonDriver);

        /// <summary>
        /// changes Data from a nonDriver with the given ID
        /// </summary>
        /// <returns>updated nonDriver</returns>
        NonDriverDto ChangeNonDriverDataByID(int id, string pw, string name, string city);

        /// <summary>
        /// returns all Carpools where the given id is included as a passenger
        /// </summary>
        /// <returns>
        /// a List of CarpoolDto
        /// </returns>
        List<CarpoolDto> GetCarpoolsForUserID(int userID);
    }
}