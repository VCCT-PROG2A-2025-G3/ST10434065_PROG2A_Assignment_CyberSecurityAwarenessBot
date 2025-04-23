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
    // This is the main class for the Cyber Security Awareness Bot application.
    class Program
    {
        // This is the main method of the application where all the methods are being called.
        static void Main(string[] args)
        {
            AudioClass.PlayStartupSound(); // Play the startup sound
            Thread.Sleep(500); // Wait for 0.5 seconds before playing the welcome message
            AudioClass.PlayWelcomeMessage(); // Play the welcome message

        //--------------------------------------------------------------------------------------------------------------//

            Thread.Sleep(1000); // Wait for 1 seconds before displaying the logo
            LogoClass.DisplayLogo(); // Display the logo


        //--------------------------------------------------------------------------------------------------------------//

            Thread.Sleep(1000); // Wait for 1 seconds before starting the chatbot
            ChatBot.BotStart(); // Start the chatbot

            Console.WriteLine("Testing CI for your Console App");
        }
    }
}
