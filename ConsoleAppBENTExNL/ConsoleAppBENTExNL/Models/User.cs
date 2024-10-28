using ConsoleAppBENTExNL.DAL;
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
					int _xpLevel, int _xp, Route _route)
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

        public Observation CreateObservation()
        {
            
        }

        // Properties voor accessen private variabelen

        public int GetId() => id;
        public void SetId(int value) => id = value;

        public string GetName() => name;
        public void SetName(string value) => name = value;

        public DateTime GetDateOfBirth() => dateofBirth;
        public void SetDateOfBirth(DateTime value) => dateofBirth = value;

        public string GetEmail() => email;
        public void SetEmail(string value) => email = value;

        public string GetPassword() => password;
        public void SetPassword(string value) => password = value;

        public int GetXpLevel() => xpLevel;
        public void SetXpLevel(int value) => xpLevel = value;

        public int GetXp() => xp;
        public void SetXp(int value) => xp = value;

        public Route GetRoute() => route;
        public void SetRoute(Route value) => route = value;


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
	}
}
