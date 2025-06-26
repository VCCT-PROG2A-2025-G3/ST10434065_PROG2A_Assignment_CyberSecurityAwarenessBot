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
    public partial class QuizForm: Form
    {
        private List<QuizQuestion> quizQuestions;
        private int currentQuestionIndex = 0;
        private int score = 0;
        
        public QuizForm()
        {
            InitializeComponent();
            quizQuestions = QuizManager.GetRandomQuestions(); // Load the quiz questions from the QuizManager
            LoadQuestion();
        }

        private void LoadQuestion()
        {

            if (currentQuestionIndex >= quizQuestions.Count)
            {
                ShowFinalScore();
                MainForm.Instance?.chatBotWrapper?.LogActivity($"Quiz completed with a score of {score}/{quizQuestions.Count}");
                return;
            }

            var question = quizQuestions[currentQuestionIndex];
            lblQuestion.Text = question.Question;

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

            lblExplanation.Visible = false;
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lblExplanation_Click(object sender, EventArgs e)
        {

        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            var question = quizQuestions[currentQuestionIndex];

            int selectedIndex = -1;
            if (rdoOption1.Checked) selectedIndex = 0;
            else if (rdoOption2.Checked) selectedIndex = 1;
            else if (rdoOption3.Checked) selectedIndex = 2;
            else if (rdoOption4.Checked) selectedIndex = 3;

            if (selectedIndex == -1)
            {
                MessageBox.Show("Please select an answer before continuing.");
                return;
            }

            if (selectedIndex == question.CorrectIndex)
            {
                score++;
                lblExplanation.Text = "Correct! \n\n" + question.Explanation;
            }
            else 
            {
                lblExplanation.Text = $"Incorrect \nCorrect Answer: {question.Options[question.CorrectIndex]}\n\n{question.Explanation}";
            }
            lblExplanation.Visible = true;

            currentQuestionIndex++;
                
            // Small delay before loading the next question
            await Task.Delay(5000);
            LoadQuestion();
        }

        private void ShowFinalScore()
        {
            MessageBox.Show($"Quiz complete!\nYour score: {score} out of {quizQuestions.Count}", "Quiz Finished"); // Show final score in a message box
            this.Close(); // Close quiz form if you want
        }
    }
}
