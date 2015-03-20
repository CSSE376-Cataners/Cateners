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
            this.heyButton = new System.Windows.Forms.Button();
            this.yoButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // heyButton
            // 
            this.heyButton.Location = new System.Drawing.Point(13, 13);
            this.heyButton.Name = "heyButton";
            this.heyButton.Size = new System.Drawing.Size(75, 23);
            this.heyButton.TabIndex = 0;
            this.heyButton.Text = "Hey";
            this.heyButton.UseVisualStyleBackColor = true;
            // 
            // yoButton
            // 
            this.yoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.yoButton.Location = new System.Drawing.Point(197, 12);
            this.yoButton.Name = "yoButton";
            this.yoButton.Size = new System.Drawing.Size(75, 23);
            this.yoButton.TabIndex = 1;
            this.yoButton.Text = "Yo";
            this.yoButton.UseVisualStyleBackColor = true;
            // 
            // MainGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.yoButton);
            this.Controls.Add(this.heyButton);
            this.Name = "MainGui";
            this.Text = "MainGui";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button heyButton;
        private System.Windows.Forms.Button yoButton;
    }
}