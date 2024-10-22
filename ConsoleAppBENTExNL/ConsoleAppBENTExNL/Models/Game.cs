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
		private Route routeId;
		private List<Question> questions = new List<Question>();

        public Game(int _id, Route _routeId)
        {
            id = _id;
            routeId = _routeId;
        }

        public void CreateGame(Game game)
        {

        }

        public void DeleteGame(Game game)
        {

        }

        public void UpdateGame(Game game)
        {

        }

        public void GetGame(Game game)
        {

        }

        public void GetQuestions(Question question)
        {

        }

        public void PlayGame(Game game)
        {

        }
    }
}
