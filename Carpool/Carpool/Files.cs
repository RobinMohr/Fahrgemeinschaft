namespace TecAlliance.Carpool.Api
{
    public class Files
    {
        public readonly string PathDriver;
        public readonly string PathNonDriver;
        public readonly string PathCarpool;

        public Files()
        {
            PathDriver = @"C:\010Pojects\020Fahrgemeinschaft\DriverInformation.csv";
            PathNonDriver = @"C:\010Pojects\020Fahrgemeinschaft\NonDriverInformation.csv";
            PathCarpool = @"C:\010Pojects\020Fahrgemeinschaft\Carpools.csv";
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
