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
		// private List<Observation> observations = new List<Observations>();
		private List<Role> roles = new List<Role>();
		private List<UserQuest> userquests = new List<UserQuest>();

		public User(int _id, string _name, DateTime _dateofBirth, string _email, string _password,
					int _xplevel, int _xp)
		{
			_id = id;
			_name = name;
			_dateofBirth = dateofBirth;
			_email = email;
			_password = password;
			_xplevel = xpLevel;
			_xp = xp;
		}

        // Properties voor accessen private variabelen
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dateofBirth; }
            set { dateofBirth = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public int xplevel
        {
            get { return xpLevel; }
            set { xpLevel = value; }
        }

        public int Xp
        {
            get { return xp; }
            set { xp = value; }
        }

        public List<Role> Roles
        {
            get { return roles; }
            set { roles = value; }
        }

        public List<UserQuest> UserQuests
        {
            get { return userquests; }
            set { userquests = value; }
        }


        public void CreateUser(User user)
		{
            SQLDAL sqldal = new SQLDAL();
            sqldal.CreateUser(user);
        }

		public void DeleteUser(int id)
		{
            SQLDAL sqldal = new SQLDAL();
            sqldal.DeleteUser(id);
        }

		public void UpdateUser(User user)
		{
            SQLDAL sqldal = new SQLDAL();
            sqldal.UpdateUser(user);
        }

		public void GetUser()
		{
            SQLDAL sqldal = new SQLDAL();
            sqldal.GetUser();
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
