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
    public class UserCreation
    {
        public string Name { get; set; }
        public string Direction { get; set; }
        public double Distance { get; set; }
        public int ID { get; set; }
        public string PW { get; set; }
        public bool Driver { get; set; }

        public override string ToString()
        {
            return $"{ID};{PW};{Name};{Direction};{Distance}";
        }

        public static void CheckForCSVFile()
        {
            Program program = new Program();
            FileInfo fi = new FileInfo(program.PathUserData);
            if (!fi.Exists)
            {
                fi.Create();
                var fileName = Assembly.GetExecutingAssembly().Location;
                System.Diagnostics.Process.Start(fileName);
                Environment.Exit(0);
            }
        }

        public static UserCreation AskForUserInformation(UserCreation user)
        {
            Program program = new Program();

            user = GetID(user);
            user = CreatePW(user);
            user = UserName(user);
            user = UserDirection(user);
            user = UserDistance(user);
            //user.ToString();


            string[] idc = new string[] { user.ToString() };

            List<string[]> entries = new List<string[]>();

            using (StreamReader sr = new StreamReader(program.PathUserData))
            {
                while (!sr.EndOfStream)
                {
                    entries.Add(sr.ReadLine().Split(';'));
                }

                entries.Add(idc);
            }
            using (StreamWriter sw = new StreamWriter(program.PathUserData))
            {
                Console.Clear();
                Console.WriteLine("╔═══════════════════════╗\n" +
                                  "║ Deine Daten sind nun: ║\n" +
                                  "╚═══════════════════════╝");
                for (int i = 0; i < entries.Count; i++)
                {
                    sw.WriteLine(String.Join(";", entries[i]));

                    if (i == entries.Count - 1)
                    {
                        Thread.Sleep(800);
                        MainMenu.PrintingUserData(user);
                        Console.ReadKey();
                    }
                }
            }
            do
            {
                Console.Clear();

                Console.WriteLine("╔═══════════════════════════════════════════╗\n" +
                                  "║ Bist du zufrieden mit deinen Daten? (y/n) ║\n" +
                                  "╚═══════════════════════════════════════════╝");

                ConsoleKeyInfo entry1 = Console.ReadKey();

                string entry = Convert.ToString(entry1.KeyChar);
                entry.ToLower();

                if (entry == "n")
                {
                    user = ChangeData(user);
                    break;
                }
                else if (entry == "y") { MainMenu.StartMenu(user); }

            } while (true);


            return user;
        }

        //public static int UserDeclaration()
        //{
        //    int user = 9;
        //    string eingabe;
        //    bool richtig;

        //    do
        //    {
        //        Console.Clear();

        //        Console.WriteLine("Möchtest du selbst Fahren und jemanden mitnehmen oder möchtest du mitgenommen werden? \nZum selbst fahren bitte 'fahren' eingeben. \nZum mitgenommen werden bitte 'mitnehmen' eingeben.");
        //        eingabe = Console.ReadLine();
        //        eingabe.ToLower();

        //        switch (eingabe)
        //        {
        //            case "fahren":
        //                user = 0;
        //                richtig = true;
        //                break;

        //            case "mitnehmen":
        //                user = 1;
        //                richtig = true;
        //                break;

        //            default:
        //                Console.WriteLine("Bitte gebe etwas ein, was den Vorgaben entspricht.");
        //                Thread.Sleep(500);
        //                richtig = false;
        //                break;
        //        }

        //    } while (!richtig);

        //    return user;
        //}

        public static UserCreation UserName(UserCreation user)
        {
            do
            {
                Console.Clear();

                Console.WriteLine("╔══════════════════════════════╗\n" +
                                  "║ Bitte gebe deinen Namen ein: ║\n" +
                                  "╚══════════════════════════════╝");
                user.Name = Console.ReadLine();

                if (user.Name == "")
                {
                    Console.Write("\nBitte gebe einen richtigen Namen ein.");
                    Thread.Sleep(500);
                    Console.Clear();
                }
                else
                {
                    break;
                }
            } while (true);
            return user;
        }

        public static UserCreation UserDirection(UserCreation user)
        {
            do
            {
                Console.Clear();

                Console.WriteLine("╔═════════════════════════════════════════════════════╗\n" +
                                  "║ Aus welcher Richtung von Weikersheim aus kommst du? ║\n" +
                                  "╚═════════════════════════════════════════════════════╝");
                user.Direction = Console.ReadLine();

                if (user.Direction == "")
                {
                    Console.WriteLine("Bitte gebe eine richtige Richtung ein.");
                    Thread.Sleep(500);
                }
                else
                {
                    break;
                }
            } while (true);
            return user;
        }

        public static UserCreation UserDistance(UserCreation user)
        {
            do
            {
                Console.Clear();

                Console.WriteLine("╔═════════════════════════════════════════════════════╗\n" +
                                  "║ Wie weit wohnst du von Weikersheim aus weg? (in km) ║\n" +
                                  "╚═════════════════════════════════════════════════════╝");
                user.Distance = Convert.ToDouble(Console.ReadLine());

                if (user.Distance <= 0 || user.Distance > 500)
                {
                    Console.WriteLine("╔════════════════════════════════════════════════════════════╗\n" +
                                      "║ Die Zahl darf nicht kleiner als 0 und größer als 500 sein. ║\n" +
                                      "╚════════════════════════════════════════════════════════════╝");
                    Thread.Sleep(500);
                }
                else
                {
                    break;
                }
            } while (true);
            return user;
        }

        public static UserCreation GetID(UserCreation user)
        {
            Program program = new Program();

            List<string[]> entries = new List<string[]>();

            using (StreamReader sr = new StreamReader(program.PathUserData))
            {
                while (!sr.EndOfStream)
                {
                    entries.Add(sr.ReadLine().Split(';'));
                }
                int i = 0;
                foreach (string[] entry in entries)
                {
                    i++;
                }
                user.ID = i;
            }
            return user;
        }

        public static UserCreation CreatePW(UserCreation user)
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════════════════╗\n" +
                              "║ Bitte erstelle ein Passwort. Mit diesem und deiner ID kannst du dich später wieder einloggen. ║\n" +
                              "╚═══════════════════════════════════════════════════════════════════════════════════════════════╝");
            user.PW = Console.ReadLine();
            return user;
        }

        public static UserCreation ChangeData(UserCreation user)
        {
            Program program = new Program();

            do
            {
                ChangeData:

                Console.Clear();

                Console.Write("\n ╔════════════════════════════╗\n" +
                                " ║                            ║\n" +
                                " ║     Nutzerdaten ändern     ║\n" +
                                " ║                            ║\n" +
                                " ╠════════════════════════════╣\n" +
                                "1╬          Passwort          ║\n" +
                                " ╟────────────────────────────╢\n" +
                                "2╬            Name            ║\n" +
                                " ╟────────────────────────────╢\n" +
                                "3╬          Richtung          ║\n" +
                                " ╟────────────────────────────╢\n" +
                                "4╬         Entfernung         ║\n" +
                                " ╚════════════════════════════╝\n");

                int entry;
                ConsoleKeyInfo userInput = Console.ReadKey();

                if (char.IsDigit(userInput.KeyChar))
                {
                    entry = int.Parse(userInput.KeyChar.ToString());

                    if (entry > 4 || entry <= 0)
                    {
                    }
                    else
                    {
                        switch (entry)
                        {
                            case 1:

                                int misses1 = 0;
                                int misses2 = 0;

                                string old_pw = user.PW;

                                EnterOldPW:

                                Console.Clear();
                                Console.WriteLine("╔═════════════════════════════════════╗\n" +
                                                  "║ Bitte gebe dein altes Passwort ein: ║\n" +
                                                  "╚═════════════════════════════════════╝");
                               
                                if (old_pw == Console.ReadLine())
                                {
                                    EnterNewPW:

                                    Console.Clear();
                                    Console.WriteLine("╔═════════════════════════════════════╗\n" +
                                                      "║ Bitte gebe dein neues Passwort ein: ║\n" +
                                                      "╚═════════════════════════════════════╝");

                                    string new_pw = Console.ReadLine();

                                ConfirmNewPW:

                                    Console.Clear();
                                    Console.WriteLine("╔═════════════════════════════════════╗\n" +
                                                      "║    Bitte bestätige dein Passwort    ║\n" +
                                                      "╚═════════════════════════════════════╝");
                                    if (new_pw == Console.ReadLine())
                                    {
                                        user.PW = new_pw;
                                    }
                                    else
                                    {
                                        misses2++;
                                        if (misses2 % 5 == 0)
                                        {
                                            goto ConfirmNewPW;
                                        }
                                        else if (misses2 % 3 == 0)
                                        {
                                            goto EnterNewPW;
                                        }
                                       
                                    }
                                }
                                else
                                {
                                    misses1++;
                                    if (misses1 == 3)
                                    {

                                    }
                                    goto EnterOldPW;
                                }
                                break;

                            case 2:
                                Console.Clear();
                                Console.WriteLine("╔════════════════════════════════╗\n" +
                                                  "║ Wie soll dein neuer Name sein? ║\n" +
                                                  "╚════════════════════════════════╝");
                                user.Name = Console.ReadLine();

                                break;

                            case 3:
                                Console.Clear();
                                Console.WriteLine("╔════════════════════════════════════════════════════╗\n" +
                                                  "║ In welcher Richtung von Weikersheim aus wohnst du? ║\n" +
                                                  "╚════════════════════════════════════════════════════╝");
                                user.Direction = Console.ReadLine();
                                break;

                            case 4:
                                Console.Clear();
                                Console.WriteLine("╔══════════════════════════════════════════════╗\n" +
                                                  "║ Wie weit wwohnst du von Weikersheim aus weg? ║\n" +
                                                  "╚══════════════════════════════════════════════╝");
                                user.Distance = Convert.ToDouble(Console.ReadLine());
                                break;

                            default:
                                break;
                        }
                        break;
                    }
                }

            
           
                
                Console.Clear();
                Console.WriteLine("╔═════════════════════════╗\n" +
                                  "║ Deine neuen Daten sind: ║\n" +
                                  "╚═════════════════════════╝");

                Thread.Sleep(300);
                MainMenu.PrintingUserData(user);

                Console.WriteLine("╔══════════════════════════════════════╗\n" +
                                  "║ Möchtest du noch etwas ändern? (y/n) ║\n" +
                                  "╚══════════════════════════════════════╝");
                ConsoleKeyInfo entry2 = Console.ReadKey();

                string entry1 = Convert.ToString(entry2.KeyChar);
                entry1.ToLower();

                if (entry1 == "y"){ goto ChangeData; }

                else if (entry1 == "n")
                {
                    List<string[]> entries = new List<string[]>();
                    
                    using (StreamReader sr = new StreamReader(program.PathUserData))
                    {
                        while (!sr.EndOfStream)
                        {
                            entries.Add(sr.ReadLine().Split(';'));
                        }
                        
                        for (int i = 0; i == entries.Count; i++)
                        {
                            if (Convert.ToInt32(entries[0][i]) == user.ID)
                            {                                                               
                                entries[1][i] = user.PW;
                                entries[2][i] = user.Name;
                                entries[3][i] = user.Direction;
                                entries[4][i] = Convert.ToString(user.Distance);                                
                            }
                        }
                    }
                    using (StreamWriter sw = new StreamWriter(program.PathUserData))
                    {
                        Console.Clear();
                        for (int i = 0; i < entries.Count; i++)
                        {
                            sw.WriteLine(String.Join(";", entries[i]));
                        }
                    }
                    break;
                }

            } while (true);

            return user;
        }

        public static UserCreation LogIn()
        {
            Program program = new Program();
            UserCreation user = new UserCreation();
            List<string[]> entries = new List<string[]>();

            bool end = false;


            do
            {
                LogInScreen:
                Console.Clear();

                Console.WriteLine("╔══════════════════════════════════════════════════════╗\n" +
                                  "║ Möchtest du dich einloggen(0), oder registrieren(1)? ║\n" +
                                  "╚══════════════════════════════════════════════════════╝");
                ConsoleKeyInfo userInput = Console.ReadKey();

                if (char.IsDigit(userInput.KeyChar))
                {
                    int entry = int.Parse(userInput.KeyChar.ToString());
                    if (entry < 0 || entry >= 2)
                    {
                       
                    }
                        
                    else if (entry == 0)
                    {
                        using (StreamReader sr = new StreamReader(program.PathUserData))
                        {
                            while (!sr.EndOfStream)
                            {
                                entries.Add(sr.ReadLine().Split(';'));
                            }
                        }

                        EnterID:

                        Console.Clear();

                        Console.WriteLine("╔══════════════════════╗\n" +
                                          "║ Wie lautet deine ID? ║\n" +
                                          "╚══════════════════════╝");
                        string id = Console.ReadLine();
                        int counter = 0;

                        if (id == "r")
                        {
                            goto LogInScreen;
                        }

                        for (int i = 0; i < entries.Count; i++)
                        {
                            if (entries[i][0] == id)
                            {
                                counter++;
                                EnterPW:
                                Console.Clear();
                                Console.WriteLine("╔═══════════════════════════════╗\n" +
                                                  "║ Bitte gebe dein Passwort ein: ║\n" +
                                                  "╚═══════════════════════════════╝");

                                int pw = Convert.ToInt32(Console.ReadLine());

                                if (Convert.ToInt32(entries[i][1]) == pw)
                                {
                                    user.ID = Convert.ToInt32(entries[i][0]);
                                    user.PW = entries[i][1];
                                    user.Name = entries[i][2];
                                    user.Direction = entries[i][3];
                                    user.Distance = Convert.ToDouble(entries[i][4]);
                                    end = true;
                                }
                                else if (Convert.ToInt32(entries[i][1]) != pw)
                                {
                                    Console.Clear();
                                    Console.WriteLine("╔═══════════════════════════════════════════════╗\n" +
                                                      "║ Dein Passwort ist falsch, versuche es erneut. ║\n" +
                                                      "╚═══════════════════════════════════════════════╝");
                                    Thread.Sleep(1500);
                                    goto EnterPW;
                                }
                            }
                        }

                        if (counter == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("╔════════════════════════════════════════════════════╗\n" +
                                              "║ Deine ID wurde nicht gefunden. Versuche es erneut! ║\n" +
                                              "╚════════════════════════════════════════════════════╝");
                            Thread.Sleep(1500);

                            goto EnterID;
                        }


                    }
                    else if (entry == 1)
                    {
                        user = UserCreation.AskForUserInformation(user);
                        break;
                    }
               
                }
            } while (!end);

            return user;
        }
    }
}
