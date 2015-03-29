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
            this.mainCreateGameButton = new System.Windows.Forms.Button();
            this.mainJoinGameButton = new System.Windows.Forms.Button();
            this.mainQuitButton = new System.Windows.Forms.Button();
            this.mainPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mainCreateGameButton
            // 
            this.mainCreateGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainCreateGameButton.Location = new System.Drawing.Point(17, 565);
            this.mainCreateGameButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainCreateGameButton.Name = "mainCreateGameButton";
            this.mainCreateGameButton.Size = new System.Drawing.Size(236, 112);
            this.mainCreateGameButton.TabIndex = 1;
            this.mainCreateGameButton.Text = "Create Game";
            this.mainCreateGameButton.UseVisualStyleBackColor = true;
            // 
            // mainJoinGameButton
            // 
            this.mainJoinGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainJoinGameButton.Location = new System.Drawing.Point(261, 565);
            this.mainJoinGameButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainJoinGameButton.Name = "mainJoinGameButton";
            this.mainJoinGameButton.Size = new System.Drawing.Size(236, 112);
            this.mainJoinGameButton.TabIndex = 2;
            this.mainJoinGameButton.Text = "Join Game";
            this.mainJoinGameButton.UseVisualStyleBackColor = true;
            this.mainJoinGameButton.Click += new System.EventHandler(this.mainJoinGameButton_Click);
            // 
            // mainQuitButton
            // 
            this.mainQuitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.mainPictureBox.Location = new System.Drawing.Point(311, 15);
            this.mainPictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainPictureBox.Name = "mainPictureBox";
            this.mainPictureBox.Size = new System.Drawing.Size(667, 234);
            this.mainPictureBox.TabIndex = 0;
            this.mainPictureBox.TabStop = false;
            // 
            // MainGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 692);
            this.Controls.Add(this.mainQuitButton);
            this.Controls.Add(this.mainJoinGameButton);
            this.Controls.Add(this.mainCreateGameButton);
            this.Controls.Add(this.mainPictureBox);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainGui";
            this.Text = "MainGui";
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox mainPictureBox;
        private System.Windows.Forms.Button mainCreateGameButton;
        private System.Windows.Forms.Button mainJoinGameButton;
        private System.Windows.Forms.Button mainQuitButton;

    }
}