using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Pathfinding
{
    internal class Graph
    {
        private List<Vertex> vertices = new List<Vertex>();
        
        public Graph() { 
        }
        public List<Vertex> GetVertices()
        {
            return vertices;
        }

        public void AddVertex(Vertex v) {
            vertices.Add(v);
        }
    }
}
