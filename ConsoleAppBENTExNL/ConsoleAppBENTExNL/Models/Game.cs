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

		public void PlayGame()
		{
			Console.WriteLine($"Spel {id} is gestart!");

			foreach (var question in questions)
			{
				Console.WriteLine(question.GetQuestionText()); // Toon de vraag
				Console.Write("Geef je antwoord: ");
				string userAnswer = Console.ReadLine(); // Vraag om input van de gebruiker

				// Controleer of het antwoord correct is
				if (userAnswer.Equals(question.GetCorrectAnswer(), StringComparison.OrdinalIgnoreCase))
				{
					Console.WriteLine("Correct! Je hebt toegang tot een nieuw routepunt.");
					// Hier zou je logica kunnen toevoegen voor toegang tot een nieuw routepunt.
				}
				else
				{
					Console.WriteLine($"Incorrect! Het juiste antwoord is: {question.GetCorrectAnswer()}");
				}
				Console.WriteLine(); // Extra regel voor netheid
			}
			Console.WriteLine("Game is afgelopen.");
		}
	}
}
