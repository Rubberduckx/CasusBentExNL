using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Models
{
	internal class Area
	{

		private int id;
		private double lat;
		private double lng;
		private string image;
		private string description;

		public Area(int id, double lat, double lng, string image, string description)
		{
			this.id = id;
			this.lat = lat;
			this.lng = lng;
			this.image = image;
			this.description = description;
		}

		public int GetId() => id;
		public double GetLat() => lat;
		public double GetLong() => lng;
		public string GetImage() => image;
		public string GetDescription() => description;
	}
}
