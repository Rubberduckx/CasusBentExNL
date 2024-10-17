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

        public void CreateQuestion(Question question)
        {

        }

        public void DeleteQuestion(Question question)
        {

        }

        public void UpdateQuestion(Question question)
        {

        }

        public void GetQuestion(Question question)
        {

        }

        public string GetAnswers(string answer)
        {
            throw new NotImplementedException();
        }

        public string GetUserQuests(string userQuest)
        {
            throw new NotImplementedException();
        }

        public void CheckAnswer(Answer answer)
        {

        }
    }
}
