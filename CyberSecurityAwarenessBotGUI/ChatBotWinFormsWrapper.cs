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
        private DateTime? tempReminderFromQuickInput = null;
        private bool askToCreateTaskFromReminder = false;
        private DateTime? pendingReminderOnly = null;
        private bool waitingForReminderTaskTitle = false;

        private List<string> activityLog = new List<string>(); // Stores user activity logs
        private const int MaxLogEntries = 50; // Maximum number of log entries to keep
        #endregion

        #region Public Method - Process User Input and Get Response
        public string GetResponse(string input)
        {
            input = input.ToLower().Trim();

            // If waiting for user's name input (first interaction)
            if (string.IsNullOrEmpty(userName) || userName == "friend")
            {
                userName = input;
                return $"Nice to meet you, {userName}!";
            }

            // Sentiment detection
            if (DetectSentiment(input, out string sentimentResponse))
                return sentimentResponse;

            // Handle task creation and reminders (complex flow)
            string taskResponse = HandleTaskCreationFlow(input);
            if (!string.IsNullOrEmpty(taskResponse))
                return taskResponse;

            // Handle quick reminder input patterns
            string quickReminderResponse = HandleQuickReminderInputs(input);
            if (!string.IsNullOrEmpty(quickReminderResponse))
                return quickReminderResponse;

            // Handle known topic requests
            foreach (var topic in ResponseClass.TopicResponses.Keys)
            {
                if (input.Contains(topic))
                {
                    lastTopic = topic;
                    AddToRecentTopics(topic);

                    string response = GetRandomResponseForTopic(topic);

                    if (!topicFollowUpMemory.ContainsKey(topic) ||
                        DateTime.Now - topicFollowUpMemory[topic] > followUpCooldown)
                    {
                        topicFollowUpMemory[topic] = DateTime.Now;
                        response += "\nWould you like to learn more?";
                    }

                    return response;
                }
            }

            // Handle "more"/"explain"/"elaborate" without specific topic
            if (input.Contains("more") || input.Contains("explain") || input.Contains("elaborate"))
            {
                if (!string.IsNullOrEmpty(lastTopic))
                    return GetRandomResponseForTopic(lastTopic);
                else if (recentTopics.Count > 0)
                    return GetRandomResponseForTopic(recentTopics.Peek());
                else
                    return GetRandomUnknownResponse();
            }
            
            if (input.Contains("what can i learn") || input.Contains("what can you teach") ||
            input.Contains("what can i ask") || input.Contains("what can you tell"))
            {
                return "I can help you with cybersecurity topics like phishing, safe browsing, passwords, scams, and more. " +
                       "I can also create and manage tasks with reminders, show recent topics, and even quiz you!";
            }

            // Handle recent topics request
            if (input.Contains("recent topics") || input.Contains("what did we talk about"))
            {
                if (recentTopics.Count == 0)
                    return "We haven't talked about much yet!";
                return "Here’s what we recently discussed:\n- " + string.Join("\n- ", recentTopics);
            }

            if (input.Contains("activity log") || input.Contains("what have i done") || input.Contains("show log"))
            {
                // Replace the line causing the error with the following:
                var recentLogs = activityLog.Skip(Math.Max(0, activityLog.Count - 5));
                return "Here’s a summary of recent actions:\n" + string.Join("\n", recentLogs) + "\nType 'Show more' to view the last 10 logs.\n";
            }

            if (input.Contains("show more"))
            {
                var moreLogs = activityLog.Skip(Math.Max(0, activityLog.Count - 10));
                return "Showing more logs:\n" + string.Join("\n", moreLogs);
            }

            // Handle enabling 2FA request
            string[] twoFaKeywords = { "enable 2fa", "turn on 2fa", "2fa settings" };
            if (twoFaKeywords.Any(keyword => input.Contains(keyword)))
                return "Two-Factor Authentication (2FA) helps protect your accounts. You can enable it via your account settings.";

            // Default unknown input response
            return GetRandomUnknownResponse();
        }
        #endregion

        #region Private Helpers

        private void AddToRecentTopics(string topic)
        {
            if (!recentTopics.Contains(topic))
            {
                if (recentTopics.Count >= MaxRecentTopics)
                    recentTopics.Dequeue();
                recentTopics.Enqueue(topic);
            }
        }

        private bool DetectSentiment(string input, out string response)
        {
            response = null;

            if (input.Contains("worried") || input.Contains("scared") || input.Contains("nervous"))
            {
                string topic = !string.IsNullOrEmpty(lastTopic) ? lastTopic : "";
                response = $"It's okay to feel that way, {userName}. Here's something that might help:";
                if (!string.IsNullOrEmpty(topic))
                    response += "\n" + GetRandomResponseForTopic(topic);
                return true;
            }

            if (input.Contains("curious"))
            {
                string topic = !string.IsNullOrEmpty(lastTopic) ? lastTopic : "";
                response = $"I love your curiosity! Let's dive into it.";
                if (!string.IsNullOrEmpty(topic))
                    response += "\n" + GetRandomResponseForTopic(topic);
                return true;
            }

            if (input.Contains("frustrated") || input.Contains("confused"))
            {
                string topic = !string.IsNullOrEmpty(lastTopic) ? lastTopic : "";
                response = $"Don’t worry. I’m here to help.";
                if (!string.IsNullOrEmpty(topic))
                    response += "\n" + GetRandomResponseForTopic(topic);
                return true;
            }

            return false;
        }

        private string HandleTaskCreationFlow(string input)
        {
            // Start task creation
            string[] addTaskKeywords = { "add task", "create a task", "make a task", "add a task", "create task", "make task" };
            if (addTaskKeywords.Any(k => input.Contains(k)) && !creatingTask)
            {
                creatingTask = true;
                waitingForTitle = true;
                return "What is the title of the task you'd like to add?";
            }

            if (creatingTask && waitingForTitle)
            {
                pendingTitle = input;
                waitingForTitle = false;
                waitingForDescription = true;
                return $"Got it! The title is '{pendingTitle}'. Now, what is the description?";
            }

            if (creatingTask && waitingForDescription)
            {
                pendingDescription = input;
                waitingForDescription = false;
                askReminderYesNo = true;
                return "Great. Would you like to set a reminder? (yes/no)";
            }

            if (creatingTask && askReminderYesNo)
            {
                if (input.Contains("yes") || input.Contains("yep"))
                {
                    askReminderYesNo = false;
                    waitingForReminder = true;
                    return "Please enter the date and time (e.g., 2025-06-01 14:00):";
                }
                else if (input.Contains("no") || input.Contains("not now") || input.Contains("nope"))
                {
                    askReminderYesNo = false;
                    waitingForReminder = false;
                    CompleteTaskCreation(null);
                    return "No reminder set. Task created successfully!";
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
                    return "That doesn't look like a valid date/time. Try again (e.g., 2025-06-01 14:00):";
                }
            }

            // Handling yes/no for creating task from reminder quick input
            if (askToCreateTaskFromReminder)
            {
                if (input.Contains("yes"))
                {
                    creatingTask = true;
                    waitingForTitle = false;
                    waitingForDescription = true;
                    pendingTitle = pendingTitle; // Already set from quick input
                    tempReminderFromQuickInput = tempReminderFromQuickInput; // Already set
                    askToCreateTaskFromReminder = false;
                    return "Please enter a description for the new task:";
                }
                else if (input.Contains("no"))
                {
                    askToCreateTaskFromReminder = false;
                    tempReminderFromQuickInput = null;
                    return "Okay, task creation cancelled.";
                }
            }

            // Handling waiting for reminder task title (when reminder only provided)
            if (waitingForReminderTaskTitle && pendingReminderOnly.HasValue)
            {
                string taskTitle = input.Trim();

                var existingTask = MainForm.Tasks.FirstOrDefault(t =>
                    t.Title.Equals(taskTitle, StringComparison.OrdinalIgnoreCase));

                if (existingTask != null)
                {
                    if (existingTask.Reminder == null)
                    {
                        existingTask.Reminder = pendingReminderOnly.Value;
                        waitingForReminderTaskTitle = false;
                        pendingReminderOnly = null;
                        return $"Reminder added to task \"{taskTitle}\" ✅";
                    }
                    else
                    {
                        waitingForReminderTaskTitle = false;
                        pendingReminderOnly = null;
                        return $"That task already has a reminder set for {existingTask.Reminder.Value:g}.";
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

        private string HandleQuickReminderInputs(string input)
        {
            // Regex patterns for quick reminder inputs
            var matchFull = System.Text.RegularExpressions.Regex.Match(input, @"remind me to (.+) at (\d{4}-\d{2}-\d{2} \d{2}:\d{2})");
            if (matchFull.Success)
            {
                string taskTitle = matchFull.Groups[1].Value.Trim();
                string dateTimeStr = matchFull.Groups[2].Value.Trim();

                if (DateTime.TryParse(dateTimeStr, out DateTime reminderTime))
                {
                    var existingTask = MainForm.Tasks.FirstOrDefault(t =>
                        t.Title.Equals(taskTitle, StringComparison.OrdinalIgnoreCase));

                    if (existingTask != null)
                    {
                        if (existingTask.Reminder == null)
                        {
                            existingTask.Reminder = reminderTime;
                            LogActivity($"Reminder set: '{existingTask.Title}' at {reminderTime:g}");
                            return $"Reminder added successfully to task \"{taskTitle}\" ✅";
                        }
                        else
                        {
                            return $"That task already has a reminder set for {existingTask.Reminder.Value:g}.";
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
                    return "That doesn't look like a valid date/time. Please use the format: yyyy-MM-dd HH:mm";
                }
            }

            var matchSetReminderFor = System.Text.RegularExpressions.Regex.Match(input, @"set a reminder for (.+) at (\d{4}-\d{2}-\d{2} \d{2}:\d{2})");
            if (matchSetReminderFor.Success)
            {
                string taskTitle = matchSetReminderFor.Groups[1].Value.Trim();
                string dateTimeStr = matchSetReminderFor.Groups[2].Value.Trim();

                if (DateTime.TryParse(dateTimeStr, out DateTime reminderTime))
                {
                    var existingTask = MainForm.Tasks.FirstOrDefault(t =>
                        t.Title.Equals(taskTitle, StringComparison.OrdinalIgnoreCase));

                    if (existingTask != null)
                    {
                        if (existingTask.Reminder == null)
                        {
                            existingTask.Reminder = reminderTime;
                            return $"Reminder added to task \"{taskTitle}\" ✅";
                        }
                        else
                        {
                            return $"Task \"{taskTitle}\" already has a reminder for {existingTask.Reminder.Value:g}.";
                        }
                    }
                    else
                    {
                        pendingTitle = taskTitle;
                        pendingDescription = "";
                        tempReminderFromQuickInput = reminderTime;
                        askToCreateTaskFromReminder = true;
                        return $"The task \"{taskTitle}\" doesn’t exist. Would you like to create it? (yes/no)";
                    }
                }
                else
                {
                    return "Invalid date/time format. Please use yyyy-MM-dd HH:mm.";
                }
            }

            // Match "set a reminder for (task name)" – no time specified yet
            var matchSetReminderForOnly = System.Text.RegularExpressions.Regex.Match(input, @"set a reminder for (.+)");
            if (matchSetReminderForOnly.Success && !input.Contains("at"))
            {
                string taskTitle = matchSetReminderForOnly.Groups[1].Value.Trim();

                var existingTask = MainForm.Tasks.FirstOrDefault(t =>
                    t.Title.Equals(taskTitle, StringComparison.OrdinalIgnoreCase));

                if (existingTask != null)
                {
                    if (existingTask.Reminder == null)
                    {
                        // Ask the user to enter the reminder date and time
                        pendingTitle = taskTitle;
                        waitingForReminder = true;
                        return $"Sure! Please enter the date and time for the reminder for \"{taskTitle}\" (e.g., 2025-07-01 09:00):";
                    }
                    else
                    {
                        return $"Task \"{taskTitle}\" already has a reminder set for {existingTask.Reminder.Value:g}.";
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

            var matchTimeOnly = System.Text.RegularExpressions.Regex.Match(input, @"set a reminder at (\d{4}-\d{2}-\d{2} \d{2}:\d{2})");
            if (matchTimeOnly.Success)
            {
                if (DateTime.TryParse(matchTimeOnly.Groups[1].Value.Trim(), out DateTime reminderTime))
                {
                    pendingReminderOnly = reminderTime;
                    waitingForReminderTaskTitle = true;
                    return "Sure! What task should I add this reminder to?";
                }
                else
                {
                    return "That doesn't look like a valid date/time. Use format: yyyy-MM-dd HH:mm";
                }
            }

            return null;
        }

        private string GetRandomResponseForTopic(string topic)
        {
            var responses = ResponseClass.TopicResponses[topic];
            return responses[random.Next(responses.Count)];
        }

        private string GetRandomUnknownResponse()
        {
            var responses = ResponseClass.UnknownInputResponses;
            return responses[random.Next(responses.Count)];
        }

        private void CompleteTaskCreation(DateTime? reminder)
        {
            reminder = reminder ?? tempReminderFromQuickInput;

            TaskItem task = new TaskItem(pendingTitle, pendingDescription, reminder);

            MainForm.AddTaskFromChat(task); 

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
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            string fullLog = $"{timestamp} - {logMessage}";

            activityLog.Add(fullLog);

            // Limit the list size
            if (activityLog.Count > MaxLogEntries)
                activityLog.RemoveAt(0); // Remove the oldest entry
        }

        #endregion
    }
}
