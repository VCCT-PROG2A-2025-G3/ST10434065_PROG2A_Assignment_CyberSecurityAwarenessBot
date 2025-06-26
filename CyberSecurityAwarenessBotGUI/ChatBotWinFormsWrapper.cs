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
using CyberSecurityAwarenessBot;

namespace CyberSecurityAwarenessBotGUI
{
    // This class wraps the chatbot logic for use in a WinForms application.
    class ChatBotWinFormsWrapper
    {
        #region Fields
        private string userName = "friend"; // Default name for the user
        private string lastTopic = ""; // Last topic discussed
        private Queue<string> recentTopics = new Queue<string>(); // Queue to store recent topics
        private const int MaxRecentTopics = 5; // Maximum number of recent topics to remember
        private Dictionary<string, DateTime> topicFollowUpMemory = new Dictionary<string, DateTime>(); // Memory for follow-up times on topics
        private TimeSpan followUpCooldown = TimeSpan.FromMinutes(1); // Cooldown period for follow-ups
        private Random random = new Random(); // Random number generator for selecting responses
        private static bool creatingTask = false; // Flag to indicate if a task is being created
        private static string pendingTitle = ""; // Title for the pending task
        private static string pendingDescription = ""; // Description for the pending task
        private static bool waitingForTitle = false; // Flag to indicate if waiting for a title input
        private static bool waitingForDescription = false; // Flag to indicate if waiting for a description input
        private static bool waitingForReminder = false; // Flag to indicate if waiting for a reminder input
        private static bool askReminderYesNo = false; // Flag to indicate if asking for a reminder input
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Get Response 
        // Constructor to initialize the chatbot
        public string GetResponse(string input)
        {
            input = input.ToLower(); // Normalize input to lowercase

            // Sentiment detection
            if (input.Contains("worried") || input.Contains("scared"))
                return $"It's okay to feel that way. Stay calm and let's focus on staying safe.";

            if (input.Contains("curious"))
                return $"Curiosity is good! What would you like to learn more about?";

            if (input.Contains("confused") || input.Contains("frustrated"))
                return $"Don't worry, I'm here to explain things clearly. Ask me anything.";

            // Special commands
            if (input.Contains("recent topics"))
            {
                if (recentTopics.Count == 0) return "We haven't talked about much yet!";
                return "Here’s what we recently discussed:\n- " + string.Join("\n- ", recentTopics);
            }

            if (input.Contains("more") || input.Contains("explain") || input.Contains("elaborate"))
            {
                if (!string.IsNullOrEmpty(lastTopic)) // Check if we have a last topic to follow up on
                    return GetRandomResponseForTopic(lastTopic); // Return a random response for the last topic discussed
                else if (recentTopics.Count > 0) // If their's no last topic, check recent topics
                    return GetRandomResponseForTopic(recentTopics.Peek()); // Return a random response for the most recent topic
                else
                    return GetRandomUnknownResponse(); // If their's no topics to follow up on, return an unknown response
            }

            // If user wants to create a task
            if (input.Contains("add task") && !creatingTask)
            {
                creatingTask = true;
                waitingForTitle = true;
                return "What is the title of the task you'd like to add?\n";
            }

            if (creatingTask && waitingForTitle)
            {
                pendingTitle = input;
                waitingForTitle = false;
                waitingForDescription = true;
                return $"Now, what is the description of {pendingTitle}?\n";
            }

            if (creatingTask && waitingForDescription)
            {
                pendingDescription = input;
                waitingForDescription = false;
                askReminderYesNo = true;
                return "Great. Would you like to set a reminder? (yes/no)\n";
            }

            if (creatingTask && askReminderYesNo)
            {
                if (input.Contains("yes") || input.Contains("yep"))
                {
                    askReminderYesNo = false;
                    waitingForReminder = true;
                    return "Please enter the date and time (e.g., 2025-06-01 14:00):\n";
                } 
                else if (input.Contains("no") || input.Contains("not now") || input.Contains("nope"))
                {
                    askReminderYesNo = false;
                    waitingForReminder = false;
                    CompleteTaskCreation(null); // No reminder set, complete task creation
                    return "No reminder set. Task created successfully!\n"; 
                }
            }

            if (creatingTask && waitingForReminder)
            {
                if (DateTime.TryParse(input, out DateTime reminder))
                {
                    CompleteTaskCreation(reminder);
                    return $"Reminder received for {reminder:g}. Your task '{pendingTitle}' has been uploaded successfully!";
                }
                else
                {
                    return "That doesn't look like a valid date/time. Try again (e.g., 2025-06-01 14:00):\n";
                }
            }

            // Check for known topics in the input
            foreach (var topic in ResponseClass.TopicResponses.Keys)
            {
                if (input.Contains(topic)) // Check if the input contains a known topic
                {
                    lastTopic = topic; // Update last topic discussed

                    // Store to recent memory
                    if (!recentTopics.Contains(topic))
                    {
                        if (recentTopics.Count >= MaxRecentTopics) // Check if we need to remove the oldest topic
                            recentTopics.Dequeue(); // Remove oldest topic if we exceed max recent topics
                        recentTopics.Enqueue(topic); // Add the new topic to the recent topics queue
                    }

                    string response = GetRandomResponseForTopic(topic); // Get a random response for the known topic

                    // Check if we can follow up on this topic
                    if (!topicFollowUpMemory.ContainsKey(topic) || DateTime.Now - topicFollowUpMemory[topic] > followUpCooldown)
                    {
                        topicFollowUpMemory[topic] = DateTime.Now; // Update the last follow-up time for this topic
                        response += "\nWould you like to learn more?"; // Add a follow-up question to the response
                    }

                    return response; // Return a random response for the known topic
                }
            }

            return GetRandomUnknownResponse(); // If no known topic is found, return an unknown response
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Get Random Response 
        // This method retrieves a random response for a given topic from the predefined responses.
        private string GetRandomResponseForTopic(string topic)
        {
            var responses = ResponseClass.TopicResponses[topic]; // Get the list of responses for the specified topic
            return responses[random.Next(responses.Count)]; // Return a random response from the list
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Get Random Unknown Response
        // This method retrieves a random response when the input does not match any known topics.
        private string GetRandomUnknownResponse()
        {
            var responses = ResponseClass.UnknownInputResponses; // Get the list of unknown input responses
            return responses[random.Next(responses.Count)]; // Return a random response from the unknown input responses
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Complete Task Creation
        // This method completes the task creation process and adds the task to the task list.
        private static void CompleteTaskCreation(DateTime? reminder)
        {
            TaskItem task = new TaskItem(pendingTitle, pendingDescription, reminder);

            MainForm.AddTaskFromChat(task); // Pass it to the form

            string response = $"Chatbot: Task '{task.Title}' added successfully!"; // Confirmation message
            if (reminder.HasValue)
            {
                response += $" Reminder set for {reminder.Value:g}."; // Append reminder information if set
            }
            else
            {
                response += " No reminder set."; // Indicate no reminder was set
            }

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
