using System.Diagnostics.CodeAnalysis;
namespace Cataners
{
    [ExcludeFromCodeCoverage]
    partial class ChangeServerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeServerForm));
            this.serverChangeButton = new System.Windows.Forms.Button();
            this.serverAddrTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // serverChangeButton
            // 
            this.serverChangeButton.Location = new System.Drawing.Point(194, 238);
            this.serverChangeButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.serverChangeButton.Name = "serverChangeButton";
            this.serverChangeButton.Size = new System.Drawing.Size(391, 211);
            this.serverChangeButton.TabIndex = 0;
            this.serverChangeButton.Text = "Change that server, Amigo!";
            this.serverChangeButton.UseVisualStyleBackColor = true;
            this.serverChangeButton.Click += new System.EventHandler(this.serverChangeButton_Click);
            // 
            // serverAddrTextBox
            // 
            this.serverAddrTextBox.Location = new System.Drawing.Point(194, 164);
            this.serverAddrTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.serverAddrTextBox.Name = "serverAddrTextBox";
            this.serverAddrTextBox.Size = new System.Drawing.Size(391, 30);
            this.serverAddrTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(285, 115);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server Address:";
            // 
            // ChangeServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gold;
            this.ClientSize = new System.Drawing.Size(771, 551);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.serverAddrTextBox);
            this.Controls.Add(this.serverChangeButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ChangeServerForm";
            this.Text = "ChangeServerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button serverChangeButton;
        private System.Windows.Forms.TextBox serverAddrTextBox;
        private System.Windows.Forms.Label label1;
    }
}