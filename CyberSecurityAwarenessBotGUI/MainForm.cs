/*
 * ST10434065 Seth Oliver
 * GROUP 3
 * PROGRAMMING 2A 
 * ASSIGNMENT POE 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CyberSecurityAwarenessBot; 


namespace CyberSecurityAwarenessBotGUI
{
    // This class represents the main form of the application where users can interact with the chatbot and manage tasks.
    public partial class MainForm: Form
    {
        #region Fields
        public static MainForm Instance; // Static reference to the form
        public ChatBotWinFormsWrapper chatBotWrapper; // Keep a consistent ChatBot session
        private TaskManager taskManager = new TaskManager(); // Task manager to handle tasks
        public static List<TaskItem> Tasks { get { return Instance?.taskManager.GetTasks(); } } // Static property to access tasks from anywhere in the application
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region MainForm Constructor
        // Constructor for the MainForm class
        public MainForm()
        {
            AudioClass.PlayStartupSound(); // Play the startup sound
            System.Threading.Thread.Sleep(500); // Wait for 0.5 seconds before playing the welcome message
            AudioClass.PlayWelcomeMessage(); // Play the welcome message
            InitializeComponent(); // Initialize the form components
            txtChatDisplay.AppendText("Bot: Hello there! What's your name?\n\n");
            chatBotWrapper = new ChatBotWinFormsWrapper(); // Initialize the chatbot logic
            Instance = this; // Set the static instance to this form
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Append User Message
        // Method to append a bot message to the chat display
        public void AppendBotMessage(string message)
        {
            txtChatDisplay.AppendText("Bot: " + message + "\n\n"); // Append the bot message to the chat display
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Add Task From Chat
        // This method is called to add a task to task list from the chat input
        public static void AddTaskFromChat(TaskItem task)
        {
            if (Instance != null) // Check if the instance of the MainForm exists
            {
                Instance.taskManager.GetTasks().Add(task); // Add the task to the task manager's list
                Instance.UpdateTaskList(); // Update the task list display in the GUI
            }
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region User Input Text Changed 
        // This method is called when the user types in the input text box
        private void txtUserInput_TextChanged(object sender, EventArgs e) { }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Button Send Click
        // This method is called when the user clicks the "Send" button
        private void btnSend_Click(object sender, EventArgs e)
        {
            string userInput = txtUserInput.Text.Trim(); // Get user input and trim whitespace
            if (string.IsNullOrEmpty(userInput)) // If input is empty, do nothing
                return; // Exit the method

            // Display user input
            txtChatDisplay.AppendText("You: " + userInput + "\n");

            // Get chatbot response
            string botResponse = chatBotWrapper.GetResponse(userInput);

            // Display bot response
            txtChatDisplay.AppendText("Bot: " + botResponse + "\n\n");

            txtUserInput.Clear(); // Clear input field
            txtUserInput.Focus(); // Set focus back for next input
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Remove Task Click
        // This method is called when the user clicks the "Remove Task" button
        private void btnRemoveTask_Click(object sender, EventArgs e)
        {
            int index = lstTasks.SelectedIndex; // Get the selected task index
            if (index >= 0) // If a task is selected
            {
                taskManager.RemoveTask(index); // Remove the task from the task manager
                UpdateTaskList(); // Update the task list display
            }
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Mark Task Done Click
        // This method is called when the user clicks the "Mark Done" button
        private void btnMarkDone_Click(object sender, EventArgs e)
        {
            int index = lstTasks.SelectedIndex; // Get the selected task index
            if (index >= 0) // If a task is selected
            {
                taskManager.MarkTaskComplete(index); // Mark the task as complete
                chatBotWrapper?.LogActivity($"Task marked complete: '{MainForm.Tasks[index].Title}'"); // Log the activity in the chatbot wrapper
                UpdateTaskList(); // Update the task list display
            }
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Clear Tasks Click
        // This method is called when the user clicks the "Clear Tasks" button
        private void btnClearTasks_Click(object sender, EventArgs e)
        {
            taskManager.ClearAll(); // Clear all tasks from the task manager
            UpdateTaskList(); // Update the task list display
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Update Task List
        // This method updates the task list display in the GUI
        private void UpdateTaskList()
        {
            lstTasks.Items.Clear(); // Clear the current task list display
            foreach (var task in taskManager.GetTasks()) // Iterate through each task in the task manager
            {
                lstTasks.Items.Add(task); // Add the task to the list box
            }
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region List Tasks Selected Index Changed
        // This method is called when the selected index of the task list changes
        private void lstTasks_SelectedIndexChanged(object sender, EventArgs e) { }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Task List Measure Item
        // This method is used to measure the height of each item in the task list
        private void lstTasks_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0) return; // If no item is selected, do nothing

            string text = lstTasks.Items[e.Index].ToString(); // Get the text of the item at the current index
            Font font = lstTasks.Font; // Get the font used for the list box

            SizeF size = e.Graphics.MeasureString(text, font, lstTasks.Width); // Measure the size of the text in the list box with the specified font and width
            e.ItemHeight = (int)size.Height + 10; // Set the item height to the measured height plus some padding
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Task List Draw Item
        // This method is used to draw each item in the task list
        private void lstTasks_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return; // If no item is selected, do nothing

            e.DrawBackground(); // Draw the background of the item
            string text = lstTasks.Items[e.Index].ToString(); // Get the text of the item at the current index
            using (SolidBrush brush = new SolidBrush(e.ForeColor)) // Create a brush with the foreground color
            {
                e.Graphics.DrawString(text, e.Font, brush, e.Bounds); // Draw the text of the item using the specified font and brush
            }
            e.DrawFocusRectangle(); // Draw the focus rectangle around the item if it is selected
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Button Start Quiz Click
        // This method is called when the user clicks the "Start Quiz" button
        private void btnStartQuiz_Click(object sender, EventArgs e)
        {
            QuizManager.Score = 0; // Reset the score before starting a new quiz
            chatBotWrapper?.LogActivity("Quiz started."); // Log the quiz start activity in the chatbot wrapper
            QuizForm quizForm = new QuizForm(); // Create a new instance of the QuizForm
            quizForm.ShowDialog(); // Show the quiz form as a modal dialog
        }
        #endregion
    }
}
