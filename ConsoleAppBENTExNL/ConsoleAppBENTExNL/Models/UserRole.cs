using ConsoleAppBENTExNL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.Models
{
    internal class UserRole
    {
        private int userId;
        private int roleId;

        public UserRole()
        {
        }

        public UserRole(int _userId, int _roleId)
        {
            userId = _userId;
            roleId = _roleId;
        }

        // Properties for accessing private variables
        public int GetUserId() => userId;
        public int GetRoleId() => roleId;

        // CRUD operations for UserRole
        public void AddUserRole(User user, Role role)
        {
            SQLDAL sqldal = SQLDAL.GetSingleton();
            sqldal.AddUserRole(user.GetId(), role.GetId());
        }

        public void RemoveUserRole(User user, Role role)
        {
            SQLDAL sqldal = SQLDAL.GetSingleton();
            sqldal.RemoveUserRole(user.GetId(), role.GetId());
        }

        public List<Role> GetUserRoles(int userId)
        {
            SQLDAL sqldal = SQLDAL.GetSingleton();
            return sqldal.GetUserRoles(userId);
        }

        // Permission check
        public bool CheckUserPermission(User user, string requiredPermission)
        {
            SQLDAL sqldal = SQLDAL.GetSingleton();
            List<Role> roles = sqldal.GetUserRoles(user.GetId());
            foreach (var role in roles)
            {
                if (role.GetPermission().Contains(requiredPermission))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
