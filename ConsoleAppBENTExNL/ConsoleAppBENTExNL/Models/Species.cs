using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Models
{
    internal class Species
    {
        private int id;
        private string name;
        private string type;
        private string description;
        private int size;

        public Species(int id, string name, string type, string description, int size)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.description = description;
            this.size = size;
        }

        public int GetId() => id;
        public string GetName() => name;
        public string GetType() => type;
        public string Description() => description;
        public int GetSize() => size;
    }
}
