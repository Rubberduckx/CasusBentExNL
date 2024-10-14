using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Models
{
	internal class Route
	{
		private int _id;
		private string _name;
		private Area _areaId;
		private List<RoutePoint> _routePoints;
		private List<Game> _games;

		public static Route CreateRoute()
		{
			throw new NotImplementedException();
		}

		public static void DeleteRoute(Route route)
		{
			throw new NotImplementedException();
		}

		public static Route GetRoute(int id)
		{
			throw new NotImplementedException();
		}

		public void CalculateRoute()
		{
			throw new NotImplementedException();
		}

		public void GetGames()
		{
			throw new NotImplementedException();
		}
	}
}
