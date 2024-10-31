using ConsoleAppBENTExNL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Models
{
	internal class Area
	{

		private int id { get; set; }
		private double lat { get; set; }
		private double lng { get; set; }
		private string image { get; set; }
		private string description { get; set; }

		public Area(double lat, double lng, string image, string description)
		{
            this.lat = lat;
            this.lng = lng;
            this.image = image;
            this.description = description;
        }
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
		public double GetLng() => lng;
		public string GetImage() => image;
		public string GetDescription() => description;


		public void CreateArea(Area area)
		{
			SQLDAL sqldal = SQLDAL.GetSingleton();
			sqldal.CreateArea(area);
		}

		public void GetArea(int id)
		{
			SQLDAL sqldal = SQLDAL.GetSingleton();
			sqldal.GetArea(id);
		}

		public void UpdateArea(Area area)
		{
			SQLDAL sqldal = SQLDAL.GetSingleton();
			sqldal.UpdateArea(area);
		}

		public void DeleteArea(int id) 
		{
			SQLDAL sqldal = SQLDAL.GetSingleton();
			sqldal.DeleteArea(id);
		}
	}
}
