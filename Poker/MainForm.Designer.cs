namespace Poker
{
    partial class Form1
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
            this.playButton = new System.Windows.Forms.Button();
            this.player1ResultLabel = new System.Windows.Forms.Label();
            this.player1Card1 = new System.Windows.Forms.PictureBox();
            this.player1Card2 = new System.Windows.Forms.PictureBox();
            this.player2Card2 = new System.Windows.Forms.PictureBox();
            this.player2Card1 = new System.Windows.Forms.PictureBox();
            this.player2ResultLabel = new System.Windows.Forms.Label();
            this.gameResultLabel = new System.Windows.Forms.Label();
            this.flopCard1 = new System.Windows.Forms.PictureBox();
            this.flopCard2 = new System.Windows.Forms.PictureBox();
            this.flopCard3 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flopLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flopCard4 = new System.Windows.Forms.PictureBox();
            this.flopCard5 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.player1Card1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player1Card2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player2Card2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player2Card1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flopCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flopCard2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flopCard3)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flopCard4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flopCard5)).BeginInit();
            this.SuspendLayout();
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(0, 0);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(75, 23);
            this.playButton.TabIndex = 0;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // player1ResultLabel
            // 
            this.player1ResultLabel.AutoSize = true;
            this.player1ResultLabel.Location = new System.Drawing.Point(20, 29);
            this.player1ResultLabel.Name = "player1ResultLabel";
            this.player1ResultLabel.Size = new System.Drawing.Size(0, 13);
            this.player1ResultLabel.TabIndex = 1;
            // 
            // player1Card1
            // 
            this.player1Card1.Location = new System.Drawing.Point(23, 59);
            this.player1Card1.Name = "player1Card1";
            this.player1Card1.Size = new System.Drawing.Size(72, 96);
            this.player1Card1.TabIndex = 2;
            this.player1Card1.TabStop = false;
            // 
            // player1Card2
            // 
            this.player1Card2.Location = new System.Drawing.Point(114, 59);
            this.player1Card2.Name = "player1Card2";
            this.player1Card2.Size = new System.Drawing.Size(72, 96);
            this.player1Card2.TabIndex = 3;
            this.player1Card2.TabStop = false;
            // 
            // player2Card2
            // 
            this.player2Card2.Location = new System.Drawing.Point(113, 63);
            this.player2Card2.Name = "player2Card2";
            this.player2Card2.Size = new System.Drawing.Size(72, 96);
            this.player2Card2.TabIndex = 8;
            this.player2Card2.TabStop = false;
            // 
            // player2Card1
            // 
            this.player2Card1.Location = new System.Drawing.Point(23, 63);
            this.player2Card1.Name = "player2Card1";
            this.player2Card1.Size = new System.Drawing.Size(72, 96);
            this.player2Card1.TabIndex = 7;
            this.player2Card1.TabStop = false;
            // 
            // player2ResultLabel
            // 
            this.player2ResultLabel.AutoSize = true;
            this.player2ResultLabel.Location = new System.Drawing.Point(20, 30);
            this.player2ResultLabel.Name = "player2ResultLabel";
            this.player2ResultLabel.Size = new System.Drawing.Size(0, 13);
            this.player2ResultLabel.TabIndex = 12;
            // 
            // gameResultLabel
            // 
            this.gameResultLabel.AutoSize = true;
            this.gameResultLabel.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameResultLabel.Location = new System.Drawing.Point(525, 293);
            this.gameResultLabel.Name = "gameResultLabel";
            this.gameResultLabel.Size = new System.Drawing.Size(0, 32);
            this.gameResultLabel.TabIndex = 13;
            // 
            // flopCard1
            // 
            this.flopCard1.Location = new System.Drawing.Point(23, 42);
            this.flopCard1.Name = "flopCard1";
            this.flopCard1.Size = new System.Drawing.Size(72, 96);
            this.flopCard1.TabIndex = 14;
            this.flopCard1.TabStop = false;
            // 
            // flopCard2
            // 
            this.flopCard2.Location = new System.Drawing.Point(113, 42);
            this.flopCard2.Name = "flopCard2";
            this.flopCard2.Size = new System.Drawing.Size(72, 96);
            this.flopCard2.TabIndex = 15;
            this.flopCard2.TabStop = false;
            // 
            // flopCard3
            // 
            this.flopCard3.Location = new System.Drawing.Point(203, 42);
            this.flopCard3.Name = "flopCard3";
            this.flopCard3.Size = new System.Drawing.Size(72, 96);
            this.flopCard3.TabIndex = 16;
            this.flopCard3.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImage = global::Poker.Properties.Resources.greenfelt;
            this.groupBox1.Controls.Add(this.player1Card2);
            this.groupBox1.Controls.Add(this.player1Card1);
            this.groupBox1.Controls.Add(this.player1ResultLabel);
            this.groupBox1.Location = new System.Drawing.Point(15, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(484, 210);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Player 1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Controls.Add(this.flopCard5);
            this.panel1.Controls.Add(this.flopCard4);
            this.panel1.Controls.Add(this.flopLabel);
            this.panel1.Controls.Add(this.flopCard3);
            this.panel1.Controls.Add(this.flopCard2);
            this.panel1.Controls.Add(this.flopCard1);
            this.panel1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Location = new System.Drawing.Point(15, 251);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(487, 177);
            this.panel1.TabIndex = 17;
            this.panel1.Tag = "sdf";
            // 
            // flopLabel
            // 
            this.flopLabel.AutoSize = true;
            this.flopLabel.Location = new System.Drawing.Point(20, 11);
            this.flopLabel.Name = "flopLabel";
            this.flopLabel.Size = new System.Drawing.Size(27, 13);
            this.flopLabel.TabIndex = 18;
            this.flopLabel.Text = "Flop";
            // 
            // groupBox2
            // 
            this.groupBox2.BackgroundImage = global::Poker.Properties.Resources.greenfelt;
            this.groupBox2.Controls.Add(this.player2Card1);
            this.groupBox2.Controls.Add(this.player2Card2);
            this.groupBox2.Controls.Add(this.player2ResultLabel);
            this.groupBox2.Location = new System.Drawing.Point(15, 428);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(484, 210);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Player 2";
            // 
            // flopCard4
            // 
            this.flopCard4.Location = new System.Drawing.Point(293, 42);
            this.flopCard4.Name = "flopCard4";
            this.flopCard4.Size = new System.Drawing.Size(72, 96);
            this.flopCard4.TabIndex = 19;
            this.flopCard4.TabStop = false;
            // 
            // flopCard5
            // 
            this.flopCard5.Location = new System.Drawing.Point(383, 42);
            this.flopCard5.Name = "flopCard5";
            this.flopCard5.Size = new System.Drawing.Size(72, 96);
            this.flopCard5.TabIndex = 20;
            this.flopCard5.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Poker.Properties.Resources.greenfelt;
            this.ClientSize = new System.Drawing.Size(784, 671);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gameResultLabel);
            this.Controls.Add(this.playButton);
            this.Name = "Form1";
            this.Text = "Poker";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.player1Card1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player1Card2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player2Card2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player2Card1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flopCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flopCard2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flopCard3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flopCard4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flopCard5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Label player1ResultLabel;
        private System.Windows.Forms.PictureBox player1Card1;
        private System.Windows.Forms.PictureBox player1Card2;
        private System.Windows.Forms.PictureBox player2Card2;
        private System.Windows.Forms.PictureBox player2Card1;
        private System.Windows.Forms.Label player2ResultLabel;
        private System.Windows.Forms.Label gameResultLabel;
        private System.Windows.Forms.PictureBox flopCard1;
        private System.Windows.Forms.PictureBox flopCard2;
        private System.Windows.Forms.PictureBox flopCard3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label flopLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox flopCard5;
        private System.Windows.Forms.PictureBox flopCard4;
    }
}

