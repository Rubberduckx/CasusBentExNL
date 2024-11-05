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

		public Game()
		{

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
