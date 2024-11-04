using ConsoleAppBENTExNL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Models
{
    internal class Question
    {
        private int id;
        private string questionText;
        private string questionType;
        private int gameId;
        private List<Answer> answers = new List<Answer>();
        private List<UserQuest> userQuests = new List<UserQuest>();

        public Question(int _id, string _questionText, string _questionType, int _gameId)
        {
            id = _id;
            questionText = _questionText;
            questionType = _questionType;
            gameId = _gameId;
        }

        public int GetQuestionId() => id;
        public string GetQuestionText() => questionText;
        public string GetQuestionType() => questionType;
        public int GetGameId() => gameId;


        public void CreateQuestion(Question question)
        {
			SQLDAL sqldal = SQLDAL.GetSingleton();
			sqldal.CreateQuestion(question);
		}

        public void DeleteQuestion(int id)
        {
			SQLDAL sqldal = SQLDAL.GetSingleton();
			sqldal.DeleteQuestion(id);
		}

        public void UpdateQuestion(Question question)
        {
			SQLDAL sqldal = SQLDAL.GetSingleton();
			sqldal.CreateQuestion(question);
		}

        public void GetQuestion(Question question)
        {
			SQLDAL sqldal = SQLDAL.GetSingleton();
			sqldal.CreateQuestion(question);
		}
    }
}
