using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Models
{
	internal class Role
	{
		private int id;
		private string type;
		private string description;
		private string permission;
		private List<User> users = new List<User>();
		//.

		public Role(int _id, string _type, string _description, string _permission)
		{
			_id = id;
			_type = type;
			_description = description;
			_permission = permission;
		}

		public void CreateUser()
		{

		}

		public void DeleteUser()
		{

		}

		public void GetUser()
		{

		}

		public void AssignUser()
		{

		}
	}
}
