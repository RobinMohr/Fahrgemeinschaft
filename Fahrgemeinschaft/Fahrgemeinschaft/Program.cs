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
        static void Main(string[] args)
        {
            FileManager.CheckAllFiles();

            //List<int> intermediate = new List<int> {11,2,6};
            //int start = 0;
            //int end = 10 ;

            //Console.WriteLine(Maps.Calculate(start, end, intermediate));
            //Console.ReadLine();

            FileManager.CheckAllFiles();
            UserCreation.LogInOrRegister();
        }
    }
}
