using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Models
{
	internal class PointOfInterest
	{
		//this is also a comments
		private int _id;
		private string _name;
		private string _description;
		private string _image;
		private RoutePoint _routePoint;

		public static PointOfInterest CreatePOI()
		{
			throw new NotImplementedException();
		}

		public static PointOfInterest GetPOI(int id)
		{
			throw new NotImplementedException();
		}

		public static void DeletePOI(PointOfInterest poi)
		{
			throw new NotImplementedException();
		}

		public static void UpdatePOI(PointOfInterest poi)
		{
			throw new NotImplementedException();
		}
	}
}
