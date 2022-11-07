using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Data.Services.Interfaces
{
    public interface ICarpoolDataService
    {
        /// <summary>
        /// looks for the Carpool with the Given ID 
        /// </summary>
        /// <returns>the Carpool with {carpoolID}</returns>
        Carpool GetCarpoolDataByID(int carpoolID);

        /// <summary>
        /// prints all of the Carpools back into the csv File
        /// </summary>
        void PrintCarpools(List<Carpool> carpools);

        /// <summary>
        /// reads all of the Carpool Data from the csv File(can even find deleted ones)
        /// </summary>
        /// <returns>List of all Carpools</returns>
        List<Carpool> ReadCarpoolData();
    }
}