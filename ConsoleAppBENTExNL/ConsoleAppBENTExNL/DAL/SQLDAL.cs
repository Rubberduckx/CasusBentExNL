﻿using ConsoleAppBENTExNL.Models;
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

		/*  ==================== Get all users from the database ==================== */
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


		/*  ==================== Create a user in the database ==================== */
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


		/*  ==================== Update a user in the database ==================== */
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


		/*  ==================== Delete a user in the database ==================== */
		public void DeleteUser(int id)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM [User] WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            connection.Close();
        }


		/*  ==================== Get a single area from the database where id is given id ==================== */
		public Area GetArea(int id)
		{
			connection.Open();

			SqlCommand command = new SqlCommand("SELECT * FROM Area WHERE id = @id", connection);
			command.Parameters.AddWithValue("@id", id);
			SqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				Area area = new Area(id, reader.GetDouble(1), reader.GetDouble(2), reader.GetString(3), reader.GetString(4));
				return area;
			}

			throw new ArgumentException("No Area found at given id.", id.ToString());
		}


		/*  ==================== Create a area in the database ==================== */
		public void CreateArea(Area area)
		{
			connection.Open();
			SqlCommand command = new SqlCommand("INSERT INTO [Area] (lat, lng, image, description )", connection);

			command.Parameters.AddWithValue("@lat", area.GetLat());
			command.Parameters.AddWithValue("@lng", area.GetLng());
			command.Parameters.AddWithValue("@image", area.GetImage());
			command.Parameters.AddWithValue("@description", area.GetLat());

			command.ExecuteNonQuery();

			connection.Close();
		}


		/*  ==================== Create a area in the database ==================== */
		public void DeleteArea(int id)
		{
			connection.Open();
			SqlCommand command = new SqlCommand("DELETE FROM [Area] WHERE id = @id", connection);
			command.Parameters.AddWithValue("@id", id);
			command.ExecuteNonQuery();
			connection.Close();
		}


		/*  ==================== Update a area in the database ==================== */
		public void UpdateArea(Area area)
		{
			connection.Open();
			SqlCommand command = new SqlCommand("Update [Area] SET lat = @lat, lng = @lng, image = @image, description = @description ", connection);

			command.Parameters.AddWithValue("@lat", area.GetLat());
			command.Parameters.AddWithValue("@lng", area.GetLng());
			command.Parameters.AddWithValue("@image", area.GetImage());
			command.Parameters.AddWithValue("@description", area.GetLat());

			command.ExecuteNonQuery();

			connection.Close();
		}


		/*  ==================== Get a single Observation from the database where id is given id ==================== */
		public Observation GetObservation(int id)
		{
			connection.Open();
			SqlCommand command = new SqlCommand("SELECT * FROM Observation WHERE id = @id", connection);
			command.Parameters.AddWithValue("@id", id);
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


		/*  ==================== Create an Observation in the database ==================== */
		public void CreateObservation(Observation observation)
		{
			connection.Open();
			SqlCommand command = new SqlCommand("INSERT INTO [Observation] (lat, lng, image, description, speciesId, userId, areaId) " +
				"VALUES (@lat, @lng, @image, @description, @speciesId, @userId, @areaId); " +
				"SELECT SCOPE_IDENTITY();", connection);

			command.Parameters.AddWithValue("@lat", observation.GetLat());
			command.Parameters.AddWithValue("@lng", observation.GetLong());
			command.Parameters.AddWithValue("@image", observation.GetImage());
			command.Parameters.AddWithValue("@description", observation.GetDescription());
			command.Parameters.AddWithValue("@speciesId", observation.GetSpecies());

			int id = Convert.ToInt32(command.ExecuteScalar());

			connection.Close();

		}


		/*  ==================== Update an Observation in the database ==================== */
		public void UpdateObservation(Observation observation)
		{
			connection.Open();
			SqlCommand command = new SqlCommand("UPDATE [Observation] SET lat = @lat, lng = @lng, image = @image, description = @description, " +
				"speciesId = @speciesId, userId = @userId, areaId = @areaId WHERE id = @id", connection);

			command.Parameters.AddWithValue("@id", observation.GetId());
			command.Parameters.AddWithValue("@lat", observation.GetLat());
			command.Parameters.AddWithValue("@lng", observation.GetLong());
			command.Parameters.AddWithValue("@image", observation.GetImage());
			command.Parameters.AddWithValue("@description", observation.GetDescription());
			command.Parameters.AddWithValue("@speciesId", observation.GetSpecies().GetId());
			command.Parameters.AddWithValue("@userId", observation.GetUser().GetId());
			command.Parameters.AddWithValue("@areaId", observation.GetArea().GetId());

			command.ExecuteNonQuery();
			connection.Close();
		}


		/*  ==================== Delete an Observation from the database ==================== */
		public void DeleteObservation(int id)
		{
			connection.Open();
			SqlCommand command = new SqlCommand("DELETE FROM [Observation] WHERE id = @id", connection);
			command.Parameters.AddWithValue("@id", id);
			command.ExecuteNonQuery();
			connection.Close();
		}


		/* ==================== Get a single Species from the database where id is given ==================== */
		public Species GetSpecies(int id)
		{
			connection.Open();
			SqlCommand command = new SqlCommand("SELECT * FROM Species WHERE id = @id", connection);
			command.Parameters.AddWithValue("@id", id);
			SqlDataReader reader = command.ExecuteReader();

			Species species = null;

			if (reader.Read())
			{
				int speciesId = reader.GetInt32(0);
				string name = reader.GetString(1);
				string type = reader.GetString(2);
				string description = reader.GetString(3);
				int size = reader.GetInt32(4);

				species = new Species(speciesId, name, type, description, size);
			}
			connection.Close();

			return species;
		}


		/* ==================== Create a Species in the database ==================== */
		public void CreateSpecies(Species species)
		{
			connection.Open();
			SqlCommand command = new SqlCommand("INSERT INTO [Species] (name, type, description, size) " +
				"VALUES (@name, @type, @description, @size); SELECT SCOPE_IDENTITY();", connection);

			command.Parameters.AddWithValue("@name", species.GetName());
			command.Parameters.AddWithValue("@type", species.GetType());
			command.Parameters.AddWithValue("@description", species.GetDescription());
			command.Parameters.AddWithValue("@size", species.GetSize());

			int id = Convert.ToInt32(command.ExecuteScalar());
			connection.Close();
		}

		/* ==================== Update a Species in the database ==================== */
		public void UpdateSpecies(Species species)
		{
			connection.Open();
			SqlCommand command = new SqlCommand("UPDATE [Species] SET name = @name, type = @type, description = @description, size = @size " +
				"WHERE id = @id", connection);

			command.Parameters.AddWithValue("@id", species.GetId());
			command.Parameters.AddWithValue("@name", species.GetName());
			command.Parameters.AddWithValue("@type", species.GetType());
			command.Parameters.AddWithValue("@description", species.GetDescription());
			command.Parameters.AddWithValue("@size", species.GetSize());

			command.ExecuteNonQuery();
			connection.Close();
		}

		/* ==================== Delete a Species from the database ==================== */
		public void DeleteSpecies(int id)
		{
			connection.Open();
			SqlCommand command = new SqlCommand("DELETE FROM [Species] WHERE id = @id", connection);
			command.Parameters.AddWithValue("@id", id);
			command.ExecuteNonQuery();
			connection.Close();
		}

		/*  ==================== Get a single POI from the database ==================== */
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


		/*  ==================== Get a single routepoint from the database where id is given id ==================== */
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


		/*  ==================== Get a single route from the database where id is given id ==================== */
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

        /*  ==================== Get all routes frm the database ==================== */
        public List<Route> GetAllRoutes(int id)
        {
            List<Route> routes = new List<Route>();
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Route", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Area area = GetArea(reader.GetInt32(2));
                Route route = new Route(id, reader.GetString(1), area);
                routes.Add(route);
            }

            return routes;
        }

        /* ==================== Create a Route in the database ==================== */
        public void CreateRoute(string name, int areaId)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Route (name, areaId) VALUES (@name, @areaId); SELECT SCOPE_IDENTITY();", connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@areaId", areaId);

            int newId = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
        }

        /* ==================== Update a Route in the database ==================== */
        public void UpdateRoute(int id, string name, int areaId)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("UPDATE Route SET name = @name, areaId = @areaId WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@areaId", areaId);

            command.ExecuteNonQuery();
            connection.Close();

        }

        /* ==================== Delete a Route from the database ==================== */
        public void DeleteRoute(int id)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM Route WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
            connection.Close();
        }

        /*  ==================== Get all roles from the database ==================== */
        public List<Role> GetAllRoles()
        {
            roles.Clear();
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


		/*  ==================== Get a single role from the database where id is given id ==================== */
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


		/*  ==================== Create a role ==================== */
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


		/*  ==================== Delete a role ==================== */
		public void DeleteRole(int id) 
        {
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM [Role] WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            connection.Close();
        }


		/*  ==================== Update a role ==================== */
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
