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
        /*  ==================== Calculate shortest route between two points ==================== */
        public static Stack<Vertex> ShortestRoute(Graph graph, Vertex startPoint, Vertex endPoint)
        {
            Stack<Vertex> stack = new Stack<Vertex>();
            Vertex[] unvisited = graph.GetVertices().ToArray(); //All vertices in graph
            Vertex[] visited = new Vertex[graph.GetVertices().ToArray().Length]; //Empty array with space for every graph
            Dictionary<Vertex, int> distance = new Dictionary<Vertex, int>();
            Dictionary<Vertex, Vertex> previous = new Dictionary<Vertex, Vertex>();
            List<Vertex> queue = new List<Vertex>();    
            foreach (Vertex vertex in graph.GetVertices()) { 
                distance[vertex] = Int32.MaxValue; //distance from start to every graph is infinite
                previous[vertex] = null; //no route determined yet, so no previous point in route
                queue.Add(vertex);
            }
            distance[startPoint] = 0; //distance from start point to itself is 0

            while (queue.Count > 0)
            {
                Vertex closest = queue.OrderBy(v => distance[v]).First(); //find vertex closest to start
                if (closest == endPoint) { //if end is reached
                    Vertex target = closest; //set targest to closest, which is equal to endPoint
                    if (previous[closest] != null || closest == startPoint) { //if a previous node is in route or endPoint is equal to startPoint
                        while (closest != null)
                        {
                            stack.Push(closest);
                            closest = previous[closest]; //find previous node in route
                        }
                        return stack;
                    }
                }
                queue.Remove(closest);
                foreach (Vertex neighbor in closest.GetNeighbors().Keys) //iterate through all neighbors
                {
                    if (queue.Contains(neighbor))
                    {
                        int alt = distance[closest] + closest.GetDistanceToNeighbor(neighbor); //alternative route found, calculating length
                        if (alt < distance[neighbor])
                        {
                            distance[neighbor] = alt; //new fastest route set to alternate route
                            previous[neighbor] = closest;
                        }
                    }
                }
            }

            return null;
        }
    }
}
