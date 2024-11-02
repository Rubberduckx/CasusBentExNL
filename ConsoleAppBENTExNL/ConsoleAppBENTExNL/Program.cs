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
            User loggedInUser = null;

            bool isRunning = true;

            while (isRunning)
            {
                if (loggedInUser == null)
                {
                    Console.Clear();
                    Console.WriteLine("1: Inloggen");
                    Console.WriteLine("2: Account aanmaken");
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
                                loggedInUser = user.GetUser().FirstOrDefault(u => u.GetName() == username);
                                if (loggedInUser != null)
                                {
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
                    Console.WriteLine("12: Uitloggen");
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

                            user.UpdateUser(userToUpdate);

                            Console.WriteLine($"Name: {userToUpdate.GetName()}, is succesvol aangepast");

                            Console.ReadKey();
                            break;

                        case "2":
                            Console.Clear();
                            Console.WriteLine("Gebruiker verwijderen");
                            Console.WriteLine();

                            foreach (User u in user.GetUser())
                            {
                                Console.WriteLine(u.GetId() + " " + u.GetName());
                            }

                            Console.WriteLine();
                            Console.WriteLine("Welk Id wilt u verwijderen?");
                            id = int.Parse(Console.ReadLine());

                            user.DeleteUser(id);

                            Console.WriteLine($"Id: {id}, is succesvol verwijderd");
                            Console.ReadLine();
                            break;

                        case "3":
                            Console.Clear();
                            Console.WriteLine("Gebruikers in de database");
                            Console.WriteLine();

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

                            areaToAdd.CreateArea(areaToAdd);

                            Console.WriteLine($"Name: {areaToAdd.GetName()}, is succesvol aangepast");
                            Console.ReadKey();
                            break;

                        case "5":
                            Console.Clear();
                            Console.WriteLine("Area aanpassen");
                            Console.WriteLine();

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

                            areaToEdit.UpdateArea(areaToEdit);

                            Console.WriteLine($"Name: {areaToEdit.GetName()}, is succesvol aangepast");
                            Console.ReadKey();
                            break;

                        case "6":
                            Console.Clear();
                            Console.WriteLine("Area verwijderen");
                            Console.WriteLine();

                            foreach (Area a in area.GetAllAreas())
                            {
                                Console.WriteLine(a.GetId() + " " + a.GetName());
                            }
                            Console.WriteLine();

                            Console.WriteLine("Voer het id in van de gebruiker die u wilt verwijderen");
                            int areaId1 = int.Parse(Console.ReadLine());

                            area.DeleteArea(areaId1);

                            Console.WriteLine($"Name: {area.GetName()} is verwijderd");
                            Console.ReadKey();
                            break;

                        case "7":
                            Console.Clear();
                            Console.WriteLine("Areas in de database");
                            Console.WriteLine();

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

                            roleToAdd.CreateRole(roleToAdd);

                            Console.WriteLine($"Type: {roleToAdd.GetTypeRole()}, is succesvol aangemaakt");
                            Console.ReadKey();
                            break;

                        case "9":
                            Console.Clear();
                            Console.WriteLine("Role aanpassen");
                            Console.WriteLine();

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

                            roleToEdit.UpdateRole(roleToEdit);

                            Console.WriteLine($"Type: {roleToEdit.GetTypeRole()}, is succesvol aangepast");
                            Console.ReadKey();

                            break;

                        case "10":
                            Console.Clear();
                            Console.WriteLine("Role verwijderen");
                            Console.WriteLine();

                            foreach (Role r in role.GetRoles())
                            {
                                Console.WriteLine(r.GetId() + " " + r.GetTypeRole());
                            }
                            Console.WriteLine();

                            Console.WriteLine("Voer het id in van de role die u wilt verwijderen");
                            int roleIdD = int.Parse(Console.ReadLine());

                            role.DeleteRole(roleIdD);

                            Console.WriteLine($"Type: {role.GetTypeRole()} is verwijderd");

                            Console.ReadKey();
                            break;

                        case "11":
                            Console.Clear();
                            Console.WriteLine("Roles in de database");
                            Console.WriteLine();

                            foreach (Role r in role.GetRoles())
                            {
                                Console.WriteLine(r.GetId() + " " + r.GetTypeRole());
                            }

                            Console.ReadLine();
                            break;

                        case "12":
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
