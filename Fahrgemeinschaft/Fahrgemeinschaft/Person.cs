using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fahrgemeinschaft
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Driver { get; set; }
        public int Amount { get; set; }
        public int Free_Spaces { get; set; }
        public string[] Time { get; set; }
        

        public Person(int driver, string name, int amount, int free_spaces, string[] time)
        {
            this.Driver = driver;
            this.Name = name;
            this.Amount = amount;
            this.Free_Spaces = free_spaces;
            this.Time = time;
        }

        public Person()
        {

        }
    }
}
