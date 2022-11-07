using TecAlliance.Carpools.Data.Services;

namespace TecAlliance.Carpools.Api
{
    public class Files
    {
        public readonly string PathDriver= @$"{Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\"))}TecAlliance.Carpool.Data\DriverInformation.csv";
        public readonly string PathNonDriver = @$"{Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\"))}TecAlliance.Carpool.Data\NonDriverInformation.csv";
        public readonly string PathCarpool = @$"{Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\"))}TecAlliance.Carpool.Data\Carpools.csv";

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
