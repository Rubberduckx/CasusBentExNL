using ConsoleAppBENTExNL.DAL;
using ConsoleAppBENTExNL.Models;
using ConsoleAppBENTExNL.Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppBENTExNL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WelcomeSign welcome = new WelcomeSign();
            User user = new User();
            Area area = new Area();
            Role role = new Role();
            Observation observation = new Observation();
            Question question = new Question();
            Species species = new Species();
            Game game = new Game();
            Answer answer = new Answer();
            UserRole userRole = new UserRole();
            Route route = new Route();

            User loggedInUser = null;
            Species FoundSpecies = null;
            bool isRunning = true;

            SQLDAL sqlDAL = SQLDAL.GetSingleton(); // Singleton instance of SQLDAL

            while (isRunning)
            {
                if (loggedInUser == null)
                {
                    Console.Clear();
                    welcome.WelcomeScreen();
                    Console.WriteLine();
                    Console.WriteLine("1: Inloggen");
                    Console.WriteLine("2: Account aanmaken");
                    Console.WriteLine();
                    Console.WriteLine("100: Exit application");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            LoginUser(ref loggedInUser);
                            break;
                        case "2":
                            CreateNewUser();
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
                    Console.WriteLine($"Welkom {loggedInUser.GetName()}");
                    Console.WriteLine();

                    // Options available to all users
                    Console.WriteLine("12: Specie + Observation aanmaken");
                    Console.WriteLine();
                    Console.WriteLine("17: Species ophalen");
                    Console.WriteLine();
                    Console.WriteLine("18: Game spelen");
                    Console.WriteLine();
                    Console.WriteLine("91: Aangemaakte Observation aanpassen");
                    Console.WriteLine("54: Aangemaakte Observation verwijderen");
                    Console.WriteLine("55: Aangemaakte Observations ophalen");
                    Console.WriteLine();
                    Console.WriteLine("80: Dijkstra Algotritme");

                    // Options restricted to admin users only
                    if (sqlDAL.CheckUserPermissions(loggedInUser.GetId(), "Admin"))
                    {
                        Console.Clear();
                        Console.WriteLine($"Admin user: {loggedInUser.GetName()}");
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
                        Console.WriteLine("90: Observation aanpassen");
                        Console.WriteLine("15: Observations ophalen");
                        Console.WriteLine("16: Species verwijderen");
                        Console.WriteLine("17: Species ophalen");
                        Console.WriteLine();
                        Console.WriteLine("18: Game spelen");
                        Console.WriteLine("19: Game aanmaken");
                        Console.WriteLine("20: Game aanpassen");
                        Console.WriteLine("21: Game verwijderen");
                        Console.WriteLine("22: Game ophalen");
                        Console.WriteLine();
                        Console.WriteLine("23: Question aanmaken");
                        Console.WriteLine("24: Question aanpassen");
                        Console.WriteLine("25: Question verwijderen");
                        Console.WriteLine("26: Questions ophalen");
                        Console.WriteLine();
                        Console.WriteLine("27: Answer aanmaken");
                        Console.WriteLine("28: Answer verwijderen");
                        Console.WriteLine();
                        Console.WriteLine("30: Route aanmaken");
                        Console.WriteLine("31: Route verwijderen");
                        Console.WriteLine("32: Routes ophalen");
                        Console.WriteLine();
                        Console.WriteLine("40: User + Role toekennen");
                        Console.WriteLine("80: Dijkstra Algotritme");
                    }

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

                            // Wanneer een eindgebruik een verkeerde keuze maakt kan deze zo terug na het keuze menu
                            Console.WriteLine("0: Terug naar hoofdmenu? druk 0. Druk een andere toets om door te gaan");
                            string GebruikAanpassenToMain = Console.ReadLine();
                            
                            if (GebruikAanpassenToMain == "0")
                            {
                                break;
                            }

                            // Als de toets != 0 is dan gaat de gebruiker verder met het aanpassen van de gebruiker
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

                            int xpLevel = 0;
                            int xp = 0;

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
                                Console.WriteLine(a.GetId() + " " + a.GetName());
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
                            Console.WriteLine("0: Terug naar hoofdmenu? druk 0. Druk een andere toets om door te gaan");
                            string SpecieAanmakenToMain = Console.ReadLine();

                            if (SpecieAanmakenToMain == "0")
                            {
                                break;
                            }
                            Console.WriteLine();

                            Console.WriteLine("Voer een naam van de Specie in");
                            string nameS = Console.ReadLine();
                            while (string.IsNullOrWhiteSpace(nameS))
                            {
                                Console.WriteLine("Naam is verplicht. Probeer het opnieuw.");
                                nameS = RequestInput("Voer een naam in");
                            }

                            Console.WriteLine("Voer een type van de Specie in");
                            string typeS = Console.ReadLine();
                            while (string.IsNullOrWhiteSpace(typeS))
                            {
                                Console.WriteLine("type is verplicht. Probeer het opnieuw.");
                                typeS = RequestInput("Voer een type in");
                            }

                            Console.WriteLine("Voer een beschrijving van de Specie in");
                            string descriptionS = Console.ReadLine();
                            while (string.IsNullOrWhiteSpace(descriptionS))
                            {
                                Console.WriteLine("Beschrijving is verplicht. Probeer het opnieuw.");
                                descriptionS = RequestInput("Voer een type in");
                            }

                            Console.WriteLine("Voer een grootte (in CM) van de Specie in ");
                            int sizeS;
                            while (!int.TryParse(Console.ReadLine(), out sizeS) || sizeS <= 0)
                            {
                                Console.WriteLine("Grootte in (CM) is verplicht en moet een positief getal zijn. Probeer het opnieuw.");
                            }

                            Species speciesToAdd = new Species(nameS, typeS, descriptionS, sizeS);

                            // Opslaan van object in database
                            speciesToAdd.CreateSpecies(speciesToAdd);

                            // Zoeken naar de specie op naam in de database en dit object opslaan in een variable
                            FoundSpecies = species.GetSpeciesList().FirstOrDefault(s => s.GetName() == nameS);
                        
                            Console.Clear();

                            Console.WriteLine("Observation aanmaken");
                            Console.WriteLine();

                            Console.WriteLine("Geef een lattitude op:");
                            double latO;
                            while (!double.TryParse(Console.ReadLine(), out latO))
                            {
                                Console.WriteLine("Lattitude is verplicht en moet een geldig getal zijn. Probeer het opnieuw.");
                            }

                            Console.WriteLine("Geef een longitude op:");
                            double lonO;
                            while (!double.TryParse(Console.ReadLine(), out lonO))
                            {
                                Console.WriteLine("Lattitude is verplicht en moet een geldig getal zijn. Probeer het opnieuw.");
                            }

                            Console.WriteLine("Geef een image op:");
                            string imageO = Console.ReadLine();
                            while (string.IsNullOrWhiteSpace(imageO))
                            {
                                Console.WriteLine("Image is verplicht. Probeer het opnieuw.");
                                imageO = RequestInput("Voer een image in");
                            }

                            Console.WriteLine("Geef een beschrijving op:");
                            string descriptionO = Console.ReadLine();
                            while (string.IsNullOrWhiteSpace(descriptionO))
                            {
                                Console.WriteLine("Beschrijving is verplicht. Probeer het opnieuw.");
                                descriptionO = RequestInput("Voer een beschrijving in");
                            }


                            Observation observationToAdd = new Observation(latO, lonO, imageO, descriptionO, 
                            FoundSpecies, loggedInUser);

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
                            Console.WriteLine("Ophalen aangemaakte observations");
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

                            Console.WriteLine("0: Terug naar hoofdmenu? druk 0. Druk een andere toets om door te gaan");
                            string SpeciesInDatabaseToMain = Console.ReadLine();

                            if (SpeciesInDatabaseToMain == "0")
                            {
                                break;
                            }
                            Console.WriteLine();

                            foreach (Species s in species.GetSpeciesList())
                            {
                                Console.WriteLine(s.GetId() + " " + s.GetName());
                            }
                            Console.ReadLine();
                            break;

                        case "18":
                            Console.Clear();
                            Console.WriteLine("Game spelen");
                            Console.WriteLine();

                            Console.WriteLine("0: Terug naar hoofdmenu? druk 0. Druk een andere toets om door te gaan");
                            string GameSpelenToMain = Console.ReadLine();

                            if (GameSpelenToMain == "0")
                            {
                                break;
                            }
                            Console.WriteLine();

                            // Games ophalen en weergeven die in de database staan
                            foreach (Game g in game.GetGames())
                            {
                                Console.WriteLine(g.getId() + " " + g.getName());
                            }
                            Console.WriteLine();

                            Console.WriteLine("Voer het id in van de game die u wilt spelen");
                            int gameIdToPlay = int.Parse(Console.ReadLine());
                           
                            Game gameToPlay = game.GetGames().FirstOrDefault(g => g.getId() == gameIdToPlay);

                            if (gameToPlay == null)
                            {
                                Console.WriteLine("Game niet gevonden.");
                                Console.ReadKey();
                                break;
                            }

                            // Vragen ophalen die bij de game horen
                            List<Question> questions = question.GetQuestionsByGameId(gameIdToPlay);

                            foreach (Question q in questions)
                            {
                                Console.Clear();
                                Console.WriteLine("Vraag: " + q.GetQuestionText());
                                string userAnswer = Console.ReadLine();

                                // Controleer of het antwoord correct is
                                string correctAnswerU = q.GetCorrectAnswerFromDatabase();
                                // Controleer of het gegeven antwoord van de gebruiker overeenkomt met het correcte antwoord, ongeacht hoofdletters.
                                if (correctAnswerU.Equals(userAnswer, StringComparison.OrdinalIgnoreCase))
                                {
                                    Console.WriteLine("Correct antwoord!");   
                                }
                                else
                                {
                                    Console.WriteLine("Fout antwoord. Het juiste antwoord is: " + correctAnswerU);
                                }

                                Console.ReadKey();
                            }

                            Console.WriteLine("Game voltooid!");
                            Console.ReadKey();
                            break;

                        case "19":
                            Console.Clear();
                            Console.WriteLine("Game aanmaken");
                            Console.WriteLine();
                            Console.WriteLine("Voer een game naam op");
                            string gameName = Console.ReadLine();
                            Console.WriteLine();
                            Console.WriteLine("Kies een route id voor het koppelen");
                            Console.WriteLine();
                            foreach (Route r in route.GetAllRoutes())
                            {
                                Console.WriteLine(r.GetId() + " " + r.GetName());
                            }

                            // De variabele aId wordt geïnitialiseerd als null.
                            // De gebruiker wordt gevraagd om invoer via de console.
                            // Als de invoer niet leeg is, wordt de invoer omgezet naar een integer en toegewezen aan aId.
                            int? rId = null;
                            string inputUserRoute = Console.ReadLine();
                            if (!string.IsNullOrEmpty(inputUserRoute))
                            {
                                rId = int.Parse(inputUserRoute);
                            }

                            Route FoundRoute = route.GetAllRoutes().FirstOrDefault(r => r.GetId() == rId);

                            Game gameToAdd = new Game(gameName, FoundRoute);
                            gameToAdd.CreateGame(gameToAdd);

                            Console.WriteLine($"Game met naam {gameName} is aangemaakt");
                            Console.ReadLine();
                            break;

                        case "20":
                            Console.Clear();
                            Console.WriteLine("Game aanpassen");
                            Console.WriteLine();

                            foreach (Game g in game.GetGames())
                            {
                                Console.WriteLine(g.getId() + " " + g.getName());
                            }

                            Console.WriteLine("Voer het id in dat je wilt aanpassen");
                            int gameIdE = int.Parse(Console.ReadLine());
                            Console.WriteLine("Voer een naam in");
                            string nameE = Console.ReadLine();

                            Game gameToEdit = new Game(gameIdE, nameE, null);

                            gameToEdit.UpdateGame(gameToEdit);

                            Console.WriteLine($"Game {nameE} is aangepast");

                            break;

                        case "21":
                            Console.Clear();
                            Console.WriteLine("Game verwijderen");
                            Console.WriteLine();

                            // Games ophalen en weergeven die in de database staan
                            foreach (Game g in game.GetGames())
                            {
                                Console.WriteLine(g.getId() + " " + g.getName());
                            }
                            Console.WriteLine();

                            Console.WriteLine("Voer het id in van de game die u wilt verwijderen");
                            int gameId = int.Parse(Console.ReadLine());

                            // Verwijderen van de game op id
                            game.DeleteGame(gameId);

                            Console.WriteLine($"Game: {game.getName()} is verwijderd");

                            Console.ReadKey();
                            break;

                        case "22":
                            Console.Clear();
                            Console.WriteLine("Games in de database");
                            Console.WriteLine();

                            // Games ophalen en weergeven die in de database staan
                            foreach (Game g in game.GetGames())
                            {
                                Console.WriteLine(g.getId() + " " + g.getName());
                            }

                            Console.ReadLine();
                            break;

                        case "23":
                            Console.Clear();
                            Console.WriteLine("Question aanmaken");
                            Console.WriteLine();
                            Console.WriteLine("Voer een vraag in");
                            string questionText = Console.ReadLine();
                            Console.WriteLine("Voer een type in");
                            string questionType = Console.ReadLine();

                            foreach (Game g in game.GetGames())
                            {
                                Console.WriteLine(g.getId() + " " + g.getName());
                            }

                            Console.WriteLine("Voer een game id in");
                            int gameIdQ = int.Parse(Console.ReadLine());

                            Game FoundGame = game.GetGames().FirstOrDefault(g => g.getId() == gameIdQ);

                            Question questionToAdd = new Question(questionText, questionType, FoundGame);
                            questionToAdd.CreateQuestion(questionToAdd);

                            Console.WriteLine($"Question met vraag: {questionToAdd.GetQuestionText()}, is aangemaakt");

                            Console.ReadLine();
                            break;

                        case "25":
                            Console.Clear();
                            Console.WriteLine("Question verwijderen");
                            Console.WriteLine();

                            // Questions ophalen en weergeven die in de database staan
                            foreach (Question q in question.GetAllQuestions())
                            {
                                Console.WriteLine(q.GetQuestionId() + " " + q.GetQuestionText());
                            }
                            Console.WriteLine();

                            Console.WriteLine("Voer het question id in die verwijderd moet worden.");
                            int questionIdToDelete = Int32.Parse(Console.ReadLine());

                            question.DeleteQuestion(questionIdToDelete);
                            Console.ReadKey();
                            break;


                        case "26":
                            Console.Clear();
                            Console.WriteLine("Questions in de database");
                            Console.WriteLine();

                            // Questions ophalen en weergeven die in de database staan
                            foreach (Question q in question.GetAllQuestions())
                            {
                                Console.WriteLine("Question id: " + q.GetQuestionId() + " Gekoppeld aan Game:" + q.GetGameId().getName());
                            }

                            Console.ReadLine();
                            break;

                        case "27":
                            Console.Clear();
                            Console.WriteLine("Answer aanmaken");
                            Console.WriteLine();

                            foreach (Question q in question.GetAllQuestions())
                            {
                                Console.WriteLine(q.GetQuestionId() + " " + q.GetQuestionText());
                            }

                            Console.WriteLine("Voer een question id in");
                            int questionIdA = int.Parse(Console.ReadLine());

                            Question FoundQuestion = question.GetAllQuestions().FirstOrDefault(q => q.GetQuestionId() == questionIdA);
                            Console.WriteLine();
                            Console.WriteLine("Voer een correct antwoord in");
                            string correctAnswer = Console.ReadLine();

                            Answer answerToAdd = new Answer(correctAnswer, FoundQuestion);
                            answerToAdd.CreateAnswer(answerToAdd);

                            Console.WriteLine($"Answer met correct antwoord: {answerToAdd.GetCorrectAnswer()}, is aangemaakt");

                            Console.ReadLine();
                            break;

                        case "28":
                            Console.Clear();
                            Console.WriteLine("Answer verwijderen");
                            Console.WriteLine();

                            // Games ophalen en weergeven die in de database staan
                            foreach (Answer a in answer.GetAnswers())
                            {
                                Console.WriteLine(a.GetId() + " " + a.GetCorrectAnswer());
                            }
                            Console.WriteLine();

                            Console.WriteLine("Voer het id in van de answer die u wilt verwijderen");
                            int answerIdToDelete = int.Parse(Console.ReadLine());

                            // Verwijderen van de game op id
                            answer.DeleteAnswer(answerIdToDelete);

                            Console.WriteLine($"Answer: {answerIdToDelete} is verwijderd");

                            Console.ReadKey();
                            break;


                        case "30":
                            Console.Clear();
                            Console.WriteLine("Route aanmaken");
                            Console.WriteLine();
                            Console.WriteLine("Geef een naam op");
                            string nameR = Console.ReadLine();

                            Console.WriteLine("Voer het id in van de area die u wilt toekennen");
                            Console.WriteLine();
                            foreach (Area a in area.GetAllAreas())
                            {
                                Console.WriteLine(a.GetId() + " " + a.GetName());
                            }

                            // De variabele aId wordt geïnitialiseerd als null.
                            // De gebruiker wordt gevraagd om invoer via de console.
                            // Als de invoer niet leeg is, wordt de invoer omgezet naar een integer en toegewezen aan aId.
                            int? aId = null;
                            string inputUser = Console.ReadLine();
                            if (!string.IsNullOrEmpty(inputUser))
                            {
                                aId = int.Parse(inputUser);
                            }

                            Area FoundArea = area.GetAllAreas().FirstOrDefault(a => a.GetId() == aId);
                            Route routeToAdd = new Route(nameR, FoundArea);

                            routeToAdd.CreateRoute(routeToAdd);
                            Console.WriteLine($"Route met naam {routeToAdd.GetName()} is aangemaakt" +
                                (FoundArea != null ? $", en gekoppeld aan Area: {FoundArea.GetName()}" : ""));

                            Console.ReadLine();
                            break;

                        case "31":
                            Console.Clear();
                            Console.WriteLine("Route aanpassen");
                            Console.WriteLine();
                            foreach (Route r in route.GetAllRoutes())
                            {
                                Console.WriteLine(r.GetId() + " " + r.GetName());
                            }

                            Console.WriteLine("Voer het id in dat je wilt verwijderen");
                            int routeIdtoDelete = int.Parse(Console.ReadLine());

                            route.DeleteRoute(routeIdtoDelete);

                            Console.WriteLine($"Route met Id {routeIdtoDelete} is verwijderd");
                            Console.ReadKey();
                            break;

                        case "32":
                            Console.Clear();
                            Console.WriteLine("Alle routes in de database");
                            foreach (Route r in route.GetAllRoutes())
                            {
                                Console.WriteLine(r.GetId() + " " + r.GetName());
                            }
                            Console.ReadKey();
                            break;

                        case "40":
                            Console.Clear();
                            Console.WriteLine("User + Role toekennen");
                            Console.WriteLine();

                            // Gebruikers ophalen en weergeven die in de database staan
                            foreach (User u in user.GetUser())
                            {
                                Console.WriteLine(u.GetId() + " " + u.GetName());
                            }

                            Console.WriteLine();
                            Console.WriteLine("Voer het id in van de gebruiker die u een rol wilt toekennen");
                            int userId = int.Parse(Console.ReadLine());

                            // Roles ophalen en weergeven die in de database staan
                            foreach (Role r in role.GetRoles())
                            {
                                Console.WriteLine(r.GetId() + " " + r.GetTypeRole());
                            }
                            Console.WriteLine();
                            Console.WriteLine("Voer het id in van de rol die u wilt toekennen");
                            int roleId = int.Parse(Console.ReadLine());

                            User FoundUser = user.GetUser().FirstOrDefault(u => u.GetId() == userId);
                            Role FoundRole = role.GetRoles().FirstOrDefault(r => r.GetId() == roleId);

                            // Toekennen van de rol aan de gebruiker
                            userRole.AddUserRole(FoundUser, FoundRole);

                            Console.WriteLine($"Rol met id: {roleId} is toegewezen aan gebruiker met id: {userId}");
                            Console.ReadKey();
                            break;

                        case "54":
                            Console.Clear();
                            Console.WriteLine("Observation verwijderen");
                            Console.WriteLine();

                            Console.WriteLine("0: Terug naar hoofdmenu? druk 0. Druk een andere toets om door te gaan");
                            string ObservationVerwijderenToMain = Console.ReadLine();

                            if (ObservationVerwijderenToMain == "0")
                            {
                                break;
                            }
                            Console.WriteLine();

                            var userObservations = observation.GetObservationsByUserId(loggedInUser.GetId());
                            foreach (Observation o in userObservations)
                            {
                                Console.WriteLine("Observation Id: " + o.GetId() + ", Beschrijving: " + o.GetDescription() +
                                    ", Aangemaakt door: " + o.GetUserId().GetName());
                            }

                            Console.WriteLine();

                            Console.WriteLine("Voer het id in van de observation die u wilt verwijderen");
                            string observationIdD = Console.ReadLine();
                            while (string.IsNullOrEmpty(observationIdD))
                            {
                                Console.WriteLine("Het id mag niet leeg zijn. Voer het id in van de observation die u wilt aanpassen");
                                observationIdD = Console.ReadLine();
                            }
                            int observationIdDToFind = int.Parse(observationIdD);

                            // Check if the observation belongs to the logged-in user
                            var observationToDelete = userObservations.FirstOrDefault(o => o.GetId() == observationIdDToFind);
                            if (observationToDelete != null)
                            {
                                // Verwijderen van de observation op id
                                observation.DeleteObservation(observationIdDToFind);
                                Console.WriteLine($"Observation: {observationToDelete.GetDescription()} is verwijderd");
                            }
                            else
                            {
                                Console.WriteLine("U heeft geen rechten om deze observation te verwijderen of deze observation bestaat niet.");
                            }

                            Console.ReadKey();
                            break;

                        case "55":
                            Console.Clear();
                            Console.WriteLine("Ophalen aangemaakte observations");
                            Console.WriteLine();

                            Console.WriteLine("0: Terug naar hoofdmenu? druk 0. Druk een andere toets om door te gaan");
                            string ObservationOphalenToMain = Console.ReadLine();

                            if (ObservationOphalenToMain == "0")
                            {
                                break;
                            }
                            Console.WriteLine();

                            foreach (Observation o in observation.GetObservationsByUserId(loggedInUser.GetId()))
                            {
                                Console.WriteLine("Observation Id: " + o.GetId() + ", Beschrijving: " + o.GetDescription() + 
                                    ", Aangemaakt door: " + o.GetUserId().GetName());
                            }

                            Console.ReadLine();
                            break;

                        case "80":
                            Console.Clear();
                            Dijkstra.ParkTestCase();
                            Console.ReadKey();
                            break;

                        case "90":
                            Console.WriteLine("Observation aanpassen");
                            Console.WriteLine();

                            foreach (Observation o in observation.GetAllObservations())
                            {
                                Console.WriteLine(o.GetId() + " " + o.GetDescription());
                            }
                            Console.WriteLine();

                            Console.WriteLine("Voer het id in van de observation die u wilt aanpassen");
                            string inputIdO = Console.ReadLine();
                            while (string.IsNullOrEmpty(inputIdO))
                            {
                                Console.WriteLine("Het id mag niet leeg zijn. Voer het id in van de observation die u wilt aanpassen");
                                inputIdO = Console.ReadLine();
                            }
                            int observationIdToUpdate = int.Parse(inputIdO);

                            Console.WriteLine("Geef een lattitude op:");
                            double latOToUpdate;
                            while (!double.TryParse(Console.ReadLine(), out latOToUpdate))
                            {
                                Console.WriteLine("Lattitude is verplicht en moet een geldig getal zijn. Probeer het opnieuw.");
                            }

                            Console.WriteLine("Geef een longitude op:");
                            double lonOToUpdate;
                            while (!double.TryParse(Console.ReadLine(), out lonOToUpdate))
                            {
                                Console.WriteLine("Longttitude is verplicht en moet een geldig getal zijn. Probeer het opnieuw.");
                            }

                            Console.WriteLine("Geef een image op:");
                            string imageOToUpdate = Console.ReadLine();
                            while (string.IsNullOrWhiteSpace(imageOToUpdate))
                            {
                                Console.WriteLine("Image is verplicht. Probeer het opnieuw.");
                                imageO = RequestInput("Voer een image in");
                            }

                            Console.WriteLine("Geef een beschrijving op:");
                            string descriptionOToUpdate = Console.ReadLine();
                            while (string.IsNullOrWhiteSpace(descriptionOToUpdate))
                            {
                                Console.WriteLine("Beschrijving is verplicht. Probeer het opnieuw.");
                                descriptionO = RequestInput("Voer een beschrijving in");
                            }

                            foreach (Species s in species.GetSpeciesList())
                            {
                                Console.WriteLine(s.GetId() + " " + s.GetName());
                            }

                            Console.WriteLine("Voer het id in van de Specie die u wilt veranderen");
                            int SpecieIdToUpdate = int.Parse(Console.ReadLine());

                            Species FoundspecieToUpdate = species.GetSpeciesList().FirstOrDefault(s => s.GetId() == SpecieIdToUpdate);


                            Observation observationToUpdate = new Observation(observationIdToUpdate, latOToUpdate, lonOToUpdate, imageOToUpdate, descriptionOToUpdate,
                            FoundspecieToUpdate, loggedInUser);

                            // Update van object in database
                            observation.UpdateObservation(observationToUpdate);

                            Console.WriteLine($"Observation met id: {observationToUpdate.GetId()}, is succesvol aangepast");

                            Console.ReadKey();
                            break;

                        case "91":
                            Console.Clear();
                            Console.WriteLine("aangemaakte Observation aanpassen");
                            Console.WriteLine();

                            Console.WriteLine("0: Terug naar hoofdmenu? druk 0. Druk een andere toets om door te gaan");
                            string ObservationAanpassenToMain = Console.ReadLine();

                            if (ObservationAanpassenToMain == "0")
                            {
                                break;
                            }
                            Console.WriteLine();

                            // Laat alleen de observaties zien die door de ingelogde gebruiker zijn aangemaakt
                            var userObservationsUpdate = observation.GetObservationsByUserId(loggedInUser.GetId());
                            foreach (Observation o in userObservationsUpdate)
                            {
                                Console.WriteLine($"Observation Id: {o.GetId()}, Beschrijving: {o.GetDescription()}, Aangemaakt door: {o.GetUserId().GetName()}");
                            }
                            Console.WriteLine();

                            Console.WriteLine("Voer het id in van de observation die u wilt aanpassen");
                            string inputIdOId = Console.ReadLine();
                            while (string.IsNullOrEmpty(inputIdOId))
                            {
                                Console.WriteLine("Het id mag niet leeg zijn. Voer het id in van de observation die u wilt aanpassen");
                                inputIdOId = Console.ReadLine();
                            }
                            int UobservationToUpdate = int.Parse(inputIdOId);

                            // Controleer of de observatie behoort tot de ingelogde gebruiker
                            var UobservationToUpdateX = userObservationsUpdate.FirstOrDefault(o => o.GetId() == UobservationToUpdate);
                            if (UobservationToUpdateX != null)
                            {
                                Console.WriteLine("Geef een lattitude op:");
                                double UlatOToUpdate;
                                while (!double.TryParse(Console.ReadLine(), out UlatOToUpdate))
                                {
                                    Console.WriteLine("Lattitude is verplicht en moet een geldig getal zijn. Probeer het opnieuw.");
                                }

                                Console.WriteLine("Geef een longitude op:");
                                double UlonOToUpdate;
                                while (!double.TryParse(Console.ReadLine(), out UlonOToUpdate))
                                {
                                    Console.WriteLine("Longttitude is verplicht en moet een geldig getal zijn. Probeer het opnieuw.");
                                }

                                Console.WriteLine("Geef een image op:");
                                string UimageOToUpdate = Console.ReadLine();
                                while (string.IsNullOrWhiteSpace(UimageOToUpdate))
                                {
                                    Console.WriteLine("Image is verplicht. Probeer het opnieuw.");
                                    imageOToUpdate = RequestInput("Voer een image in");
                                }

                                Console.WriteLine("Geef een beschrijving op:");
                                string UdescriptionOToUpdate = Console.ReadLine();
                                while (string.IsNullOrWhiteSpace(UdescriptionOToUpdate))
                                {
                                    Console.WriteLine("Beschrijving is verplicht. Probeer het opnieuw.");
                                    descriptionOToUpdate = RequestInput("Voer een beschrijving in");
                                }

                                // Haal de geselecteerde soort op
                                foreach (Species s in species.GetSpeciesList())
                                {
                                    Console.WriteLine($"{s.GetId()} {s.GetName()}");
                                }

                                Console.WriteLine("Voer het id in van de Soort die u wilt veranderen");
                                int specieIdToUpdate = int.Parse(Console.ReadLine());
                                Species foundSpecieToUpdate = species.GetSpeciesList().FirstOrDefault(s => s.GetId() == specieIdToUpdate);

                                // Maak een nieuwe observatie-instantie aan met de bijgewerkte gegevens
                                Observation updatedObservation = new Observation(
                                    UobservationToUpdateX.GetId(),
                                    UlatOToUpdate,
                                    UlonOToUpdate,
                                    UimageOToUpdate,
                                    UdescriptionOToUpdate,
                                    foundSpecieToUpdate,
                                    loggedInUser
                                );

                                // Update de observatie in de database
                                observation.UpdateObservation(updatedObservation);

                                Console.WriteLine($"Observatie met id: {updatedObservation.GetId()} is succesvol aangepast.");
                            }
                            else
                            {
                                Console.WriteLine("U heeft geen rechten om deze observatie aan te passen of de observatie bestaat niet.");
                            }

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

		/*  ==================== Helping method for returning strings instead of multiple console writelines for requesting data ==================== */
		static string RequestInput(string prompt)
		{
			Console.WriteLine(prompt);
			return Console.ReadLine();
		}

		/*  ==================== Method for creating a new user ==================== */
		static void CreateNewUser()
		{
			User user = new User();

            Console.Clear();
            Console.WriteLine("Gebruiker aanmaken");
            Console.WriteLine();

            string name = RequestInput("Voer een naam in");
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Naam is verplicht. Probeer het opnieuw.");
                name = RequestInput("Voer een naam in");
            }

            // Vraag om de geboortedatum en controleer of de gebruiker minstens 14 jaar oud is
            DateTime dateOfBirth = DateTime.MinValue;
            bool isDateOfBirthValid = false;
            while (!isDateOfBirthValid)
            {
                string dateOfBirthInput = RequestInput("Voer uw geboortedatum in (dd-mm-jjjj)");
                if (string.IsNullOrWhiteSpace(dateOfBirthInput))
                {
                    Console.WriteLine("Geboortedatum is verplicht. Probeer het opnieuw.");
                    continue;
                }
                if (DateTime.TryParse(dateOfBirthInput, out dateOfBirth) && user.IsAgeValid(dateOfBirth))
                {
                    isDateOfBirthValid = true;
                }
                else
                {
                    Console.WriteLine("U moet minstens 14 jaar oud zijn. Probeer het opnieuw.");
                }
            }

            string email = RequestInput("Voer uw email in");
            while (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email is verplicht. Probeer het opnieuw.");
                email = RequestInput("Voer uw email in");
            }

            // Vraag om het wachtwoord en valideer
            string passwordU = "";
            bool isPasswordValid = false;
            while (!isPasswordValid)
            {
                passwordU = RequestInput("Voer uw wachtwoord in (minstens 8 karakters, één hoofdletter, één cijfer)");
                if (string.IsNullOrWhiteSpace(passwordU))
                {
                    Console.WriteLine("Wachtwoord is verplicht. Probeer het opnieuw.");
                    continue;
                }
                if (user.IsPasswordValid(passwordU))
                {
                    isPasswordValid = true;
                }
                else
                {
                    Console.WriteLine("Het wachtwoord voldoet niet aan de eisen. Probeer opnieuw.");
                }
            }

            int xpLevel = 0;
            int xp = 0;

            User userToAdd = new User(name, dateOfBirth, email, passwordU, xpLevel, xp);
            user.CreateUser(userToAdd);

            Console.WriteLine($"Naam: {userToAdd.GetName()} is succesvol aangemaakt");
            Console.ReadKey();
        }
        /*  ==================== Method for logging in ==================== */

        static void LoginUser(ref User loggedInUser)
        {
            User user = new User();

            // Check if user is logged in
            bool isLoggedIn = false;

            while (!isLoggedIn)
            {
                Console.Clear();
                Console.WriteLine("Inloggen");
                Console.WriteLine();

                // Vraag om de username
                Console.Write("Voer uw gebruikersnaam in: ");
                string username = Console.ReadLine();

                // Als de username leeg is vraag opnieuw om een in te vullen
                while (string.IsNullOrWhiteSpace(username))
                {
                    Console.WriteLine("Username is verplicht. Probeer het opnieuw.");
                    username = RequestInput("Voer uw username in");
                }

                // Vraag om de gebruik zijn wachtwoord.
                Console.Write("Voer uw wachtwoord in: ");
                // Deze methode zorgt ervoor dat het wachtwoord niet zichtbaar is in de console.
                string password = user.GetSecurePassword();

                if (user.ValidateUser(username, password))
                {
                    // Check if username and password exist in database
                    loggedInUser = user.GetUser().FirstOrDefault(u => u.GetName() == username);
                    if (loggedInUser != null)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Welkom, {loggedInUser.GetName()}!");
                        Console.WriteLine("Druk op een toets om door te gaan");
                        Console.ReadKey();

                        // Set logged in to true
                        isLoggedIn = true;
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Ongeldige gebruikersnaam of wachtwoord");
                    Console.WriteLine("Druk op een toets om opnieuw te proberen");
                    Console.ReadKey();
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






/* ==================== Bronnen ==================== */

// Helper methode voor een string in plaats van meerdere console writelines.
// https://stackoverflow.com/questions/73018976/with-only-one-console-writeline-how-can-i-give-multiple-console-writelines
