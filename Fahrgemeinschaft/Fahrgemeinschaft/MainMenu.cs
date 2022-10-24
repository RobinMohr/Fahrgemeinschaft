using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Fahrgemeinschaft
{
    public class MainMenu
    {
        public static void StartMenu(UserCreation user)
        {


            Console.Clear();

            Console.Write("\n ╔════════════════════════════╗\n" +
                            " ║                            ║\n" +
                            " ║ Hauptmenü Fahrgemeinschaft ║\n" +
                            " ║                            ║\n" +
                            " ╠════════════════════════════╣\n" +
                            "1╬  Fahrgemeinschaft planen:  ║\n" +
                            " ╟────────────────────────────╢\n" +
                            "2╬  Fahrgemeinschaft suchen:  ║\n" +
                            " ╟────────────────────────────╢\n" +
                            "3╬  meine Fahrten verwalten:  ║\n" +
                            " ╟────────────────────────────╢\n" +
                            "4╬  Fahrten und Suchanzeigen  ║\n" +
                            " ╟────────────────────────────╢\n" +
                            "5╬    Nutzerdaten ausgeben    ║\n" +
                            " ╟────────────────────────────╢\n" +
                            "6╬     Nutzerdaten ändern     ║\n" +
                            " ╟────────────────────────────╢\n" +
                            "7╬         ausloggen          ║\n" +
                            " ╚════════════════════════════╝\n");
            MainMenuMethod(user);
        }
        public static void PrintingUserData(UserCreation user)
        {
            int most = 0;

            string id = Convert.ToString(user.ID);
            string pw = "";

            foreach (char c in user.PW)
            {
                pw += "*";
            }


            string name = Convert.ToString(user.Name);
            string city = Convert.ToString(user.City);

            int amountID = 7 + id.Count(),
                 amountPW = 7 + pw.Count(),
                 amountName = 9 + name.Count(),
                 amountCity = 10 + city.Count();

            List<int> amounts = new List<int> { amountID, amountPW, amountName, amountCity };

            foreach (int amount in amounts)
            {
                if (most < amount)
                {
                    most = amount;
                }
            }

            string[,] userData = new string[13, most];

            for (int j = 0; j < 13; j++)
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
                        string DATA = "Nutzerdaten:".PadLeft(12 / 2 + most / 2);
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
                        string id_ = $"ID:{user.ID}".PadRight((id.Count() + 3) / 2 + most / 2);
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
                        string pw_ = $"PW:{pw}".PadLeft((pw.Count() + 3) / 2 + most / 2);
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
                        string name_ = $" Name:{user.Name}".PadLeft((name.Count() + 5) / 2 + most / 2);
                        name_ = name_.PadRight(most - 4);


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

                    else if (j == 11)
                    {
                        string city_ = $" Stadt:{user.City}".PadLeft((city.Count() + 9) / 2 + most / 2);
                        city_ = city_.PadRight(most - 4);

                        List<char> chars = new List<char>();

                        foreach (char x in city_)
                        {
                            chars.Add(x);
                        }

                        if (i < chars.Count)
                        {
                            userData[j, i] = Convert.ToString(chars[i]);
                        }
                    }
                   
                    if (j == 12)
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
                    if (j == 6 || j == 8 || j == 10 )
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
            Console.Clear();

            for (int j = 0; j < 13; j++)
            {
                for (int i = 0; i < most; i++)
                {
                    Console.Write(userData[j, i]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("\n");
            }
        }
        public static void MainMenuMethod(UserCreation user)
        {
            do
            {
                int entry;
                ConsoleKeyInfo userInput = Console.ReadKey();

                if (userInput.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }

                if (char.IsDigit(userInput.KeyChar))
                {
                    entry = int.Parse(userInput.KeyChar.ToString());

                    if (entry >= 8 || entry <= 0)
                    {
                    }
                    else
                    {
                        switch (entry)
                        {
                            case 1:

                                CarPool.Planning(user);
                                StartMenu(user);
                                break;
                            case 2:

                                CarPool.Joining(user);
                                StartMenu(user);
                                break;
                            case 3:

                                CarPool.Change(user);
                                StartMenu(user);
                                break;
                            case 4:
                                FileManager.AllData(user);
                                StartMenu(user);
                                break;
                            case 5:
                                PrintingUserData(user);
                                Console.ReadKey();
                                StartMenu(user);
                                break;
                            case 6:
                                user = UserCreation.ChangeData(user);
                                StartMenu(user);
                                break;
                            case 7:
                                var fileName = Assembly.GetExecutingAssembly().Location;
                                System.Diagnostics.Process.Start(fileName);
                                Environment.Exit(0);
                                StartMenu(user);
                                break;
                            default:
                                App.ResetCursor();
                                break;
                        }
                    }
                }
            } while (true);
        }       
    }
}
