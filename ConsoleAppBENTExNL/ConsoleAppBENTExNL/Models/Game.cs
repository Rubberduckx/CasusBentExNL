using ConsoleAppBENTExNL.DAL;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Models
{
	internal class Game
	{
		private int id;
		private string name;
		private Route routeId;
		private List<Question> questions = new List<Question>();

        public Game()
        {

        }

		public Game(int _id)
		{
			id = _id;
        }

		public Game(string _name)
		{
			name = _name;
		}

		public Game(string _name, Route _routeId)
        {
            name = _name;
            routeId = _routeId;
        }

        public Game(int _id, string _name, Route _routeId)
		{
			id = _id;
            name = _name;
            routeId = _routeId;
        }


		public int getId() => id;
        public string getName() => name;
        public Route getRouteId() => routeId;

        public void CreateGame(Game game)
        {
			SQLDAL sqldal = SQLDAL.GetSingleton();
            sqldal.CreateGame(game);
        }

        public void DeleteGame(int id)
        {
			SQLDAL sqldal = SQLDAL.GetSingleton();
            sqldal.DeleteGame(id);
        }

        public void UpdateGame(Game game)
        {
			SQLDAL sqldal = SQLDAL.GetSingleton();
            sqldal.UpdateGame(game);
        }

        public void GetGame(Game game)
        {

        }

		public List<Game> GetGames()
		{
			SQLDAL sqldal = SQLDAL.GetSingleton();
			return sqldal.GetGames();
		}

        public void GetQuestions(Question question)
        {

        }
	}
}
