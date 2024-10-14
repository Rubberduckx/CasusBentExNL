using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Models
{
	internal class RoutePoint
	{
		private int _id;
		private string _name;
		private double latitude;
		private double longitude;
		private List<PointOfInterest> pointOfInterests;
		private List<Route> routes;

		public static RoutePoint CreateRoutePoint()
		{
			throw new NotImplementedException();
		}

		public static void DeleteRoutePoint(RoutePoint routePoint)
		{
			throw new NotImplementedException();
		}

		public static RoutePoint UpdateRoutePoint(/* Arguments neccesary? */)
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
	}
}
