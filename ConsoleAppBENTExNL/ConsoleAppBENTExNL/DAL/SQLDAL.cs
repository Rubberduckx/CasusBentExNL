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
		public List<Game> games;
		public List<Role> roles;
        public List<Area> areas;
        public List<UserQuest> userQuests;
        public List<Observation> observations;
        public List<Question> question;
        public List<Answer> answers;
        public List<Species> speciesL;
        public List<Route> routes;

        // Deze code implementeert een singleton-patroon voor de SQLDAL-klasse.  
        // De SQLDAL-klasse is verantwoordelijk voor databasebewerkingen met betrekking tot gebruikers,
        // rollen, routes, points of interest en observaties.

        // Het singleton-patroon zorgt ervoor dat er slechts één instantie van de SQLDAL-klasse wordt gemaakt en gebruikt
        // in de hele applicatie.  

        private static readonly SQLDAL _singleton = new SQLDAL();


        public static SQLDAL GetSingleton()
        {
            return _singleton;
        }

        private SQLDAL()
        {
            users = new List<User>();
            roles = new List<Role>();
            areas = new List<Area>();
            userQuests = new List<UserQuest>();
            observations = new List<Observation>();
            question = new List<Question>();
            answers = new List<Answer>();
            speciesL = new List<Species>();
            games = new List<Game>();
            routes = new List<Route>();

            //connectionString
            connectionString = "**";

            // Create a new SqlConnection object
            connection = new SqlConnection(connectionString);
        }


        // NOTE: USER RELATED METHODS
        /*  ==================== Get all users from the database ==================== */
        public List<User> GetUser()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                users.Clear();
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
                    int? routeId = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7);
                    Route route = routeId.HasValue ? GetRoute(routeId.Value) : null;

                    users.Add(new User(id, name, dateofBirth, email, password, xpLevel, xp, route));
                }
                
            }
            connection.Close();

            return users;
        }

        public User GetUserById(int id)
        {
            User user = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM [User] WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            DateTime dateOfBirth = reader.GetDateTime(2);
                            string email = reader.GetString(3);
                            string password = reader.GetString(4);
                            int xpLevel = reader.GetInt32(5);
                            int xp = reader.GetInt32(6);
                            Route route = reader.IsDBNull(7) ? null : GetRoute(reader.GetInt32(7));

                            user = new User(userId, name, dateOfBirth, email, password, xpLevel, xp, route);
                        }
                    }
                }
            }
            return user;
        }

        /*  ==================== Create a user in the database ==================== */
        public void CreateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
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

                var route = user.GetRoute();
                if (route != null)
                {
                    command.Parameters.AddWithValue("@routeId", route.GetId());
                }
                else
                {
                    command.Parameters.AddWithValue("@routeId", DBNull.Value);
                }
                command.ExecuteNonQuery();
            }
        }


		/*  ==================== Update a user in the database ==================== */
		public void UpdateUser(User user) 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
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

                if (user.GetRoute() != null)
                {
                    command.Parameters.AddWithValue("@routeId", user.GetRoute().GetId());
                }
                else
                {
                    command.Parameters.AddWithValue("@routeId", DBNull.Value);
                }

                command.ExecuteNonQuery();
            }
        }


		/*  ==================== Delete a user in the database ==================== */
		public void DeleteUser(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM [User] WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        // NOTE: AREA RELATED METHODS
		/*  ==================== Get a single area from the database where id is given id ==================== */
        public Area GetArea(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Area WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int areaId = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    double lat = reader.GetDouble(2);
                    double lng = reader.GetDouble(3);
                    string image = reader.GetString(4);
                    string description = reader.GetString(5);

                    return new Area(areaId, name, lat, lng, image, description);
                }

                throw new ArgumentException("No Area found at given id.", id.ToString());
            }
        }

        /*  ==================== Get a all areas from the database ==================== */
        public List<Area> GetAllAreas()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                areas.Clear();

                SqlCommand command = new SqlCommand("SELECT * FROM [Area]", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Area area = new Area(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2), reader.GetDouble(3),
                        reader.GetString(4), reader.GetString(5));

                    areas.Add(area);
                }

                return areas;
            }
        }


        /*  ==================== Create a area in the database ==================== */
        public void CreateArea(Area area)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Area (name, lat, long, image, description) VALUES " +
                    "(@name, @lat, @long, @image, @description)", connection);

                command.Parameters.AddWithValue("@name", area.GetName());
                command.Parameters.AddWithValue("@lat", area.GetLat());
                command.Parameters.AddWithValue("@long", area.GetLng());
                command.Parameters.AddWithValue("@image", area.GetImage());
                command.Parameters.AddWithValue("@description", area.GetDescription());

                command.ExecuteNonQuery();
            }
        }


		/*  ==================== Delete a area in the database ==================== */
		public void DeleteArea(int id)
		{
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM [Area] WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                
            }
		}


		/*  ==================== Update a area in the database ==================== */
		public void UpdateArea(Area area)
		{
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Update [Area] SET name = @name, lat = @lat, long = @long, image = @image, description = @description ", connection);

                command.Parameters.AddWithValue("@name", area.GetName());
                command.Parameters.AddWithValue("@lat", area.GetLat());
                command.Parameters.AddWithValue("@long", area.GetLng());
                command.Parameters.AddWithValue("@image", area.GetImage());
                command.Parameters.AddWithValue("@description", area.GetLat());

                command.ExecuteNonQuery();
            }
		}

        // NOTE: OBSERVATION RELATED METHODS
		/*  ==================== Get a single Observation from the database where id is given id ==================== */
		public Observation GetObservation(int id)
		{
            using (SqlConnection connection = new SqlConnection(connectionString))
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

                return observation;
            }
		}

        public List<Observation> GetAllObservations()
        {
            observations.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Observation", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            double lat = reader.GetDouble(1);
                            double lng = reader.GetDouble(2);
                            string image = reader.GetString(3);
                            string description = reader.GetString(4);
                            Species species = reader.IsDBNull(5) ? null : GetSpecies(reader.GetInt32(5));
                            // Hier nog even na kijken of dat mooier kan
                            User user = reader.IsDBNull(6) ? null : GetUser().FirstOrDefault(u => u.GetId() == reader.GetInt32(6));
                            Area area = reader.IsDBNull(7) ? null : GetArea(reader.GetInt32(7));

                            observations.Add(new Observation(id, lat, lng, image, description, species, user, area));
                        }
                    }
                }
                
            }
            return observations;
        }

        public List<Observation> GetObservationsByUserId(int userId)
        {
            observations.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Observation WHERE UserId = @UserId", connection))
                {
                    // Voeg de parameter toe om de juiste UserId te filteren
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            double lat = reader.GetDouble(1);
                            double lng = reader.GetDouble(2);
                            string image = reader.GetString(3);
                            string description = reader.GetString(4);
                            Species species = reader.IsDBNull(5) ? null : GetSpecies(reader.GetInt32(5));
                            //User user = reader.IsDBNull(6) ? null : GetUser().FirstOrDefault(u => u.GetId() == reader.GetInt32(6));
                            User user = reader.IsDBNull(6) ? null : GetUserById(reader.GetInt32(6));
                            Area area = reader.IsDBNull(7) ? null : GetArea(reader.GetInt32(7));

                            observations.Add(new Observation(id, lat, lng, image, description, species, user, area));
                        }
                    }
                }
            }
            return observations;
        }



        /*  ==================== Create an Observation in the database ==================== */
        public void CreateObservation(Observation observation)
		{
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Observation (lat, long, image, description, specieId, userId, areaId) "
                    + "VALUES (@lat, @long, @image, @description, @specieId, @userId, @areaId);", connection);

                command.Parameters.AddWithValue("@lat", observation.GetLat());
                command.Parameters.AddWithValue("@long", observation.GetLong());
                command.Parameters.AddWithValue("@image", observation.GetImage());
                command.Parameters.AddWithValue("@description", observation.GetDescription());
                command.Parameters.AddWithValue("@specieId", observation.GetSpecieId().GetId());// Specie is verplicht
                command.Parameters.AddWithValue("@userId", observation.GetUserId().GetId());  // User is verplicht
                command.Parameters.AddWithValue("@areaId", observation.GetAreaId()?.GetId() ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
		}


		/*  ==================== Update an Observation in the database ==================== */
		public void UpdateObservation(Observation observation)
		{
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Observation SET lat = @lat, long = @long, image = @image, description = @description, " +
                    "specieId = @specieId, userId = @userId, areaId = @areaId WHERE id = @id", connection);

                command.Parameters.AddWithValue("@id", observation.GetId());
                command.Parameters.AddWithValue("@lat", observation.GetLat());
                command.Parameters.AddWithValue("@long", observation.GetLong());
                command.Parameters.AddWithValue("@image", observation.GetImage());
                command.Parameters.AddWithValue("@description", observation.GetDescription());
                command.Parameters.AddWithValue("@specieId", observation.GetSpecieId().GetId());
                command.Parameters.AddWithValue("@userId", observation.GetUserId().GetId());
                command.Parameters.AddWithValue("@areaId", observation.GetAreaId()?.GetId() ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
                
		}


		/*  ==================== Delete an Observation from the database ==================== */
		public void DeleteObservation(int id)
		{
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM [Observation] WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
		}

        // NOTE: SPECIE RELATED METHODS
		/* ==================== Get a single Species from the database where id is given ==================== */
		public Species GetSpecies(int id)
		{
            using (SqlConnection connection = new SqlConnection(connectionString))
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

                return species;
            }
		}

        public List<Species> GetAllSpecies()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                speciesL.Clear();
                SqlCommand command = new SqlCommand("SELECT * FROM Species", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int speciesId = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string type = reader.GetString(2);
                    string description = reader.GetString(3);
                    int size = reader.GetInt32(4);

                    speciesL.Add(new Species(speciesId, name, type, description, size));
                }

                return speciesL;
            }
        }


		/* ==================== Create a Species in the database ==================== */
		public void CreateSpecies(Species species)
		{
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Species (name, type, description, size) " +
                    "VALUES (@name, @type, @description, @size)", connection);

                command.Parameters.AddWithValue("@name", species.GetName());
                command.Parameters.AddWithValue("@type", species.GetType());
                command.Parameters.AddWithValue("@description", species.GetDescription());
                command.Parameters.AddWithValue("@size", species.GetSize());

                command.ExecuteNonQuery();
            }
                
		}

		/* ==================== Update a Species in the database ==================== */
		public void UpdateSpecies(Species species)
		{
            using (SqlConnection connection = new SqlConnection(connectionString))
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
            }
		}

		/* ==================== Delete a Species from the database ==================== */
		public void DeleteSpecies(int id)
		{
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM [Species] WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
		}

        // NOTE: POI RELATED METHODS
		/*  ==================== Get a single POI from the database ==================== */
		public PointOfInterest GetPOI(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
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

        }

        // NOTE: ROUTEPOINT RELATED METHODS
		/*  ==================== Get a single routepoint from the database where id is given id ==================== */
		public RoutePoint GetRoutePoint(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
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
        }

        // NOTE: ROLE RELATED METHODS
        /*  ==================== Get all roles from the database ==================== */
        public List<Role> GetAllRoles()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
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

                

                return roles;
            }
                
        }


        /*  ==================== Get a single role from the database where id is given id ==================== */
        public Role GetRoleById(int id)
        {
            Role role = null;

            // Using a local connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Role WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string type = reader.GetString(1);
                    string description = reader.GetString(2);
                    string permissions = reader.GetString(3);
                    role = new Role(id, type, description, permissions);
                }
            }
         
            return role;
        }


        /*  ==================== Create a role ==================== */
        public void CreateRole(Role role)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO [Role] (type, description, permission) " +
                    "VALUES (@type, @description, @permission)", connection);

                command.Parameters.AddWithValue("@type", role.GetTypeRole());
                command.Parameters.AddWithValue("@description", role.GetDescription());
                command.Parameters.AddWithValue("@permission", role.GetPermission());

                command.ExecuteNonQuery();

            }
                
        }


		/*  ==================== Delete a role ==================== */
		public void DeleteRole(int id) 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM [Role] WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }


		/*  ==================== Update a role ==================== */
		public void UpdateRole(Role role)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("UPDATE [Role] SET type = @type, description = @description, " +
                    "permission = @permission WHERE id = @id", connection);

                command.Parameters.AddWithValue("@id", role.GetId());
                command.Parameters.AddWithValue("@type", role.GetTypeRole());
                command.Parameters.AddWithValue("@description", role.GetDescription());
                command.Parameters.AddWithValue("@permission", role.GetPermission());

                command.ExecuteNonQuery();
                
            }
                
        }

        /*  ==================== Delete a awnser ==================== */
		public void DeleteAnswer(int id)
		{
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM [Answer] WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                
            }
                
		}

        // NOTE: LOGIN RELATED METHODS
        public User GetUserByUsernameAndPassword(string name, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(
                    "SELECT * FROM [User] WHERE name = @name AND Password = @password",
                    connection
                );
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@password", password);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User(
                            reader.GetInt32(0),    // id
                            reader.GetString(1),    // name
                            reader.GetDateTime(2),  // dateofBirth
                            reader.GetString(3),    // email
                            reader.GetString(4),    // password
                            reader.GetInt32(5),     // xpLevel
                            reader.GetInt32(6),     // xp
                            null                    // route
                        );
                    }

                    return null;
                }
            }
        }

        // FIXME: USERROLE RELATED METHODS
        /*  ==================== AddUserRole ==================== */
        public void AddUserRole(int userId, int roleId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO UserRole (userId, roleId) VALUES (@userId, @roleId)", connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@roleId", roleId);
                command.ExecuteNonQuery();
            
            }
        }

        /*  ==================== RemoveUserRole ==================== */
        public void RemoveUserRole(int userId, int roleId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM UserRole WHERE userId = @userId AND roleId = @roleId", connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@roleId", roleId);
                command.ExecuteNonQuery();
            }
        }

        /*  ==================== GetUserRoles ==================== */
        public List<Role> GetUserRoles(int userId)
        {
            List<Role> roles = new List<Role>();

            // Using a local connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM UserRole WHERE userId = @userId", connection);
                command.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int roleId = reader.GetInt32(2);
                    roles.Add(GetRoleById(roleId)); // Fetch each role independently
                }
            }
            return roles;
        }

        /*  ==================== CheckUserPermissions==================== */
        public bool CheckUserPermissions(int userId, string type)
        {
            List<Role> roles = GetUserRoles(userId);
            if (roles == null) return false;

            foreach (Role role in roles)
            {
                if (role != null && role.GetTypeRole().Contains(type))
                {
                    return true;
                }
            }
            return false;
        }


        // NOTE: AWNSER RELATED METHODS
        /*  ==================== Get all Answers from the database ==================== */
        public List<Answer> GetAnswers()
		{
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
			    answers.Clear();
                connection.Open();
			    SqlCommand command = new SqlCommand("SELECT * FROM Answer", connection);
			    SqlDataReader reader = command.ExecuteReader();

			    while (reader.Read())
			    {
				    int id = reader.GetInt32(0);
				    string correctAnswer = reader.GetString(1);
                    int questionId = reader.GetInt32(3);

                    Question question = GetQuestion(questionId);

				    answers.Add(new Answer(id, correctAnswer, question));
			    }
			    return answers;
            }
		}


		/*  ==================== Create a Answer ==================== */
		public void CreateAnswer(Answer answer)
		{
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
			    SqlCommand command = new SqlCommand("INSERT INTO Answer (correctAnswer, questionId) " +
				    "VALUES (@correctAnswer, @questionId)", connection);

			    command.Parameters.AddWithValue("@correctAnswer", answer.GetCorrectAnswer());
			    command.Parameters.AddWithValue("@questionId", answer.GetQuestionId().GetQuestionId());

			    command.ExecuteNonQuery();
            }
		}

        //NOTE: QUESTION RELATED METHODS
		/*  ==================== Get a single question from the database where id is given id ==================== */
		public Question GetQuestion(int id)
		{
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
			    SqlCommand command = new SqlCommand("SELECT * FROM Question WHERE id = @id", connection);
			    command.Parameters.AddWithValue("@id", id);
			    SqlDataReader reader = command.ExecuteReader();
			    while (reader.Read())
			    {
                    int idQ = reader.GetInt32(0);
                    string questionText = reader.GetString(1);
                    string questionType = reader.GetString(2);
                    Game gameId = GetGames()[reader.GetInt32(3)];
                    Question question = new Question(idQ, questionText, questionType, gameId);
				    return question;

			    }
			
			    throw new ArgumentException("No Area found at given id.", id.ToString());
            }
		}

        public List<Question> GetAllQuestions() 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                question.Clear();
                SqlCommand command = new SqlCommand("SELECT * FROM Question", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string questionText = reader.GetString(1);
                    string questionType = reader.GetString(2);
                    Game gameId = GetGameById(reader.GetInt32(3));
                    question.Add(new Question(id, questionText, questionType, gameId));
                }

                return question;
            }
        }


        /*  ==================== Create a question in the database ==================== */
        public void CreateQuestion(Question question)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Question (questionText, questionType, gameId) VALUES " +
                    "(@questionText, @questionType, @gameId)", connection);

                command.Parameters.AddWithValue("@questionText", question.GetQuestionText());
                command.Parameters.AddWithValue("@questionType", question.GetQuestionType());
                command.Parameters.AddWithValue("@gameId", question.GetGameId().getId());

                command.ExecuteNonQuery();
            }
        }
		


		/*  ==================== Delete a Question in the database ==================== */
		public void DeleteQuestion(int id)
		{
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
            connection.Open();
			SqlCommand command = new SqlCommand("DELETE FROM Question WHERE id = @id", connection);
			command.Parameters.AddWithValue("@id", id);
			command.ExecuteNonQuery();
            }
		}


		/*  ==================== Update a Question in the database ==================== */
		public void UpdateQuestion(Question question)
		{
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
            connection.Open();
			SqlCommand command = new SqlCommand("Update Question SET questionText = @questionText, questionType = @questionType, gameId = @gameId ", connection);

			command.Parameters.AddWithValue("@questionText", question.GetQuestionText());
			command.Parameters.AddWithValue("@questionType", question.GetQuestionType());
			command.Parameters.AddWithValue("@gameId", question.GetGameId().getId());

			command.ExecuteNonQuery();
            }
		}

        // NOTE: GAME RELATED METHODS
		/*  ==================== Get all Games from the database ==================== */
		public List<Game> GetGames()
		{
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                games.Clear();

				SqlCommand command = new SqlCommand("SELECT * FROM Game", connection);
				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int? routeId = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2);
                    Route route = routeId.HasValue ? GetRoute(routeId.Value) : null;

					games.Add(new Game(id, name, route));
				}
			    return games;
            }
        }

        public Game GetGameById(int id)
        {
            Game game = null;
            string query = "SELECT * FROM Game WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int gameId = reader.GetInt32(reader.GetOrdinal("Id"));
                        string name = reader.GetString(reader.GetOrdinal("Name"));
                        int routeId = reader.GetInt32(reader.GetOrdinal("RouteId"));
                        Route route = GetRoute(routeId);

                        game = new Game(gameId, name, route);
                    }
                }
                connection.Close();
            }
            return game;
        }

        public void CreateGame(Game game) 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Game (name, routeId) VALUES (@name, @routeId)", connection);

                command.Parameters.AddWithValue("@name", game.getName());
                command.Parameters.AddWithValue("@routeId", game.getRouteId()?.GetId() ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateGame(Game game)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Game SET name = @name, routeId = @routeId WHERE id = @id", connection);

                command.Parameters.AddWithValue("@id", game.getId());
                command.Parameters.AddWithValue("@name", game.getName());
                command.Parameters.AddWithValue("@routeId", game.getRouteId()?.GetId() ?? (object)DBNull.Value);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteGame(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Game WHERE id = @id", connection);

                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
        }

        // NOTE: ROUTE RELATED METHODS

        public void CreateRoute(Route route)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Route (name, areaId) VALUES (@name, @areaId)", connection);

                command.Parameters.AddWithValue("@name", route.GetName());
                command.Parameters.AddWithValue("@areaId", route.GetAreaId()?.GetId() ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateRoute(Route route)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Route SET name = @name, areaId = @areaId WHERE id = @id", connection);

                command.Parameters.AddWithValue("@id", route.GetId());
                command.Parameters.AddWithValue("@name", route.GetName());
                command.Parameters.AddWithValue("@areaId", route.GetAreaId().GetId());

                command.ExecuteNonQuery();
            }
        }

        public void DeleteRoute(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Route WHERE id = @id", connection);

                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
        }

        /*  ==================== Get a single route from the database where id is given id ==================== */
        public Route GetRoute(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Route WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int areaId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2); // Default to 0 or handle as needed
                    Area area = areaId > 0 ? GetArea(areaId) : null; // Only call GetArea if areaId is valid
                    Route route = new Route(id, reader.GetString(1), area);
                    return route;
                }
            }
            throw new ArgumentException("No Route found at given id.", id.ToString());
        }

        /*  ==================== Get all routes frm the database ==================== */
        public List<Route> GetAllRoutes()
        {
            List<Route> routes = new List<Route>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM Route", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            Area area = reader.IsDBNull(2) ? null : GetArea(reader.GetInt32(2));
                            Route route = new Route(id, name, area);
                            routes.Add(route);
                        }
                    }
                }
                return routes;
            }
        }
        /*  ==================== related to playing the Game ==================== */
        public List<Question> GetQuestionsByGameId(int gameId)
        {
            List<Question> questions = new List<Question>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Question WHERE GameId = @GameId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GameId", gameId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("Id"));
                            string questionText = reader.GetString(reader.GetOrdinal("QuestionText"));
                            string questionType = reader.GetString(reader.GetOrdinal("QuestionType"));
                            int gameIdFromDb = reader.GetInt32(reader.GetOrdinal("GameId"));

                            Game game = new Game(gameIdFromDb);
                            Question question = new Question(id, questionText, questionType, game);
                            questions.Add(question);
                        }
                    }
                }
            }

            return questions;
        }
        public string GetCorrectAnswerByQuestionId(int questionId)
        {
            string correctAnswer = string.Empty;
            using (SqlConnection connnetion = new SqlConnection(connectionString))
            {
                string query = "SELECT CorrectAnswer FROM Answer WHERE QuestionId = @QuestionId";
                SqlCommand cmd = new SqlCommand(query, connnetion);
                cmd.Parameters.AddWithValue("@QuestionId", questionId);
                connnetion.Open();
                // Voert het SQL-commando uit en haalt de eerste kolom van de eerste rij in de resultaatset op.
                // Als het resultaat niet null is, wordt het omgezet naar een string.
                correctAnswer = cmd.ExecuteScalar()?.ToString();
            }
            return correctAnswer;
        }

    }
}
