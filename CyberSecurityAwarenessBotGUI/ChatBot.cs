/*
 * ST10434065 Seth Oliver
 * GROUP 3
 * PROGRAMMING 2A 
 * ASSIGNMENT POE 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyberSecurityAwarenessBotGUI;

namespace CyberSecurityAwarenessBot
{
    // This class is responsible for the chatbot functionality.
    class ChatBot
    {
        #region Fields
        private static string userName = ""; // Variable to store the user's name
        private static string lastTopic = ""; // Variable to store the last topic discussed
        private static Queue<string> recentTopics = new Queue<string>(); // Queue to store recent topics for follow-up questions
        private const int MaxRecentTopics = 5; // Limit memory size to avoid overflow
        private static Dictionary<string, DateTime> topicFollowUpMemory = new Dictionary<string, DateTime>(); // Dictionary to store the last follow-up time for each topic
        private static TimeSpan followUpCooldown = TimeSpan.FromMinutes(1); // Cooldown period for follow-up questions
        private static bool creatingTask = false; // Flag to indicate if a task is being created
        private static string pendingTitle = ""; // Title for the pending task
        private static string pendingDescription = ""; // Description for the pending task
        private static bool waitingForTitle = false; // Flag to indicate if waiting for title input
        private static bool waitingForDescription = false; // Flag to indicate if waiting for description input
        private static bool waitingForReminder = false; // Flag to indicate if waiting for reminder input
        private static bool askReminderYesNo = false; // Flag to indicate if asking for a reminder input
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Bot Start
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
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Process Input
        // Method to handle user responses
        private static void ProcessInput(string input)
        {
            if (DetectSentiment(input)) return; // Check if the input contains any sentiment-related keywords

            string matchedTopic = null; // Variable to store the matched topic from the input

            // Topic Matching and Memory Update
            foreach (var response in ResponseClass.TopicResponses.Keys)
            {
                if (input.Contains(response)) // Check if the input contains any of the known topics
                {
                    matchedTopic = response; // Store the matched topic
                    lastTopic = response; // Update the last topic discussed

                    if (!recentTopics.Contains(response)) // If the topic is not already in the recent topics queue
                    {
                        if (recentTopics.Count >= MaxRecentTopics)
                            recentTopics.Dequeue();
                        recentTopics.Enqueue(response);
                    }

                    break;
                }
            }

            // If user asked for more info (explain/elaborate/etc.)
            if ((input.Contains("more") || input.Contains("explain") || input.Contains("elaborate")))
            {
                if (!string.IsNullOrEmpty(lastTopic))
                {
                    PrintRandomTopicResponse(lastTopic);
                }
                else if (recentTopics.Any())
                {
                    string fallbackTopic = recentTopics.Last();
                    PrintRandomTopicResponse(fallbackTopic);
                }
                else
                {
                    PrintRandom(ResponseClass.UnknownInputResponses);
                }
                return;
            }

            // If user asked to view recent topics
            if (input.Contains("what did we talk about") || input.Contains("recent topics"))
            {
                if (recentTopics.Any())
                {
                    Console.WriteLine("Chatbot: Here are some topics we recently discussed:");
                    foreach (var t in recentTopics)
                        Console.WriteLine($"- {t}");
                }
                else
                {
                    Console.WriteLine("Chatbot: We haven't talked about much yet!");
                }
                return;
            }

            // If user wants to create a task
            if (input.Contains("add task") && !creatingTask)
            {
                creatingTask = true; // Set the flag to indicate task creation has started
                waitingForTitle = true; // Set the flag to indicate waiting for title input
                TypeOut("Chatbot: What is the title of the task you'd like to add?");
                return; // Exit the method to wait for user input
            }
            if (creatingTask && waitingForTitle) // If we are waiting for the title input
            {
                pendingTitle = input; // Store the pending title
                waitingForTitle = false; // Set the flag to indicate we are no longer waiting for title input
                waitingForDescription = true; // Set the flag to indicate we are now waiting for description input
                TypeOut($"Chatbot: Got it! The title is '{pendingTitle}'. Now, what is the description?");
                return; // Exit the method to wait for user input
            }
            if (creatingTask && waitingForDescription) // If we are waiting for the description input
            {
                pendingDescription = input;
                waitingForDescription = false;
                askReminderYesNo = true;
                TypeOut("Chatbot: Great. Would you like to set a reminder? (yes/no)");
                return;
            }
            if (creatingTask && askReminderYesNo)
            {
                if (input.Contains("yes"))
                {
                    askReminderYesNo = false;
                    waitingForReminder = true;
                    TypeOut("Chatbot: Please enter the date and time (e.g., 2025-06-01 14:00):");
                    return;
                }
                else
                {
                    CompleteTaskCreation(null); // no reminder
                    return;
                }
            }
            if (creatingTask && waitingForReminder)
            {
                if (DateTime.TryParse(input, out DateTime reminder))
                {
                    CompleteTaskCreation(reminder);
                }
                else
                {
                    TypeOut("Chatbot: Hmm, that doesn't look like a valid date and time. Try again (e.g., 2025-06-01 14:00):");
                }
                return;
            }

            // Print response if a topic matched
            if (!string.IsNullOrEmpty(matchedTopic))
            {
                var responses = ResponseClass.TopicResponses[matchedTopic];
                var random = new Random();
                string response = responses[random.Next(responses.Count)];
                Console.WriteLine("Chatbot: " + response);

                if (!topicFollowUpMemory.ContainsKey(matchedTopic) || DateTime.Now - topicFollowUpMemory[matchedTopic] > followUpCooldown)
                {
                    Console.Write("Chatbot: Would you like to learn more? (yes/no): ");
                    string followUpAnswer = Console.ReadLine()?.ToLower();
                    topicFollowUpMemory[matchedTopic] = DateTime.Now;

                    if (followUpAnswer.Contains("yes"))
                    {
                        string followUpResponse = responses[random.Next(responses.Count)];
                        Console.WriteLine("Chatbot: " + followUpResponse);
                    }
                    else
                    {
                        Console.WriteLine("Chatbot: Alright, let’s move on. Feel free to ask anything else.");
                    }
                }
            }
            else
            {
                // No topic matched
                PrintRandom(ResponseClass.UnknownInputResponses);
            }
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Print Random Topic Response
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
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Print Random Response
        // Method to print a random response from the unknown input responses
        private static void PrintRandom(List<string> responses)
        {
            var random = new Random(); // Create a new random number generator
            string response = responses[random.Next(responses.Count)]; // Get a random response from the list
            Console.ForegroundColor = ConsoleColor.Green; // Set console text color to green
            TypeOut($"Chatbot: {response}"); // Print the response
            Console.ResetColor(); // Reset console text color
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Detect Sentiment
        // Method to detect the sentiment of the user's input
        private static bool DetectSentiment(string input)
        {
            string currentTopic = ResponseClass.TopicResponses.Keys
            .FirstOrDefault(topic => input.Contains(topic.ToLower()));

            if (input.Contains("worried") || input.Contains("scared") || input.Contains("nervous")) // Check for negative sentiment 
            {
                string topic = !string.IsNullOrEmpty(currentTopic) ? currentTopic : lastTopic; // Use the current topic if available, otherwise use the last topic
                TypeOut($"Chatbot: It's okay to feel that way, {userName}. Here's something that might help:"); // Print a reassuring message
                if (!string.IsNullOrEmpty(topic)) // Check if there is a topic to respond to
                    PrintRandomTopicResponse(topic); // Print a random response for the topic
                return true; // Exit the method after handling the sentiment
            }
            else if (input.Contains("curious")) // Check for positive sentiment
            {
                string topic = !string.IsNullOrEmpty(currentTopic) ? currentTopic : lastTopic; // Use the current topic if available, otherwise use the last topic
                TypeOut($"Chatbot: I love your curiosity! Let's dive into it."); // Print a message encouraging curiosity
                if (!string.IsNullOrEmpty(topic)) // Check if there is a topic to respond to
                    PrintRandomTopicResponse(topic); // Print a random response for the topic
                return true; // Exit the method after handling the sentiment
            }
            else if (input.Contains("frustrated") || input.Contains("confused")) // Check for frustration or confusion
            {
                string topic = !string.IsNullOrEmpty(currentTopic) ? currentTopic : lastTopic; // Use the current topic if available, otherwise use the last topic
                TypeOut($"Chatbot: Don’t worry. I’m here to help."); // Print a message offering help
                if (!string.IsNullOrEmpty(topic)) // Check if there is a topic to respond to
                    PrintRandomTopicResponse(topic); // Print a random response for the topic
                return true; // Exit the method after handling the sentiment
            }
            // If no sentiment is detected, return false
            return false;
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Type Out
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
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Complete Task Creation
        // Method to complete the task creation process
        private static void CompleteTaskCreation(DateTime? reminder)
        {
            TaskItem task = new TaskItem(pendingTitle, pendingDescription, reminder);

            MainForm.AddTaskFromChat(task); // Pass it to the form

            TypeOut("Chatbot: Task added successfully! 🎉");

            // Reset flags
            creatingTask = false;
            pendingTitle = "";
            pendingDescription = "";
            waitingForReminder = false;
            askReminderYesNo = false;
        }
        #endregion 
    }
}
