using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleAppBENTExNL.Models
{
    internal class WelcomeSign
    {
        public WelcomeSign()
        {
            
        }
        public void WelcomeScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;

            // ASCII-art logo
            string logo = @"
             
            >>======================================================================================<<
            ||                                                                                      ||
            ||   ____                                                                               ||
            ||  |  _ \  ___                                                                         ||
            ||  | | | |/ _ \                                                                        ||
            ||  | |_| |  __/                                                                        ||
            ||  |____/ \___| __                      _ _                        _                   ||
            ||  | ____|_ __ / _| __ _  ___   ___  __| | |__   _____      ____ _| | _____ _ __ ___   ||
            ||  |  _| | '__| |_ / _` |/ _ \ / _ \/ _` | '_ \ / _ \ \ /\ / / _` | |/ / _ \ '__/ __|  ||
            ||  | |___| |  |  _| (_| | (_) |  __/ (_| | |_) |  __/\ V  V / (_| |   <  __/ |  \__ \  ||
            ||  |_____|_|  |_|  \__, |\___/ \___|\__,_|_.__/ \___| \_/\_/ \__,_|_|\_\___|_|  |___/  ||
            ||                  |___/                                                               ||
            ||                                                                                      ||
            >>======================================================================================<<                                          
            ";

            // Bereken de breedte van het logo
            int logoWidth = logo.Split('\n')[0].Length;
            // Zet de cursor naar de juiste positie aan de rechterkant van het scherm
            int logoX = Console.WindowWidth - logoWidth - 2; // De -2 zorgt voor een kleine marge aan de rechterkant
            int logoY = 0; // Zet het logo bovenaan het scherm

            // Print het logo aan de rechterkant
            Console.SetCursorPosition(logoX, logoY);
            Console.WriteLine(logo);

            // Animatie
            string message = " Logan Boer, Quintin Roumen, Xavier Prickaerts en Maikel Heijen ";
            int x = (Console.WindowWidth - message.Length) / 2;
            int y = Console.WindowHeight / 2;

            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine(message);
            }

            Console.SetCursorPosition(x, y);
            Console.WriteLine(message);
            Console.ResetColor();
        }

    }
}
