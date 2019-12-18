using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_6_Ant
{
    class Map
    {
        readonly static Random r = new Random();
        readonly public int n;
        readonly public int[][] distance;
        readonly public Point[] position;

        public Map(int n, int width = 40, int height = 40)
        {
            position = new Point[n];
            for (int i = 0; i < n; i++)
            {
                position[i] = new Point(r.Next(width), r.Next(height));
            }

            distance = new int[n][];
            for (int i = 0; i < n; i++)
            {
                distance[i] = new int[n];
            }

            for (int i = 0; i < n; i++)
            {
                distance[i][i] = -1;
                for (int j = i + 1; j < n; j++)
                {
                    distance[i][j] = distance[j][i] = Point.GetDistance(position[i], position[j]);
                }
            }
        }

    }

    class Point
    {
        int x, y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static int GetDistance(Point a, Point b)
        {
            return (int)Math.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
        }
    }
}
