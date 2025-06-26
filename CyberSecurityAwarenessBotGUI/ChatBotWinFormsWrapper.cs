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
    public class ChatBotWinFormsWrapper
    {
        #region Fields
        private string userName = "friend"; // Default name for the user
        private string lastTopic = ""; // Last topic discussed
        private Queue<string> recentTopics = new Queue<string>(); // Queue to store recent topics
        private const int MaxRecentTopics = 5; // Maximum number of recent topics to remember
        private Dictionary<string, DateTime> topicFollowUpMemory = new Dictionary<string, DateTime>(); // Memory for follow-up times on topics
        private TimeSpan followUpCooldown = TimeSpan.FromMinutes(1); // Cooldown period for follow-ups
        private Random random = new Random(); // Random number generator for selecting responses

        private bool creatingTask = false; // Flag to indicate if a task is being created
        private string pendingTitle = ""; // Title for the pending task
        private string pendingDescription = ""; // Description for the pending task
        private bool waitingForTitle = false; // Flag to indicate if waiting for a title input
        private bool waitingForDescription = false; // Flag to indicate if waiting for a description input
        private bool waitingForReminder = false; // Flag to indicate if waiting for a reminder input
        private bool askReminderYesNo = false; // Flag to indicate if asking for a reminder input

        // Flags for reminder quick input flow
        private DateTime? tempReminderFromQuickInput = null; // Temporary storage for reminder from quick input
        private bool askToCreateTaskFromReminder = false; // Flag to indicate if asking to create a task from reminder quick input
        private DateTime? pendingReminderOnly = null; // Pending reminder for quick input without task title
        private bool waitingForReminderTaskTitle = false; // Flag to indicate if waiting for a task title when only reminder is provided

        private List<string> activityLog = new List<string>(); // Stores user activity logs
        private const int MaxLogEntries = 50; // Maximum number of log entries to keep
        #endregion
        //--------------------------------------------------------------------------------------------------------------// 
        #region Get Response
        // This method processes user input and returns a response based on the chatbot logic.
        public string GetResponse(string input)
        {
            input = input.ToLower().Trim(); // Normalize input to lowercase and trim whitespace

            // If waiting for user's name input (first interaction)
            if (string.IsNullOrEmpty(userName) || userName == "friend")
            {
                userName = input;
                return $"Nice to meet you, {userName}!"; // Acknowledge the user's name
            }

            // Sentiment detection
            if (DetectSentiment(input, out string sentimentResponse))
                return sentimentResponse;

            // Handle task creation and reminders (complex flow)
            string taskResponse = HandleTaskCreationFlow(input);
            if (!string.IsNullOrEmpty(taskResponse)) // If in task creation flow, return the response
                return taskResponse;

            // Handle quick reminder input patterns
            string quickReminderResponse = HandleQuickReminderInputs(input);
            if (!string.IsNullOrEmpty(quickReminderResponse)) // If quick reminder input was detected, return the response
                return quickReminderResponse;

            // Handle known topic requests
            foreach (var topic in ResponseClass.TopicResponses.Keys)
            {
                if (input.Contains(topic)) // Check if input contains any known topic
                {
                    lastTopic = topic; // Update last topic discussed
                    AddToRecentTopics(topic); // Add topic to recent topics queue

                    string response = GetRandomResponseForTopic(topic); // Get a random response for the topic

                    if (!topicFollowUpMemory.ContainsKey(topic) ||
                        DateTime.Now - topicFollowUpMemory[topic] > followUpCooldown) // Check if follow-up is allowed
                    {
                        topicFollowUpMemory[topic] = DateTime.Now; // Update follow-up memory with current time
                        response += "\nWould you like to learn more?"; // Ask if the user wants to learn more about the topic
                    }

                    return response; // Return the response for the topic
                }
            }

            // Handle "more"/"explain"/"elaborate" without specific topic
            if (input.Contains("more") || input.Contains("explain") || input.Contains("elaborate"))
            {
                if (!string.IsNullOrEmpty(lastTopic)) // If there is a last topic discussed
                    return GetRandomResponseForTopic(lastTopic); // Return a random response for the last topic discussed
                else if (recentTopics.Count > 0) // If there are recent topics
                    return GetRandomResponseForTopic(recentTopics.Peek()); // Return a random response for the most recent topic
                else
                    return GetRandomUnknownResponse(); // If no topics are available, return an unknown response
            }

            // Handle "what can I learn" or similar requests
            if (input.Contains("what can i learn") || input.Contains("what can you teach") ||
            input.Contains("what can i ask") || input.Contains("what can you tell"))
            {
                return "I can help you with cybersecurity topics like phishing, safe browsing, passwords, scams, and more. " +
                       "I can also create and manage tasks with reminders, show recent topics, and even quiz you!";
            }

            // Handle recent topics request
            if (input.Contains("recent topics") || input.Contains("what did we talk about"))
            {
                if (recentTopics.Count == 0) // If no recent topics are available
                    return "We haven't talked about much yet!";
                return "Here’s what we recently discussed:\n- " + string.Join("\n- ", recentTopics); // Return recent topics
            }

            // Handle activity log requests
            if (input.Contains("activity log") || input.Contains("what have i done") || input.Contains("show log"))
            {
                var recentLogs = activityLog.Skip(Math.Max(0, activityLog.Count - 10)); // Get the last 10 logs
                return "Here’s a summary of recent actions:\n" + string.Join("\n", recentLogs) + "\nType 'Show more' to view the last 10 logs.\n";
            }

            // Handle enabling 2FA request
            string[] twoFaKeywords = { "enable 2fa", "turn on 2fa", "2fa settings" };
            if (twoFaKeywords.Any(keyword => input.Contains(keyword))) // Check if input contains any 2FA keywords
                return "Two-Factor Authentication (2FA) helps protect your accounts. You can enable it via your account settings.";

            // Default unknown input response
            return GetRandomUnknownResponse();
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Add to Recent Topics
        // This method adds a topic to the recent topics queue, ensuring it doesn't exceed the maximum size.
        private void AddToRecentTopics(string topic)
        {
            if (!recentTopics.Contains(topic)) // Only add if the topic is not already in the queue
            {
                if (recentTopics.Count >= MaxRecentTopics) // If the queue is full, remove the oldest topic
                    recentTopics.Dequeue(); // Remove the oldest topic from the queue
                recentTopics.Enqueue(topic); // Add the new topic to the queue
            }
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Sentiment Detection
        // This method detects the sentiment of the user's input and returns a response based on it.
        private bool DetectSentiment(string input, out string response)
        {
            response = null; // Initialize response to null

            // Check for worried and similar sentiments
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("nervous"))
            {
                string topic = !string.IsNullOrEmpty(lastTopic) ? lastTopic : ""; // Get last topic if available
                response = $"It's okay to feel that way, {userName}."; // Acknowledge the user's sentiment
                if (!string.IsNullOrEmpty(topic)) // If there is a last topic discussed
                    response += "\n" + GetRandomResponseForTopic(topic); // Get a random response for the last topic
                return true; // Indicate that sentiment was detected
            }

            // Check for curious sentiment
            if (input.Contains("curious"))
            {
                string topic = !string.IsNullOrEmpty(lastTopic) ? lastTopic : ""; // Get last topic if available
                response = $"I love your curiosity {userName}! Let's dive into it."; // Acknowledge curiosity
                if (!string.IsNullOrEmpty(topic)) // If there is a last topic discussed
                    response += "\n" + GetRandomResponseForTopic(topic); // Get a random response for the last topic
                return true; // Indicate that sentiment was detected
            }

            // Check for frustrated or confused sentiments
            if (input.Contains("frustrated") || input.Contains("confused"))
            {
                string topic = !string.IsNullOrEmpty(lastTopic) ? lastTopic : ""; // Get last topic if available
                response = $"Don’t worry. I’m here to help."; // Acknowledge frustration or confusion
                if (!string.IsNullOrEmpty(topic)) // If there is a last topic discussed
                    response += "\n" + GetRandomResponseForTopic(topic); // Get a random response for the last topic
                return true; // Indicate that sentiment was detected
            }
            // If no sentiment was detected, return false
            return false; 
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Handke Task Creation Flow 
        // This method handles the task creation flow based on user input.
        private string HandleTaskCreationFlow(string input)
        {
            // Start task creation
            string[] addTaskKeywords = { "add task", "create a task", "make a task", "add a task", "create task", "make task" };
            if (addTaskKeywords.Any(k => input.Contains(k)) && !creatingTask)
            {
                creatingTask = true; // Set creating task flag to true
                waitingForTitle = true; // Set waiting for title flag to true
                return "What is the title of the task you'd like to add?"; // Ask for task title
            }

            // If already creating a task, handle the input based on the current state
            if (creatingTask && waitingForTitle)
            {
                pendingTitle = input; // Set the pending title from user input
                waitingForTitle = false; // Set waiting for title flag to false
                waitingForDescription = true; // Set waiting for description flag to true
                return $"Got it! The title is '{pendingTitle}'. Now, what is the description?"; // Ask for task description
            }

            // If already creating a task and waiting for description
            if (creatingTask && waitingForDescription)
            {
                pendingDescription = input; // Set the pending description from user input
                waitingForDescription = false; // Set waiting for description flag to false
                askReminderYesNo = true; // Set flag to ask for reminder
                return "Great. Would you like to set a reminder? (yes/no)"; // Ask if the user wants to set a reminder
            }

            // If already creating a task and asking for reminder
            if (creatingTask && askReminderYesNo)
            {
                if (input.Contains("yes") || input.Contains("yep")) // If user wants to set a reminder
                {
                    askReminderYesNo = false; // Reset the ask reminder flag
                    waitingForReminder = true; // Set waiting for reminder flag to true
                    return "Please enter the date and time (e.g., 2025-06-01 14:00):"; // Ask for reminder date and time
                }
                else if (input.Contains("no") || input.Contains("not now") || input.Contains("nope")) // If user does not want to set a reminder
                {
                    askReminderYesNo = false; // Reset the ask reminder flag
                    waitingForReminder = false; // Set waiting for reminder flag to false
                    CompleteTaskCreation(null); // Complete task creation without a reminder
                    return "No reminder set. Task created successfully!"; // Acknowledge task creation without a reminder
                }
            }

            // If already creating a task and waiting for reminder input
            if (creatingTask && waitingForReminder)
            {
                if (DateTime.TryParse(input, out DateTime reminder)) // Try to parse the reminder date and time 
                {
                    CompleteTaskCreation(reminder); // Complete task creation with the reminder
                    return $"Reminder received for {reminder:g}. Your task '{pendingTitle}' has been uploaded successfully!"; // Acknowledge task creation with reminder
                }
                else
                {
                    return "That doesn't look like a valid date/time. Try again (e.g., 2025-06-01 14:00):"; // If parsing fails, ask for valid date/time format
                }
            }

            // Handling yes/no for creating task from reminder quick input
            if (askToCreateTaskFromReminder)
            {
                if (input.Contains("yes") || input.Contains("yep") || input.Contains("sure") || input.Contains("ok")) // If user wants to create a task from reminder quick input
                { 
                    creatingTask = true; // Set creating task flag to true
                    waitingForTitle = false; // Reset waiting for title flag
                    waitingForDescription = true; // Set waiting for description flag to true
                    pendingTitle = pendingTitle; // Already set from quick input
                    tempReminderFromQuickInput = tempReminderFromQuickInput; // Already set
                    askToCreateTaskFromReminder = false; // Reset ask to create task from reminder flag
                    return "Please enter a description for the new task:"; // Ask for task description
                }
                else if (input.Contains("no")) // If user does not want to create a task from reminder quick input
                {
                    askToCreateTaskFromReminder = false; // Reset ask to create task from reminder flag
                    tempReminderFromQuickInput = null; // Clear temporary reminder from quick input
                    return "Okay, task creation cancelled."; // Acknowledge cancellation of task creation
                }
            }

            // Handling waiting for reminder task title (when reminder only provided)
            if (waitingForReminderTaskTitle && pendingReminderOnly.HasValue)
            {
                string taskTitle = input.Trim(); // Get the task title from user input

                var existingTask = MainForm.Tasks.FirstOrDefault(t =>
                    t.Title.Equals(taskTitle, StringComparison.OrdinalIgnoreCase)); // Find existing task by title

                if (existingTask != null) // If the task already exists
                {
                    if (existingTask.Reminder == null) // If the existing task does not have a reminder set
                    {
                        existingTask.Reminder = pendingReminderOnly.Value; // Set the reminder for the existing task
                        waitingForReminderTaskTitle = false; // Reset waiting for reminder task title flag
                        pendingReminderOnly = null; // Clear pending reminder only
                        return $"Reminder added to task \"{taskTitle}\""; // Acknowledge reminder addition
                    }
                    else
                    {
                        waitingForReminderTaskTitle = false; // Reset waiting for reminder task title flag
                        pendingReminderOnly = null; // Clear pending reminder only
                        return $"That task already has a reminder set for {existingTask.Reminder.Value:g}."; // Acknowledge that the task already has a reminder
                    }
                }
                else
                {
                    // Task doesn't exist — go into create task mode
                    creatingTask = true;
                    waitingForTitle = false;
                    waitingForDescription = true;
                    pendingTitle = taskTitle;
                    tempReminderFromQuickInput = pendingReminderOnly;
                    waitingForReminderTaskTitle = false;
                    pendingReminderOnly = null;
                    return $"Task \"{taskTitle}\" doesn’t exist. I’ll create it.\nPlease enter a description:";
                }
            }

            return null; // Not in task creation flow
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Handle Quick Reminder Inputs
        // This method handles quick reminder inputs from the user.
        private string HandleQuickReminderInputs(string input)
        {
            // Regex patterns for quick reminder inputs
            var matchFull = System.Text.RegularExpressions.Regex.Match(input, @"remind me to (.+) at (\d{4}-\d{2}-\d{2} \d{2}:\d{2})");
            if (matchFull.Success)
            {
                string taskTitle = matchFull.Groups[1].Value.Trim(); // Extract task title from the match
                string dateTimeStr = matchFull.Groups[2].Value.Trim(); // Extract date/time string from the match

                if (DateTime.TryParse(dateTimeStr, out DateTime reminderTime)) // Try to parse the date/time string
                {
                    var existingTask = MainForm.Tasks.FirstOrDefault(t =>
                        t.Title.Equals(taskTitle, StringComparison.OrdinalIgnoreCase)); // Find existing task by title

                    if (existingTask != null) // If the task already exists
                    {
                        if (existingTask.Reminder == null) // If the existing task does not have a reminder set
                        {
                            existingTask.Reminder = reminderTime; // Set the reminder for the existing task
                            LogActivity($"Reminder set: '{existingTask.Title}' at {reminderTime:g}"); // Log the reminder setting activity
                            return $"Reminder added successfully to task \"{taskTitle}\""; // Acknowledge reminder addition
                        }
                        else
                        {
                            return $"That task already has a reminder set for {existingTask.Reminder.Value:g}."; // Acknowledge that the task already has a reminder
                        }
                    }
                    else
                    {
                        // Start task creation flow
                        pendingTitle = taskTitle;
                        pendingDescription = "";
                        tempReminderFromQuickInput = reminderTime;
                        askToCreateTaskFromReminder = true;
                        return $"The task \"{taskTitle}\" doesn't exist. Do you want me to create it? (yes/no)";
                    }
                }
                else
                {
                    return "That doesn't look like a valid date/time. Please use the format: yyyy-MM-dd HH:mm"; // Acknowledge invalid date/time format
                }
            }

            // Match "remind me to (task name) at (date/time)" – no time specified yet
            var matchSetReminderFor = System.Text.RegularExpressions.Regex.Match(input, @"set a reminder for (.+) at (\d{4}-\d{2}-\d{2} \d{2}:\d{2})");
            if (matchSetReminderFor.Success)
            {
                string taskTitle = matchSetReminderFor.Groups[1].Value.Trim(); // Extract task title from the match
                string dateTimeStr = matchSetReminderFor.Groups[2].Value.Trim(); // Extract date/time string from the match

                if (DateTime.TryParse(dateTimeStr, out DateTime reminderTime)) // Try to parse the date/time string
                {
                    var existingTask = MainForm.Tasks.FirstOrDefault(t =>
                        t.Title.Equals(taskTitle, StringComparison.OrdinalIgnoreCase)); // Find existing task by title

                    if (existingTask != null) // If the task already exists
                    {
                        if (existingTask.Reminder == null) // If the existing task does not have a reminder set
                        {
                            existingTask.Reminder = reminderTime; // Set the reminder for the existing task
                            return $"Reminder added to task \"{taskTitle}\""; // Acknowledge reminder addition
                        }
                        else
                        {
                            return $"Task \"{taskTitle}\" already has a reminder for {existingTask.Reminder.Value:g}."; // Acknowledge that the task already has a reminder
                        }
                    }
                    else
                    {
                        // Task doesn't exist — go into create task mode
                        pendingTitle = taskTitle;
                        pendingDescription = "";
                        tempReminderFromQuickInput = reminderTime;
                        askToCreateTaskFromReminder = true;
                        return $"The task \"{taskTitle}\" doesn’t exist. Would you like to create it? (yes/no)";
                    }
                }
                else
                {
                    return "Invalid date/time format. Please use yyyy-MM-dd HH:mm."; // Acknowledge invalid date/time format
                }
            }

            // Match "set a reminder for (task name)" – no time specified yet
            var matchSetReminderForOnly = System.Text.RegularExpressions.Regex.Match(input, @"set a reminder for (.+)");
            if (matchSetReminderForOnly.Success && !input.Contains("at")) // Ensure "at" is not present in the input
            {
                string taskTitle = matchSetReminderForOnly.Groups[1].Value.Trim(); // Extract task title from the match

                var existingTask = MainForm.Tasks.FirstOrDefault(t =>
                    t.Title.Equals(taskTitle, StringComparison.OrdinalIgnoreCase)); // Find existing task by title

                if (existingTask != null) // If the task already exists
                {
                    if (existingTask.Reminder == null) // If the existing task does not have a reminder set
                    {
                        // Ask the user to enter the reminder date and time
                        pendingTitle = taskTitle;
                        waitingForReminder = true;
                        return $"Sure! Please enter the date and time for the reminder for \"{taskTitle}\" (e.g., 2025-07-01 09:00):";
                    }
                    else
                    {
                        return $"Task \"{taskTitle}\" already has a reminder set for {existingTask.Reminder.Value:g}."; // Acknowledge that the task already has a reminder
                    }
                }
                else
                {
                    // Task not found – offer to create it
                    pendingTitle = taskTitle;
                    pendingDescription = "";
                    waitingForReminder = true;
                    askToCreateTaskFromReminder = true;
                    return $"The task \"{taskTitle}\" doesn't exist. Would you like to create it?\nPlease enter a reminder date and time (e.g., 2025-07-01 09:00):";
                }
            }

            // Match "set a reminder at (date/time)" – no task specified
            var matchTimeOnly = System.Text.RegularExpressions.Regex.Match(input, @"set a reminder at (\d{4}-\d{2}-\d{2} \d{2}:\d{2})");
            if (matchTimeOnly.Success) // If the input matches the pattern for setting a reminder with only date/time
            {
                if (DateTime.TryParse(matchTimeOnly.Groups[1].Value.Trim(), out DateTime reminderTime)) // Try to parse the date/time string
                {
                    // If no task title is specified, set the reminder only
                    pendingReminderOnly = reminderTime;
                    waitingForReminderTaskTitle = true;
                    return "Sure! What task should I add this reminder to?";
                }
                else
                {
                    return "That doesn't look like a valid date/time. Use format: yyyy-MM-dd HH:mm"; // Acknowledge invalid date/time format
                }
            }

            return null; // No quick reminder input detected, return null
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Get Random Response for Topic
        // This method retrieves a random response for a given topic from the predefined responses.
        private string GetRandomResponseForTopic(string topic)
        {
            var responses = ResponseClass.TopicResponses[topic]; // Get the list of responses for the specified topic
            return responses[random.Next(responses.Count)]; // Return a random response from the list
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Get Random Unknown Response
        // This method retrieves a random response for unknown inputs.
        private string GetRandomUnknownResponse()
        {
            var responses = ResponseClass.UnknownInputResponses; // Get the list of unknown input responses
            return responses[random.Next(responses.Count)]; // Return a random response from the list
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Complete Task Creation
        // This method completes the task creation process with the provided reminder.
        private void CompleteTaskCreation(DateTime? reminder)
        {
            reminder = reminder ?? tempReminderFromQuickInput; // Use the provided reminder or the temporary reminder from quick input

            TaskItem task = new TaskItem(pendingTitle, pendingDescription, reminder); // Create a new task item with the provided title, description, and reminder

            MainForm.AddTaskFromChat(task); // Add the task to the main form's task list

            // Log the activity
            LogActivity($"Task added: '{task.Title}' with description '{task.Description}'" +
           (reminder.HasValue ? $" (Reminder: {reminder.Value:g})" : ""));

            // Reset flags
            creatingTask = false;
            pendingTitle = "";
            pendingDescription = "";
            waitingForReminder = false;
            askReminderYesNo = false;
            tempReminderFromQuickInput = null;
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Log Activity
        // This method logs user activity to the activity log queue.
        public void LogActivity(string logMessage)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm"); // Get the current timestamp in a specific format
            string fullLog = $"{timestamp} - {logMessage}"; // Format the log message with the timestamp

            activityLog.Add(fullLog); // Add the log message to the activity log

            // Limit the list size
            if (activityLog.Count > MaxLogEntries)
                activityLog.RemoveAt(0); // Remove the oldest entry
        }
        #endregion
    }
}
