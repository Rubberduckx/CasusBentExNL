using ConsoleAppBENTExNL.DAL;
using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Linq;
using System.Security;
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

        public void AddObservartion(Observation observation)
        {
            observations.Add(observation);
        }

        public bool IsAgeValid(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Now.AddYears(-age)) age--;
            return age >= 14;
        }

        public bool IsPasswordValid(string password)
        {
            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsDigit);
        }

        public string GetSecurePassword()
        {
            StringBuilder password = new StringBuilder();

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return password.ToString();
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    // Verwijder het laatste karakter uit de StringBuilder
                    password.Remove(password.Length - 1, 1);

                    // Verwijder het laatste karakter uit de console
                    Console.Write("\b \b");
                }
                else
                {
                    // Voeg het getypte karakter toe aan de StringBuilder
                    password.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
        }
    }
}
