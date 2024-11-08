using ConsoleAppBENTExNL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Models
{
    internal class Answer
    {
        private int id;
        private string correctAnswer;
        private Question questionId;

        public Answer()
        {

        }

        public Answer(string _correctAnswer, Question _questionId)
        {
            correctAnswer = _correctAnswer;
            questionId = _questionId;
        }

        public Answer(int _id, string _correctAnswer, Question _questionId)
        {
            id = _id;
            correctAnswer = _correctAnswer;
            questionId = _questionId;
        }

		public int GetId() => id;
        public string GetCorrectAnswer() => correctAnswer;
        public Question GetQuestionId() => questionId;

		public void CreateAnswer(Answer answer)
        {
			SQLDAL sqldal = SQLDAL.GetSingleton();
			sqldal.CreateAnswer(answer);
		}

        public void DeleteAnswer(int id)
        {
			SQLDAL sqldal = SQLDAL.GetSingleton();
			sqldal.DeleteAnswer(id);
		}

		public List<Answer> GetAnswers()
        {
			SQLDAL sqldal = SQLDAL.GetSingleton();
			return sqldal.GetAnswers();
		}
    }
}
