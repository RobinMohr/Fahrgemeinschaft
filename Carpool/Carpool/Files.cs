using TecAlliance.Carpools.Data.Services;

namespace TecAlliance.Carpool.Api
{
    public class Files
    {
        public readonly string PathDriver;
        public readonly string PathNonDriver;
        public readonly string PathCarpool;

        public Files()
        {
            PathDriver = DriverDataService.GetPath();
            PathNonDriver = NonDriverDataService.GetPath();
            PathCarpool = CarpoolDataService.GetPath();
        }

        public void CheckForAllCsvFiles()
        {
            List<string> allPaths = new List<string> { PathDriver, PathNonDriver, PathCarpool };
            foreach (string path in allPaths)
            {
                CheckCsv(path);
            }
        }
        public void CheckCsv(string path)
        {
            FileInfo fi = new FileInfo(path);
            if (!fi.Exists)
            {
                FileStream fs = fi.Create();
                fs.Close();
            }
        }
    }
}
