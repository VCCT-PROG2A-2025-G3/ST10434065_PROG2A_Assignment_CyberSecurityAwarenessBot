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
using System.Threading.Tasks;

namespace CyberSecurityAwarenessBot
{
    // This class is responsible for the chatbot functionality.
    class ChatBot
    {
        // Main method to start the chatbot.
        public static void BotStart()
        {
            Console.ForegroundColor = ConsoleColor.Green; // Set the console color to green
            Console.WriteLine("\nAsk me anything related to cybersecurity, type 'exit' or 'quit' to exit the program. \n"); // Instructing the user about the bot's purpose

            bool running = true; // Variable to control the chatbot loop

            // Loop until the user decides to exit
            while (running)
            {
                Console.ForegroundColor = ConsoleColor.Cyan; // Set the console color to cyan
                // Prompt the user for input
                Console.Write("You: ");
                string input = Console.ReadLine()?.ToLower().Trim(); // Read user input and convert it to lowercase
                Console.ResetColor(); // Reset the console color to default

                // Check if the input is empty or whitespace
                if (string.IsNullOrWhiteSpace(input)) continue;

                // Check if the user wants to exit
                if (input == "exit" || input == "quit")
                {
                    running = false; // Set running to false to exit the loop
                    Console.WriteLine("Chatbot: Stay safe out there. Goodbye!"); // Print goodbye message
                }
                else
                {
                    UserResponse(input); // Call the method to handle user response
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------//

        // Method to handle user responses
        private static void UserResponse(string input)
        {
            Console.ForegroundColor = ConsoleColor.Green; // Set the console color to green

            // Check for specific keywords in the user input and respond accordingly
            if (input.Contains("hey") || input.Contains("hello"))
            {
                Console.WriteLine("Chatbot: ");
                TypeOut("Hey! What cyber security questions do you have for me?"); // Print chatbot response
            }
            else if (input.Contains("how are you"))
            {
                Console.WriteLine("Chatbot: "); 
                TypeOut("I'm fully patched and running smooth. Thanks for asking!"); // Print chatbot response
            }
            else if (input.Contains("your purpose") || input.Contains("why are you here"))
            {
                Console.WriteLine("Chatbot: ");
                TypeOut("I'm here to help you stay safe online and teach you about cybersecurity."); // Print chatbot response
            }
            else if (input.Contains("what can i ask") || input.Contains("help"))
            {
                Console.WriteLine("Chatbot: ");
                TypeOut("You can ask me about phishing, password safety, safe browsing or anything cyber related."); // Print chatbot response
            }
            else if (input.Contains("phishing"))
            {
                Console.WriteLine("Chatbot: ");
                TypeOut("Phishing is when attackers try to trick you into giving personal info via fake emails or messages. Always double check URLs and never click suspicious links."); // Print chatbot response
            }
            else if (input.Contains("password"))
            {
                Console.WriteLine("Chatbot: ");
                TypeOut("Use strong passwords, avoid reusing them, and enable multi factor authentication wherever possible."); // Print chatbot response
            }
            else if (input.Contains("safe browsing"))
            {
                Console.WriteLine("Chatbot: ");
                TypeOut("Use HTTPS websites, avoid downloading random files, and keep your browser updated for a safer experience."); // Print chatbot response
            }
            else
            {
                Console.WriteLine("Chatbot: ");
                TypeOut("I didn’t understand that. Try asking something related to cybersecurity or type 'help' to see what I can answer."); // Print chatbot response
            }

            Console.ResetColor(); // Reset the console color to default
        }

        //--------------------------------------------------------------------------------------------------------------//

        // Method for typing out messages with a delay 
        private static void TypeOut(string message, int delay = 30)
        {
            foreach (char c in message) // Loop through each character in the message
            {
                Console.Write(c); // Print the character
                System.Threading.Thread.Sleep(delay); // Wait for a short delay before printing the next character
            }
            Console.WriteLine(); // move to next line after message
        }

        /* References
         * -----------
         * AI Declaration 
         * --------------
         * I used ChatGPT to help with debugging a issue with my code.
         * Heres the link to my chat: https://chatgpt.com/c/68090d88-a2ec-800b-a915-ff68fbd59ed2
         */
    }
}
