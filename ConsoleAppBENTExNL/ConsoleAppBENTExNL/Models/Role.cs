using ConsoleAppBENTExNL.DAL;
using System;
using System.Collections.Generic;
using System.Data;
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

		public Role()
        {

        }

		public Role(string _type, string _description, string _permission)
		{
            type = _type;
            description = _description;
            permission = _permission;
        }

        public Role(int _id, string _type, string _description, string _permission)
		{
			id = _id;
			type = _type;
			description = _description;
			permission = _permission;
		}

		public int GetId() => id;
        public string GetTypeRole() => type;
        public string GetDescription() => description;
        public string GetPermission() => permission;


        public void CreateRole(Role role)
		{
			SQLDAL sqldal = SQLDAL.GetSingleton();
            sqldal.CreateRole(role);
        }

		public void UpdateRole(Role role)
        {
            SQLDAL sqldal = SQLDAL.GetSingleton();
            sqldal.UpdateRole(role);
        }

        public void DeleteRole(int id)
		{
            SQLDAL sqldal = SQLDAL.GetSingleton();
            sqldal.DeleteRole(id);
        }
		public List<Role> GetRoles()
        {
            SQLDAL sqldal = SQLDAL.GetSingleton();
			return sqldal.GetAllRoles();
        }
        public void GetRoleById(int id)
		{
            SQLDAL sqldal = SQLDAL.GetSingleton();
			sqldal.GetRoleById(id);
        }

		public void AssignUser()
		{

		}
	}
}
