﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Models
{
	internal class PointOfInterest
	{
		//this is also a comments
		private int id;
		private string name;
		private string description;
		private string image;

        public PointOfInterest(int id, string name, string description, string image)
        {
			this.id = id;
			this.name = name;
			this.description = description;
			this.image = image;
		}

        public PointOfInterest CreatePOI()
		{
			throw new NotImplementedException();
		}

		public static PointOfInterest GetPOI(int id)
		{
			throw new NotImplementedException();
		}

		public void DeletePOI(PointOfInterest poi)
		{
			throw new NotImplementedException();
		}

		public void UpdatePOI(PointOfInterest poi)
		{
			throw new NotImplementedException();
		}
	}
}
