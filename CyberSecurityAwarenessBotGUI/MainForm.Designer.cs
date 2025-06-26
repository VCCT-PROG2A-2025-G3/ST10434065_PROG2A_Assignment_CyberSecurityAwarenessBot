namespace CyberSecurityAwarenessBotGUI
{
    partial class MainForm
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
            this.txtChatDisplay = new System.Windows.Forms.RichTextBox();
            this.txtUserInput = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.grpTaskManager = new System.Windows.Forms.GroupBox();
            this.btnClearTasks = new System.Windows.Forms.Button();
            this.btnRemoveTask = new System.Windows.Forms.Button();
            this.btnMarkDone = new System.Windows.Forms.Button();
            this.lstTasks = new System.Windows.Forms.ListBox();
            this.btnStartQuiz = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpTaskManager.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtChatDisplay
            // 
            this.txtChatDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChatDisplay.Location = new System.Drawing.Point(3, 2);
            this.txtChatDisplay.Name = "txtChatDisplay";
            this.txtChatDisplay.ReadOnly = true;
            this.txtChatDisplay.Size = new System.Drawing.Size(549, 410);
            this.txtChatDisplay.TabIndex = 0;
            this.txtChatDisplay.Text = "";
            // 
            // txtUserInput
            // 
            this.txtUserInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserInput.Location = new System.Drawing.Point(3, 418);
            this.txtUserInput.Name = "txtUserInput";
            this.txtUserInput.Size = new System.Drawing.Size(549, 20);
            this.txtUserInput.TabIndex = 0;
            this.txtUserInput.TextChanged += new System.EventHandler(this.txtUserInput_TextChanged);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(558, 418);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 20);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // grpTaskManager
            // 
            this.grpTaskManager.Controls.Add(this.btnClearTasks);
            this.grpTaskManager.Controls.Add(this.btnRemoveTask);
            this.grpTaskManager.Controls.Add(this.btnMarkDone);
            this.grpTaskManager.Controls.Add(this.lstTasks);
            this.grpTaskManager.Location = new System.Drawing.Point(558, 2);
            this.grpTaskManager.Name = "grpTaskManager";
            this.grpTaskManager.Size = new System.Drawing.Size(231, 240);
            this.grpTaskManager.TabIndex = 2;
            this.grpTaskManager.TabStop = false;
            this.grpTaskManager.Text = "Task Manager";
            // 
            // btnClearTasks
            // 
            this.btnClearTasks.Location = new System.Drawing.Point(6, 205);
            this.btnClearTasks.Name = "btnClearTasks";
            this.btnClearTasks.Size = new System.Drawing.Size(92, 23);
            this.btnClearTasks.TabIndex = 5;
            this.btnClearTasks.Text = "Clear All Tasks";
            this.btnClearTasks.UseVisualStyleBackColor = true;
            this.btnClearTasks.Click += new System.EventHandler(this.btnClearTasks_Click);
            // 
            // btnRemoveTask
            // 
            this.btnRemoveTask.Location = new System.Drawing.Point(94, 176);
            this.btnRemoveTask.Name = "btnRemoveTask";
            this.btnRemoveTask.Size = new System.Drawing.Size(82, 23);
            this.btnRemoveTask.TabIndex = 4;
            this.btnRemoveTask.Text = "Remove Task";
            this.btnRemoveTask.UseVisualStyleBackColor = true;
            this.btnRemoveTask.Click += new System.EventHandler(this.btnRemoveTask_Click);
            // 
            // btnMarkDone
            // 
            this.btnMarkDone.Location = new System.Drawing.Point(6, 176);
            this.btnMarkDone.Name = "btnMarkDone";
            this.btnMarkDone.Size = new System.Drawing.Size(82, 23);
            this.btnMarkDone.TabIndex = 3;
            this.btnMarkDone.Text = "Mark as Done";
            this.btnMarkDone.UseVisualStyleBackColor = true;
            this.btnMarkDone.Click += new System.EventHandler(this.btnMarkDone_Click);
            // 
            // lstTasks
            // 
            this.lstTasks.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lstTasks.FormattingEnabled = true;
            this.lstTasks.Location = new System.Drawing.Point(6, 22);
            this.lstTasks.Name = "lstTasks";
            this.lstTasks.Size = new System.Drawing.Size(219, 147);
            this.lstTasks.TabIndex = 2;
            this.lstTasks.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstTasks_DrawItem);
            this.lstTasks.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lstTasks_MeasureItem);
            this.lstTasks.SelectedIndexChanged += new System.EventHandler(this.lstTasks_SelectedIndexChanged);
            // 
            // btnStartQuiz
            // 
            this.btnStartQuiz.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartQuiz.Location = new System.Drawing.Point(6, 12);
            this.btnStartQuiz.Name = "btnStartQuiz";
            this.btnStartQuiz.Size = new System.Drawing.Size(117, 38);
            this.btnStartQuiz.TabIndex = 3;
            this.btnStartQuiz.Text = "Start Quiz";
            this.btnStartQuiz.UseVisualStyleBackColor = true;
            this.btnStartQuiz.Click += new System.EventHandler(this.btnStartQuiz_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStartQuiz);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(559, 248);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 58);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Click here to start the quiz!";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpTaskManager);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtUserInput);
            this.Controls.Add(this.txtChatDisplay);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.grpTaskManager.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtChatDisplay;
        private System.Windows.Forms.TextBox txtUserInput;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.GroupBox grpTaskManager;
        private System.Windows.Forms.ListBox lstTasks;
        private System.Windows.Forms.Button btnRemoveTask;
        private System.Windows.Forms.Button btnMarkDone;
        private System.Windows.Forms.Button btnClearTasks;
        private System.Windows.Forms.Button btnStartQuiz;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

