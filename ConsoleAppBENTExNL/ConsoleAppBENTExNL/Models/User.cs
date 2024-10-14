using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Models
{
	internal class User
	{
		private int id;
		private string name;
		private DateTime dateofBirth;
		private string email;
		private string password;
		private int level;
		private int xp;
		// private List<Observation> observations = new List<Observations>();
		private List<Role> roles = new List<Role>();
		private List<UserQuest> userquests = new List<UserQuest>();

		public User(int _id, string _name, DateTime _dateofBirth, string _email, string _password,
					int _level, int _xp)
		{
			_id = id;
			_name = name;
			_dateofBirth = dateofBirth;
			_email = email;
			_password = password;
			_level = level;
			_xp = xp;
		}

		public void CreateUser()
		{

		}

		public void DeleteUser()
		{

		}

		public void UpdateUser()
		{

		}

		public void GetUser()
		{

		}

		public void GetAnswers()
		{

		}

		public void CalculateLevel()
		{

		}

		public void CheckPemission()
		{

		}

		public void GiveAnswerQuestion()
		{

		}
	}
}
