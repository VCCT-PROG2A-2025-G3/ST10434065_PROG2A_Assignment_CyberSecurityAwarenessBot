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

namespace CyberSecurityAwarenessBotGUI
{
    // This class represents the quiz form where users can answer questions and see their scores.
    public partial class QuizForm: Form
    {
        #region Fields
        // Fields to hold the quiz questions, current question index, and score
        private List<QuizQuestion> quizQuestions;
        private int currentQuestionIndex = 0;
        private int score = 0;
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Quiz Form Constructor
        // Constructor for the QuizForm
        public QuizForm()
        {
            InitializeComponent(); // Initialize the form components
            quizQuestions = QuizManager.GetRandomQuestions(); // Load the quiz questions from the QuizManager
            LoadQuestion(); // Load the first question
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Load Question
        // Method to load the current question and its options into the form
        private void LoadQuestion()
        {

            if (currentQuestionIndex >= quizQuestions.Count) // Check if all questions have been answered
            {
                ShowFinalScore(); // Show the final score and close the quiz form
                MainForm.Instance?.chatBotWrapper?.LogActivity($"Quiz completed with a score of {score}/{quizQuestions.Count}"); // Log the quiz completion activity in the chatbot wrapper
                return; 
            }

            var question = quizQuestions[currentQuestionIndex]; // Get the current question from the list
            lblQuestion.Text = question.Question; // Set the question text in the label

            // Hide all radio buttons first
            rdoOption1.Visible = false;
            rdoOption2.Visible = false;
            rdoOption3.Visible = false;
            rdoOption4.Visible = false;

            // Clear selections
            rdoOption1.Checked = false;
            rdoOption2.Checked = false;
            rdoOption3.Checked = false;
            rdoOption4.Checked = false;

            // Dynamically show based on available options
            for (int i = 0; i < question.Options.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        rdoOption1.Text = question.Options[0];
                        rdoOption1.Visible = true;
                        break;
                    case 1:
                        rdoOption2.Text = question.Options[1];
                        rdoOption2.Visible = true;
                        break;
                    case 2:
                        rdoOption3.Text = question.Options[2];
                        rdoOption3.Visible = true;
                        break;
                    case 3:
                        rdoOption4.Text = question.Options[3];
                        rdoOption4.Visible = true;
                        break;
                }
            }

            lblExplanation.Visible = false; // Hide the explanation label initially
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Radio Button Checked Changed
        // This method is called when the radio button selection changes
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Explanation Label Click
        // This method is called when the explanation label is clicked
        private void lblExplanation_Click(object sender, EventArgs e) { }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Button Next Click
        // This method is called when the "Next" button is clicked
        private async void btnNext_Click(object sender, EventArgs e)
        {
            var question = quizQuestions[currentQuestionIndex]; // Get the current question

            int selectedIndex = -1; // Determine which radio button is selected
            if (rdoOption1.Checked) selectedIndex = 0; // Check if Option 1 is selected
            else if (rdoOption2.Checked) selectedIndex = 1; // Check if Option 2 is selected
            else if (rdoOption3.Checked) selectedIndex = 2; // Check if Option 3 is selected
            else if (rdoOption4.Checked) selectedIndex = 3; // Check if Option 4 is selected

            if (selectedIndex == -1) // If no option is selected
            {
                MessageBox.Show("Please select an answer before continuing."); // Show a message to the user
                return;
            }

            if (selectedIndex == question.CorrectIndex) // Check if the selected answer is correct
            {
                score++; // Increment the score
                lblExplanation.Text = "Correct! \n\n" + question.Explanation; // Show the explanation for the correct answer
            }
            else // If the answer is incorrect
            {
                lblExplanation.Text = $"Incorrect \nCorrect Answer: {question.Options[question.CorrectIndex]}\n\n{question.Explanation}"; // Show the explanation for the incorrect answer
            }
            lblExplanation.Visible = true; // Make the explanation label visible

            currentQuestionIndex++; // Move to the next question

            // Small delay before loading the next question
            await Task.Delay(5000);
            LoadQuestion();
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------//
        #region Button Exit Click
        // This method is called to show the final score and close the quiz form
        private void ShowFinalScore()
        {
            MessageBox.Show($"Quiz complete!\nYour score: {score} out of {quizQuestions.Count}", "Quiz Finished"); // Show final score in a message box
            this.Close(); // Close quiz form if you want
        }
        #endregion
    }
}
