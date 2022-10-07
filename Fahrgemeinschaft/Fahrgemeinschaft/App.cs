using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fahrgemeinschaft
{
    public class App
    {
        public static void CheckForCSVFile()
        {
            Program program = new Program();
            FileInfo fi = new FileInfo(program.PathRouteData);

            if (!fi.Exists)
            {
                fi.Create();
                var fileName = Assembly.GetExecutingAssembly().Location;
                System.Diagnostics.Process.Start(fileName);
                Environment.Exit(0);
            }
        }

        public static void Planning(UserCreation user)
        {
            Program program = new Program();
            Person person = new Person();

            person.Name = user.Name;
            person.Id = user.ID;
            person.Driver = true;

            int amount_;

            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════╗\n" +
                              "║ Wie viele Plätze hast du in deinem Auto frei? ║\n" +
                              "╚═══════════════════════════════════════════════╝\n");
            string amount = Console.ReadLine();
            int.TryParse(amount, out amount_);

            person.Free_Spaces = amount_;

            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗\n" +
                              "║         An welchem Datum und Uhrzeit wirst du Fahren?          ║\n" +
                              "║     Bitte in dem Format (Tag,Monat,Jahr,Uhrzeit) eingeben      ║\n" +
                              "║              ein Beispiel wäre: 07.10.2022.17:30               ║\n" +
                              "╚════════════════════════════════════════════════════════════════╝");

            person.Time = Console.ReadLine().Split('.');

            string[] array = new string[] { 1 + $";{person.Name};{person.Id};{String.Join(".",person.Time)};{person.Amount};{person.Free_Spaces}" };

            List<string[]> entries = new List<string[]>();

            using (StreamReader sr = new StreamReader(program.PathRouteData))
            {
                while (!sr.EndOfStream)
                {
                    entries.Add(sr.ReadLine().Split(';'));
                }
                entries.Add(array);
            }

            using (StreamWriter sw = new StreamWriter(program.PathRouteData))
            {
                for (int i = 0; i < entries.Count; i++)
                {
                    sw.WriteLine(String.Join(";", entries[i]));
                }
            }

            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗\n" +
                              "║ Möchtest du dir alle möglichen Mitfahrer angucken? (y/n) ║\n" +
                              "╚══════════════════════════════════════════════════════════╝");

            ConsoleKeyInfo entry1 = Console.ReadKey();

            string entry = Convert.ToString(entry1.KeyChar);
            entry.ToLower();

            int count = 0;

            Console.Clear();

            List<Person> nonDrivers = new List<Person>();

            List<string[]> allEntries = new List<string[]>();
            if (entry == "y")
            {
                using (StreamReader sr = new StreamReader(program.PathRouteData))
                {
                    while (!sr.EndOfStream)
                    {
                        allEntries.Add(sr.ReadLine().Split(';'));
                    }
                }

                    for (int j = 0; j < allEntries.Count; j++)
                    {
                        Person person1 = new Person();

                        if (Convert.ToInt32(allEntries[j][0]) == 0 && Convert.ToInt32(allEntries[j][2]) != user.ID)
                        {
                            person1.Time = allEntries[j][3].Split('.');
                            person1.Name = allEntries[j][1];
                            person1.Id = Convert.ToInt32(allEntries[j][2]);
                            person1.Amount = Convert.ToInt32(allEntries[j][4]);                            

                            if (person.Time[0] == person1.Time[0] && person.Time[1] == person1.Time[1] && person.Time[1] == person1.Time[1] && person.Free_Spaces >= person1.Amount)
                            {
                                nonDrivers.Add(person1);
                                count++;
                            }
                        }
                    }
                
                foreach (Person p in nonDrivers)
                {
                    int most = 0;


                    string id = Convert.ToString(p.Id);
                    string name = Convert.ToString(p.Name);
                    string time = String.Join(".", p.Time);
                    string amountPeople = Convert.ToString(p.Amount);


                    int amountID = 7 + id.Count(),
                         amountTime = 22 + time.Count(),
                         amountName = 9 + name.Count(),
                         amountAmountPeople = 10 + amountPeople.Count();


                    List<int> allAmount = new List<int> { amountID, amountTime, amountName, amountAmountPeople };

                    foreach (int amount1 in allAmount)
                    {
                        if (most < amount1)
                        {
                            most = amount1;
                        }
                    }
                    string[,] userData = new string[11, most];

                    for (int j = 0; j < 11; j++)
                    {
                        for (int i = 0; i < most; i++)
                        {
                            if (j == 0)
                            {
                                if (i == 0)
                                {
                                    userData[j, i] = "╔";
                                }
                                else if (i == most - 1)
                                {
                                    userData[j, i] = "╗";
                                }
                                else
                                {
                                    userData[j, i] = "═";
                                }
                            }

                            else if (j == 2)
                            {
                                string name_ = $"{p.Name}";

                                int leftPadding = (most / 2) + (name_.Length / 2);

                                name_ = name_.PadLeft(leftPadding);

                                List<char> chars = new List<char>();

                                foreach (char x in name_)
                                {
                                    chars.Add(x);
                                }

                                if (i < chars.Count)
                                {
                                    userData[j, i] = Convert.ToString(chars[i]);
                                }
                            }

                            else if (j == 5)
                            {
                                string id_ = $"ID:{p.Id}".PadRight((id.Count() + 3) / 2 + most / 2);
                                id_ = id_.PadLeft(most);

                                List<char> chars = new List<char>();

                                foreach (char x in id_)
                                {
                                    chars.Add(x);
                                }
                                if (i < chars.Count)
                                {
                                    userData[j, i] = Convert.ToString(chars[i]);
                                }
                            }
                            else if (j == 7)
                            {
                                string pw_ = $"Datum und Uhrzeit:{String.Join(".",p.Time)}".PadLeft((Convert.ToString(amountTime).Count() + 18) / 2 + most / 2);
                                pw_ = pw_.PadRight(most - 4);


                                List<char> chars = new List<char>();

                                foreach (char x in pw_)
                                {
                                    chars.Add(x);
                                }

                                if (i < chars.Count)
                                {
                                    userData[j, i + 1] = Convert.ToString(chars[i]);
                                }
                            }

                            else if (j == 9)
                            {
                                string direction_ = $"Anzahl:{p.Amount}".PadLeft((amountPeople.Count() + 7) / 2 + most / 2);
                                direction_ = direction_.PadRight(most - 4);

                                List<char> chars = new List<char>();

                                foreach (char x in direction_)
                                {
                                    chars.Add(x);
                                }

                                if (i < chars.Count)
                                {
                                    userData[j, i + 1] = Convert.ToString(chars[i]);
                                }
                            }

                            if (j == 10)
                            {
                                if (i == 0)
                                {
                                    userData[j, i] = "╚";
                                }
                                else if (i == most - 1)
                                {
                                    userData[j, i] = "╝";
                                }
                                else
                                {
                                    userData[j, i] = "═";
                                }
                            }
                            if (j == 4)
                            {
                                if (i == 0)
                                {
                                    userData[j, i] = "╠";
                                }
                                else if (i == most - 1)
                                {
                                    userData[j, i] = "╣";
                                }
                                else
                                {
                                    userData[j, i] = "═";
                                }
                            }
                            if (j == 6 || j == 8)
                            {
                                if (i == 0)
                                {
                                    userData[j, i] = "╟";
                                }
                                else if (i == most - 1)
                                {
                                    userData[j, i] = "╢";
                                }
                                else
                                {
                                    userData[j, i] = "─";
                                }
                            }

                            else if (i == 0 || i == most - 1)
                            {
                                if (userData[j, i] != "╢" && userData[j, i] != "╟" && userData[j, i] != "╝" && userData[j, i] != "╚" && userData[j, i] != "╔" && userData[j, i] != "╗" && userData[j, i] != "╣" && userData[j, i] != "╠")
                                {
                                    userData[j, i] = "║";
                                }
                            }

                            else if (userData[j, i] == "" || userData[j, i] == null)
                            {
                                userData[j, i] = " ";
                            }
                        }
                    }

                    for (int j = 0; j < 11; j++)
                    {
                        for (int i = 0; i < most; i++)
                        {
                            Console.Write(userData[j, i]);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write("\n");
                    }
                    Console.ReadKey();
                }

                if (count == 0)
                {
                    Console.WriteLine("╔══════════════════════════════════════════════════╗\n" +
                                      "║ Niemand sucht gerade nach einer Fahrgemeinschaft ║\n" +
                                      "╚══════════════════════════════════════════════════╝");
                    Thread.Sleep(1500);
                }
                else
                {
                    
                    Console.WriteLine("╔════════════════════════════════════════════════════════╗\n" +
                                      "║ Möchtest du jemanden von den Personen mitnehmen? (y/n) ║\n" +
                                      "╚════════════════════════════════════════════════════════╝\n");

                    ConsoleKeyInfo askingUserToPickSomeoneUp = Console.ReadKey();
                    if (Convert.ToString(askingUserToPickSomeoneUp.KeyChar) == "y")
                    {
                        Console.Clear();
                        int x = 0;
                        foreach (Person p in nonDrivers)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"{x}: {p.Name}, {string.Join(".", p.Time)}, {p.Amount}\n");
                            Console.WriteLine("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
                            x++;
                        }
                    chooseNonDriver:


                        Console.WriteLine("\n╔════════════════════════════╗\n" +
                                            "║ Wen möchtest du mitnehmen? ║\n" +
                                            "╚════════════════════════════╝\n");

                        int.TryParse(Convert.ToString(Console.ReadKey().KeyChar), out int chooseNonDriver);
                        
                                allEntries.Remove(allEntries[chooseNonDriver]);
                            person.Free_Spaces = person.Free_Spaces - nonDrivers[chooseNonDriver].Amount;
                        if (person.Free_Spaces == 0)
                        {
                            allEntries.Remove(allEntries[allEntries.Count]);
                        }


                        using (StreamWriter sw = new StreamWriter(program.PathRouteData))
                        {
                            for (int i = 0; i < allEntries.Count; i++)
                            {
                                sw.WriteLine(String.Join(";", allEntries[i]));
                            }
                        }









                    }
                }
            }
            




        }

        public static void Joining(UserCreation user)
        {
            Program program = new Program();
            Person person = new Person();

            person.Name = user.Name;
            person.Id = user.ID;
            person.Driver = false;

            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗\n" +
                              "║ Gehen mit dir noch andere Leute mit? (y/n) ║\n" +
                              "╚════════════════════════════════════════════╝");

            ConsoleKeyInfo entry1 = Console.ReadKey();

            string entry1_ = Convert.ToString(entry1.KeyChar);
            entry1_.ToLower();

            int amount_;

            if (entry1_ == "y")
            {
                Console.Clear();
                Console.WriteLine("╔═══════════════════════════════════════════╗\n" +
                                  "║ Wie viele Leute gehen außer dir noch mit? ║\n" +
                                  "╚═══════════════════════════════════════════╝");

                string amount = Console.ReadLine();
                int.TryParse(amount, out amount_);

                person.Amount = amount_ + 1;
            }
            else 
            {
                person.Amount = 1;
            }
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗\n" +
                              "║         An welchem Datum und Uhrzeit wirst du Fahren?          ║\n" +
                              "║     Bitte in dem Format (Tag,Monat,Jahr,Uhrzeit) eingeben      ║\n" +
                              "║              ein Beispiel wäre: 07.10.2022.17:30               ║\n" +
                              "╚════════════════════════════════════════════════════════════════╝");

            person.Time = Console.ReadLine().Split(',');

            string[] array = new string[] { 0 + $";{person.Name};{person.Id};{String.Join(".", person.Time)};{person.Amount};{person.Free_Spaces}" };

            List<string[]> entries = new List<string[]>();

            using (StreamReader sr = new StreamReader(program.PathRouteData))
            {
                while (!sr.EndOfStream)
                {
                    entries.Add(sr.ReadLine().Split(';'));
                }
                entries.Add(array);
            }

            using (StreamWriter sw = new StreamWriter(program.PathRouteData))
            {
                for (int i = 0; i < entries.Count; i++)
                {
                    sw.WriteLine(String.Join(";", entries[i]));
                }
            }

            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════╗\n" +
                              "║ Möchtest du dir alle möglichen Fahrer angucken? (y/n) ║\n" +
                              "╚═══════════════════════════════════════════════════════╝");

            ConsoleKeyInfo entry2 = Console.ReadKey();

            string entry = Convert.ToString(entry2.KeyChar);
            entry.ToLower();

            int count = 0;

            Console.Clear();

            List<Person> drivers = new List<Person>();

            List<string[]> allEntries = new List<string[]>();

            using (StreamReader sr = new StreamReader(program.PathRouteData))
            {
                while (!sr.EndOfStream)
                {
                    allEntries.Add(sr.ReadLine().Split(';'));
                }
            }

            if (entry == "y")
            {
                for (int j = 0; j < allEntries.Count; j++)
                {
                    Person person1 = new Person();

                    if (Convert.ToInt32(allEntries[j][0]) == 1 && Convert.ToInt32(allEntries[j][2]) != user.ID)
                    {
                        person1.Time = allEntries[j][3].Split('.');
                        person1.Name = allEntries[j][1];
                        person1.Id = Convert.ToInt32(allEntries[j][2]);
                        person1.Amount = Convert.ToInt32(allEntries[j][4]);                        

                        if (person.Time[0] == person1.Time[0] && person.Time[1] == person1.Time[1] && person.Time[2] == person1.Time[2] && person1.Free_Spaces >= person.Amount)
                        {
                            drivers.Add(person1);
                            count++;
                        }

                    }
                }

                foreach (Person p in drivers)
                {
                    int most = 0;

                    string id = Convert.ToString(p.Id);
                    string name = Convert.ToString(p.Name);
                    string time = String.Join(".", p.Time);
                    string amountPeople = Convert.ToString(p.Amount);


                    int amountID = 7 + id.Count(),
                         amountTime = 22 + time.Count(),
                         amountName = 9 + name.Count(),
                         amountAmountPeople = 10 + amountPeople.Count();


                    List<int> allAmount = new List<int> { amountID, amountTime, amountName, amountAmountPeople };

                    foreach (int amount1 in allAmount)
                    {
                        if (most < amount1)
                        {
                            most = amount1;
                        }
                    }

                    string[,] userData = new string[11, most];

                    for (int j = 0; j < 11; j++)
                    {
                        for (int i = 0; i < most; i++)
                        {
                            if (j == 0)
                            {
                                if (i == 0)
                                {
                                    userData[j, i] = "╔";
                                }
                                else if (i == most - 1)
                                {
                                    userData[j, i] = "╗";
                                }
                                else
                                {
                                    userData[j, i] = "═";
                                }
                            }

                            else if (j == 2)
                            {
                                string DATA = $"{p.Name}".PadLeft(name.Count() / 2 + most / 2);
                                DATA = DATA.PadRight(most);

                                List<char> chars = new List<char>();

                                foreach (char x in DATA)
                                {
                                    chars.Add(x);
                                }
                                if (i < chars.Count)
                                {
                                    userData[j, i] = Convert.ToString(chars[i]);
                                }
                            }

                            else if (j == 5)
                            {
                                string id_ = $"ID:{p.Id}".PadRight((id.Count() + 3) / 2 + most / 2);
                                id_ = id_.PadLeft(most);

                                List<char> chars = new List<char>();

                                foreach (char x in id_)
                                {
                                    chars.Add(x);
                                }
                                if (i < chars.Count)
                                {
                                    userData[j, i] = Convert.ToString(chars[i]);
                                }
                            }
                            else if (j == 7)
                            {
                                string pw_ = $"Datum und Uhrzeit:{p.Time}".PadLeft((Convert.ToString(amountTime).Count() + 18) / 2 + most / 2);
                                pw_ = pw_.PadRight(most - 4);


                                List<char> chars = new List<char>();

                                foreach (char x in pw_)
                                {
                                    chars.Add(x);
                                }

                                if (i < chars.Count)
                                {
                                    userData[j, i] = Convert.ToString(chars[i]);
                                }
                            }

                            else if (j == 9)
                            {
                                string direction_ = $" Anzahl:{user.Direction}".PadLeft((amountPeople.Count() + 7) / 2 + most / 2);
                                direction_ = direction_.PadRight(most - 4);

                                List<char> chars = new List<char>();

                                foreach (char x in direction_)
                                {
                                    chars.Add(x);
                                }

                                if (i < chars.Count)
                                {
                                    userData[j, i] = Convert.ToString(chars[i]);
                                }
                            }

                            if (j == 10)
                            {
                                if (i == 0)
                                {
                                    userData[j, i] = "╚";
                                }
                                else if (i == most - 1)
                                {
                                    userData[j, i] = "╝";
                                }
                                else
                                {
                                    userData[j, i] = "═";
                                }
                            }
                            if (j == 4)
                            {
                                if (i == 0)
                                {
                                    userData[j, i] = "╠";
                                }
                                else if (i == most - 1)
                                {
                                    userData[j, i] = "╣";
                                }
                                else
                                {
                                    userData[j, i] = "═";
                                }
                            }
                            if (j == 6 || j == 8)
                            {
                                if (i == 0)
                                {
                                    userData[j, i] = "╟";
                                }
                                else if (i == most - 1)
                                {
                                    userData[j, i] = "╢";
                                }
                                else
                                {
                                    userData[j, i] = "─";
                                }
                            }

                            else if (i == 0 || i == most - 1)
                            {
                                if (userData[j, i] != "╢" && userData[j, i] != "╟" && userData[j, i] != "╝" && userData[j, i] != "╚" && userData[j, i] != "╔" && userData[j, i] != "╗" && userData[j, i] != "╣" && userData[j, i] != "╠")
                                {
                                    userData[j, i] = "║";
                                }
                            }

                            else if (userData[j, i] == "" || userData[j, i] == null)
                            {
                                userData[j, i] = " ";
                            }
                        }
                    }

                    for (int j = 0; j < 11; j++)
                    {
                        for (int i = 0; i < most; i++)
                        {
                            Console.Write(userData[j, i]);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write("\n");
                    }
                    Console.ReadKey();
                }

                if (count == 0)
                {
                    Console.WriteLine("╔════════════════════════════════════════════════╗\n" +
                                      "║ Niemand bietet gerade eine Fahrgemeinschaft an ║\n" +
                                      "╚════════════════════════════════════════════════╝");
                    Thread.Sleep(1500);
                }
            }
        }

        public static void Change(UserCreation user)
        {
            Program program = new Program();
            List<string[]> entries = new List<string[]>();

            using (StreamReader sr = new StreamReader(program.PathRouteData))
            {
                while (!sr.EndOfStream)
                {
                    entries.Add(sr.ReadLine().Split(';'));
                }
            }

            List<string[]> userEntries = new List<string[]>();

            for (int i = 0; i < entries.Count; i++)
            {
                if (Convert.ToInt32(entries[i][2]) == user.ID)
                {
                    userEntries.Add(entries[i]);
                }
            }

            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════╗\n" +
                             $"║ Du hast gerade {userEntries.Count} Einträge\t║\n" +
                              "╚═══════════════════════════════╝");


            List<Person> person = new List<Person>();


            for (int j = 0; j < userEntries.Count; j++)
            {
                if (Convert.ToInt32(userEntries[j][2]) == user.ID)
                {
                    Person person1 = new Person();
                    if (Convert.ToInt32(userEntries[j][0]) == 1)
                    {
                        person1.Driver = true;
                    }
                    else if (Convert.ToInt32(userEntries[j][0]) == 0)
                    {
                        person1.Driver = false;
                    }
                    person1.Name = userEntries[j][1];
                    person1.Id = Convert.ToInt32(userEntries[j][2]);
                    person1.Time = userEntries[j][3].Split(',');
                    person1.Amount = Convert.ToInt32(userEntries[j][4]);
                    person1.Free_Spaces = Convert.ToInt32(userEntries[j][5]);

                    person.Add(person1);

                }
            }
            foreach (Person p in person)
            {
                int most = 0;


                string id = Convert.ToString(p.Id);
                string name = Convert.ToString(p.Name);
                string time = Convert.ToString(p.Time);
                string amountPeople = Convert.ToString(p.Amount);
                string freeSpaces = Convert.ToString(p.Free_Spaces);


                int amountID = 7 + id.Count(),
                     amountTime = 22 + time.Count(),
                     amountName = 9 + name.Count(),
                     amountAmountPeople = 10 + amountPeople.Count(),
                     amountFreeSpaces = 17 + freeSpaces.Count();


                List<int> allAmount = new List<int> { amountID, amountTime, amountName, amountAmountPeople, amountFreeSpaces};

                foreach (int amount1 in allAmount)
                {
                    if (most < amount1)
                    {
                        most = amount1;
                    }
                }

                string[,] userData = new string[11, most];

                //schöne Tabelle erstellen
                for (int j = 0; j < 11; j++)
                {
                    for (int i = 0; i < most; i++)
                    {
                        if (j == 0)
                        {
                            if (i == 0)
                            {
                                userData[j, i] = "╔";
                            }
                            else if (i == most - 1)
                            {
                                userData[j, i] = "╗";
                            }
                            else
                            {
                                userData[j, i] = "═";
                            }
                        }

                        else if (j == 2)
                        {
                            string DATA = $"{p.Name}".PadLeft(name.Count() / 2 + most / 2);
                            DATA = DATA.PadRight(most);

                            List<char> chars = new List<char>();

                            foreach (char x in DATA)
                            {
                                chars.Add(x);
                            }
                            if (i < chars.Count)
                            {
                                userData[j, i] = Convert.ToString(chars[i]);
                            }
                        }

                        else if (j == 5)
                        {
                            string id_ = $"ID:{p.Id}".PadRight((id.Count() + 3) / 2 + most / 2);
                            id_ = id_.PadLeft(most);

                            List<char> chars = new List<char>();

                            foreach (char x in id_)
                            {
                                chars.Add(x);
                            }
                            if (i < chars.Count)
                            {
                                userData[j, i] = Convert.ToString(chars[i]);
                            }
                        }
                        else if (j == 7)
                        {
                            string pw_ = $"Datum und Uhrzeit:{p.Time}".PadLeft(((Convert.ToString(time).Count() + 22) / 2) + (most / 2));

                            List<char> chars = new List<char>();

                            foreach (char x in pw_)
                            {
                                chars.Add(x);
                            }

                            if (i < chars.Count)
                            {
                                userData[j, i] = Convert.ToString(chars[i]);
                            }
                        }

                        else if (j == 9)
                        {
                            if (p.Driver)
                            {
                                string direction_ = $" Freie Plätze:{p.Free_Spaces}".PadLeft((freeSpaces.Count() + 13) / 2 + most / 2);
                                direction_ = direction_.PadRight(most - 4);

                                List<char> chars = new List<char>();

                                foreach (char x in direction_)
                                {
                                    chars.Add(x);
                                }

                                if (i < chars.Count)
                                {
                                    userData[j, i] = Convert.ToString(chars[i]);
                                }
                            }

                            else if (!p.Driver)
                            {
                                string amount_ = $" Anzahl:{p.Amount}".PadLeft((amountPeople.Count() + 7) / 2 + most / 2);
                                amount_ = amount_.PadRight(most - 4);

                                List<char> chars = new List<char>();

                                foreach (char x in amount_)
                                {
                                    chars.Add(x);
                                }

                                if (i < chars.Count)
                                {
                                    userData[j, i] = Convert.ToString(chars[i]);
                                }
                            }

                            
                        }

                        if (j == 10)
                        {
                            if (i == 0)
                            {
                                userData[j, i] = "╚";
                            }
                            else if (i == most - 1)
                            {
                                userData[j, i] = "╝";
                            }
                            else
                            {
                                userData[j, i] = "═";
                            }
                        }
                        if (j == 4)
                        {
                            if (i == 0)
                            {
                                userData[j, i] = "╠";
                            }
                            else if (i == most - 1)
                            {
                                userData[j, i] = "╣";
                            }
                            else
                            {
                                userData[j, i] = "═";
                            }
                        }
                        if (j == 6 || j == 8)
                        {
                            if (i == 0)
                            {
                                userData[j, i] = "╟";
                            }
                            else if (i == most - 1)
                            {
                                userData[j, i] = "╢";
                            }
                            else
                            {
                                userData[j, i] = "─";
                            }
                        }

                        else if (i == 0 || i == most - 1)
                        {
                            if (userData[j, i] != "╢" && userData[j, i] != "╟" && userData[j, i] != "╝" && userData[j, i] != "╚" && userData[j, i] != "╔" && userData[j, i] != "╗" && userData[j, i] != "╣" && userData[j, i] != "╠")
                            {
                                userData[j, i] = "║";
                            }
                        }

                        else if (userData[j, i] == "" || userData[j, i] == null)
                        {
                            userData[j, i] = " ";
                        }
                    }
                }

                for (int j = 0; j < 11; j++)
                {
                    for (int i = 0; i < most; i++)
                    {
                        Console.Write(userData[j, i]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write("\n");
                }
                Console.ReadKey();
            }

            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗\n" +
                              "║ Möchtest du einen der Einträge bearbeiten oder löschen? (y/n) ║\n" +
                              "╚═══════════════════════════════════════════════════════════════╝");

            ConsoleKeyInfo entry2 = Console.ReadKey();

            string entry = Convert.ToString(entry2.KeyChar);
            entry.ToLower();

            if (entry == "y")
            {

            EntryDelOrChange:

                int i = 0;

                Console.Clear();

                foreach (Person p in person)
                {
                    if (!p.Driver)
                    {
                        Console.WriteLine($"{i}: Zeit: {p.Time} Anzahl: {p.Amount}\n");
                    }

                    else if (p.Driver)
                    {
                        Console.WriteLine($"{i}: Zeit: {p.Time} freie Plätze: {p.Free_Spaces}\n");
                    }
                    
                    i++;
                }

                Console.WriteLine("╔══════════════════════════════════════════════╗\n" +
                                  "║ Welchen der Einträge möchtest du bearbeiten? ║\n" +
                                  "╚══════════════════════════════════════════════╝\n");

                int.TryParse(Console.ReadLine(), out int entry1);

            DelOrChange:

                Console.Clear();

                Console.WriteLine("╔═════════════════════════════════════════════════════╗\n" +
                                  "║ Möchtest du diesen löschen (0) oder bearbeiten (1)? ║\n" +
                                  "╚═════════════════════════════════════════════════════╝\n");

                ConsoleKeyInfo delOrChange = Console.ReadKey();

                int.TryParse(Convert.ToString(delOrChange.KeyChar), out int result);

                int counter = 0;

                if (result == 0)
                {
                    for (int k = 0; k < entries.Count; k++)
                    {
                        if (entries[k][0] == Convert.ToString(person[entry1].Driver) && entries[k][1] == person[entry1].Name && entries[k][2] == Convert.ToString(person[entry1].Id) && entries[k][3].Split(',') == person[entry1].Time && entries[k][4] == Convert.ToString(person[entry1].Amount) && entries[k][5] == Convert.ToString(person[entry1].Free_Spaces))
                        {
                            entries.Remove(entries[k]);
                            counter++;
                        }
                    }
                    if (counter == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("╔═════════════════════════════════════════════════════════════════════╗\n" +
                                          "║ Dein Eintrag wurde nicht gefunden und konnte nicht gelöscht werden. ║\n" +
                                          "╚═════════════════════════════════════════════════════════════════════╝\n");
                        Thread.Sleep(1500);
                        goto DelOrChange;
                    }
                }

                else if (result == 1)
                {
                goBack:

                    if (Convert.ToInt32(person[entry1].Driver) == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("╔════════════════════════════════════════════╗\n" +
                                          "║ Welche Information möchtest du Bearbeiten? ║\n" +
                                          "║      Zeit (1), Anzahl der Mitfahrer (2)    ║\n" +
                                          "╚════════════════════════════════════════════╝\n");
                    }

                    else if (Convert.ToInt32(person[entry1].Driver) == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("╔════════════════════════════════════════════╗\n" +
                                          "║ Welche Information möchtest du Bearbeiten? ║\n" +
                                          "║   Anzahl der freien Plätze (0), Zeit (1)   ║\n" +
                                          "╚════════════════════════════════════════════╝\n");
                    }

                    ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

                    int.TryParse(Convert.ToString(consoleKeyInfo.KeyChar), out int entry4);

                    if (entry4 == 0)
                    {
                        for (int k = 0; k < entries.Count; k++)
                        {
                            if (entries[k][0] == Convert.ToString(person[entry1].Driver) && entries[k][1] == person[entry1].Name && entries[k][2] == Convert.ToString(person[entry1].Id) && entries[k][3].Split(',') == person[entry1].Time && entries[k][4] == Convert.ToString(person[entry1].Amount) && entries[k][5] == Convert.ToString(person[entry1].Free_Spaces))
                            {
                                Console.Clear();
                                Console.WriteLine("╔═══════════════════════════════════════════════════╗\n" +
                                                  "║ Bitte gebe eine neue Anzahl von freien Plätzen an ║\n" +
                                                  "╚═══════════════════════════════════════════════════╝\n");

                                int.TryParse(Console.ReadLine(), out int new_entry);
                                entries[k][5] = Convert.ToString(new_entry);
                            }
                        }
                    }

                    if (entry4 == 1)
                    {
                        for (int k = 0; k < entries.Count; k++)
                        {
                            if (entries[k][0] == Convert.ToString(person[entry1].Driver) && entries[k][1] == person[entry1].Name && entries[k][2] == Convert.ToString(person[entry1].Id) && entries[k][3].Split(',') == person[entry1].Time && entries[k][4] == Convert.ToString(person[entry1].Amount) && entries[k][5] == Convert.ToString(person[entry1].Free_Spaces))
                            {
                                Console.Clear();
                                Console.WriteLine("╔══════════════════════════════╗\n" +
                                                  "║ Bitte gebe eine neue Zeit an ║\n" +
                                                  "╚══════════════════════════════╝\n");

                                entries[k][3] = Console.ReadLine();
                            }
                        }
                    }

                    if (entry4 == 2)
                    {
                        for (int k = 0; k < entries.Count; k++)
                        {
                            if (entries[k][0] == Convert.ToString(person[entry1].Driver) && entries[k][1] == person[entry1].Name && entries[k][2] == Convert.ToString(person[entry1].Id) && entries[k][3].Split(',') == person[entry1].Time && entries[k][4] == Convert.ToString(person[entry1].Amount) && entries[k][5] == Convert.ToString(person[entry1].Free_Spaces))
                            {
                                Console.Clear();
                                Console.WriteLine("╔════════════════════════════════════════════════╗\n" +
                                                  "║ Bitte gebe an wie viele Leute mitfahren wollen ║\n" +
                                                  "╚════════════════════════════════════════════════╝\n");

                                int.TryParse(Console.ReadLine(), out int new_entry);
                                entries[k][4] = Convert.ToString(new_entry);
                            }
                        }
                    }

                    else
                    {
                        goto goBack;
                    }

                    Console.Clear();
                    Console.WriteLine("╔═════════════════════════════════════════╗\n" +
                                      "║ Möchtest du noch etwas verändern? (y/n) ║\n" +
                                      "╚═════════════════════════════════════════╝\n");

                    ConsoleKeyInfo entry5 = Console.ReadKey();

                    string entry6 = Convert.ToString(entry5.KeyChar);
                    entry6.ToLower();

                    if (entry6 == "y")
                    {
                        goto EntryDelOrChange;
                    }

                    else if (entry6 == "n")
                    {
                        MainMenu.StartMenu(user);
                    }
                }

                else
                {
                    goto DelOrChange;
                }
            }
        }
    }
}
