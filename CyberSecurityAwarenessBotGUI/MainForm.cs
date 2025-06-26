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
    public partial class MainForm: Form
    {
        #region Fields
        public static MainForm Instance; // Static reference to the form
        private ChatBotWinFormsWrapper chatBotWrapper; // Keep a consistent ChatBot session
        private TaskManager taskManager = new TaskManager(); // Task manager to handle tasks
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region MainForm Constructor
        // Constructor for the MainForm class
        public MainForm()
        {
            AudioClass.PlayStartupSound(); // Play the startup sound
            System.Threading.Thread.Sleep(500); // Wait for 0.5 seconds before playing the welcome message
            AudioClass.PlayWelcomeMessage();
            InitializeComponent(); // Initialize the form components
            chatBotWrapper = new ChatBotWinFormsWrapper(); // Initialize the chatbot logic
            Instance = this; // Set the static instance to this form
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Add Task From Chat
        // This method is called to add a task to task list from the chat input
        public static void AddTaskFromChat(TaskItem task)
        {
            if (Instance != null)
            {
                Instance.taskManager.GetTasks().Add(task); // Add the task to the task manager's list
                Instance.UpdateTaskList(); // Update the task list display in the GUI
            }
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region User Input Text Changed 
        private void txtUserInput_TextChanged(object sender, EventArgs e)
        {

        }
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
        private void lstTasks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        private void lstTasks_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0) return;

            string text = lstTasks.Items[e.Index].ToString();
            Font font = lstTasks.Font;

            SizeF size = e.Graphics.MeasureString(text, font, lstTasks.Width);
            e.ItemHeight = (int)size.Height + 10;
        }

        private void lstTasks_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();
            string text = lstTasks.Items[e.Index].ToString();
            using (SolidBrush brush = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void btnStartQuiz_Click(object sender, EventArgs e)
        {
            QuizManager.Score = 0; // Reset the score before starting a new quiz
            QuizForm quizForm = new QuizForm(); // Create a new instance of the QuizForm
            quizForm.ShowDialog(); // Show the quiz form as a modal dialog
        }
    }
}
