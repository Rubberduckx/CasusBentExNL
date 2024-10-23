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
            connectionString = "*";

            // Create a new SqlConnection object
            connection = new SqlConnection(connectionString);
        }
        public List<User> GetUser()
        {
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM [User]", connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                DateTime dateofBirth = reader.GetDateTime(2);
                string email = reader.GetString(3);
                string password = reader.GetString(4);
                int xpLevel = reader.GetInt32(5);
                int xp = reader.GetInt32(6);
                int routeId = reader.GetInt32(7);

                users.Add(new User(id, name, dateofBirth, email, password, xpLevel, xp, routeId));
            }
            connection.Close();
            return users;
        }

        public void CreateUser(User user)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [User] (name, dateofBirth, email, password, xlLevel, xp, routeId) " +
                "VALUES (@name, @dateofBirth, @email, @password, @xplevel, @xp, @routeId)", connection);

            command.Parameters.AddWithValue("@name", user.GetName());
            command.Parameters.AddWithValue("@dateofBirth", user.GetDateOfBirth());
            command.Parameters.AddWithValue("@email", user.GetEmail());
            command.Parameters.AddWithValue("@password", user.GetPassword());
            command.Parameters.AddWithValue("@xplevel", user.GetXpLevel());
            command.Parameters.AddWithValue("@xp", user.GetXp());
            command.Parameters.AddWithValue("@routeId", user.GetRouteId());

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void UpdateUser(User user) 
        {
            connection.Open();

            SqlCommand command = new SqlCommand("UPDATE [User] SET name = @name, dateofBirth = @dateofBirth, " +
                "email = @email, password = @password, " +
                "xlLevel = @xplevel, xp = @xp, routeId = @routeId WHERE id = @id", connection);

            command.Parameters.AddWithValue("@id", user.GetId());
            command.Parameters.AddWithValue("@name", user.GetName());
            command.Parameters.AddWithValue("@dateofBirth", user.GetDateOfBirth());
            command.Parameters.AddWithValue("@email", user.GetEmail());
            command.Parameters.AddWithValue("@password", user.GetPassword());
            command.Parameters.AddWithValue("@xplevel", user.GetXpLevel());
            command.Parameters.AddWithValue("@xp", user.GetXp());
            command.Parameters.AddWithValue("@routeId", user.GetRouteId());

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteUser(int id)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM [User] WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public PointOfInterest GetPOI(int id) {
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM POI WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                RoutePoint routePoint = GetRoutePoint(reader.GetInt32(4));
                PointOfInterest POI = new PointOfInterest(id, reader.GetString(1), reader.GetString(2), reader.GetString(3), routePoint);
                return POI;
            }

            throw new ArgumentException("No PointOfInterest found at given id.", id.ToString());
        }
        
        public RoutePoint GetRoutePoint(int id)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM RoutePoint WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                RoutePoint routePoint = new RoutePoint(id, reader.GetString(1), reader.GetFloat(2), reader.GetFloat(3));
                return routePoint;
            }

            throw new ArgumentException("No RoutePoint found at given id.", id.ToString());
        }

        public Route GetRoute(int id)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Route WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Area area = GetArea(reader.GetInt32(2));
                Route route = new Route(id, reader.GetString(1), area);
                return route;
            }

            throw new ArgumentException("No Route found at given id.", id.ToString());
        }

        public Area GetArea(int id)
        {
            throw new NotImplementedException();
        }

    }
}
