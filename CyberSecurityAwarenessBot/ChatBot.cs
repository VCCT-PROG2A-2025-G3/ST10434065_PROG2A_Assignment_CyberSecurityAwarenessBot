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
        private static string userName = ""; // Variable to store the user's name
        private static string lastTopic = ""; // Variable to store the last topic discussed
        private static Dictionary<string, DateTime> topicFollowUpMemory = new Dictionary<string, DateTime>(); // Dictionary to store the last follow-up time for each topic
        private static TimeSpan followUpCooldown = TimeSpan.FromMinutes(1); // Cooldown period for follow-up questions

        // Main method to start the chatbot.
        public static void BotStart()
        {
            Console.ForegroundColor = ConsoleColor.Green; // Set console text color to green
            Console.Write("\nChatbot: Hello there! What's your name? "); // Prompt the user for their name
            Console.ResetColor(); // Reset console text color
            userName = Console.ReadLine()?.Trim(); // Read the user's input and trim any whitespace
            if (string.IsNullOrWhiteSpace(userName)) // If the user didn't provide a name, set a default name
                userName = "friend"; // Default name if no input is given

            Console.ForegroundColor = ConsoleColor.Green; // Set console text color to green
            Console.WriteLine($"\nChatbot: Nice to meet you, {userName}! Ask me anything about cybersecurity. Type 'exit' or 'quit' to leave.\n"); // Greet the user and provide instructions
            Console.ResetColor(); // Reset console text color

            bool running = true; // Flag to control the chatbot loop
            while (running) // Main loop for the chatbot
            {
                Console.ForegroundColor = ConsoleColor.Cyan; // Set console text color to cyan
                Console.Write("You: "); // Prompt the user for input
                string input = Console.ReadLine()?.ToLower().Trim(); // Read the user's input and convert it to lowercase
                Console.ResetColor(); // Reset console text color

                if (string.IsNullOrWhiteSpace(input)) continue; // If the input is empty or whitespace, skip to the next iteration

                if (input == "exit" || input == "quit") // Check if the user wants to exit 
                {
                    Console.WriteLine($"\nChatbot: Stay safe out there, {userName}. Goodbye!"); // Farewell message
                    running = false; // Set the running flag to false to exit the loop
                }
                else
                {
                    ProcessInput(input); // Process the user's input
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------//
        // Method to handle user responses
        private static void ProcessInput(string input)
        {
            if (DetectSentiment(input)) return; // Check if the input contains any sentiment-related keywords

            string matchedTopic = null; // Variable to store the matched topic
            foreach (var response in ResponseClass.TopicResponses.Keys) // Loop through the known topics
            {
                if (input.Contains(response)) // Check if the input contains the topic
                {
                    matchedTopic = response; // Store the matched topic
                    lastTopic = response; // Update the last topic
                    break; // Exit the loop if a match is found
                }
            }
            // Check if the user wants to learn more about the last topic
            if (input.Contains("more") || input.Contains("explain") || input.Contains("elaborate"))
            {
                if (!string.IsNullOrEmpty(lastTopic)) // Check if there is a last topic
                {
                    PrintRandomTopicResponse(lastTopic); // Print a random response for the last topic
                    return; // Exit the method after printing the response
                }
            }
            // Check if the user wants to learn more about a specific topic
            if (matchedTopic != null)
            {
                PrintRandomTopicResponse(matchedTopic); // Print a random response for the matched topic
            }
            else
            {
                PrintRandom(ResponseClass.UnknownInputResponses); // If no topic matched, print a random unknown input response
            }

            // Loop through known topics to detect a match
            string topic = null;
            foreach (var t in ResponseClass.TopicResponses.Keys) // Loop through the known topics
            {
                if (input.Contains(t.ToLower())) // Check if the input contains the topic
                {
                    topic = t; // Store the matched topic
                    break; // Exit the loop if a match is found
                }
            }
            // If a topic was matched, print a random response for that topic
            if (!string.IsNullOrEmpty(topic))
            {
                // Print a random response for the topic
                var responses = ResponseClass.TopicResponses[topic];
                var random = new Random(); // Create a new random number generator
                string response = responses[random.Next(responses.Count)]; // Get a random response from the list
                Console.WriteLine("Chatbot: " + response); // Print the response

                // Check if we should ask a follow-up
                if (!topicFollowUpMemory.ContainsKey(topic) || DateTime.Now - topicFollowUpMemory[topic] > followUpCooldown)
                {
                    Console.Write("Chatbot: Would you like to learn more? (yes/no): "); // Ask if the user wants to learn more
                    string followUpAnswer = Console.ReadLine()?.ToLower(); // Get the user's answer
                    topicFollowUpMemory[topic] = DateTime.Now; // Update memory to avoid asking too often

                    // Check if the user wants to learn more
                    if (followUpAnswer.Contains("yes"))
                    {
                        string followUpResponse = responses[random.Next(responses.Count)]; // Get another random response
                        Console.WriteLine("Chatbot: " + followUpResponse); // Print the follow-up response
                    }
                    else
                    {
                        Console.WriteLine("Chatbot: Alright, let’s move on. Feel free to ask anything else."); // Print a message to move on
                    }
                }
            }
            else
            {
                // If no topic matched, print a random unknown input response
                PrintRandom(ResponseClass.UnknownInputResponses);
            }
        }

        //--------------------------------------------------------------------------------------------------------------//
        // Method to print a random response based on the topic
        private static void PrintRandomTopicResponse(string topic)
        {
            var responses = ResponseClass.TopicResponses[topic]; // Get the responses for the topic
            var random = new Random(); // Create a new random number generator
            string response = responses[random.Next(responses.Count)]; // Get a random response from the list
            Console.ForegroundColor = ConsoleColor.Green; // Set console text color to green
            TypeOut($"Chatbot: {response}"); // Print the response
            Console.ResetColor(); // Reset console text color
        }

        //--------------------------------------------------------------------------------------------------------------//
        // Method to print a random response from the unknown input responses
        private static void PrintRandom(List<string> responses)
        {
            var random = new Random(); // Create a new random number generator
            string response = responses[random.Next(responses.Count)]; // Get a random response from the list
            Console.ForegroundColor = ConsoleColor.Green; // Set console text color to green
            TypeOut($"Chatbot: {response}"); // Print the response
            Console.ResetColor(); // Reset console text color
        }

        //--------------------------------------------------------------------------------------------------------------//
        // Method to detect the sentiment of the user's input
        private static bool DetectSentiment(string input)
        {
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("nervous")) // Check for negative sentiment 
            {
                TypeOut($"Chatbot: It's completely understandable to feel that way, {userName}. Let me guide you through some tips to stay safe."); // Print a reassuring message
                if (!string.IsNullOrEmpty(lastTopic)) // Check if there is a last topic
                    PrintRandomTopicResponse(lastTopic); // Print a random response for the last topic
                return true; // Exit the method after handling the sentiment
            }
            else if (input.Contains("curious")) // Check for positive sentiment
            {
                TypeOut($"Chatbot: I love your curiosity, {userName}! Let's dive into it."); // Print a message encouraging curiosity
                if (!string.IsNullOrEmpty(lastTopic)) // Check if there is a last topic
                    PrintRandomTopicResponse(lastTopic); // Print a random response for the last topic
                return true; // Exit the method after handling the sentiment
            }
            else if (input.Contains("frustrated") || input.Contains("confused")) // Check for frustration or confusion
            {
                TypeOut($"Chatbot: Don't worry, {userName}. I’m here to make things clearer."); // Print a reassuring message
                if (!string.IsNullOrEmpty(lastTopic)) // Check if there is a last topic
                    PrintRandomTopicResponse(lastTopic); // Print a random response for the last topic
                return true; // Exit the method after handling the sentiment
            }
            // If no sentiment is detected, return false
            return false;
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

        //--------------------------------------------------------------------------------------------------------------//
    }
}
