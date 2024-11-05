using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Pathfinding
{
    internal class Vertex
    {
        public string name;
        private Dictionary<Vertex, int> neighbors = new Dictionary<Vertex, int>();

        public Vertex(string name)
        {
            this.name = name;
        }

        public void AddNeighbor(Vertex neighbor, int distance)
        {
            neighbors[neighbor] = distance;
        }

        public void AddTwoWayNeighbor(Vertex neighbor, int distance)
        {
            this.neighbors[neighbor] = distance;

            if (!neighbor.neighbors.ContainsKey(this))
            {
                neighbor.neighbors[this] = distance;
            }
        }

        public void RemoveNeighbors(Vertex neighbor)
        {
            neighbors.Remove(neighbor);
        }

        public Dictionary<Vertex, int> GetNeighbors()
        {
            return neighbors;
        }

        public int GetDistanceToNeighbor(Vertex neighbor)
        {
            return neighbors[neighbor];
        }

        public Vertex GetClosestNeighbor()
        {
            return neighbors.OrderBy(c => c.Value).FirstOrDefault().Key;
        }
    }
}
