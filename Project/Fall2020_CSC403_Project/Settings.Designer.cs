﻿namespace Fall2020_CSC403_Project
{
    partial class Settings
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
            
            // 
            // Settings
            // New Windows Form is displayed in the center of the screen
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(this.WIDTH, this.HEIGHT);
            this.StartPosition = Program.FrmLevelInstance.StartPosition;
            this.Name = "Settings";
            this.Text = "Settings";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmLevel_KeyDown);
            this.ResumeLayout(false);


           
            // 
            // RestartButton
            // 
            this.RestartButton = new System.Windows.Forms.Button();
            this.RestartButton.Location = new System.Drawing.Point(this.WIDTH/5, this.HEIGHT/3);
            this.RestartButton.Name = "RestartButton";
            this.RestartButton.Size = new System.Drawing.Size(300, 150);
            this.RestartButton.TabIndex = 0;
            this.RestartButton.Text = "Restart";
            this.RestartButton.UseVisualStyleBackColor = true;
            this.Controls.Add(this.RestartButton);
            this.RestartButton.Click += new System.EventHandler(this.RestartButton_Click);

            //
            // UnpauseButton
            //
            this.UnpauseButton = new System.Windows.Forms.Button();
            this.UnpauseButton.Location = new System.Drawing.Point(this.WIDTH / 2, this.HEIGHT / 3);
            this.UnpauseButton.Name = "UnpauseButton";
            this.UnpauseButton.Size = new System.Drawing.Size(300, 150);
            this.UnpauseButton.TabIndex = 0;
            this.UnpauseButton.Text = "Unpause";
            this.UnpauseButton.UseVisualStyleBackColor = true;
            this.Controls.Add(this.UnpauseButton);
            this.UnpauseButton.Click += new System.EventHandler(this.UnpauseButton_Click);

            this.SuspendLayout();
        }

        #endregion

        private System.Windows.Forms.Button RestartButton;
        private System.Windows.Forms.Button UnpauseButton;
    }
}