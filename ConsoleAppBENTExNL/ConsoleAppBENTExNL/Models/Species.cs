using ConsoleAppBENTExNL.DAL;
using System;
using System.Collections.Generic;

namespace ConsoleAppBENTExNL.Models
{
	internal class Species
	{
		private int id;
		private string name;
		private string type;
		private string description;
		private int size;
		private List<Observation> observations = new List<Observation>();

		public Species()
		{

		}

		public Species(string name, string type, string description, int size)
		{
			this.name = name;
            this.type = type;
            this.description = description;
            this.size = size;
        }

        public Species(int id, string name, string type, string description, int size)
		{
			this.id = id;
			this.name = name;
			this.type = type;
			this.description = description;
			this.size = size;
		}

		public int GetId() => id;
		public string GetName() => name;
		public string GetType() => type;
		public string GetDescription() => description;
		public int GetSize() => size;

		public void CreateSpecies(Species species)
		{
			SQLDAL sqldal = SQLDAL.GetSingleton();
			sqldal.CreateSpecies(species);
		}

		public List<Species> GetSpeciesList() 
		{
            SQLDAL sqldal = SQLDAL.GetSingleton();
            return sqldal.GetAllSpecies();
        }

		public void GetSpecies(int id)
		{
			SQLDAL sqldal = SQLDAL.GetSingleton();
			sqldal.GetSpecies(id);
		}

		public void UpdateSpecies(Species species)
		{
			SQLDAL sqldal = SQLDAL.GetSingleton();
			sqldal.UpdateSpecies(species);
		}

		public void DeleteSpecies(int id)
		{
			SQLDAL sqldal = SQLDAL.GetSingleton();
			sqldal.DeleteSpecies(id);
		}

		public void AddObservation(Observation observation)
        {
            observations.Add(observation);
        }
    }
}
