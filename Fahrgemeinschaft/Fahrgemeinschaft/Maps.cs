using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace Fahrgemeinschaft
{
    public class Maps
    {
        public static string Calculate(int start, int end, List<int> intermediate)
        {
            double km = 0;
            string fullOrder = "";            
            List<int> drivingOrder = FindClosest(start, end, intermediate);
            int prev = drivingOrder[0];
            foreach (int order in drivingOrder)
            {
                km += Distance(prev, order);
                fullOrder += Convert.ToString(order)+",";
                prev = order;

            }

            return $"Der kürzeste Weg wäre {fullOrder} mit {km} Kilometern.";

        }
        
        public static List<int> FindClosest(int start, int end, List<int> intermediate)
        {
            double least = 1000000;
            int order =0;
            List<int> orders = new List<int>();
            orders.Add(start);
            
                for (int j = 0; j < intermediate.Count; j++)
                {
                    double startToInter = Distance(start, intermediate[j]);
                    double interToEnd = Distance(intermediate[j], end);
                    if (startToInter + interToEnd < least)
                    {
                        least = startToInter +interToEnd;
                        order = j;
                    }
                }
                orders.Add(intermediate[order]);
                intermediate.RemoveAt(order);
            order = 0;
            for (int j = 0; j < intermediate.Count-1; j++)
            {
                for (int i = 0; i < intermediate.Count; i++)
                {
                    double interToNext = Distance(orders[orders.Count - 1], intermediate[i]);
                    double interToEnd = Distance(intermediate[i], end);
                    if (interToNext + interToEnd < least)
                    {
                        least = interToNext+interToEnd;
                        order = i;
                    }
                }
                orders.Add(intermediate[order]);
                intermediate.RemoveAt(order);
            }
            orders.Add(intermediate[intermediate.Count-1]);
            orders.Add(end);
            
            return orders;
        }

        public static double Distance(int startingPoint, int endPoint)
        {
            List<double[]> entries = FileManager.ReadMapData();

            double distance = 0;

            for (int i = 0; i < entries.Count; i++)
            {
                if (startingPoint == endPoint)
                {
                    break;
                }
                if (Convert.ToDouble(Convert.ToInt32(entries[i][0])) == startingPoint || Convert.ToDouble(Convert.ToInt32(entries[i][0])) == endPoint)
                {
                    if (Convert.ToDouble(Convert.ToInt32(entries[i][1])) == endPoint || Convert.ToDouble(Convert.ToInt32(entries[i][1])) == startingPoint)
                    {
                        distance = entries[i][2];
                        break;
                    }
                }
            }
              return distance;            
        }
    }
}          