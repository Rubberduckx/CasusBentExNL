using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Models
{
	internal class RoutePoint
	{
		private int id;
		private string name;
		private double latitude;
		private double longitude;
		private List<PointOfInterest> pointsOfInterest;
		private List<Route> routes;

        public RoutePoint(int id, string name, double latitude, double longitude)
        {
            this.id = id;
			this.name = name;
			this.latitude = latitude;
			this.longitude = longitude;
        }

        public void CreateRoutePoint()
		{
			throw new NotImplementedException();
		}

		public void DeleteRoutePoint(RoutePoint routePoint)
		{
			throw new NotImplementedException();
		}

		public void UpdateRoutePoint(/* Arguments neccesary? */)
		{
			throw new NotImplementedException();
		}

		public static RoutePoint GetRoutePoint(int id)
		{
			throw new NotImplementedException();
		}

		public void GetPointOfInterest(int pointOfInterestId = 0)
		{
			throw new NotImplementedException();
		}

		public void AddPointOfInterest(PointOfInterest pointOfInterest)
		{
			pointsOfInterest.Add(pointOfInterest);
		}
	}
}
