/*
 * ST10434065 Seth Oliver
 * GROUP 3
 * PROGRAMMING 2A 
 * ASSIGNMENT PART 1
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CyberSecurityAwarenessBot
{
    // This class is responsible for displaying the logo of the application.
    class LogoClass
    {
        // This method displays the logo of the application.
        public static void DisplayLogo()
        {
            string[] logo = new[] // The logo of the application
            {
            "                    .--------.",
            "                   / .------. \\",
            "                  / /        \\ \\",
            "                  | |        | |",
            "                 _| |________| |_",
            "               .' |_|        |_| '.",
            "               '._____ ____ _____.'",
            "               |     .'____'.     |",
            "               '.__.'.'    '.'.__.'",
            "               '.__  |      |  __.'",
            "               |   '.'.____.'.'   |",
            "               '.____'.____.'____.'",
            "               '.________________.'",
            "                                               ",
            " ----------------------------------------------",
            "    Welcome To The CyberSecurity Awareness Bot ",
            "              Helping You Stay Safe            ",
            " ----------------------------------------------",
            };

            // Define the colors to be used for each line of the logo
            ConsoleColor[] colors =
            {
                ConsoleColor.Blue,
                ConsoleColor.DarkBlue,
                ConsoleColor.DarkYellow,
                ConsoleColor.Magenta,
                ConsoleColor.Green,
            };

            // Loop through each line of the logo and print it in the specified color
            for (int color = 0; color < logo.Length; color++)
            {
                PrintColored(logo[color], colors[color % colors.Length]); // Print the logo line in the specified color
                Thread.Sleep(200); // Wait for 0.2 seconds before printing the next line
            }
            Console.ResetColor(); // Reset the console color to default
        }

        //------------------------------------------------------------------------------------------------------------------//

        // This method prints a message in the specified color.
        public static void PrintColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}

