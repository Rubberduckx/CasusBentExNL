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
    }
}
