using ConsoleAppBENTExNL.DAL;
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
		private Species specieId;
		private User userId;
		private Area areaId;

		public Observation()
		{

		}

        public Observation(double lat, double lng, string image, string description, Species specieId,
			 User userId)
        {
            this.lat = lat;
            this.lng = lng;
			this.image = image;
            this.description = description;
            this.specieId = specieId;
            this.userId = userId;
            //this.areaId = areaId;
        }

        public Observation(int id, double lat, double lng, string image, string description,
             Species specieId, User userId, Area areaId)
        {
			this.id = id;
            this.lat = lat;
            this.lng = lng;
            this.image = image;
            this.description = description;
            this.specieId = specieId;
            this.userId = userId;
            this.areaId = areaId;
        }

        public Observation(int id, double lat, double lng, Species specie, User user, Area area, 
			string image = "", string description = "")
		{
			this.id = id;
			this.lat = lat;
			this.lng = lng;
			this.specieId = specie;
			this.userId = user;
			this.areaId = area;
			this.image = image;
			this.description = description;
		}

		public int GetId() => id;
		public double GetLat() => lat;
		public double GetLong() => lng;
		public string GetImage() => image;
		public string GetDescription() => description;
		public Species GetSpecieId() => specieId;
		public User GetUserId() => userId;
		public Area GetAreaId() => areaId;

		public void CreateObservation(Observation observation)
		{
			SQLDAL sqldal = SQLDAL.GetSingleton();
			sqldal.CreateObservation(observation);
		}

		public List<Observation> GetAllObservations()
        {
            SQLDAL sqldal = SQLDAL.GetSingleton();
            return sqldal.GetAllObservations();
        }

        public void GetObservation(int id)
		{
			SQLDAL sqldal = SQLDAL.GetSingleton();
			sqldal.GetObservation(id);
		}

		public void UpdateObservation(Observation observation)
		{
			SQLDAL sqldal = SQLDAL.GetSingleton();
			sqldal.UpdateObservation(observation);
		}

		public void DeleteObservation(int id)
		{
			SQLDAL sqldal = SQLDAL.GetSingleton();
			sqldal.DeleteObservation(id);
		}
	}
}
