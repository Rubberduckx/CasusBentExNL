using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Models
{
	internal class UserQuest
	{
		private int id;
		private bool isCorrect;
		private string givenAnswer;
		private User userId;
		private Question questionId;

		public UserQuest()
        {

        }

        public UserQuest(int _id, bool _isCorrect, string _givenAnswer, User _userId, Question _questionId)
		{
			id = _id;
			isCorrect = _isCorrect;
			givenAnswer = _givenAnswer;
			userId = _userId;
			questionId = _questionId;
		}

		public int GetId() => id;

		public bool IsCorrect() => isCorrect;
		public string GetGivenAnswer() => givenAnswer;
        public User GetUserId() => userId;
        public Question GetQuestionId() => questionId;

		public List<UserQuest> GetAllUserQuests()
		{
			return new List<UserQuest>();
		}

		public void GetUserQuestById()
		{

		}

        public void CreateUserQuest()
		{

		}

		public void UpdateUserQuest()
        {

        }

        public void DeleteUserQuest()
		{

		}

        public void SetCorrectStatus()
		{

		}
	}
}
