using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Pathfinding
{
    internal class Dijkstra
    {
        public static Stack<Vertex> ShortestRoute(Graph graph, Vertex startPoint, Vertex endPoint)
        {
            Stack<Vertex> stack = new Stack<Vertex>();
            Vertex[] unvisited = graph.GetVertices().ToArray();
            Vertex[] visited = new Vertex[graph.GetVertices().ToArray().Length];
            Dictionary<Vertex, int> distance = new Dictionary<Vertex, int>();
            Dictionary<Vertex, Vertex> previous = new Dictionary<Vertex, Vertex>();
            List<Vertex> queue = new List<Vertex>();    
            foreach (Vertex vertex in graph.GetVertices()) { 
                distance[vertex] = Int32.MaxValue;
                previous[vertex] = null;
                queue.Add(vertex);
            }
            distance[startPoint] = 0;

            while (queue.Count > 0)
            {
                Vertex u = queue.OrderBy(v => distance[v]).First();
                if (u == endPoint) { 
                    Vertex target = u;
                    if (previous[u] != null || u == startPoint) {
                        while (u != null)
                        {
                            stack.Push(u);
                            u = previous[u];
                        }
                        return stack;
                    }
                }
                queue.Remove(u);
                foreach (Vertex v in u.GetNeighbors().Keys)
                {
                    if (queue.Contains(v))
                    {
                        int alt = distance[u] + u.GetDistanceToNeighbor(v);
                        if (alt < distance[v])
                        {
                            distance[v] = alt;
                            previous[v] = u;
                        }
                    }
                }
            }

            return null;
        }
    }
}
