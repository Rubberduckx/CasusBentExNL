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
                Vertex closest = queue.OrderBy(v => distance[v]).First();
                if (closest == endPoint) { 
                    Vertex target = closest;
                    if (previous[closest] != null || closest == startPoint) {
                        while (closest != null)
                        {
                            stack.Push(closest);
                            closest = previous[closest];
                        }
                        return stack;
                    }
                }
                queue.Remove(closest);
                foreach (Vertex neighbor in closest.GetNeighbors().Keys)
                {
                    if (queue.Contains(neighbor))
                    {
                        int alt = distance[closest] + closest.GetDistanceToNeighbor(neighbor);
                        if (alt < distance[neighbor])
                        {
                            distance[neighbor] = alt;
                            previous[neighbor] = closest;
                        }
                    }
                }
            }

            return null;
        }
    }
}
