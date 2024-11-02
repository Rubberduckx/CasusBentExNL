using ConsoleAppBENTExNL.DAL;
using System;
using System.Collections.Generic;
using System.Data.Sql;
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
		private int xpLevel;
		private int xp;
        private Route route;
		private List<Observation> observations = new List<Observation>();
		private List<Role> roles = new List<Role>();
		private List<UserQuest> userquests = new List<UserQuest>();

		public User()
        {

        }

        public User(string _name, DateTime _dateofBirth, string _email, string _password,
                    int _xpLevel, int _xp, Route _route = null)
        {
            name = _name;
            dateofBirth = _dateofBirth;
            email = _email;
            password = _password;
            xpLevel = _xpLevel;
            xp = _xp;
            route = _route;
        }

        public User(int _id, string _name, DateTime _dateofBirth, string _email, string _password,
					int _xpLevel, int _xp, Route _route = null)
		{
			id = _id;
			name = _name;
			dateofBirth = _dateofBirth;
			email = _email;
			password = _password;
			xpLevel = _xpLevel;
			xp = _xp;
            route = _route;
        }

        // Properties voor accessen private variabelen

        public int GetId() => id;

        public string GetName() => name;

        public DateTime GetDateOfBirth() => dateofBirth;

        public string GetEmail() => email;

        public string GetPassword() => password;
 
        public int GetXpLevel() => xpLevel;

        public int GetXp() => xp;

        public Route GetRoute() => route;


        public void CreateUser(User user)
		{
            SQLDAL sqldal = SQLDAL.GetSingleton();
            sqldal.CreateUser(user);
        }

		public void DeleteUser(int id)
		{
            SQLDAL sqldal = SQLDAL.GetSingleton();
            sqldal.DeleteUser(id);
        }

		public void UpdateUser(User user)
		{
            SQLDAL sqldal = SQLDAL.GetSingleton();
            sqldal.UpdateUser(user);
        }

		public List<User> GetUser()
		{
            SQLDAL sqldal = SQLDAL.GetSingleton();
            return sqldal.GetUser();
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

        public bool ValidateUser(string name, string password)
        {
            SQLDAL sqldal = SQLDAL.GetSingleton();
            User user = sqldal.GetUserByUsernameAndPassword(name, password);
            return user != null;
        }
    }
}
