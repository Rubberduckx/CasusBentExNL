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
            // TEMPORARY CODE START
            // DijkstraTest();
            // TEMPORARY CODE END

            User user = new User();
            Area area = new Area();

            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("1: Gebruiker aanmaken");
                Console.WriteLine("2: Gebruiker aanpassen");
                Console.WriteLine("3: Gebruiker verwijderen");
                Console.WriteLine("4: Gebruikers ophalen");
                Console.WriteLine();
                Console.WriteLine("5: Area aanmaken");
                Console.WriteLine("6: Area aanpassen");
                Console.WriteLine("7: Area verwijderen");
                Console.WriteLine("8: Area ophalen");
                Console.WriteLine();
                Console.WriteLine("10: Exit application");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Gebruiker aanmaken");
                        Console.WriteLine();

                        Console.WriteLine("Voer een naam in");
                        string name = Console.ReadLine();

                        Console.WriteLine("Voer uw geboorte datum in");
                        DateTime dateofBirth = DateTime.Parse(Console.ReadLine());

                        Console.WriteLine("Voor uw email in");
                        string email = Console.ReadLine();

                        Console.WriteLine("Voer uw wachtwoord in");
                        string password = Console.ReadLine();

                        int xpLevel = 10;
                        int xp = 50;

                        User userToAdd = new User(name, dateofBirth, email, password, xpLevel, xp);

                        user.CreateUser(userToAdd);

                        Console.WriteLine($"Name: {user.GetName()}, is succesvol aangemaakt");
                        Console.ReadKey();
                        break;

                    case "2":
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
                        name = Console.ReadLine();

                        Console.WriteLine("Voer uw geboorte datum in");
                        dateofBirth = DateTime.Parse(Console.ReadLine());

                        Console.WriteLine("Voor uw email in");
                        email = Console.ReadLine();

                        Console.WriteLine("Voer uw wachtwoord in");
                        password = Console.ReadLine();

                        xpLevel = 10;
                        xp = 50;

                        User userToUpdate = new User(id, name, dateofBirth, email, password, xpLevel, xp);

                        user.UpdateUser(userToUpdate);

                        Console.WriteLine($"Name: {userToUpdate.GetName()}, is succesvol aangepast");
                        Console.ReadKey();
                        break;

                    case "3":
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

                    case "4":
                        Console.Clear();
                        Console.WriteLine("Gebruikers in de database");
                        Console.WriteLine();

                        foreach (User u in user.GetUser())
                        {
                            Console.WriteLine(u.GetId() + " " + u.GetName());
                        }

                        Console.ReadLine();
                        break;

                    case "5":
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

                    case "6":
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

                    case "7":
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

                    case "8":
                        Console.Clear();
                        Console.WriteLine("Areas in de database");
                        Console.WriteLine();

                        foreach (Area a in area.GetAllAreas())
                        {
                            Console.WriteLine(a.GetId() + " " + a.GetName());
                        }

                        Console.ReadLine();
                        break;

                    case "10":
                        isRunning = false;
                        break;



                    default:
                        Console.WriteLine("Foutieve invoer");
                        Console.ReadKey();
                        break;
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
