namespace Final_Project_Game
{
    partial class GameWindow
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
            this.components = new System.ComponentModel.Container();
            this.GameLoop = new System.Windows.Forms.Timer(this.components);
            this.ScoreDisplay = new System.Windows.Forms.Panel();
            this.PlayerScore = new System.Windows.Forms.Label();
            this.Score = new System.Windows.Forms.Label();
            this.PauseOverlay = new System.Windows.Forms.Panel();
            this.Paused = new System.Windows.Forms.Label();
            this.PauseBtn = new System.Windows.Forms.PictureBox();
            this.ParentBox = new System.Windows.Forms.PictureBox();
            this.ScoreDisplay.SuspendLayout();
            this.PauseOverlay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PauseBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParentBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GameLoop
            // 
            this.GameLoop.Enabled = true;
            this.GameLoop.Interval = 60;
            this.GameLoop.Tick += new System.EventHandler(this.GameLoop_Tick);
            // 
            // ScoreDisplay
            // 
            this.ScoreDisplay.BackColor = System.Drawing.Color.Transparent;
            this.ScoreDisplay.BackgroundImage = global::Final_Project_Game.Properties.Resources.ScoreDisplay;
            this.ScoreDisplay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ScoreDisplay.Controls.Add(this.PlayerScore);
            this.ScoreDisplay.Controls.Add(this.Score);
            this.ScoreDisplay.Location = new System.Drawing.Point(496, 4);
            this.ScoreDisplay.Name = "ScoreDisplay";
            this.ScoreDisplay.Size = new System.Drawing.Size(183, 105);
            this.ScoreDisplay.TabIndex = 3;
            // 
            // PlayerScore
            // 
            this.PlayerScore.Font = new System.Drawing.Font("MS Reference Sans Serif", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerScore.ForeColor = System.Drawing.Color.Yellow;
            this.PlayerScore.Location = new System.Drawing.Point(40, 49);
            this.PlayerScore.Name = "PlayerScore";
            this.PlayerScore.Size = new System.Drawing.Size(100, 44);
            this.PlayerScore.TabIndex = 1;
            this.PlayerScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Score
            // 
            this.Score.AutoSize = true;
            this.Score.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Score.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Score.Location = new System.Drawing.Point(49, 16);
            this.Score.Name = "Score";
            this.Score.Size = new System.Drawing.Size(91, 26);
            this.Score.TabIndex = 0;
            this.Score.Text = "SCORE";
            // 
            // PauseOverlay
            // 
            this.PauseOverlay.BackColor = System.Drawing.Color.Transparent;
            this.PauseOverlay.BackgroundImage = global::Final_Project_Game.Properties.Resources.PauseOverlay;
            this.PauseOverlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PauseOverlay.Controls.Add(this.Paused);
            this.PauseOverlay.Font = new System.Drawing.Font("MS Reference Sans Serif", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PauseOverlay.ForeColor = System.Drawing.Color.Yellow;
            this.PauseOverlay.Location = new System.Drawing.Point(155, 99);
            this.PauseOverlay.Name = "PauseOverlay";
            this.PauseOverlay.Size = new System.Drawing.Size(364, 145);
            this.PauseOverlay.TabIndex = 2;
            this.PauseOverlay.Visible = false;
            // 
            // Paused
            // 
            this.Paused.AutoSize = true;
            this.Paused.Location = new System.Drawing.Point(72, 45);
            this.Paused.Name = "Paused";
            this.Paused.Size = new System.Drawing.Size(228, 60);
            this.Paused.TabIndex = 0;
            this.Paused.Text = "PAUSED";
            // 
            // PauseBtn
            // 
            this.PauseBtn.BackColor = System.Drawing.Color.Transparent;
            this.PauseBtn.BackgroundImage = global::Final_Project_Game.Properties.Resources.Pause_Button;
            this.PauseBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PauseBtn.Location = new System.Drawing.Point(12, 12);
            this.PauseBtn.Name = "PauseBtn";
            this.PauseBtn.Size = new System.Drawing.Size(43, 42);
            this.PauseBtn.TabIndex = 1;
            this.PauseBtn.TabStop = false;
            this.PauseBtn.Click += new System.EventHandler(this.PauseBtn_Click);
            // 
            // ParentBox
            // 
            this.ParentBox.BackColor = System.Drawing.Color.Transparent;
            this.ParentBox.Image = global::Final_Project_Game.Properties.Resources.Transparent_background;
            this.ParentBox.Location = new System.Drawing.Point(0, 0);
            this.ParentBox.Name = "ParentBox";
            this.ParentBox.Size = new System.Drawing.Size(700, 406);
            this.ParentBox.TabIndex = 4;
            this.ParentBox.TabStop = false;
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 367);
            this.Controls.Add(this.ScoreDisplay);
            this.Controls.Add(this.PauseOverlay);
            this.Controls.Add(this.PauseBtn);
            this.Controls.Add(this.ParentBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "GameWindow";
            this.Text = "Endless Runner";
            this.Load += new System.EventHandler(this.GameWindow_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameWindow_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ScoreDisplay.ResumeLayout(false);
            this.ScoreDisplay.PerformLayout();
            this.PauseOverlay.ResumeLayout(false);
            this.PauseOverlay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PauseBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParentBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer GameLoop;
        private System.Windows.Forms.Panel PauseOverlay;
        private System.Windows.Forms.Label Paused;
        private System.Windows.Forms.PictureBox PauseBtn;
        private System.Windows.Forms.Panel ScoreDisplay;
        private System.Windows.Forms.Label PlayerScore;
        private System.Windows.Forms.Label Score;
        private System.Windows.Forms.PictureBox ParentBox;
    }
}

