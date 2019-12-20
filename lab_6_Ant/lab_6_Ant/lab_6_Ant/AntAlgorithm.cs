using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_6_Ant
{    
    class AntAlgorithm
    {
        public static readonly Random r = new Random();
        
        public static Path GetShortestPath(Map map, int nIter, double alpha, double beta, double Q, double ro)
        {
            Path minPath = new Path(int.MaxValue);
            int n = map.n;
            double[][] pheromones = Program.InitMatr(n, 0.1);

            List<Ant> ants = new List<Ant>();
            for (int t = 0; t < nIter; t++) // цикл по дням
            {
                ants = InitAnts(map, n);

                for (int i = 0; i < n; i++)
                {
                    double[][] pheromonesIter = Program.InitMatr(n, (double)0);
                    for (int j = 0; j < ants.Count(); j++) // цикл по муравьям
                    {
                        double sumChance = 0, chance = 0;
                        bool flag = false;
                        Ant curAnt = ants[j];
                        int curCity = curAnt.path.route.Last();
                        for (int k = 0; k < n; k++)
                        {
                            if (curAnt.visited[k] == false) // есть непосещенные города
                            {
                                sumChance += Math.Pow(pheromones[curCity][k], alpha) * Math.Pow(1 / (map.distance[curCity][k]), beta);
                                flag = true;
                            }
                        }
                        if (flag)
                        {
                            double x = r.NextDouble();
                            int k = 0;
                            for (; (x > 0) && (k < n); k++)
                            {
                                if (curAnt.visited[k] == false)
                                {
                                    chance = Math.Pow(pheromones[curCity][k], alpha) * Math.Pow(1 / (map.distance[curCity][k]), beta);
                                    chance /= sumChance;
                                    x -= chance;
                                }
                            }
                            k--;
                            ants[j].VisitedTown(k);
                            pheromonesIter[curCity][k] += Q / (map.distance[curCity][k]);
                        }
                    }

                    // Испарение феромонов
                    for (int ii = 0; ii < n; ii++)
                        for (int j = 0; j < n; j++)
                            pheromones[ii][j] = (1 - ro) * pheromones[ii][j] + pheromonesIter[ii][j];
                }
                
                // Находим минимальные пути этого дня
                foreach (Ant a in ants)
                {
                    if (a.GetDistance() < minPath.distance)
                    {
                        a.VisitedTown(a.iStartTown);
                        minPath = a.path;
                    }
                }                
            }
            return minPath;
        }

        public static List<Ant> InitAnts(Map m, int n)
        {
            List<Ant> ants = new List<Ant>();
            for (int i = 0; i < n; i++)
            {
                ants.Add(new Ant(m, 1));
            }
            return ants;
        }
    }


    class Ant
    {
        public Path path;
        public bool[] visited;
        public int iStartTown;

        public Ant(Map m, int iStartTown)
        {
            path = new Path(m, iStartTown);
            visited = new bool[m.n];
            visited[iStartTown] = true;
            this.iStartTown = iStartTown;
        }

        public void VisitedTown(int i)
        {
            path.AddVertex(i);
            visited[i] = true;
        }

        public int GetDistance()
        {
            return path.distance;
        }
    }
}
