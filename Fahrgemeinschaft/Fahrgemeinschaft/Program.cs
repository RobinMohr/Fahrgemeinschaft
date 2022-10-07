using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fahrgemeinschaft
{
    public class Program
    {
        public string PathUserData { get; }
        public string PathRouteData { get; }


        public Program()
        {
            PathUserData = @"C:\010Pojects\020Fahrgemeinschaft\UserInformation.csv";
            PathRouteData = @"C:\010Pojects\020Fahrgemeinschaft\DrivingInformation.csv";
        }


        static void Main(string[] args)
        {
            UserCreation.CheckForCSVFile();
            App.CheckForCSVFile();

            UserCreation user = UserCreation.LogIn();

            MainMenu.StartMenu(user);








            Console.ReadKey();
        }
    }
}
