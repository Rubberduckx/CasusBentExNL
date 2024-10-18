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
		// private Question questionId;

		public UserQuest(int _id, bool _isCorrect, string _givenAnswer, User _userId /*, Question _questionId*/)
		{
			id = _id;
			isCorrect = _isCorrect;
			givenAnswer = _givenAnswer;
			userId = _userId;
			//_questionId = questionId
		}

		public void CreateUserQuest()
		{

		}

		public void SerCorrectStatus()
		{

		}
	}
}
