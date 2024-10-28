using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Models
{
    internal class Observation
    {
        private int id;
        private double lat;
        private double lng;
        private string image;
        private string description;
        private Species species;
        private User user;
        private Area area;

        public Observation(int id, double lat, double lng, Species species, User user, Area area = null, string image = "", string description = "")
        {
            this.id = id;
            this.lat = lat;
            this.lng = lng;
            this.species = species;
            this.user = user;
            this.area = area;
            this.image = image;
            this.description = description;
        }

        public int GetId() => id;
        public double GetLat() => lat;
        public double GetLong() => lng;
        public string GetImage() => image;
        public string GetDescription() => description;
        public Species GetSpecies() => species;
        public User GetUser() => user;
        public Area GetArea() => area;
    }
}
