using ConsoleAppBENTExNL.Models;
using ConsoleAppBENTExNL.Pathfinding;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleAppBENTExNL.DAL
{
    internal class SQLDAL
    {
        public SqlConnection connection;
        public string connectionString;

        public List<User> users;
        public List<Role> roles;

        // Deze code implementeert een singleton-patroon voor de SQLDAL-klasse.  
        // De SQLDAL-klasse is verantwoordelijk voor databasebewerkingen met betrekking tot gebruikers,
        // rollen, routes, points of interest en observaties.

        // Het singleton-patroon zorgt ervoor dat er slechts één instantie van de SQLDAL-klasse wordt gemaakt en gebruikt
        // in de hele applicatie.  

        // Waarom is dit static?
        // Graag even uitleggen voor verdeging
        private static readonly SQLDAL _singleton = new SQLDAL();
        // Waarom is dit static?
        // Graag even uitleggen voor verdeging
        public static SQLDAL GetSingleton()
        {
            return _singleton;
        }

        private SQLDAL()
        {
            users = new List<User>();
            roles = new List<Role>();

            //connectionString
            connectionString = "*";

            // Create a new SqlConnection object
            connection = new SqlConnection(connectionString);
        }

        // USER
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
                Route route = GetRoute(reader.GetInt32(7));

                users.Add(new User(id, name, dateofBirth, email, password, xpLevel, xp, route));
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
            command.Parameters.AddWithValue("@routeId", user.GetRoute().GetId());

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
            command.Parameters.AddWithValue("@routeId", user.GetRoute().GetId());

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

        // POI
        public PointOfInterest GetPOI(int id) {
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM POI WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                PointOfInterest POI = new PointOfInterest(id, reader.GetString(1), reader.GetString(2), reader.GetString(3));
                return POI;
            }

            throw new ArgumentException("No PointOfInterest found at given id.", id.ToString());
        }

        // ROUTE

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

        //TODO: IMPLEMENT
        public Area GetArea(int id)
        {
            throw new NotImplementedException();
        }
        //TODO: IMPLEMENT
        public Species GetSpecies(int id)
        {
            throw new NotImplementedException();
        }

        // OBSERVATION
        public Observation CreateObservation(double lat, double lng, string image, string description, int speciesId, User user, int areaId)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Observation] (lat, long, image, description, speciesId, userId, areaId) " +
        "VALUES (@lat, @long, @image, @description, @speciesId, @userId, @areaId); " +
        "SELECT SCOPE_IDENTITY();", connection);

            command.Parameters.AddWithValue("@lat", lat);
            command.Parameters.AddWithValue("@long", lng);
            command.Parameters.AddWithValue("@image", image);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@speciesId", speciesId);
            command.Parameters.AddWithValue("@userId", user.GetId());
            command.Parameters.AddWithValue("@areaId", areaId);

            //TODO: Test if this works properly.
            int id = Convert.ToInt32(command.ExecuteScalar());

            connection.Close();

            Observation observation = new Observation(id, lat, lng, null, user, GetArea(areaId), image, description);
            return observation;
        }

        public Observation GetObservation(int id)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Observation WHERE id = @id", connection);
            SqlDataReader reader = command.ExecuteReader();

            Observation observation = null;

            while (reader.Read())
            {
                double lat = reader.GetDouble(1);
                double lng = reader.GetDouble(2);
                Species species = GetSpecies(reader.GetInt32(3));
                User user = GetUser()[reader.GetInt32(4)];
                Area area = GetArea(reader.GetInt32(5));
                string image = reader.GetString(6);
                string description = reader.GetString(7);
                observation = new Observation(id, lat, lng, species, user, area, image, description);
            }
            connection.Close();
            
            return observation;
        }
        public void DeleteObservation(int id)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM [Observation] WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        // ROLE

        public List<Role> GetAllRoles()
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Role", connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string type = reader.GetString(1);
                string description = reader.GetString(2);
                string permissions = reader.GetString(3);
                roles.Add(new Role(id, type, description, permissions));
            }

            connection.Close();

            return roles;
        }

        public Role GetRoleById(int id)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Role WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                id = reader.GetInt32(0);
                string type = reader.GetString(1);
                string description = reader.GetString(2);
                string permissions = reader.GetString(3);
                Role role = new Role(id, type, description, permissions);
                
                return role;
            }

            connection.Close();

            throw new ArgumentException("No Role found at given id.", id.ToString());
        }

        public void CreateRole(Role role)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Role] (type, description, permissions) " +
                "VALUES (@type, @description, @permissions)", connection);

            command.Parameters.AddWithValue("@type", role.GetTypeRole());
            command.Parameters.AddWithValue("@description", role.GetDescription());
            command.Parameters.AddWithValue("@permissions", role.GetPermission());

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void DeleteRole(int id) 
        {
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM [Role] WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateRole(Role role)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("UPDATE [Role] SET type = @type, description = @description, " +
                "permissions = @permissions WHERE id = @id", connection);

            command.Parameters.AddWithValue("@id", role.GetId());
            command.Parameters.AddWithValue("@type", role.GetTypeRole());
            command.Parameters.AddWithValue("@description", role.GetDescription());
            command.Parameters.AddWithValue("@permissions", role.GetPermission());

            command.ExecuteNonQuery();
            connection.Close();
        }

    }
}
