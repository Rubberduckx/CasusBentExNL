﻿using System;
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
		private Area area;
		private List<RoutePoint> routePoints;
		private List<Game> games;

        public Route(int id, string name, Area area)
        {
            this.id = id;
			this.name = name;
			this.area = area;
        }

		public int GetId() => id;
		public string GetName() => name;
		public Area GetArea() => area;

        public void CreateRoute()
		{
			throw new NotImplementedException();
		}

		public void DeleteRoute()
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
