using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrgemeinschaft
{
    public class Maps
    {
        //public static double Calculate(List<int> cities)
        //{
        //    List<int> distances = new List<int>();


        //    foreach (int city in cities)
        //    {
        //        foreach (int city2 in cities)
        //        {
        //            Distance(city, city2);
        //        }
        //    }
        //}




        //public static double Distance(int startingPoint, int endPoint)
        //{
        //    double distance;

        //    if (startingPoint == endPoint)
        //    {
        //        distance = 0;
        //    }

        //    else if (startingPoint == 0 || endPoint == 0)
        //    {
        //        if (startingPoint == 1 || endPoint == 1)
        //        {
        //            distance = 13.2;
        //        }

        //        else if (startingPoint == 2 || endPoint == 2)
        //        {
        //            distance = 7.3;
        //        }

        //        else if (startingPoint == 3 || endPoint == 3)
        //        {
        //            distance = 24.6;
        //        }

        //        else if (startingPoint == 4 || endPoint == 4)
        //        {
        //            distance = 29.6;
        //        }

        //        else if (startingPoint == 5 || endPoint == 5)
        //        {
        //            distance = 21.3;
        //        }

        //        else if (startingPoint == 6 || endPoint == 6)
        //        {
        //            distance = 6.3;
        //        }

        //        else if (startingPoint == 7 || endPoint == 7)
        //        {
        //            distance = 9.6;
        //        }

        //        else if (startingPoint == 8 || endPoint == 8)
        //        {
        //            distance = 13;
        //        }

        //        else if (startingPoint == 9 || endPoint == 9)
        //        {
        //            distance = 14.6;
        //        }

        //        else if (startingPoint == 10 || endPoint == 10)
        //        {
        //            distance = 13.6;
        //        }

        //        else if (startingPoint == 11 || endPoint == 11)
        //        {
        //            distance = 17.1;
        //        }

        //        else if (startingPoint == 12 || endPoint == 12)
        //        {
        //            distance = 21.2;
        //        }

        //        else if (startingPoint == 13 || endPoint == 13)
        //        {
        //            distance = 34.8;
        //        }
        //    }

        //    else if (startingPoint == 1 || endPoint == 1)
        //    {
        //        if (startingPoint == 2 || endPoint == 2)
        //        {
        //            distance = 4.1;
        //        }

        //        else if (startingPoint == 3 || endPoint == 3)
        //        {
        //            distance = 11.4;
        //        }

        //        else if (startingPoint == 4 || endPoint == 4)
        //        {
        //            distance = 19.5;
        //        }

        //        else if (startingPoint == 5 || endPoint == 5)
        //        {
        //            distance = 11.2;
        //        }

        //        else if (startingPoint == 6 || endPoint == 6)
        //        {
        //            distance = 15.7;
        //        }

        //        else if (startingPoint == 7 || endPoint == 7)
        //        {
        //            distance = 20.1;
        //        }

        //        else if (startingPoint == 8 || endPoint == 8)
        //        {
        //            distance = 22.5;
        //        }

        //        else if (startingPoint == 9 || endPoint == 9)
        //        {
        //            distance = 25.1;
        //        }

        //        else if (startingPoint == 10 || endPoint == 10)
        //        {
        //            distance = 23.9;
        //        }

        //        else if (startingPoint == 11 || endPoint == 11)
        //        {
        //            distance = 20.8;
        //        }

        //        else if (startingPoint == 12 || endPoint == 12)
        //        {
        //            distance = 26.2;
        //        }

        //        else if (startingPoint == 13 || endPoint == 13)
        //        {
        //            distance = 18.5;
        //        }
        //    }

        //    else if (startingPoint == 2 || endPoint == 2)
        //    {
        //                        if (startingPoint == 3 || endPoint == 3)
        //        {
        //            distance = 17.1;
        //        }

        //        else if (startingPoint == 4 || endPoint == 4)
        //        {
        //            distance = 18.8;
        //        }

        //        else if (startingPoint == 5 || endPoint == 5)
        //        {
        //            distance = 13.7;
        //        }

        //        else if (startingPoint == 6 || endPoint == 6)
        //        {
        //            distance = 11.1;
        //        }

        //        else if (startingPoint == 7 || endPoint == 7)
        //        {
        //            distance = 14.4;
        //        }

        //        else if (startingPoint == 8 || endPoint == 8)
        //        {
        //            distance = 17.9;
        //        }

        //        else if (startingPoint == 9 || endPoint == 9)
        //        {
        //            distance = 20.5;
        //        }

        //        else if (startingPoint == 10 || endPoint == 10)
        //        {
        //            distance = 14.9;
        //        }

        //        else if (startingPoint == 11 || endPoint == 11)
        //        {
        //            distance = 22.6;
        //        }

        //        else if (startingPoint == 12 || endPoint == 12)
        //        {
        //            distance = 27.1;
        //        }

        //        else if (startingPoint == 13 || endPoint == 13)
        //        {
        //            distance = 24.2;
        //        }
        //    }

        //    else if (startingPoint == 3 || endPoint == 3)
        //    {
        //        if (startingPoint == 4 || endPoint == 4)
        //        {
        //            distance = 14;
        //        }

        //        else if (startingPoint == 5 || endPoint == 5)
        //        {
        //            distance = 22.1;
        //        }

        //        else if (startingPoint == 6 || endPoint == 6)
        //        {
        //            distance = 28.5;
        //        }

        //        else if (startingPoint == 7 || endPoint == 7)
        //        {
        //            distance = 31.9;
        //        }

        //        else if (startingPoint == 8 || endPoint == 8)
        //        {
        //            distance = 37.4;
        //        }

        //        else if (startingPoint == 9 || endPoint == 9)
        //        {
        //            distance = 37.9;
        //        }

        //        else if (startingPoint == 10 || endPoint == 10)
        //        {
        //            distance = 30.7;
        //        }

        //        else if (startingPoint == 11 || endPoint == 11)
        //        {
        //            distance = 40;
        //        }

        //        else if (startingPoint == 12 || endPoint == 12)
        //        {
        //            distance = 44.4;
        //        }

        //        else if (startingPoint == 13 || endPoint == 13)
        //        {
        //            distance = 9.5;
        //        }
        //    }

        //    else if (startingPoint == 4 || endPoint == 4)
        //    {
        //        if (startingPoint == 5 || endPoint == 5)
        //        {
        //            distance = 9.8;
        //        }

        //        else if (startingPoint == 6 || endPoint == 6)
        //        {
        //            distance = 33.6;
        //        }

        //        else if (startingPoint == 7 || endPoint == 7)
        //        {
        //            distance = 36.9;
        //        }

        //        else if (startingPoint == 8 || endPoint == 8)
        //        {
        //            distance = 40.3;
        //        }

        //        else if (startingPoint == 9 || endPoint == 9)
        //        {
        //            distance = 45.2;
        //        }

        //        else if (startingPoint == 10 || endPoint == 10)
        //        {
        //            distance = 32.3;
        //        }

        //        else if (startingPoint == 11 || endPoint == 11)
        //        {
        //            distance = 36;
        //        }

        //        else if (startingPoint == 12 || endPoint == 12)
        //        {
        //            distance = 38.3;
        //        }

        //        else if (startingPoint == 13 || endPoint == 13)
        //        {
        //            distance = 22;
        //        }
        //    }

        //    else if (startingPoint == 5 || endPoint == 5)
        //    {
        //       if (startingPoint == 6 || endPoint == 6)
        //        {
        //            distance = 25.2;
        //        }

        //        else if (startingPoint == 7 || endPoint == 7)
        //        {
        //            distance = 28.5;
        //        }

        //        else if (startingPoint == 8 || endPoint == 8)
        //        {
        //            distance = 31.9;
        //        }

        //        else if (startingPoint == 9 || endPoint == 9)
        //        {
        //            distance = 34.5;
        //        }

        //        else if (startingPoint == 10 || endPoint == 10)
        //        {
        //            distance = 23.9;
        //        }

        //        else if (startingPoint == 11 || endPoint == 11)
        //        {
        //            distance = 23.6;
        //        }

        //        else if (startingPoint == 12 || endPoint == 12)
        //        {
        //            distance = 29.9;
        //        }

        //        else if (startingPoint == 13 || endPoint == 13)
        //        {
        //            distance = 29.5;
        //        }
        //    }

        //    else if (startingPoint == 6 || endPoint == 6)
        //    {
        //       if (startingPoint == 7 || endPoint == 7)
        //        {
        //            distance = 4.1;
        //        }

        //        else if (startingPoint == 8 || endPoint == 8)
        //        {
        //            distance = 7.6;
        //        }

        //        else if (startingPoint == 9 || endPoint == 9)
        //        {
        //            distance = 11.3;
        //        }

        //        else if (startingPoint == 10 || endPoint == 10)
        //        {
        //            distance = 16.4;
        //        }

        //        else if (startingPoint == 11 || endPoint == 11)
        //        {
        //            distance = 19.3;
        //        }

        //        else if (startingPoint == 12 || endPoint == 12)
        //        {
        //            distance = 23.8;
        //        }

        //        else if (startingPoint == 13 || endPoint == 13)
        //        {
        //            distance = 33.5;
        //        }
        //    }

        //    else if (startingPoint == 7 || endPoint == 7)
        //    {
        //        if (startingPoint == 8 || endPoint == 8)
        //        {
        //            distance = 4.1;
        //        }

        //        else if (startingPoint == 9 || endPoint == 9)
        //        {
        //            distance = 9.5;
        //        }

        //        else if (startingPoint == 10 || endPoint == 10)
        //        {
        //            distance = 16.8;
        //        }

        //        else if (startingPoint == 11 || endPoint == 11)
        //        {
        //            distance = 20.8;
        //        }

        //        else if (startingPoint == 12 || endPoint == 12)
        //        {
        //            distance = 24.3;
        //        }

        //        else if (startingPoint == 13 || endPoint == 13)
        //        {
        //            distance = 37.8;
        //        }
        //    }

        //    else if (startingPoint == 8 || endPoint == 8)
        //    {
        //       if (startingPoint == 9 || endPoint == 9)
        //        {
        //            distance = 5.4;
        //        }

        //        else if (startingPoint == 10 || endPoint == 10)
        //        {
        //            distance = 19.5;
        //        }

        //        else if (startingPoint == 11 || endPoint == 11)
        //        {
        //            distance = 23.5;
        //        }

        //        else if (startingPoint == 12 || endPoint == 12)
        //        {
        //            distance = 25.7;
        //        }

        //        else if (startingPoint == 13 || endPoint == 13)
        //        {
        //            distance = 41.6;
        //        }
        //    }

        //    else if (startingPoint == 9 || endPoint == 9)
        //    {
        //       if (startingPoint == 10 || endPoint == 10)
        //        {
        //            distance = 17.2;
        //        }

        //        else if (startingPoint == 11 || endPoint == 11)
        //        {
        //            distance = 15.2;
        //        }

        //        else if (startingPoint == 12 || endPoint == 12)
        //        {
        //            distance = 20.4;
        //        }

        //        else if (startingPoint == 13 || endPoint == 13)
        //        {
        //            distance = 44.9;
        //        }
        //    }

        //    else if (startingPoint == 10 || endPoint == 10)
        //    {
        //       if (startingPoint == 11 || endPoint == 11)
        //        {
        //            distance = 4.6;
        //        }

        //        else if (startingPoint == 12 || endPoint == 12)
        //        {
        //            distance = 8;
        //        }

        //        else if (startingPoint == 13 || endPoint == 13)
        //        {
        //            distance = 39.8;
        //        }
        //    }

        //    else if (startingPoint == 11 || endPoint == 11)
        //    {
        //       if (startingPoint == 12 || endPoint == 12)
        //        {
        //            distance = 6;
        //        }

        //        else if (startingPoint == 13 || endPoint == 13)
        //        {
        //            distance = 43;
        //        }
        //    }

        //    else if (startingPoint == 12 || endPoint == 12)
        //    {
        //        if (startingPoint == 13 || endPoint == 13)
        //        {
        //            distance = 47.4;
        //        }
        //    }


        //    return distance;









        //}
    }
}          