using System.Diagnostics.CodeAnalysis;
namespace Cataners
{
    partial class MainGui
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        [ExcludeFromCodeCoverage]
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainGui));
            this.createGameButton = new System.Windows.Forms.Button();
            this.joinGameButton = new System.Windows.Forms.Button();
            this.mainQuitButton = new System.Windows.Forms.Button();
            this.mainPictureBox = new System.Windows.Forms.PictureBox();
            this.usernameTextbox = new System.Windows.Forms.TextBox();
            this.passwordTextbox = new System.Windows.Forms.TextBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.loginButton = new System.Windows.Forms.Button();
            this.signUpButton = new System.Windows.Forms.Button();
            this.startWaveTestButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // createGameButton
            // 
            this.createGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createGameButton.ForeColor = System.Drawing.Color.Black;
            this.createGameButton.Location = new System.Drawing.Point(24, 567);
            this.createGameButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.createGameButton.Name = "createGameButton";
            this.createGameButton.Size = new System.Drawing.Size(236, 112);
            this.createGameButton.TabIndex = 1;
            this.createGameButton.Text = "Create Game";
            this.createGameButton.UseVisualStyleBackColor = true;
            this.createGameButton.Visible = false;
            this.createGameButton.Click += new System.EventHandler(this.createGameButton_Click);
            // 
            // joinGameButton
            // 
            this.joinGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.joinGameButton.Location = new System.Drawing.Point(281, 565);
            this.joinGameButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.joinGameButton.Name = "joinGameButton";
            this.joinGameButton.Size = new System.Drawing.Size(236, 112);
            this.joinGameButton.TabIndex = 2;
            this.joinGameButton.Text = "Join Game";
            this.joinGameButton.UseVisualStyleBackColor = true;
            this.joinGameButton.Visible = false;
            this.joinGameButton.Click += new System.EventHandler(this.joinGameButton_Click);
            // 
            // mainQuitButton
            // 
            this.mainQuitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainQuitButton.Location = new System.Drawing.Point(1060, 565);
            this.mainQuitButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainQuitButton.Name = "mainQuitButton";
            this.mainQuitButton.Size = new System.Drawing.Size(236, 112);
            this.mainQuitButton.TabIndex = 3;
            this.mainQuitButton.Text = "Exit";
            this.mainQuitButton.UseVisualStyleBackColor = true;
            this.mainQuitButton.Click += new System.EventHandler(this.mainQuitButton_Click);
            // 
            // mainPictureBox
            // 
            this.mainPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("mainPictureBox.Image")));
            this.mainPictureBox.Location = new System.Drawing.Point(359, 26);
            this.mainPictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainPictureBox.Name = "mainPictureBox";
            this.mainPictureBox.Size = new System.Drawing.Size(501, 193);
            this.mainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mainPictureBox.TabIndex = 0;
            this.mainPictureBox.TabStop = false;
            // 
            // usernameTextbox
            // 
            this.usernameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameTextbox.Location = new System.Drawing.Point(359, 326);
            this.usernameTextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.usernameTextbox.Name = "usernameTextbox";
            this.usernameTextbox.Size = new System.Drawing.Size(499, 27);
            this.usernameTextbox.TabIndex = 4;
            // 
            // passwordTextbox
            // 
            this.passwordTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordTextbox.Location = new System.Drawing.Point(359, 420);
            this.passwordTextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.passwordTextbox.Name = "passwordTextbox";
            this.passwordTextbox.Size = new System.Drawing.Size(499, 27);
            this.passwordTextbox.TabIndex = 5;
            this.passwordTextbox.UseSystemPasswordChar = true;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.Location = new System.Drawing.Point(433, 298);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(130, 29);
            this.usernameLabel.TabIndex = 6;
            this.usernameLabel.Text = "Username:";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordLabel.Location = new System.Drawing.Point(433, 391);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(126, 29);
            this.passwordLabel.TabIndex = 7;
            this.passwordLabel.Text = "Password:";
            // 
            // loginButton
            // 
            this.loginButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginButton.Location = new System.Drawing.Point(387, 465);
            this.loginButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(204, 64);
            this.loginButton.TabIndex = 8;
            this.loginButton.Text = "Log In";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // signUpButton
            // 
            this.signUpButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signUpButton.Location = new System.Drawing.Point(633, 465);
            this.signUpButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.signUpButton.Name = "signUpButton";
            this.signUpButton.Size = new System.Drawing.Size(204, 64);
            this.signUpButton.TabIndex = 9;
            this.signUpButton.Text = "Sign Up";
            this.signUpButton.UseVisualStyleBackColor = true;
            this.signUpButton.Click += new System.EventHandler(this.signUpButton_Click);
            // 
            // startWaveTestButton
            // 
            this.startWaveTestButton.Location = new System.Drawing.Point(1149, 14);
            this.startWaveTestButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.startWaveTestButton.Name = "startWaveTestButton";
            this.startWaveTestButton.Size = new System.Drawing.Size(147, 63);
            this.startWaveTestButton.TabIndex = 10;
            this.startWaveTestButton.Text = "Start Wave Test";
            this.startWaveTestButton.UseVisualStyleBackColor = true;
            this.startWaveTestButton.Click += new System.EventHandler(this.startWaveTestButton_Click);
            // 
            // MainGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(1312, 692);
            this.Controls.Add(this.startWaveTestButton);
            this.Controls.Add(this.signUpButton);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.passwordTextbox);
            this.Controls.Add(this.usernameTextbox);
            this.Controls.Add(this.mainQuitButton);
            this.Controls.Add(this.joinGameButton);
            this.Controls.Add(this.createGameButton);
            this.Controls.Add(this.mainPictureBox);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainGui";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainGui";
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mainPictureBox;
        private System.Windows.Forms.Button createGameButton;
        private System.Windows.Forms.Button joinGameButton;
        private System.Windows.Forms.Button mainQuitButton;
        private System.Windows.Forms.TextBox usernameTextbox;
        private System.Windows.Forms.TextBox passwordTextbox;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Button signUpButton;
        private System.Windows.Forms.Button startWaveTestButton;

    }
}