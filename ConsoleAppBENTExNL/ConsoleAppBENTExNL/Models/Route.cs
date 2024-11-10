using ConsoleAppBENTExNL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Models
{
	internal class Route
	{
		private int id;
		private string name;
		private Area areaId;
		private List<RoutePoint> routePoints;
		private List<Game> games;

        public Route()
		{

		}
        public Route(string name, Area areaId)
        {
            this.name = name;
            this.areaId = areaId;
        }

        public Route(int id, string name, Area areaId)
        {
            this.id = id;
			this.name = name;
			this.areaId = areaId;
        }

		public int GetId() => id;
		public string GetName() => name;
		public Area GetAreaId() => areaId;

		public List<Route> GetAllRoutes()
		{
			SQLDAL sqldal = SQLDAL.GetSingleton();
			return sqldal.GetAllRoutes();
        }

        public void CreateRoute(Route route)
		{
			SQLDAL sqldal = SQLDAL.GetSingleton();
            sqldal.CreateRoute(route);
        }

		public void DeleteRoute(int id)
		{
			SQLDAL sqldal = SQLDAL.GetSingleton();
            sqldal.DeleteRoute(id);
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
