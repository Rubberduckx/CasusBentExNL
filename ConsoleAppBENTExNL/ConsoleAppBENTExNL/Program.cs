﻿using ConsoleAppBENTExNL.Models;
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
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("1: Gebruiker aanmaken");
                Console.WriteLine("2: Gebruiker aanpassen");
                Console.WriteLine("3: Gebruiker verwijderen");
                Console.WriteLine("4: Gebruikers ophalen");
                Console.WriteLine();
                Console.WriteLine("10: Exit application");

                string input = Console.ReadLine();

                if (input == "1")
                {
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
                    int routeId = 1;

                    User userToAdd = new User(name, dateofBirth, email, password, xpLevel, xp, routeId);

                    user.CreateUser(userToAdd);

                    Console.WriteLine("Gebruiker is aangemaakt");
                    Console.ReadKey();
                }

                else if (input == "2")
                {
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

                    Console.WriteLine("Voor uw email in");
                    string email = Console.ReadLine();

                    Console.WriteLine("Voer uw wachtwoord in");
                    string password = Console.ReadLine();

                    int xpLevel = 10;
                    int xp = 50;
                    int routeId = 1;

                    User userToUpdate = new User(id, name, dateofBirth, email, password, xpLevel, xp, routeId);

                    user.UpdateUser(userToUpdate);

                    Console.WriteLine($"Id: {userToUpdate.GetId()}, is succesvol aangepast");

                    Console.ReadKey();
                }

                else if (input == "3")
                {
                    Console.Clear();
                    Console.WriteLine("Gebruiker verwijderen");
                    Console.WriteLine();

                    foreach (User u in user.GetUser())
                    {
                        Console.WriteLine(u.GetId() + " " + u.GetName());
                    }

                    Console.WriteLine();
                    Console.WriteLine("Welk Id wilt u verwijderen?");
                    int id = int.Parse(Console.ReadLine());

                    user.DeleteUser(id);

                    Console.WriteLine($"Id: {id}, is succesvol verwijderd");

                    Console.ReadLine();
                }

                else if (input == "4")
                {
                    Console.Clear();
                    Console.WriteLine("Gebruikers in de database");

                    Console.WriteLine();

                    foreach (User u in user.GetUser())
                    {
                        Console.WriteLine(u.GetId() + " "+u.GetName());
                    }

                    Console.ReadLine();
                }

                else if (input == "10")
                {
                    isRunning = false;
                }

                else
                {
                    Console.WriteLine("Foutieve invoer");
                    Console.ReadKey();
                }
            }
        }
    }
}
