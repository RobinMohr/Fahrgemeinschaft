using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fahrgemeinschaft
{
    public class FileManager
    {
        public string PathUserData { get; }
        public string PathRouteData { get; }
        public string PathMapData { get; }
        public string PathMessages { get; }

        public FileManager()
        {
            PathUserData = @"C:\010Pojects\020Fahrgemeinschaft\UserInformation.csv";
            PathRouteData = @"C:\010Pojects\020Fahrgemeinschaft\DrivingInformation.csv";
            PathMapData = @"C:\010Pojects\020Fahrgemeinschaft\Distances.csv";
            PathMessages = @"C:\010Pojects\020Fahrgemeinschaft\Messages.csv";
        }       

        //
        public static void CheckAllFiles()
        {
            FileManager fileManager = new FileManager();
            List<string> filePath = new List<string> { fileManager.PathUserData, fileManager.PathMapData, fileManager.PathMapData, fileManager.PathMessages};
        foreach (string x in filePath)
            {
                CheckForCSVFile(x);
            }           
        }

        //Check if the File with the given Path is existant, and if not it will create it
        public static void CheckForCSVFile(string path)
        {
            FileInfo fi = new FileInfo(path);

            if (!fi.Exists)
            {
                FileStream fs = fi.Create();
                fs.Close();
                if (path == @"C:\010Pojects\020Fahrgemeinschaft\Distances.csv")
                {
                    AddDistancesToCSV();
                }
            }
        }

        private static void AddDistancesToCSV()
        {
            FileManager fileManager= new FileManager();
            using (StreamWriter sw = new StreamWriter(fileManager.PathMapData))
            {
                List<string[]> idk = new List<string[]>();
                string[] arr1 = new string[] { "0", "1", "13,2" };
                idk.Add(arr1);
                arr1 = new string[] { "0", "2", "7,3" };
                idk.Add(arr1);
                arr1 = new string[] { "0", "3", "24,6" };
                idk.Add(arr1);
                arr1 = new string[] { "0", "4", "29,6" };
                idk.Add(arr1);
                arr1 = new string[] { "0", "5", "21,3" };
                idk.Add(arr1);
                arr1 = new string[] { "0", "6", "6,3" };
                idk.Add(arr1);
                arr1 = new string[] { "0", "7", "9,6" };
                idk.Add(arr1);
                arr1 = new string[] { "0", "8", "13" };
                idk.Add(arr1);
                arr1 = new string[] { "0", "9", "14,6" };
                idk.Add(arr1);
                arr1 = new string[] { "0", "10", "13,6" };
                idk.Add(arr1);
                arr1 = new string[] { "0", "11", "17,1" };
                idk.Add(arr1);
                arr1 = new string[] { "0", "12", "21,2" };
                idk.Add(arr1);
                arr1 = new string[] { "0", "13", "34,8" };
                idk.Add(arr1);
                arr1 = new string[] { "1", "2", "4,1" };
                idk.Add(arr1);
                arr1 = new string[] { "1", "3", "11,4" };
                idk.Add(arr1);
                arr1 = new string[] { "1", "4", "19,5" };
                idk.Add(arr1);
                arr1 = new string[] { "1", "5", "11,2" };
                idk.Add(arr1);
                arr1 = new string[] { "1", "6", "15,7" };
                idk.Add(arr1);
                arr1 = new string[] { "1", "7", "20,1" };
                idk.Add(arr1);
                arr1 = new string[] { "1", "8", "22,5" };
                idk.Add(arr1);
                arr1 = new string[] { "1", "9", "25,1" };
                idk.Add(arr1);
                arr1 = new string[] { "1", "10", "23,9" };
                idk.Add(arr1);
                arr1 = new string[] { "1", "11", "20,8" };
                idk.Add(arr1);
                arr1 = new string[] { "1", "12", "26,2" };
                idk.Add(arr1);
                arr1 = new string[] { "1", "13", "18,5" };
                idk.Add(arr1);
                arr1 = new string[] { "2", "3", "17,1" };
                idk.Add(arr1);
                arr1 = new string[] { "2", "4", "18,8" };
                idk.Add(arr1);
                arr1 = new string[] { "2", "5", "13,7" };
                idk.Add(arr1);
                arr1 = new string[] { "2", "6", "11,1" };
                idk.Add(arr1);
                arr1 = new string[] { "2", "7", "14,4" };
                idk.Add(arr1);
                arr1 = new string[] { "2", "8", "17,9" };
                idk.Add(arr1);
                arr1 = new string[] { "2", "9", "20,5" };
                idk.Add(arr1);
                arr1 = new string[] { "2", "10", "14,9" };
                idk.Add(arr1);
                arr1 = new string[] { "2", "11", "22,6" };
                idk.Add(arr1);
                arr1 = new string[] { "2", "12", "27,1" };
                idk.Add(arr1);
                arr1 = new string[] { "2", "13", "24,2" };
                idk.Add(arr1);
                arr1 = new string[] { "3", "4", "14" };
                idk.Add(arr1);
                arr1 = new string[] { "3", "5", "22,1" };
                idk.Add(arr1);
                arr1 = new string[] { "3", "6", "28,5" };
                idk.Add(arr1);
                arr1 = new string[] { "3", "7", "31,9" };
                idk.Add(arr1);
                arr1 = new string[] { "3", "8", "37,4" };
                idk.Add(arr1);
                arr1 = new string[] { "3", "9", "37,9" };
                idk.Add(arr1);
                arr1 = new string[] { "3", "10", "30,7" };
                idk.Add(arr1);
                arr1 = new string[] { "3", "11", "40" };
                idk.Add(arr1);
                arr1 = new string[] { "3", "12", "44,4" };
                idk.Add(arr1);
                arr1 = new string[] { "3", "13", "9,5" };
                idk.Add(arr1);
                arr1 = new string[] { "4", "5", "9,8" };
                idk.Add(arr1);
                arr1 = new string[] { "4", "6", "33,6" };
                idk.Add(arr1);
                arr1 = new string[] { "4", "7", "36,9" };
                idk.Add(arr1);
                arr1 = new string[] { "4", "8", "40,3" };
                idk.Add(arr1);
                arr1 = new string[] { "4", "9", "45,2" };
                idk.Add(arr1);
                arr1 = new string[] { "4", "10", "32,3" };
                idk.Add(arr1);
                arr1 = new string[] { "4", "11", "36" };
                idk.Add(arr1);
                arr1 = new string[] { "4", "12", "38,3" };
                idk.Add(arr1);
                arr1 = new string[] { "4", "13", "22" };
                idk.Add(arr1);
                arr1 = new string[] { "5", "6", "25,2" };
                idk.Add(arr1);
                arr1 = new string[] { "5", "7", "28,5" };
                idk.Add(arr1);
                arr1 = new string[] { "5", "8", "31,9" };
                idk.Add(arr1);
                arr1 = new string[] { "5", "9", "34,5" };
                idk.Add(arr1);
                arr1 = new string[] { "5", "10", "23,9" };
                idk.Add(arr1);
                arr1 = new string[] { "5", "11", "23,6" };
                idk.Add(arr1);
                arr1 = new string[] { "5", "12", "29,9" };
                idk.Add(arr1);
                arr1 = new string[] { "5", "13", "29,5" };
                idk.Add(arr1);
                arr1 = new string[] { "6", "7", "4,1" };
                idk.Add(arr1);
                arr1 = new string[] { "6", "8", "7,6" };
                idk.Add(arr1);
                arr1 = new string[] { "6", "9", "11,3" };
                idk.Add(arr1);
                arr1 = new string[] { "6", "10", "16,4" };
                idk.Add(arr1);
                arr1 = new string[] { "6", "11", "19,3" };
                idk.Add(arr1);
                arr1 = new string[] { "6", "12", "23,8" };
                idk.Add(arr1);
                arr1 = new string[] { "6", "13", "33,5" };
                idk.Add(arr1);
                arr1 = new string[] { "7", "8", "4,1" };
                idk.Add(arr1);
                arr1 = new string[] { "7", "9", "9,5" };
                idk.Add(arr1);
                arr1 = new string[] { "7", "10", "16,8" };
                idk.Add(arr1);
                arr1 = new string[] { "7", "11", "20,8" };
                idk.Add(arr1);
                arr1 = new string[] { "7", "12", "24,3" };
                idk.Add(arr1);
                arr1 = new string[] { "7", "13", "37,8" };
                idk.Add(arr1);
                arr1 = new string[] { "8", "9", "5,4" };
                idk.Add(arr1);
                arr1 = new string[] { "8", "10", "19,5" };
                idk.Add(arr1);
                arr1 = new string[] { "8", "11", "23,5" };
                idk.Add(arr1);
                arr1 = new string[] { "8", "12", "25,7" };
                idk.Add(arr1);
                arr1 = new string[] { "8", "13", "41,6" };
                idk.Add(arr1);
                arr1 = new string[] { "9", "10", "17,2" };
                idk.Add(arr1);
                arr1 = new string[] { "9", "11", "15,2" };
                idk.Add(arr1);
                arr1 = new string[] { "9", "12", "20,4" };
                idk.Add(arr1);
                arr1 = new string[] { "9", "13", "44,9" };
                idk.Add(arr1);
                arr1 = new string[] { "10", "11", "4,6" };
                idk.Add(arr1);
                arr1 = new string[] { "10", "12", "8" };
                idk.Add(arr1);
                arr1 = new string[] { "10", "13", "39,8" };
                idk.Add(arr1);
                arr1 = new string[] { "11", "12", "6" };
                idk.Add(arr1);
                arr1 = new string[] { "11", "13", "43" };
                idk.Add(arr1);
                arr1 = new string[] { "12", "13", "47,4" };
                idk.Add(arr1);
                for (int i = 0; i < idk.Count; i++)
                {
                    sw.WriteLine(String.Join(";", idk[i]));
                }
            }
        }
        public static List<double[]> ReadMapData()
        {
            FileManager fileManager = new FileManager();
            List<double[]> entries = new List<double[]>();

            using (StreamReader sr = new StreamReader(fileManager.PathMapData))
            {
                while (!sr.EndOfStream)
                {
                    var baa = sr.ReadLine().Split(';');
                    entries.Add(new double[] { Convert.ToDouble(baa[0]), Convert.ToDouble(baa[1]), Convert.ToDouble(baa[2]) });
                }
            }
            return entries;
        }


        public static void WriteUserData(List<string[]> entries)
        {
            FileManager fileManager = new FileManager();

            using (StreamWriter sw = new StreamWriter(fileManager.PathUserData))
            {
                for (int p = 0; p < entries.Count; p++)
                {
                    sw.WriteLine(String.Join(";", entries[p]));
                }
            }
        }
        public static List<string[]> ReadUserData()
        {
            FileManager fileManager = new FileManager();
            List<string[]> entries = new List<string[]>();

            using (StreamReader sr = new StreamReader(fileManager.PathUserData))
            {
                while (!sr.EndOfStream)
                {
                    entries.Add(sr.ReadLine().Split(';'));
                }
            }
            return entries;
        }

        public static void WriteRouteData(List<string[]> entries)
        {
            FileManager fileManager = new FileManager();

            using (StreamWriter sw = new StreamWriter(fileManager.PathRouteData))
            {
                for (int p = 0; p < entries.Count; p++)
                {
                    sw.WriteLine(String.Join(";", entries[p]));
                }
            }
        }
        public static List<string[]> ReadRouteData()
        {
            FileManager fileManager = new FileManager();
            List<string[]> entries = new List<string[]>();

            using (StreamReader sr = new StreamReader(fileManager.PathRouteData))
            {
                while (!sr.EndOfStream)
                {
                    entries.Add(sr.ReadLine().Split(';'));
                }
            }
            return entries;
        }

        public static (int, int) LookForUserInFile(UserCreation user)
        {
            List<string[]> entries = FileManager.ReadUserData();
            int counter = 0;

            for (int i = 0; i < entries.Count; i++)
            {
                if (Convert.ToInt32(entries[i][0]) == user.ID)
                {
                    counter++;
                    return (i, counter);
                }
            }
            return (0, counter);
        }
        public static void AllData(UserCreation user)
        {
            List<string[]> entries = ReadRouteData();

            List<string> drivers = new List<string>();
            List<string> nonDrivers = new List<string>();


            foreach (string[] entry in entries)
            {
                if (Convert.ToInt32(entry[0]) == 0)
                {
                    nonDrivers.Add($"Der User: {entry[1]}, mit der ID: {entry[2]} möchte am {entry[3]} mit {entry[4]} anderen Leuten von --- nach ---.");
                    
                }
                else if (Convert.ToInt32(entry[0]) == 1)
                {
                    drivers.Add($"Der User: {entry[1]}, mit der ID: {entry[2]} möchte am {entry[3]} mit {entry[5]} freien Plätzen von --- nach ---.");
                }
            }

        AskUserForData:
            Console.Clear();
            Console.WriteLine("möchtest du alle daten(0), nur die suchanfragen(1), oder nur die Fahrangebote(2) angezeigt bekommen?");


            ConsoleKeyInfo whatUserData = Console.ReadKey();

            switch (Convert.ToInt32(Convert.ToString(whatUserData.KeyChar)))
            {
                case 0:
                    if (drivers.Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Momentan gibt es keine Fahrangebote.");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Das sind alle Fahrangebote:");
                        foreach (string driver in drivers)
                        {
                            Console.WriteLine(driver);
                        }
                    }

                    if (nonDrivers.Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Momentan gibt es keine Suchanfragen.");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Das sind alle Suchanfragen:");
                        foreach (string nonDriver in nonDrivers)
                        {
                            Console.WriteLine(nonDriver);
                        }
                    }
                    break;

                case 1:

                    if (drivers.Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Momentan gibt es keine Fahrangebote.");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Das sind alle Fahrangebot:");
                        foreach (string driver in drivers)
                        {
                            Console.WriteLine(driver);
                        }
                    }
                    break;

                case 2:

                    if (nonDrivers.Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Momentan gibt es keine Suchanfragen.");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Das sind alle Suchanfragen:");
                        foreach (string nonDriver in nonDrivers)
                        {
                            Console.WriteLine(nonDriver);
                        }
                    }
                    break;

                default:
                    goto AskUserForData;

            }
            Console.ReadKey();
        }
    }
}
