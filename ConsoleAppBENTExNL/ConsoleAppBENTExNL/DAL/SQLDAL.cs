using ConsoleAppBENTExNL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL.DAL
{
    internal class SQLDAL
    {
        public SqlConnection connection;
        public string connectionString;
        public List<User> users;

        public SQLDAL()
        {
            users = new List<User>();

            //connectionString
            connectionString = "";

            // Create a new SqlConnection object
            connection = new SqlConnection(connectionString);
        }

        public void GetUser()
        {
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM User", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                User user = new User(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetString(3), 
                    reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6));
                
                users.Add(user);
            }

            connection.Close();
        }

        public void CreateUser(User user)
        {
            connection.Open();
            // Create a new SqlCommand object
            SqlCommand command = new SqlCommand("INSERT INTO User (name, dateofBirth, email, password, level, xp) " +
                "VALUES (@name, @dateofBirth, @email, @password, @level, @xp)", connection);

            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@dateofBirth", user.DateOfBirth);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@level", user.xplevel);
            command.Parameters.AddWithValue("@xp", user.Xp);            

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void UpdateUser(User user) 
        {
            connection.Open();

            SqlCommand command = new SqlCommand("UPDATE User SET name = @name, dateofBirth = @dateofBirth, " +
                "email = @email, password = @password, level = @level, xp = @xp WHERE id = @id", connection);

            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@dateofBirth", user.DateOfBirth);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@level", user.xplevel);
            command.Parameters.AddWithValue("@xp", user.Xp);
            command.Parameters.AddWithValue("@id", user.Id);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteUser(int id)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM User WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            connection.Close();
        }


    }
}
