using ConsoleAppBENTExNL.Models;
using ConsoleAppBENTExNL.Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            Area area = new Area();
            Role role = new Role();
            Observation observation = new Observation();
            Species species = new Species();

            User loggedInUser = null;
            Species FoundSpecies = null;

            bool isRunning = true;

            while (isRunning)
            {
                if (loggedInUser == null)
                {
                    Console.Clear();
                    Console.WriteLine("1: Inloggen");
                    Console.WriteLine("2: Account aanmaken");
                    Console.WriteLine();
                    Console.WriteLine("100: Exit application");

                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("Inloggen");
                            Console.WriteLine();

                            Console.WriteLine("Voer uw gebruikersnaam in");
                            string username = Console.ReadLine();

                            Console.WriteLine("Voer uw wachtwoord in");
                            string password = Console.ReadLine();

                            if (user.ValidateUser(username, password))
                            {
                                // Zoeken naar de gebruiker in de database
                                loggedInUser = user.GetUser().FirstOrDefault(u => u.GetName() == username);
                                if (loggedInUser != null)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine($"Welkom, {loggedInUser.GetName()}!");
                                    Console.WriteLine("Druk op een toets om door te gaan");
                                    Console.ReadKey();
                                    continue;
                                }
                            }

                            Console.WriteLine("Ongeldige gebruikersnaam of wachtwoord");
                            Console.WriteLine("Druk op een toets om opnieuw te proberen");
                            Console.ReadKey();

                            break;

                        case "2":
                            Console.Clear();
                            Console.WriteLine("Gebruiker aanmaken");
                            Console.WriteLine();

                            Console.WriteLine("Voer een naam in");
                            string name = Console.ReadLine();

                            Console.WriteLine("Voer uw geboorte datum in");
                            DateTime dateofBirth = DateTime.Parse(Console.ReadLine());

                            Console.WriteLine("Voer uw email in");
                            string email = Console.ReadLine();

                            Console.WriteLine("Voer uw wachtwoord in");
                            string passwordU = Console.ReadLine();

                            int xpLevel = 10;
                            int xp = 50;

                            User userToAdd = new User(name, dateofBirth, email, passwordU, xpLevel, xp);

                            // Opslaan van object in database
                            user.CreateUser(userToAdd);

                            Console.WriteLine($"Name: {userToAdd.GetName()}, is succesvol aangemaakt");
                            Console.ReadKey();
                            break;

                        case "100":
                            isRunning = false;
                            break;

                        default:
                            Console.WriteLine("Foutieve invoer");
                            Console.ReadKey();
                            break;
                    }
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine($"Ingelogd als: {loggedInUser.GetName()}");
                    Console.WriteLine();
                    Console.WriteLine("1: Gebruiker aanpassen");
                    Console.WriteLine("2: Gebruiker verwijderen");
                    Console.WriteLine("3: Gebruikers ophalen");
                    Console.WriteLine();
                    Console.WriteLine("4: Area aanmaken");
                    Console.WriteLine("5: Area aanpassen");
                    Console.WriteLine("6: Area verwijderen");
                    Console.WriteLine("7: Areas ophalen");
                    Console.WriteLine();
                    Console.WriteLine("8: Role aanmaken");
                    Console.WriteLine("9: Role aanpassen");
                    Console.WriteLine("10: Role verwijderen");
                    Console.WriteLine("11: Roles ophalen");
                    Console.WriteLine();
                    Console.WriteLine("12: Specie + Observation aanmaken");
                    Console.WriteLine("13: Observation verwijderen");
                    Console.WriteLine("15: Observations ophalen");
                    Console.WriteLine("17: Species ophalen");
                    Console.WriteLine();
                    Console.WriteLine("80: Dijkstra Algo");
                    Console.WriteLine();
                    Console.WriteLine("99: Uitloggen");
                    Console.WriteLine("100: Exit application");

                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("Gebruiker aanpassen");
                            Console.WriteLine();

                            foreach (User u in user.GetUser())
                            {
                                Console.WriteLine(u.GetId() + " " + u.GetName());
                            }

                            Console.WriteLine();
                            Console.WriteLine("Voer het id in van de gebruiker die u wilt aanpassen");
                            int id = int.Parse(Console.ReadLine());

                            Console.WriteLine("Voer een naam in");
                            string name = Console.ReadLine();

                            Console.WriteLine("Voer uw geboorte datum in");
                            DateTime dateofBirth = DateTime.Parse(Console.ReadLine());

                            Console.WriteLine("Voer uw email in");
                            string email = Console.ReadLine();

                            Console.WriteLine("Voer uw wachtwoord in");
                            string password = Console.ReadLine();

                            int xpLevel = 10;
                            int xp = 50;

                            User userToUpdate = new User(id, name, dateofBirth, email, password, xpLevel, xp);

                            // Updaten van user in de database
                            user.UpdateUser(userToUpdate);

                            Console.WriteLine($"Name: {userToUpdate.GetName()}, is succesvol aangepast");

                            Console.ReadKey();
                            break;

                        case "2":
                            Console.Clear();
                            Console.WriteLine("Gebruiker verwijderen");
                            Console.WriteLine();

                            // Gebruikers ophalen en weergeven die in de database staan
                            foreach (User u in user.GetUser())
                            {
                                Console.WriteLine(u.GetId() + " " + u.GetName());
                            }

                            Console.WriteLine();
                            Console.WriteLine("Welk Id wilt u verwijderen?");
                            id = int.Parse(Console.ReadLine());

                            // Verwijderen van de gebruiker op id
                            user.DeleteUser(id);

                            Console.WriteLine($"Id: {id}, is succesvol verwijderd");
                            Console.ReadLine();
                            break;

                        case "3":
                            Console.Clear();
                            Console.WriteLine("Gebruikers in de database");
                            Console.WriteLine();

                            // Gebruikers ophalen en weergeven die in de database staan
                            foreach (User u in user.GetUser())
                            {
                                Console.WriteLine(u.GetId() + " " + u.GetName());
                            }

                            Console.ReadLine();
                            break;

                        case "4":
                            Console.Clear();
                            Console.WriteLine("Area aanmaken");
                            Console.WriteLine();

                            Console.WriteLine("Voer een naam in");
                            string nameArea = Console.ReadLine();

                            Console.WriteLine("Voer een breedtegraad in");
                            double lat = double.Parse(Console.ReadLine());

                            Console.WriteLine("Voer een lengtegraad in");
                            double lon = double.Parse(Console.ReadLine());

                            Console.WriteLine("Voer een afbeelding in");
                            string image = Console.ReadLine();

                            Console.WriteLine("Voer een beschrijving in");
                            string description = Console.ReadLine();

                            Area areaToAdd = new Area(nameArea, lat, lon, image, description);

                            // Opslaan van object in database
                            areaToAdd.CreateArea(areaToAdd);

                            Console.WriteLine($"Name: {areaToAdd.GetName()}, is succesvol aangepast");
                            Console.ReadKey();
                            break;

                        case "5":
                            Console.Clear();
                            Console.WriteLine("Area aanpassen");
                            Console.WriteLine();

                            // Areas ophalen en weergeven die in de database staan
                            foreach (Area a in area.GetAllAreas())
                            {
                                Console.WriteLine(a.GetId() + " " + a.GetLat());
                            }
                            Console.WriteLine();

                            Console.WriteLine("Voer het id in van de gebruiker die u wilt aanpassen");
                            int areaId = int.Parse(Console.ReadLine());

                            Console.WriteLine("Voer een naam in");
                            string name1 = Console.ReadLine();

                            Console.WriteLine("Voer een breedtegraad in");
                            double lat1 = double.Parse(Console.ReadLine());

                            Console.WriteLine("Voer een lengtegraad in");
                            double lon1 = double.Parse(Console.ReadLine());

                            Console.WriteLine("Voer een afbeelding in");
                            string image1 = Console.ReadLine();

                            Console.WriteLine("Voer een beschrijving in");
                            string description1 = Console.ReadLine();

                            Area areaToEdit = new Area(areaId, name1, lat1, lon1, image1, description1);

                            // Updaten van area in de database
                            areaToEdit.UpdateArea(areaToEdit);

                            Console.WriteLine($"Name: {areaToEdit.GetName()}, is succesvol aangepast");
                            Console.ReadKey();
                            break;

                        case "6":
                            Console.Clear();
                            Console.WriteLine("Area verwijderen");
                            Console.WriteLine();

                            // Areas ophalen en weergeven die in de database staan
                            foreach (Area a in area.GetAllAreas())
                            {
                                Console.WriteLine(a.GetId() + " " + a.GetName());
                            }
                            Console.WriteLine();

                            Console.WriteLine("Voer het id in van de gebruiker die u wilt verwijderen");
                            int areaId1 = int.Parse(Console.ReadLine());

                            // Verwijderen van de area op id
                            area.DeleteArea(areaId1);

                            Console.WriteLine($"Name: {area.GetName()} is verwijderd");
                            Console.ReadKey();
                            break;

                        case "7":
                            Console.Clear();
                            Console.WriteLine("Areas in de database");
                            Console.WriteLine();

                            // Areas ophalen en weergeven die in de database staan
                            foreach (Area a in area.GetAllAreas())
                            {
                                Console.WriteLine(a.GetId() + " " + a.GetName());
                            }

                            Console.ReadLine();
                            break;

                        case "8":
                            Console.Clear();
                            Console.WriteLine("Role aanmaken");
                            Console.WriteLine();

                            Console.WriteLine("Voer een type in");
                            string typeC = Console.ReadLine();

                            Console.WriteLine("Voer een beschrijving in");
                            string descriptionRoleC = Console.ReadLine();

                            Console.WriteLine("Voer een permissie in");
                            string permissionC = Console.ReadLine();

                            Role roleToAdd = new Role(typeC, descriptionRoleC, permissionC);

                            // Opslaan van object in database
                            roleToAdd.CreateRole(roleToAdd);

                            Console.WriteLine($"Type: {roleToAdd.GetTypeRole()}, is succesvol aangemaakt");
                            Console.ReadKey();
                            break;

                        case "9":
                            Console.Clear();
                            Console.WriteLine("Role aanpassen");
                            Console.WriteLine();

                            // Roles ophalen en weergeven die in de database staan
                            foreach (Role r in role.GetRoles())
                            {
                                Console.WriteLine(r.GetId() + " " + r.GetTypeRole());
                            }
                            Console.WriteLine();

                            Console.WriteLine("Voer het id in van de role die u wilt aanpassen");
                            int roleIdE = int.Parse(Console.ReadLine());

                            Console.WriteLine("Voer een type in");
                            string typeE = Console.ReadLine();

                            Console.WriteLine("Voer een beschrijving in");
                            string descriptionRoleE = Console.ReadLine();

                            Console.WriteLine("Voer een permissie in");
                            string permissionE = Console.ReadLine();

                            Role roleToEdit = new Role(roleIdE, typeE, descriptionRoleE, permissionE);

                            // Updaten van role in de database
                            roleToEdit.UpdateRole(roleToEdit);

                            Console.WriteLine($"Type: {roleToEdit.GetTypeRole()}, is succesvol aangepast");
                            Console.ReadKey();

                            break;

                        case "10":
                            Console.Clear();
                            Console.WriteLine("Role verwijderen");
                            Console.WriteLine();

                            // Roles ophalen en weergeven die in de database staan
                            foreach (Role r in role.GetRoles())
                            {
                                Console.WriteLine(r.GetId() + " " + r.GetTypeRole());
                            }
                            Console.WriteLine();

                            Console.WriteLine("Voer het id in van de role die u wilt verwijderen");
                            int roleIdD = int.Parse(Console.ReadLine());

                            // Verwijderen van de role op id
                            role.DeleteRole(roleIdD);

                            Console.WriteLine($"Type: {role.GetTypeRole()} is verwijderd");

                            Console.ReadKey();
                            break;

                        case "11":
                            Console.Clear();
                            Console.WriteLine("Roles in de database");
                            Console.WriteLine();

                            // Roles ophalen en weergeven die in de database staan
                            foreach (Role r in role.GetRoles())
                            {
                                Console.WriteLine(r.GetId() + " " + r.GetTypeRole());
                            }

                            Console.ReadLine();
                            break;

                        case "12":
                            Console.Clear();
                            Console.WriteLine("Specie aanmaken");
                            Console.WriteLine();
                            Console.WriteLine("Voer een naam van de Specie in");
                            string nameS = Console.ReadLine();
                            Console.WriteLine("Voer een type van de Specie in");
                            string typeS = Console.ReadLine();
                            Console.WriteLine("Voer een beschrijving van de Specie in");
                            string descriptionS = Console.ReadLine();
                            Console.WriteLine("Voer een grootte (in CM) van de Specie in ");
                            int sizeS = Int32.Parse(Console.ReadLine());

                            Species speciesToAdd = new Species(nameS, typeS, descriptionS, sizeS);
                            // Opslaan van object in database
                            speciesToAdd.CreateSpecies(speciesToAdd);

                            // Zoeken naar de specie op naam in de database en dit object opslaan in een variable
                            FoundSpecies = species.GetSpeciesList().FirstOrDefault(s => s.GetName() == nameS);
                        
                            Console.Clear();

                            Console.WriteLine("Observation aanmaken");
                            Console.WriteLine();
                            Console.WriteLine("Geef een lattitude op:");
                            double latO = double.Parse(Console.ReadLine());
                            Console.WriteLine("Geef een longitude op:");
                            double lonO = double.Parse(Console.ReadLine());
                            Console.WriteLine("Geef een image op:");
                            string imageO = Console.ReadLine();
                            Console.WriteLine("Geef een beschrijving op:");
                            string descriptionO = Console.ReadLine();

                            Observation observationToAdd = new Observation(latO, lonO, imageO, descriptionO, 
                                FoundSpecies, loggedInUser);

                            // Lijsten vullen met objecten
                            loggedInUser.AddObservartion(observationToAdd);
                            FoundSpecies.AddObservation(observationToAdd);

                            // Opslaan van object in database
                            observationToAdd.CreateObservation(observationToAdd);

                            Console.WriteLine($"Observation {observationToAdd.GetDescription()} is succesvol aangemaakt");
                            Console.ReadKey();
                            break;

                        case "13":
                            Console.Clear();
                            Console.WriteLine("Observation verwijderen");
                            Console.WriteLine();

                            // Observations ophalen en weergeven die in de database staan
                            foreach (Observation o in observation.GetAllObservations())
                            {
                                Console.WriteLine(o.GetId() + " " + o.GetDescription());
                            }
                            Console.WriteLine();

                            Console.WriteLine("Voer het id in van de observation die u wilt verwijderen");
                            int observationId = int.Parse(Console.ReadLine());

                            // Verwijderen van de observation op id
                            observation.DeleteObservation(observationId);

                            Console.WriteLine($"Observation: {observation.GetDescription()} is verwijderd");

                            Console.ReadKey();

                            break;

                        case "15":
                            Console.Clear();
                            Console.WriteLine("Observations in de database");
                            Console.WriteLine();

                            // Observations ophalen en weergeven die in de database staan
                            foreach (Observation o in observation.GetAllObservations())
                            {
                                Console.WriteLine(o.GetId() + " " + o.GetDescription()+ " UserId: " + o.GetUserId().GetId());
                            }

                            Console.ReadLine();
                            break;

                        case "16":
                            Console.Clear();
                            Console.WriteLine("Species verwijderen");
                            Console.WriteLine();
                            // Species ophalen en weergeven die in de database staan
                            foreach (Species s in species.GetSpeciesList())
                            {
                                Console.WriteLine(s.GetId() + " " + s.GetName());
                            }

                            Console.WriteLine();
                            Console.WriteLine("Voer het id in dat verwijderd moet worden");

                            // Verwijderen van de specie op id
                            int speciesId = int.Parse(Console.ReadLine());

                            Console.WriteLine();
                            Console.WriteLine($"Specie met Id: {speciesId} is verwijderd");

                            species.DeleteSpecies(speciesId);
                            Console.ReadKey();
                            break;

                        case "17":
                            Console.Clear();
                            Console.WriteLine("Species in de database");
                            Console.WriteLine();
                            foreach (Species s in species.GetSpeciesList())
                            {
                                Console.WriteLine(s.GetId() + " " + s.GetName());
                            }
                            Console.ReadLine();
                            break;


                        case "80":
                            Console.Clear();
                            Dijkstra.ParkTestCase();
                            Console.ReadKey();
                            break;

                        case "99":
                            loggedInUser = null;
                            break;

                        case "100":
                            isRunning = false;
                            break;

                        default:
                            Console.WriteLine("Foutieve invoer");
                            Console.ReadKey();
                            break;
                    }
                }
            }
        }

        public static void DijkstraTest()
        {
            Graph graph = new Graph();
            Vertex a = new Vertex("A");
            Vertex b = new Vertex("B");
            Vertex c = new Vertex("C");
            Vertex d = new Vertex("D");
            Vertex e = new Vertex("E");

            // Add vertices to the graph
            graph.AddVertex(a);
            graph.AddVertex(b);
            graph.AddVertex(c);
            graph.AddVertex(d);
            graph.AddVertex(e);

            // Define edges with distances
            a.AddNeighbor(b, 6);
            a.AddNeighbor(d, 1);
            b.AddNeighbor(c, 5);
            b.AddNeighbor(d, 2);
            c.AddNeighbor(e, 5);
            d.AddNeighbor(e, 1);

            // Run Dijkstra's algorithm from A to E
            Stack<Vertex> path = Dijkstra.ShortestRoute(graph, a, e);

            // Print the path
            Console.WriteLine("Shortest path from A to E:");
            while (path.Count > 0)
            {
                Console.Write(path.Pop().name + (path.Count > 0 ? " -> " : "\n"));
            }

            Console.ReadKey();
        }
    }
}
