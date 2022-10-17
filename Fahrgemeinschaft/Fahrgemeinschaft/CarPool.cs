using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fahrgemeinschaft
{
    public class CarPool
    {
        public static void Planning(UserCreation user)
        {
            Person person = new Person
            {
                Name = user.Name,
                Id = user.ID,
                Driver = 1
            };

            Console.Clear();
            Console.Write("╔═══════════════════════════════════════════════╗\n" +
                          "║ Wie viele Plätze hast du in deinem Auto frei? ║\n" +
                          "╚═══════════════════════════════════════════════╝\n\n\n\n");
            Console.Write("╔══════════════════════════════════════════════════════════╗\n" +
                          "║ An welchem Datum und an welchem Uhrzeit wirst du Fahren? ║\n" +
                          "║  Bitte in dem Format (Jahr,Monat,Tag,Uhrzeit) eingeben   ║\n" +
                          "║           ein Beispiel wäre: 2022.12.31.05:30            ║\n" +
                          "╚══════════════════════════════════════════════════════════╝\n\n");

            Console.SetCursorPosition(0, 4);
            (int personFreeSpaces, int back) = App.UserNumEntries();
            if (back != 0)
            {
                MainMenu.StartMenu(user);
            }
            person.Free_Spaces = personFreeSpaces;

            Console.SetCursorPosition(0, 12);
            person.Time = App.GetDate().Split('.');

            string[] array = new string[] { 1 + $";{person.Name};{person.Id};{String.Join(".", person.Time)};{person.Amount};{person.Free_Spaces}" };

            List<string[]> entries = FileManager.ReadRouteData();
            entries.Add(array);

            FileManager.WriteRouteData(entries);

            (List<Person> nonDrivers, int count) = App.LookForDifferentUser(person, user);

            List<string[]> allEntries = FileManager.ReadRouteData();

            if (count == 0)
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════════════════════════╗\n" +
                                  "║ Niemand sucht gerade nach einer Fahrgemeinschaft ║\n" +
                                  "╚══════════════════════════════════════════════════╝");
                Thread.Sleep(1500);
            }
            else 
            { 

            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗\n" +
                              "║ Möchtest du dir alle möglichen Mitfahrer angucken? (y/n) ║\n" +
                              "╚══════════════════════════════════════════════════════════╝");
                if (App.YesOrNo())
                {
                    App.PrintPerson(nonDrivers);


                    Console.WriteLine("╔════════════════════════════════════════════════════════╗\n" +
                                      "║ Möchtest du jemanden von den Personen mitnehmen? (y/n) ║\n" +
                                      "╚════════════════════════════════════════════════════════╝\n");

                    ConsoleKeyInfo askingUserToPickSomeoneUp = Console.ReadKey();
                    if (Convert.ToString(askingUserToPickSomeoneUp.KeyChar) == "y")
                    {
                    ChooseNonDriver:
                        Console.Clear();
                        int x = 0;

                        Console.WriteLine("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
                        foreach (Person p in nonDrivers)
                        {
                            Console.WriteLine($"\n{x}: {p.Name}, {string.Join(".", p.Time)}, {p.Amount}\n");
                            Console.WriteLine("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
                            x++;
                        }

                        Console.WriteLine("\n╔════════════════════════════╗\n" +
                                            "║ Wen möchtest du mitnehmen? ║\n" +
                                            "╚════════════════════════════╝\n");
                        do
                        {

                            ConsoleKeyInfo chooseNonDriver = Console.ReadKey();
                            if (int.TryParse(Convert.ToString(chooseNonDriver.KeyChar), out int whichNonDriver))
                            {
                                ChooseCarPool(allEntries, whichNonDriver);
                                ConsoleKeyInfo info = Console.ReadKey();

                                if (Convert.ToString(info.KeyChar) == "y")
                                {
                                    allEntries.Remove(allEntries[whichNonDriver]);
                                    person.Free_Spaces = person.Free_Spaces - nonDrivers[whichNonDriver].Amount;
                                    if (person.Free_Spaces == 0)
                                    {
                                        allEntries.Remove(allEntries[allEntries.Count - 1]);
                                    }
                                    break;
                                }
                                else
                                {
                                    goto ChooseNonDriver;
                                }




                            }
                        } while (true);

                        FileManager.WriteRouteData(allEntries);
                    }
                }
            }
        }

        public static void Joining(UserCreation user)
        {
            Person person = new Person
            {
                Name = user.Name,
                Id = user.ID,
                Driver = 0
            };

            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗\n" +
                              "║ Gehen mit dir noch andere Leute mit? (y/n) ║\n" +
                              "╚════════════════════════════════════════════╝");

            ConsoleKeyInfo entry1 = Console.ReadKey();

            string entry1_ = Convert.ToString(entry1.KeyChar);
            entry1_.ToLower();

            if (entry1_ == "y")
            {
                Console.Clear();
                Console.WriteLine("╔═══════════════════════════════════════════╗\n" +
                                  "║ Wie viele Leute gehen außer dir noch mit? ║\n" +
                                  "╚═══════════════════════════════════════════╝");

                string amount = Console.ReadLine();
                int.TryParse(amount, out int amount_);

                person.Amount = amount_ + 1;
            }
            else
            {
                person.Amount = 1;
            }
            Console.Clear();
            Console.Write("╔════════════════════════════════════════════════════════════════════╗\n" +
                          "║ An welchem Datum und an welchem Uhrzeit willst du gefahren werden? ║\n" +
                          "║       Bitte in dem Format (Jahr,Monat,Tag,Uhrzeit) eingeben        ║\n" +
                          "║                ein Beispiel wäre: 2022.12.31.05:30                 ║\n" +
                          "╚════════════════════════════════════════════════════════════════════╝\n\n");
            Console.SetCursorPosition(0, 6);

            //Time from user
            string userTime = App.GetDate();

            person.Time = userTime.Split('.');

            string[] array = new string[] { 0 + $";{person.Name};{person.Id};{String.Join(".", person.Time)};{person.Amount};{person.Free_Spaces}" };

            List<string[]> entries = FileManager.ReadRouteData();
            entries.Add(array);

            FileManager.WriteRouteData(entries);

            Console.WriteLine("╔═══════════════════════════════════════════════════════╗\n" +
                              "║ Möchtest du dir alle möglichen Fahrer angucken? (y/n) ║\n" +
                              "╚═══════════════════════════════════════════════════════╝");


            List<string[]> allEntries = FileManager.ReadRouteData();

            if (App.YesOrNo())
            {
            ChooseDriver:
                (List<Person> drivers, int count) = App.LookForDifferentUser(person, user);

                App.PrintPerson(drivers);

                if (count == 0)
                {
                    Console.WriteLine("╔════════════════════════════════════════════════╗\n" +
                                      "║ Niemand bietet gerade eine Fahrgemeinschaft an ║\n" +
                                      "╚════════════════════════════════════════════════╝");
                    Thread.Sleep(1500);
                }
                else
                {

                    Console.WriteLine("╔════════════════════════════════════════════════════════════╗\n" +
                                      "║ Möchtest du bei jemanden von den Personen mitfahren? (y/n) ║\n" +
                                      "╚════════════════════════════════════════════════════════════╝\n");

                    ConsoleKeyInfo askingUserToPickSomeoneUp = Console.ReadKey();
                    if (Convert.ToString(askingUserToPickSomeoneUp.KeyChar) == "y")
                    {
                        Console.Clear();
                        int x = 0;
                        foreach (Person p in drivers)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"{x}: {p.Name}, {string.Join(".", p.Time)}, {p.Free_Spaces}\n");
                            Console.WriteLine("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
                            x++;
                        }

                        Console.WriteLine("\n╔════════════════════════════════╗\n" +
                                            "║ Bei wem möchtest du mitfahren? ║\n" +
                                            "╚════════════════════════════════╝\n");

                        int.TryParse(Convert.ToString(Console.ReadKey().KeyChar), out int chooseDriver);

                        ChooseCarPool(allEntries, chooseDriver);

                        ConsoleKeyInfo info = Console.ReadKey();

                        if (Convert.ToString(info.KeyChar) == "y")
                        {
                            allEntries.Remove(allEntries[allEntries.Count - 1]);
                            allEntries[chooseDriver][5] = Convert.ToString(Convert.ToInt32(allEntries[chooseDriver][5]) - person.Amount);
                            if (person.Free_Spaces == 0)
                            {
                                allEntries.Remove(allEntries[chooseDriver]);
                            }
                        }
                        else
                        {
                            goto ChooseDriver;
                        }

                        FileManager.WriteRouteData(allEntries);
                    }
                }
            }
        }

        private static void ChooseCarPool(List<string[]> allEntries, int whichNonDriver)
        {
            Console.Clear();
            Console.WriteLine("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            Console.WriteLine($"\nDu hast ausgewählt: {allEntries[whichNonDriver][1]}, {string.Join(".", allEntries[whichNonDriver][3])}, {allEntries[whichNonDriver][4]}\n");
            Console.WriteLine("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");

            Thread.Sleep(500);

            Console.WriteLine("\n╔═════════════════════════════╗\n" +
                                "║ Bitte mit ''y'' bestätigen? ║\n" +
                                "╚═════════════════════════════╝\n");
        }

        public static void Change(UserCreation user)
        {
        AllEntries:
            List<string[]> entries = FileManager.ReadRouteData();
            List<string[]> userEntries = new List<string[]>();

            for (int i = 0; i < entries.Count; i++)
            {
                if (Convert.ToInt32(entries[i][2]) == user.ID)
                {
                    userEntries.Add(entries[i]);
                }
            }

            if (userEntries.Count <= 0)
            {
                Console.Clear();
                Console.WriteLine("╔═══════════════════════════════╗\n" +
                                  "║ Du hast gerade keine Einträge ║\n" +
                                  "╚═══════════════════════════════╝");
                Thread.Sleep(1500);
                MainMenu.StartMenu(user);
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
                        person1.Driver = 1;
                    }
                    else if (Convert.ToInt32(userEntries[j][0]) == 0)
                    {
                        person1.Driver = 0;
                    }
                    person1.Name = userEntries[j][1];
                    person1.Id = Convert.ToInt32(userEntries[j][2]);
                    person1.Time = userEntries[j][3].Split(',');
                    person1.Amount = Convert.ToInt32(userEntries[j][4]);
                    person1.Free_Spaces = Convert.ToInt32(userEntries[j][5]);

                    person.Add(person1);

                }
            }
App.PrintPerson(person);

            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗\n" +
                              "║ Möchtest du einen der Einträge bearbeiten oder löschen? (y/n) ║\n" +
                              "╚═══════════════════════════════════════════════════════════════╝");

            ConsoleKeyInfo entry2 = Console.ReadKey();

            string entry = Convert.ToString(entry2.KeyChar);

            entry.ToLower();

            if (entry2.Key == ConsoleKey.Escape)
            {
                MainMenu.StartMenu(user);
            }

            else if (entry == "y")
            {

            EntryDelOrChange:

                int i = 0;

                Console.Clear();

                foreach (Person p in person)
                {
                    Console.WriteLine("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬\n");
                    if (p.Driver == 0)
                    {
                        Console.WriteLine($"{i}: Zeit: {String.Join(".", p.Time)} Anzahl: {p.Amount}\n");
                    }

                    else if (p.Driver == 1)
                    {
                        Console.WriteLine($"{i}: Zeit: {String.Join(".", p.Time)} freie Plätze: {p.Free_Spaces}\n");
                    }
                    Console.WriteLine("");

                    i++;
                }

                Console.WriteLine("╔══════════════════════════════════════════════╗\n" +
                                  "║ Welchen der Einträge möchtest du bearbeiten? ║\n" +
                                  "╚══════════════════════════════════════════════╝\n");

                (int entry1, int back) =App.UserNumEntries();

                if (back != 0)
                {
                    goto AllEntries;
                }

                    if (entry1 < 0 || entry1 >= userEntries.Count)
                    {
                        Console.Clear();
                        Console.WriteLine("╔═════════════════════════════════════════════════════════════════════╗\n" +
                                          "║ Dein Eintrag wurde nicht gefunden und konnte nicht gelöscht werden. ║\n" +
                                          "╚═════════════════════════════════════════════════════════════════════╝\n");
                        Thread.Sleep(1500);
                        goto EntryDelOrChange;
                    }
                DelOrChange:

                    Console.Clear();

                    Console.WriteLine("╔═════════════════════════════════════════════════════╗\n" +
                                      "║ Möchtest du diesen löschen (0) oder bearbeiten (1)? ║\n" +
                                      "╚═════════════════════════════════════════════════════╝\n");

                    ConsoleKeyInfo delOrChange = Console.ReadKey();

                    if (delOrChange.Key == ConsoleKey.Escape)
                    {
                        goto EntryDelOrChange;
                    }

                    int.TryParse(Convert.ToString(delOrChange.KeyChar), out int result);

                    int counter = 0;



                    if (result == 0)
                    {
                        for (int k = 0; k < entries.Count; k++)
                        {
                            if (entries[k][1] == person[entry1].Name && Convert.ToInt32(entries[k][2]) == person[entry1].Id && entries[k][3] == String.Join(".", person[entry1].Time) && entries[k][4] == Convert.ToString(person[entry1].Amount) && entries[k][5] == Convert.ToString(person[entry1].Free_Spaces))
                            {
                                entries.Remove(entries[k]);
                                counter++;
                                break;
                            }
                        }
                        if (counter == 0)
                        {
                            Console.WriteLine("sum went wrong");
                            Console.ReadLine();
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

                        
                        (int delOrChangeEntry, int back1) = App.UserNumEntries();
                        if (back1 != 0)
                        {
                            goto DelOrChange;
                        }

                        if (delOrChangeEntry == 0)
                    {
                        ChangeFreeSpaces(entries, person, entry1);
                    }

                    if (delOrChangeEntry == 1)
                    {
                        ChangeTime(entries, person, entry1);
                    }

                    if (delOrChangeEntry == 2)
                    {
                        ChangeUserAmount(entries, person, entry1);
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

                        if (entry5.Key == ConsoleKey.Y)
                        {
                            goto EntryDelOrChange;
                        }

                        else
                        {
                            MainMenu.StartMenu(user);
                        }
                    }

                
                FileManager.WriteRouteData(entries);
            }
        }

        private static void ChangeUserAmount(List<string[]> entries, List<Person> person, int entry1)
        {
            for (int k = 0; k < entries.Count; k++)
            {
                if (entries[k][0] == Convert.ToString(person[entry1].Driver) && entries[k][1] == person[entry1].Name && entries[k][2] == Convert.ToString(person[entry1].Id) && String.Join(".", entries[k][3]) == String.Join(".", person[entry1].Time) && entries[k][4] == Convert.ToString(person[entry1].Amount) && entries[k][5] == Convert.ToString(person[entry1].Free_Spaces))
                {
                    Console.Clear();
                    Console.WriteLine("╔════════════════════════════════════════════════╗\n" +
                                      "║ Bitte gebe an wie viele Leute mitfahren wollen ║\n" +
                                      "╚════════════════════════════════════════════════╝\n");
                    entries[k][4] = Convert.ToString(App.UserNumEntries());
                    FileManager.WriteRouteData(entries);
                }
            }
        }

        private static void ChangeTime(List<string[]> entries, List<Person> person, int entry1)
        {
            for (int k = 0; k < entries.Count; k++)
            {
                if (entries[k][0] == Convert.ToString(person[entry1].Driver) && entries[k][1] == person[entry1].Name && entries[k][2] == Convert.ToString(person[entry1].Id) && String.Join(".", entries[k][3]) == String.Join(".", person[entry1].Time) && entries[k][4] == Convert.ToString(person[entry1].Amount) && entries[k][5] == Convert.ToString(person[entry1].Free_Spaces))
                {
                    Console.Clear();
                    Console.Write("╔══════════════════════════════════════════════════════════════════╗\n" +
                                  "║ An welchem Datum und an welchem Uhrzeit willst du fahren werden? ║\n" +
                                  "║       Bitte in dem Format (Jahr,Monat,Tag,Uhrzeit) eingeben      ║\n" +
                                  "║                ein Beispiel wäre: 2022.12.31.05:30               ║\n" +
                                  "╚══════════════════════════════════════════════════════════════════╝\n\n");
                    Console.SetCursorPosition(0, 6);
                    entries[k][3] = App.GetDate();
                    FileManager.WriteRouteData(entries);
                }
            }
        }

        private static void ChangeFreeSpaces(List<string[]> entries, List<Person> person, int entry1)
        {
            for (int k = 0; k < entries.Count; k++)
            {
                if (entries[k][0] == Convert.ToString(person[entry1].Driver) && entries[k][1] == person[entry1].Name && entries[k][2] == Convert.ToString(person[entry1].Id) && String.Join(".", entries[k][3]) == String.Join(".", person[entry1].Time) && entries[k][4] == Convert.ToString(person[entry1].Amount) && entries[k][5] == Convert.ToString(person[entry1].Free_Spaces))
                {
                    Console.Clear();
                    Console.WriteLine("╔═══════════════════════════════════════════════════╗\n" +
                                      "║ Bitte gebe eine neue Anzahl von freien Plätzen an ║\n" +
                                      "╚═══════════════════════════════════════════════════╝\n");
                    Console.SetCursorPosition(0, 4);
                    entries[k][5] = Convert.ToString(App.UserNumEntries());
                    FileManager.WriteRouteData(entries);
                }
            }
        }
    }
}
