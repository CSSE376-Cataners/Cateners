namespace Cataners
{
    partial class ChatBox
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
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.textEntryBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(3, 3);
            this.richTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(644, 206);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            // 
            // textEntryBox
            // 
            this.textEntryBox.AcceptsReturn = true;
            this.textEntryBox.Location = new System.Drawing.Point(3, 213);
            this.textEntryBox.Multiline = true;
            this.textEntryBox.Name = "textEntryBox";
            this.textEntryBox.Size = new System.Drawing.Size(591, 20);
            this.textEntryBox.TabIndex = 1;
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(600, 213);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(47, 23);
            this.sendButton.TabIndex = 2;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.send_Click);
            // 
            // ChatBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(657, 241);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.textEntryBox);
            this.Controls.Add(this.richTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ChatBox";
            this.Text = "Chat";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.TextBox textEntryBox;
        private System.Windows.Forms.Button sendButton;
    }
}