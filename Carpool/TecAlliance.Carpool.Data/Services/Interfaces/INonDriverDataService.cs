using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Data.Services.Interfaces
{
    public interface INonDriverDataService
    {
        /// <summary>
        /// looks for the non Driver with the Given ID 
        /// </summary>
        /// <returns>the non Driver with {carpoolID}</returns>
        NonDriver GetNonDriverByID(int userId);

        /// <summary>
        /// prints all of the non Driver back into the csv File
        /// </summary>
        void PrintUserData(List<NonDriver> nonDrivers);

        /// <summary>
        /// reads all of the non Driver Data from the csv File(can even find deleted ones)
        /// </summary>
        /// <returns>List of all non Driver</returns>
        List<NonDriver> ReadNonDriverData();
    }
}