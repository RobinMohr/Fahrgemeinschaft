using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Business.Services.Interfaces
{
    public interface IDriverBusinessService
    {
        /// <summary>
        /// creates and prints a new Driver to the CSV file
        /// </summary>
        /// <returns></returns>
        DriverDto CreateNewDriver(string password, string name, string city);

        /// <summary>
        /// Deletes a Driver with the given ID
        /// </summary>
        /// <returns>delted Driver</returns>
        DriverDto DelDriverByID(int id, string password);

        /// <summary>
        /// Converts a driver to a driverDto
        /// </summary>
        /// <returns>driverDto</returns>
        DriverDto ConvertDriverToDto(Driver driver);

        /// <summary>
        /// reads all Drivers from the csv file and returns them
        /// </summary>
        /// <returns>List of Drivers</returns>
        List<DriverDto> GetAllDriver();

        /// <summary>
        /// searches in the csv file for a Driver with the given ID
        /// </summary>
        /// <returns>found Driver or null if not found</returns>
        DriverDto GetDriverById(int id);

        /// <summary>
        /// Changes the data of a Driver with the given ID
        /// </summary>
        /// <returns>changed Driver</returns>
        DriverDto ChangeDriverDataByID(int id, string pw, string name, string city);

        /// <summary>
        /// returns all Carpools where the given id is the owner ID
        /// </summary>
        /// <returns>
        /// a List of CarpoolDto
        /// </returns>
        List<Carpool> GetCarpoolsForUserID(int userID);
    }
}