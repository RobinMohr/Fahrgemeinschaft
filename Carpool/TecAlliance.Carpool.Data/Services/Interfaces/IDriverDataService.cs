using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Data.Services.Interfaces
{
    public interface IDriverDataService
    {
        /// <summary>
        /// looks for the non Driver with the Given ID 
        /// </summary>
        /// <returns>the non Driver with {carpoolID}</returns>
        Driver GetDriverByID(int userId);

        /// <summary>
        /// prints all of the non Driver back into the csv File
        /// </summary>
        void PrintUserData(List<Driver> drivers);

        /// <summary>
        /// reads all of the non Driver Data from the csv File(can even find deleted ones)
        /// </summary>
        /// <returns>List of all non Driver</returns>
        List<Driver> ReadNonDriverData();
    }
}