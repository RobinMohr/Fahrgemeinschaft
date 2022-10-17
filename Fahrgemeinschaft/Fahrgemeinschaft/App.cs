using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fahrgemeinschaft
{
    public class App
    {
        public static void EntryWasNotCool()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗\n" +
                              "║ Deine Eingabe war ungültig! Versuche es erneut ║\n" +
                              "╚════════════════════════════════════════════════╝\n");
            Thread.Sleep(1500);
        }
        //public static string CheckPW()
        //{

        //}
        public static void ResetCursor()
        {
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Console.Write(' ');
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }
        public static string DelLastChar(string foo)
        {
            Console.Write(' ');
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            foo = foo.Substring(0, foo.Length - 1);
            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
            Console.Write(' ');
            Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop);
            return foo;
        }
        public static string GetDate()
        {
        SetTime:
            ConsoleKeyInfo chooseTime;
            string userTime = "";
            int prev = 0;
            int prevprev = 0;
            string month = "";
            do
            {
            WrongInput:
                chooseTime = Console.ReadKey();

                if (chooseTime.Key == ConsoleKey.Escape)
                {
                    goto SetTime;
                }

                if (chooseTime.Key == ConsoleKey.Backspace && userTime.Length != 0)
                {
                    if (month.Count() > 0)
                    {
                        if (userTime.Count() == 5 || userTime.Count() == 6)
                        {
                            month = month.Substring(0, month.Length - 1);
                        }
                    }
                    prev = prevprev;

                    if (userTime.Count() == 5 || userTime.Count() == 8 || userTime.Count() == 11 || userTime.Count() == 14)
                    {
                        App.ResetCursor();
                        userTime = userTime.Substring(0, userTime.Length - 1);
                    }
                    Console.Write(' ');
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    userTime = userTime.Substring(0, userTime.Length - 1);


                    goto WrongInput;
                }

                if (int.TryParse(Convert.ToString(chooseTime.KeyChar), out int chosenTime))
                {
                    if (userTime.Count() == 0 && chosenTime != 2)
                    {
                        ResetCursor();
                        goto WrongInput;
                    }
                    if (userTime.Count() == 1 && chosenTime > 1)
                    {
                        App.ResetCursor();
                        goto WrongInput;
                    }
                    if (userTime.Count() == 5 && chosenTime > 1)
                    {
                        App.ResetCursor();
                        goto WrongInput;
                    }
                    if (userTime.Count() == 6 && prev == 1)
                    {
                        if (chosenTime > 2)
                        {
                            App.ResetCursor();
                            goto WrongInput;
                        }
                    }
                    if (userTime.Count() == 5 || userTime.Count() == 6)
                    {
                        month += Convert.ToString(chosenTime);
                    }
                    if (userTime.Count() > 7)
                    {
                        if (Convert.ToInt32(month) == 01 || Convert.ToInt32(month) == 03 || Convert.ToInt32(month) == 05 || Convert.ToInt32(month) == 7 || Convert.ToInt32(month) == 8 || Convert.ToInt32(month) == 10 || Convert.ToInt32(month) == 12)
                        {
                            if (userTime.Count() == 8 && chosenTime > 3)
                            {
                                App.ResetCursor();
                                goto WrongInput;
                            }
                            if (userTime.Count() == 9 && prev == 3)
                            {
                                if (chosenTime > 1)
                                {
                                    App.ResetCursor();
                                    goto WrongInput;
                                }
                            }
                        }
                        else if (Convert.ToInt32(month) == 02)
                        {
                            if (userTime.Count() == 8 && chosenTime > 2)
                            {
                                App.ResetCursor();
                                goto WrongInput;
                            }
                            if (userTime.Count() == 9 && prev == 2)
                            {
                                if (chosenTime > 9)
                                {
                                    App.ResetCursor();
                                    goto WrongInput;
                                }
                            }
                        }
                        else
                        {
                            if (userTime.Count() == 8 && chosenTime > 3)
                            {
                                App.ResetCursor();
                                goto WrongInput;
                            }
                            if (userTime.Count() == 9 && prev == 3)
                            {
                                if (chosenTime > 0)
                                {
                                    App.ResetCursor();
                                    goto WrongInput;
                                }
                            }
                        }
                    }
                    if (userTime.Count() == 12 && chosenTime > 2)
                    {
                        App.ResetCursor();
                        goto WrongInput;
                    }
                    if (userTime.Count() == 13 && prev == 2)
                    {
                        if (chosenTime > 4)
                        {
                            App.ResetCursor();
                            goto WrongInput;
                        }
                    }
                    if (userTime.Count() == 15 && chosenTime > 6)
                    {
                        App.ResetCursor();
                        goto WrongInput;
                    }
                    if (userTime.Count() == 16 && prev == 6)
                    {
                        if (chosenTime > 0)
                        {
                            ResetCursor();
                            goto WrongInput;
                        }
                    }

                    userTime += chosenTime;
                    prevprev = prev;
                    prev = chosenTime;
                }

                if (!int.TryParse(Convert.ToString(chooseTime.KeyChar), out chosenTime))
                {
                    App.ResetCursor();
                }

                if (userTime.Count() == 4 || userTime.Count() == 7 || userTime.Count() == 10)
                {
                    Console.Write(".");
                    userTime += ".";
                }

                if (userTime.Count() == 13)
                {
                    Console.Write(":");
                    userTime += ":";
                }

            } while (userTime.Count() < 16);
            return userTime;
        }
        public static (int, int) UserNumEntries()
        {
        WrongInput:

            string foo = "";
            ConsoleKeyInfo baa;
            int back = 0;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write("→ ←");
            Console.SetCursorPosition(1, Console.CursorTop);

            do
            {
                baa = Console.ReadKey();
                if (baa.Key == ConsoleKey.Enter)
                {
                    if (foo.Length > 0)
                    {
                        break;
                    }
                    else
                    {
                        goto WrongInput;
                    }
                }
                else if (baa.Key == ConsoleKey.Escape)
                {
                    back++;
                    foo = "0";
                    break;
                }
                else if (baa.Key == ConsoleKey.Backspace)
                {
                    if (foo.Count() != 0 && Console.CursorLeft >0)
                    {
                        foo = DelLastChar(foo);
                    }
                    if (Console.CursorLeft == 0)
                    {
                        Console.Write("→");
                    }
                }
                else if (baa.Key != ConsoleKey.Enter)
                {
                    if (int.TryParse(Convert.ToString(baa.KeyChar), out int userKey))
                    {
                        foo += userKey;
                    }
                    else
                    {
                        ResetCursor();
                    }
                }
                Console.Write("←");
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            } while (foo.Length < 9);

            return (Convert.ToInt32(foo), back);
        }
        public static (string, int) UserStringEntry()
        {
        WrongInput:

            string foo = "";
            ConsoleKeyInfo baa;
            int back = 0;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write("→ ←");
            Console.SetCursorPosition(1, Console.CursorTop);

            do
            {
                baa = Console.ReadKey();
                if (baa.Key == ConsoleKey.Enter)
                {
                    if (foo.Length > 0)
                    {
                        break;
                    }
                    else
                    {
                        goto WrongInput;
                    }
                }
                else if (baa.Key == ConsoleKey.Escape)
                {
                    back++;
                    foo = "0";
                    break;
                }
                else if (baa.Key == ConsoleKey.Backspace)
                {
                    if (foo.Count() != 0 && Console.CursorLeft > 0)
                    {
                        foo = DelLastChar(foo);
                    }
                    if (Console.CursorLeft == 0)
                    {
                        Console.Write("→");
                    }
                }
                else if (baa.Key != ConsoleKey.Enter)
                {
                    foo += Convert.ToString(baa.KeyChar);
                }
                Console.Write("←");
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            } while (true);
            return (foo, back);
        }
        public static bool YesOrNo()
        {
        WrongInput:
            ConsoleKeyInfo foo;
            bool yes;
            do
            {
                foo = Console.ReadKey();
                if (int.TryParse(Convert.ToString(foo), out int baa))
                {
                    ResetCursor();
                    goto WrongInput;
                }
                else
                {
                    if (foo.Key == ConsoleKey.Y)
                    {
                        yes = true;
                        break;
                    }
                    else
                    {
                        yes = false;
                    }
                }
            } while (true);
            return yes;
        }
        public static (List<Person>, int) LookForDifferentUser(Person person, UserCreation user)
        {
            int count = 0;

            List<Person> foo = new List<Person>();

            List<string[]> baa = FileManager.ReadRouteData();

            for (int j = 0; j < baa.Count; j++)
            {
                Person person1 = new Person();

                if (Convert.ToInt32(baa[j][0]) != person.Driver && Convert.ToInt32(baa[j][2]) != user.ID)
                {
                    person1.Time = baa[j][3].Split('.');
                    person1.Name = baa[j][1];
                    person1.Id = Convert.ToInt32(baa[j][2]);
                    person1.Amount = Convert.ToInt32(baa[j][4]);

                    if (person.Time[0] == person1.Time[0] && person.Time[1] == person1.Time[1] && person.Time[1] == person1.Time[1] && person.Free_Spaces >= person1.Amount)
                    {
                        foo.Add(person1);
                        count++;
                    }
                }
            }
            return (foo, count);
        }
        public static List<char> StringToChars(string foo)
        {
            List<char> chars = new List<char>();
            foreach (char x in foo)
            {
                chars.Add(x);
            }
            return chars;
        }
        public static void PrintPerson(List<Person> person)
        {
            List<string[]> allEntries = FileManager.ReadRouteData();

            foreach (Person p in person)
            {
                int most = 0;


                string id = Convert.ToString(p.Id);
                string name = Convert.ToString(p.Name);
                string time = String.Join(".", p.Time);
                string amountPeople = Convert.ToString(p.Amount);
                string freeSpaces = Convert.ToString(p.Free_Spaces);


                int amountID = 7 + id.Count(),
                    amountTime = 22 + time.Count(),
                    amountName = 9 + name.Count(),
                    amountAmountPeople = 10 + amountPeople.Count(),
                    amountFreeSpaces = 17 + freeSpaces.Count();

                
                List<int> allAmount = new List<int> { amountID, amountTime, amountName, amountAmountPeople , amountFreeSpaces};

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
                            string name_ = $"{p.Name}".PadLeft((most / 2) + (name.Length / 2));
                            List<char> chars = StringToChars(name_);
                            if (i < chars.Count)
                            {
                                userData[j, i] = Convert.ToString(chars[i]);
                            }
                        }

                        else if (j == 5)
                        {
                            string id_ = $"ID:{p.Id}".PadRight((id.Count() + 3) / 2 + most / 2);
                            List<char> chars = StringToChars(id_);

                            if (i < chars.Count)
                            {
                                userData[j, i] = Convert.ToString(chars[i]);
                            }
                        }
                        else if (j == 7)
                        {
                            string dateAndTime = $"Datum und Uhrzeit:{String.Join(".", p.Time)}".PadLeft((Convert.ToString(amountTime).Count() + 18) / 2 + most / 2);

                            List<char> chars = StringToChars(dateAndTime);

                            if (i < chars.Count)
                            {
                                userData[j, i + 1] = Convert.ToString(chars[i]);
                            }
                        }

                        else if (j == 9)
                        {
                            if (p.Driver == 0)
                            {
                                string amount_ = $"Anzahl:{p.Amount}".PadLeft((amountPeople.Count() + 7) / 2 + most / 2);
                                amount_ = amount_.PadRight(most - 4);

                                List<char> chars = new List<char>();

                                foreach (char x in amount_)
                                {
                                    chars.Add(x);
                                }

                                if (i < chars.Count)
                                {
                                    userData[j, i + 1] = Convert.ToString(chars[i]);
                                }
                            }
                            else if (p.Driver == 1)
                            {
                                string direction_ = $"Freie Plätze:{p.Free_Spaces}".PadLeft((freeSpaces.Count() + 13) / 2 + most / 2);
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
                Thread.Sleep(1500);
            }
        }
    }
}