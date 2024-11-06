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
		private Route routeId;
		private List<Question> questions = new List<Question>();

        public Game(int _id, Route _routeId)
        {
            id = _id;
            routeId = _routeId;
        }

        public int GetId() => id;

        public void CreateGame(Game game)
        {
            SQLDAL sqlDal = SQLDAL.GetSingleton();
            sqlDal.CreateGame();
        }

        public void UpdateGame(Game game)
        {
            SQLDAL sqlDal = SQLDAL.GetSingleton();
            sqlDal.UpdateGame();
        }

        public void DeleteGame(int id)
        {
            SQLDAL sqlDal = SQLDAL.GetSingleton();
            sqlDal.DeleteGame();
        }

        public List<Game> GetAllGames(Game game)
        {
            SQLDAL sqlDal = SQLDAL.GetSingleton();
            return sqlDal.GetAllGames();
        }

        public void GetQuestions(Question question)
        {

        }

        public void PlayGame(Game game)
        {

        }
    }
}
