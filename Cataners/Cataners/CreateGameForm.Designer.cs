namespace Cataners
{
    partial class CreateGameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateGameForm));
            this.createGameButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gameNameTextBox = new System.Windows.Forms.TextBox();
            this.gameNameLabel = new System.Windows.Forms.Label();
            this.timePerTurnLabel = new System.Windows.Forms.Label();
            this.maxTimeComboBox = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // createGameButton
            // 
            this.createGameButton.Font = new System.Drawing.Font("Britannic Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createGameButton.Location = new System.Drawing.Point(562, 357);
            this.createGameButton.Name = "createGameButton";
            this.createGameButton.Size = new System.Drawing.Size(165, 62);
            this.createGameButton.TabIndex = 0;
            this.createGameButton.Text = "Create Game";
            this.createGameButton.UseVisualStyleBackColor = true;
            this.createGameButton.Click += new System.EventHandler(this.createGameButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Britannic Bold", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(277, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "Create Game";
            // 
            // gameNameTextBox
            // 
            this.gameNameTextBox.Font = new System.Drawing.Font("Britannic Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameNameTextBox.Location = new System.Drawing.Point(214, 164);
            this.gameNameTextBox.Name = "gameNameTextBox";
            this.gameNameTextBox.Size = new System.Drawing.Size(295, 33);
            this.gameNameTextBox.TabIndex = 2;
            // 
            // gameNameLabel
            // 
            this.gameNameLabel.AutoSize = true;
            this.gameNameLabel.Font = new System.Drawing.Font("Britannic Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameNameLabel.Location = new System.Drawing.Point(70, 164);
            this.gameNameLabel.Name = "gameNameLabel";
            this.gameNameLabel.Size = new System.Drawing.Size(138, 27);
            this.gameNameLabel.TabIndex = 4;
            this.gameNameLabel.Text = "Game Name";
            // 
            // timePerTurnLabel
            // 
            this.timePerTurnLabel.AutoSize = true;
            this.timePerTurnLabel.Font = new System.Drawing.Font("Britannic Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timePerTurnLabel.Location = new System.Drawing.Point(28, 260);
            this.timePerTurnLabel.Name = "timePerTurnLabel";
            this.timePerTurnLabel.Size = new System.Drawing.Size(501, 27);
            this.timePerTurnLabel.TabIndex = 5;
            this.timePerTurnLabel.Text = "Maximum Amount of Time (minutes) Per Turn";
            // 
            // maxTimeComboBox
            // 
            this.maxTimeComboBox.Font = new System.Drawing.Font("Britannic Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maxTimeComboBox.FormattingEnabled = true;
            this.maxTimeComboBox.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "No Time Limit"});
            this.maxTimeComboBox.Location = new System.Drawing.Point(553, 257);
            this.maxTimeComboBox.Name = "maxTimeComboBox";
            this.maxTimeComboBox.Size = new System.Drawing.Size(199, 34);
            this.maxTimeComboBox.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(562, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(190, 121);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // CreateGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LawnGreen;
            this.ClientSize = new System.Drawing.Size(787, 452);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.maxTimeComboBox);
            this.Controls.Add(this.timePerTurnLabel);
            this.Controls.Add(this.gameNameLabel);
            this.Controls.Add(this.gameNameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.createGameButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateGameForm";
            this.Text = "Create Game";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createGameButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox gameNameTextBox;
        private System.Windows.Forms.Label gameNameLabel;
        private System.Windows.Forms.Label timePerTurnLabel;
        private System.Windows.Forms.ComboBox maxTimeComboBox;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}