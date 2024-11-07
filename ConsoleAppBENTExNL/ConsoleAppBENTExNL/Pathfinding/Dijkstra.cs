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

        // HULP GEHAD VAN CHATGPT

        /*  ==================== Calculate shortest route between two points ==================== */
        public static Stack<Vertex> ShortestRoute(Graph graph, Vertex startPoint, Vertex endPoint)
        {
            Stack<Vertex> stack = new Stack<Vertex>();
            Vertex[] unvisited = graph.GetVertices().ToArray(); //All vertices in graph
            Vertex[] visited = new Vertex[graph.GetVertices().ToArray().Length]; //Empty array with space for every graph
            Dictionary<Vertex, int> distance = new Dictionary<Vertex, int>();
            Dictionary<Vertex, Vertex> previous = new Dictionary<Vertex, Vertex>();
            List<Vertex> queue = new List<Vertex>();
            foreach (Vertex vertex in graph.GetVertices())
            {
                distance[vertex] = Int32.MaxValue; //distance from start to every graph is infinite
                previous[vertex] = null; //no route determined yet, so no previous point in route
                queue.Add(vertex);
            }
            distance[startPoint] = 0; //distance from start point to itself is 0

            while (queue.Count > 0)
            {
                Vertex closest = queue.OrderBy(v => distance[v]).First(); //find vertex closest to start
                if (closest == endPoint)
                { //if end is reached
                    Vertex target = closest; //set targest to closest, which is equal to endPoint
                    if (previous[closest] != null || closest == startPoint)
                    { //if a previous node is in route or endPoint is equal to startPoint
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
        public static void ParkTestCase()
        {
            Graph graph = new Graph();
            Vertex a = new Vertex("Montfort");
            Vertex b = new Vertex("Sint Odiliënberg");
            Vertex c = new Vertex("Linne");
            Vertex d = new Vertex("Horn");
            Vertex e = new Vertex("Ell");
            Vertex f = new Vertex("Stramproy");
            Vertex g = new Vertex("Roggel");
            Vertex h = new Vertex("Laar");
            Vertex i = new Vertex("Ospeldijk");
            Vertex j = new Vertex("Egchel");

            graph.AddVertex(a);
            graph.AddVertex(b);
            graph.AddVertex(c);
            graph.AddVertex(d);
            graph.AddVertex(e);
            graph.AddVertex(f);
            graph.AddVertex(g);
            graph.AddVertex(h);
            graph.AddVertex(i);
            graph.AddVertex(j);

            a.AddTwoWayNeighbor(b, 3);
            a.AddTwoWayNeighbor(c, 3);
            a.AddTwoWayNeighbor(d, 6);
            a.AddTwoWayNeighbor(e, 9);
            a.AddTwoWayNeighbor(f, 12);
            a.AddTwoWayNeighbor(g, 12);
            a.AddTwoWayNeighbor(h, 17);
            a.AddTwoWayNeighbor(i, 16);
            a.AddTwoWayNeighbor(j, 13);
            b.AddTwoWayNeighbor(c, 2);
            b.AddTwoWayNeighbor(d, 5);
            b.AddTwoWayNeighbor(e, 12);
            b.AddTwoWayNeighbor(f, 14);
            b.AddTwoWayNeighbor(g, 11);
            b.AddTwoWayNeighbor(h, 18);
            b.AddTwoWayNeighbor(i, 16);
            b.AddTwoWayNeighbor(j, 11);
            c.AddTwoWayNeighbor(d, 3);
            c.AddTwoWayNeighbor(e, 10);
            c.AddTwoWayNeighbor(f, 11);
            c.AddTwoWayNeighbor(g, 8);
            c.AddTwoWayNeighbor(h, 16);
            c.AddTwoWayNeighbor(i, 14);
            c.AddTwoWayNeighbor(j, 9);
            d.AddTwoWayNeighbor(e, 6);
            d.AddTwoWayNeighbor(f, 10);
            d.AddTwoWayNeighbor(g, 5);
            d.AddTwoWayNeighbor(h, 13);
            d.AddTwoWayNeighbor(i, 14);
            d.AddTwoWayNeighbor(j, 7);
            e.AddTwoWayNeighbor(f, 3);
            e.AddTwoWayNeighbor(g, 7);
            e.AddTwoWayNeighbor(h, 7);
            e.AddTwoWayNeighbor(i, 7);
            e.AddTwoWayNeighbor(j, 11);
            f.AddTwoWayNeighbor(g, 10);
            f.AddTwoWayNeighbor(h, 5);
            f.AddTwoWayNeighbor(i, 9);
            f.AddTwoWayNeighbor(j, 14);
            g.AddTwoWayNeighbor(h, 10);
            g.AddTwoWayNeighbor(i, 4);
            g.AddTwoWayNeighbor(j, 4);
            h.AddTwoWayNeighbor(i, 4);
            h.AddTwoWayNeighbor(j, 13);
            i.AddTwoWayNeighbor(j, 8);

            Stack<Vertex> path = Dijkstra.ShortestRoute(graph, a, i);

            Console.WriteLine("Shortest path from A to I:");
            while (path.Count > 0)
            {
                Console.Write(path.Pop().name + (path.Count > 0 ? " -> " : "\n"));
            }

            Console.ReadKey();
        }
    }
}
