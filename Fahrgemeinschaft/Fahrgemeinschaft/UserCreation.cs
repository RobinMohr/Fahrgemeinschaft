using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fahrgemeinschaft
{
    public class UserCreation
    {
        public string Name { get; set; }
        public string City { get; set; }
        public int ID { get; set; }
        public string PW { get; set; }

        public override string ToString()
        {
            return $"{ID};{PW};{Name};{City}";
        }

        public static void LogInOrRegister()
        {
            UserCreation user = new UserCreation();
            bool end = false;
            do
            {
            LogInScreen:
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════════════════════════════╗\n" +
                                  "║ Möchtest du dich einloggen(0), oder registrieren(1)? ║\n" +
                                  "╚══════════════════════════════════════════════════════╝");
                ConsoleKeyInfo entry1 = Console.ReadKey();
                if (entry1.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                if (!int.TryParse(Convert.ToString(entry1.KeyChar), out int entry) || entry < 0 || entry > 1)
                {
                    goto LogInScreen;
                }
                if (entry == 0)
                {
                    user = LogIn();
                    end = true;
                }
                else if (entry == 1)
                {
                    user = UserCreation.AskForUserInformation(user);
                    end = true;
                }
            } while (!end);
            MainMenu.StartMenu(user);
        }

        public static UserCreation LogIn()
        {
            UserCreation user = new UserCreation();
            int counter = 0;
            List<string[]> listOfUsers = FileManager.ReadUserData();
        EnterUserID:
            (int userPos, int personID) = EnterUserID(user);
        EnterPW:
            if (counter != 0)
            {
                PrintLogIn(counter, Convert.ToString(personID));
            }
            Console.SetCursorPosition(0, 10);

            (string pw, int back) = EnterPassword();

            if (back != 0)
            {
                goto EnterUserID;
            }            
            if (listOfUsers[userPos][1] == pw)
            {
                user.ID = Convert.ToInt32(listOfUsers[userPos][0]);
                user.PW = listOfUsers[userPos][1];
                user.Name = listOfUsers[userPos][2];
                user.City = listOfUsers[userPos][3];
            }
            else if (listOfUsers[userPos][1] != pw)
            {
                counter++;

                back = PasswordWasNotCool(counter);
                if (back == 1)
                {
                    goto EnterUserID;

                }
                else if (back == 2)
                {
                    LogInOrRegister();
                }
                goto EnterPW;
            }
            return user;
        }
        private static void PrintLogIn(int counter, string ID)
        {
            if (counter != 0 || ID != null)
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════╗\n" +
                                  "║ Bitte gebe deine ID ein: ║\n" +
                                  "╚══════════════════════════╝\n" +
                                  $"\n{ID}\n");
                Console.WriteLine("╔═══════════════════════════════╗\n" +
                                  "║ Bitte gebe dein Passwort ein: ║\n" +
                                  "╚═══════════════════════════════╝\n");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════╗\n" +
                                  "║ Bitte gebe deine ID ein: ║\n" +
                                  "╚══════════════════════════╝\n\n\n");
                Console.WriteLine("╔═══════════════════════════════╗\n" +
                                  "║ Bitte gebe dein Passwort ein: ║\n" +
                                  "╚═══════════════════════════════╝\n");
            }
        }
        public static (int, int) EnterUserID(UserCreation user)
        {
        WrongInput:
            PrintLogIn(0, null);
            Console.Write("→ ←");
            Console.SetCursorPosition(1, 4);
            (int personID, int back) = App.UserNumEntries();
            if (back != 0)
            {
                LogInOrRegister();
            }
            user.ID = personID;


            (int userPos, int counter) = FileManager.LookForUserInFile(user);
            if (counter == 0)
            {
                WrongID();
                goto WrongInput;
            }
            return (userPos, personID);
        }
        private static void WrongID()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════╗\n" +
                              "║ Diese ID existiert nicht. Versuche es erneut! ║\n" +
                              "╚═══════════════════════════════════════════════╝");
            Thread.Sleep(1500);
        }
        public static (string, int) EnterPassword()
        {
        WrongInput:

            string password = "";
            int back = 0;
            ConsoleKeyInfo foo;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write("→ ←");
            Console.SetCursorPosition(1, Console.CursorTop);

            do
            {
                foo = Console.ReadKey();

                if (foo.Key == ConsoleKey.Enter)
                {
                    if (password.Length > 0)
                    {
                        break;
                    }
                    else
                    {
                        goto WrongInput;
                    }
                }
                if (foo.Key == ConsoleKey.Escape)
                {
                    back++;
                    break;
                }

                if (foo.Key == ConsoleKey.Backspace)
                {
                    if (password.Length != 0 && Console.CursorLeft >0)
                    {
                        Console.Write(' ');
                        Console.Write(' ');
                        Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop);

                    }
                    if (Console.CursorLeft == 0)
                    {
                        Console.Write("→");
                    }
                }

                else if (foo.Key != ConsoleKey.Enter)
                {
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    if (Convert.ToInt32(foo.KeyChar) != 0)
                    {
                        Console.Write('*');
                        password += foo.KeyChar;
                    }
                }
                Console.Write("←");
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

            } while (true);
            return (password, back);
        }

        public static UserCreation AskForUserInformation(UserCreation user)
        {
            user = GetUserData(user);

            string[] idc = new string[] { user.ToString() };

            List<string[]> entries = FileManager.ReadUserData();
            entries.Add(idc);


            Console.Clear();
            Console.WriteLine("╔═══════════════════════╗\n" +
                              "║ Deine Daten sind nun: ║\n" +
                              "╚═══════════════════════╝");
            for (int i = 0; i < entries.Count; i++)
            {
                FileManager.WriteUserData(entries);

                if (i == entries.Count - 1)
                {
                    Thread.Sleep(800);
                    MainMenu.PrintingUserData(user);
                    Console.ReadKey();
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

        public static UserCreation GetUserData(UserCreation user)
        {
            user = GetID(user);
            user = CreatePW(user);
            user = UserName(user);
            user.City = UserCity();
            return user;
        }

        public static UserCreation CreatePW(UserCreation user)
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════════════════╗\n" +
                              "║ Bitte erstelle ein Passwort. Mit diesem und deiner ID kannst du dich später wieder einloggen. ║\n" +
                              "╚═══════════════════════════════════════════════════════════════════════════════════════════════╝");
            (string pw, int back) = EnterPassword();
            user.PW = pw;
            if (back != 0)
            {
                LogIn();
            }
            return user;
        }

        public static UserCreation UserName(UserCreation user)
        {
            Console.Clear();

            Console.WriteLine("╔══════════════════════════════╗\n" +
                              "║ Bitte gebe deinen Namen ein: ║\n" +
                              "╚══════════════════════════════╝");
            (string userName, int back) = App.UserStringEntry();
            if (back != 0)
            {
                GetUserData(user);
            }
            user.Name = userName;
            return user;
        }

        public static string UserCity()
        {
            WrongEntry:
            Console.Clear();
            Console.WriteLine("  ╔═══════════════════════════════════════════════════════╗\n" +
                              "  ║ Welche der Aufgelisteten Städten ist dir am nächsten? ║\n" +
                              "  ╠══════════════════════════╦════════════════════════════╝\n" +
                              " 0╬    Weikersheim           ║\n" +
                              "  ╟──────────────────────────╢\n" +
                              " 1╬    Bad Mergentheim       ║\n" +
                              "  ╟──────────────────────────╢\n" +
                              " 2╬    Igersheim             ║\n" +
                              "  ╟──────────────────────────╢\n" +
                              " 3╬    Lauda-Königshofen     ║\n" +
                              "  ╟──────────────────────────╢\n" +
                              " 4╬    Boxberg               ║\n" +
                              "  ╟──────────────────────────╢\n" +
                              " 5╬    Assamstadt            ║\n" +
                              "  ╟──────────────────────────╢\n" +
                              " 6╬    Tauberrettersheim     ║\n" +
                              "  ╟──────────────────────────╢\n" +
                              " 7╬    Röttingen             ║\n" +
                              "  ╟──────────────────────────╢\n" +
                              " 8╬    Bieberehren           ║\n" +
                              "  ╟──────────────────────────╢\n" +
                              " 9╬    Creglingen            ║\n" +
                              "  ╟──────────────────────────╢\n" +
                              "10╬    Niederstetten         ║\n" +
                              "  ╟──────────────────────────╢\n" +
                              "11╬    Oberstetten           ║\n" +
                              "  ╟──────────────────────────╢\n" +
                              "12╬    Schrozberg            ║\n" +
                              "  ╟──────────────────────────╢\n" +
                              "13╬    Tauberbischofsheim    ║\n" +
                              "  ╚══════════════════════════╝\n");
            (int foo, int back) = App.UserNumEntries();
            if (back != 0)
            {

            }
            string city = ConvertIntToCity(foo);
            if (city == null)
            {
                goto WrongEntry;
            }
            return city;
        }
        public static string ConvertIntToCity(int foo)
        {
            string baa;
            switch (foo)
            {
                case 0:
                    baa = "Weikersheim";
                    break;
                case 1:
                    baa = "Bad Mergentheim";
                    break;
                case 2:
                    baa = "Igersheim";
                    break;
                case 3:
                    baa = "Lauda-Königshofen";
                    break;
                case 4:
                    baa = "Boxberg";
                    break;
                case 5:
                    baa = "Assamstadt";
                    break;
                case 6:
                    baa = "Tauberrettersheim";
                    break;
                case 7:
                    baa = "Röttingen";
                    break;
                case 8:
                    baa = "Bieberehren";
                    break;
                case 9:
                    baa = "Creglingen";
                    break;
                case 10:
                    baa = "Niederstetten";
                    break;
                case 11:
                    baa="Oberstetten";
                    break;
                case 12:
                    baa = "Schrozberg";
                    break;
                case 13:
                    baa = "Tauberbischofsheim";
                    break;
                default:
                    baa = null;
                    break;
            }
            return baa;
        }
        public static int ConvertCityToInt(string baa)
        {
            int foo;
            switch (baa)
            {
                case "Weikersheim":
                    foo = 0;
                    break;

                case "Bad Mergentheim":
                    foo = 1;
                    break;

                case "Igersheim":
                    foo = 2;
                    break;

                case "Lauda-Königshofen":
                    foo = 3;
                    break;

                case "Boxberg":
                    foo = 4;
                    break;

                case "Assamstadt":
                    foo = 5;
                    break;

                case "Tauberrettersheim":
                    foo = 6;
                    break;

                case "Röttingen":
                    foo = 7;
                    break;

                case "Bieberehren":
                    foo = 8;
                    break;

                case "Creglingen":
                    foo = 9;
                    break;

                case "Niederstetten":
                    foo = 10;
                    break;
                case "Oberstetten":
                    foo = 11;
                    break;
                case "Schrozberg":
                    foo = 12;
                    break;
                case "Tauberbischofsheim":
                    foo = 13;
                    break;
                default:
                    foo = 0;
                    break;
            }
            return foo;
        }

        public static UserCreation GetID(UserCreation user)
        {
            FileManager fileManager = new FileManager();
            List<string[]> entries = new List<string[]>();

            using (StreamReader sr = new StreamReader(fileManager.PathUserData))
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

        public static UserCreation ChangeData(UserCreation user)
        {
            FileManager fileManager = new FileManager();
            do
            {
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
                                "3╬            Stadt           ║\n" +                                
                                " ╚════════════════════════════╝\n");                                    
                    ConsoleKeyInfo entry = Console.ReadKey();
                    switch (Convert.ToInt32(Convert.ToString(entry.KeyChar)))
                    {
                        case 1:
                            string pw = ChangePW(user);
                            if ( pw == "1")
                            {
                                goto ChangeData;
                            }
                            user.PW = pw;
                            break;
                        case 2:
                            user = UserName(user);
                            goto ChangeAnother;
                        case 3:
                            user.City = UserCity();
                            break;
                        default:
                            break;
                    }
                    break;
                } while (true);
                


            ChangeAnother:

                Console.Clear();
                Console.WriteLine("╔═════════════════════════╗\n" +
                                  "║ Deine neuen Daten sind: ║\n" +
                                  "╚═════════════════════════╝");

                Thread.Sleep(1500);
                MainMenu.PrintingUserData(user);

                Console.WriteLine("╔══════════════════════════════════════╗\n" +
                                  "║ Möchtest du noch etwas ändern? (y/n) ║\n" +
                                  "╚══════════════════════════════════════╝");
                ConsoleKeyInfo entry2 = Console.ReadKey();

                string entry1 = Convert.ToString(entry2.KeyChar);
                entry1.ToLower();

                if (entry1 == "y") { goto ChangeAnother; }

                else if (entry1 == "n")
                {
                    List<string[]> entries = FileManager.ReadUserData();
                    int i = 0;
                        foreach(var entry in entries)
                        {
                            if (Convert.ToInt32(entries[i][0]) == user.ID)
                            {
                                entries[i][1] = user.PW;
                                entries[i][2] = user.Name;
                                entries[i][3] = user.City;
                            }
                            else
                            {
                                Console.WriteLine("USER ID KAPUT");
                                Console.ReadLine();
                            }
                        i++;                        
                    }
                    FileManager.WriteUserData(entries);
                    break;
                }

            } while (true);

            return user;
        }

        public static string ChangePW(UserCreation user)
        {
            int misses1 = 0;
            int misses2 = 0;
        EnterOldPW:

            Console.Clear();
            Console.WriteLine("╔═════════════════════════════════════╗\n" +
                              "║ Bitte gebe dein altes Passwort ein: ║\n" +
                              "╚═════════════════════════════════════╝");
            (string pw, int back) = EnterPassword();
            if (back != 0)
            {
                return "1";
            }
            if (user.PW == pw)
            {
            EnterNewPW:

                Console.Clear();
                Console.WriteLine("╔═════════════════════════════════════╗\n" +
                                  "║ Bitte gebe dein neues Passwort ein: ║\n" +
                                  "╚═════════════════════════════════════╝");
                (string newPassword, int back3) = EnterPassword();
                if (back3 != 0)
                {
                    goto EnterOldPW;
                }
                if (user.PW == newPassword)
                {
                    Console.WriteLine("╔══════════════════════════════════════════════════════════╗\n" +
                                      "║ Dein neues Passwort darf nicht dein altes Passwort sein. ║\n" +
                                      "╚══════════════════════════════════════════════════════════╝");
                    Thread.Sleep(1500);
                    goto EnterNewPW;
                }

            ConfirmNewPW:

                Console.Clear();
                Console.WriteLine("╔═════════════════════════════════════╗\n" +
                                  "║    Bitte bestätige dein Passwort    ║\n" +
                                  "╚═════════════════════════════════════╝");
                (string newPassword1, int back2) = EnterPassword();
                if (back2 != 0)
                {
                    goto EnterNewPW;
                }
                if (newPassword == newPassword1)
                {
                    return newPassword;
                }
                else
                {
                    misses2++;
                    PasswordWasNotCool(misses2);
                    goto ConfirmNewPW;
                }
            }
            else
            {
                misses1++;
                back = PasswordWasNotCool(misses1);
                if (back == 2)
                {
                    return "1";
                }
                goto EnterOldPW;
            }
        }       

        public static int PasswordWasNotCool(int counter)
        {
            int back = 0;
            Console.Clear();
            if (counter % 3 == 0)
            {
                Console.WriteLine("╔═════════════════════════════════════════════════════╗\n" +
                                  "║ Dein Passwort ist falsch, du wirst zurückgeleitet!  ║\n" +
                                  "╚═════════════════════════════════════════════════════╝\n");
                back = 1;
            }

            if (counter % 5 == 0)
            {
                Console.WriteLine("╔════════════════════════════════════════════════════════════╗\n" +
                                  "║ Dein Passwort war zu oft falsch, du wirst zurückgeleitet!  ║\n" +
                                  "╚════════════════════════════════════════════════════════════╝\n");
                back = 2;
            }
            else
            {
                Console.WriteLine("╔════════════════════════════════════════════════╗\n" +
                                  "║ Dein Passwort ist falsch, versuche es erneut!  ║\n" +
                                  "╚════════════════════════════════════════════════╝\n");
            }

            Thread.Sleep(1500);
            return back;
        }
        //public static void PasswordWrongTooOften()
        //{
        //    Console.Clear();
        //    Console.WriteLine("╔═══════════════════════════════════════════════════════════╗\n" +
        //                      "║ Dein Passwort war zu oft Falsch, du wirst zurückgeleitet. ║\n" +
        //                      "╚═══════════════════════════════════════════════════════════╝");
        //    Thread.Sleep(1500);
        //}



    }
}

