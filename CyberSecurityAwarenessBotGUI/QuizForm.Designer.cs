namespace CyberSecurityAwarenessBotGUI
{
    partial class QuizForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.rdoOption1 = new System.Windows.Forms.RadioButton();
            this.rdoOption2 = new System.Windows.Forms.RadioButton();
            this.rdoOption3 = new System.Windows.Forms.RadioButton();
            this.rdoOption4 = new System.Windows.Forms.RadioButton();
            this.lblExplanation = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.grpOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(246, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cybersecurity Quiz";
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion.Location = new System.Drawing.Point(12, 60);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(113, 25);
            this.lblQuestion.TabIndex = 1;
            this.lblQuestion.Text = "Question:";
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.rdoOption4);
            this.grpOptions.Controls.Add(this.rdoOption3);
            this.grpOptions.Controls.Add(this.rdoOption2);
            this.grpOptions.Controls.Add(this.rdoOption1);
            this.grpOptions.Location = new System.Drawing.Point(17, 118);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(492, 126);
            this.grpOptions.TabIndex = 2;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // rdoOption1
            // 
            this.rdoOption1.AutoSize = true;
            this.rdoOption1.Location = new System.Drawing.Point(7, 20);
            this.rdoOption1.Name = "rdoOption1";
            this.rdoOption1.Size = new System.Drawing.Size(85, 17);
            this.rdoOption1.TabIndex = 0;
            this.rdoOption1.TabStop = true;
            this.rdoOption1.Text = "radioButton1";
            this.rdoOption1.UseVisualStyleBackColor = true;
            this.rdoOption1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rdoOption2
            // 
            this.rdoOption2.AutoSize = true;
            this.rdoOption2.Location = new System.Drawing.Point(7, 44);
            this.rdoOption2.Name = "rdoOption2";
            this.rdoOption2.Size = new System.Drawing.Size(85, 17);
            this.rdoOption2.TabIndex = 1;
            this.rdoOption2.TabStop = true;
            this.rdoOption2.Text = "radioButton2";
            this.rdoOption2.UseVisualStyleBackColor = true;
            // 
            // rdoOption3
            // 
            this.rdoOption3.AutoSize = true;
            this.rdoOption3.Location = new System.Drawing.Point(7, 68);
            this.rdoOption3.Name = "rdoOption3";
            this.rdoOption3.Size = new System.Drawing.Size(85, 17);
            this.rdoOption3.TabIndex = 2;
            this.rdoOption3.TabStop = true;
            this.rdoOption3.Text = "radioButton3";
            this.rdoOption3.UseVisualStyleBackColor = true;
            // 
            // rdoOption4
            // 
            this.rdoOption4.AutoSize = true;
            this.rdoOption4.Location = new System.Drawing.Point(7, 92);
            this.rdoOption4.Name = "rdoOption4";
            this.rdoOption4.Size = new System.Drawing.Size(85, 17);
            this.rdoOption4.TabIndex = 3;
            this.rdoOption4.TabStop = true;
            this.rdoOption4.Text = "radioButton4";
            this.rdoOption4.UseVisualStyleBackColor = true;
            // 
            // lblExplanation
            // 
            this.lblExplanation.AutoSize = true;
            this.lblExplanation.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExplanation.ForeColor = System.Drawing.Color.Blue;
            this.lblExplanation.Location = new System.Drawing.Point(12, 258);
            this.lblExplanation.Name = "lblExplanation";
            this.lblExplanation.Size = new System.Drawing.Size(125, 25);
            this.lblExplanation.TabIndex = 3;
            this.lblExplanation.Text = "Explanation";
            this.lblExplanation.Visible = false;
            this.lblExplanation.Click += new System.EventHandler(this.lblExplanation_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(713, 415);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // QuizForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lblExplanation);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.label1);
            this.Name = "QuizForm";
            this.Text = "Form1";
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.RadioButton rdoOption4;
        private System.Windows.Forms.RadioButton rdoOption3;
        private System.Windows.Forms.RadioButton rdoOption2;
        private System.Windows.Forms.RadioButton rdoOption1;
        private System.Windows.Forms.Label lblExplanation;
        private System.Windows.Forms.Button btnNext;
    }
}