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
        private Dictionary<Vertex, int> children = new Dictionary<Vertex, int>();

        public Vertex(string name)
        {
            this.name = name;
        }

        public void AddChild(Vertex child, int distance)
        {
            children.Add(child, distance);
        }

        public void RemoveChild(Vertex child)
        {
            children.Remove(child);
        }

        public Dictionary<Vertex, int> GetChildren()
        {
            return children;
        }

        public int GetDistanceToChild(Vertex child)
        {
            return children[child];
        }

        public Vertex GetClosestChild()
        {
            return children.OrderBy(c => c.Value).FirstOrDefault().Key;
        }
    }
}
