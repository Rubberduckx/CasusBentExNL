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

        public Answer(int _id, string _correctAnswer, Question _questionId)
        {
            id = _id;
            correctAnswer = _correctAnswer;
            questionId = _questionId;
        }

        public void CreateAnswer(Answer answer)
        {

        }

        public void DeleteAnswer(Answer answer)
        {

        }

        public void UpdateAnswer(Answer answer)
        {

        }

        public void GetAnswer(Answer answer)
        {

        }
    }
}
