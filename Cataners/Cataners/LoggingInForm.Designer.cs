using System.Diagnostics.CodeAnalysis;
namespace Cataners
{
    partial class LoggingInForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoggingInForm));
            this.loggingInLabel1 = new System.Windows.Forms.Label();
            this.loggingInLabel2 = new System.Windows.Forms.Label();
            this.loggingInLabel3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // loggingInLabel1
            // 
            this.loggingInLabel1.AutoSize = true;
            this.loggingInLabel1.Font = new System.Drawing.Font("Britannic Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loggingInLabel1.Location = new System.Drawing.Point(114, 47);
            this.loggingInLabel1.Name = "loggingInLabel1";
            this.loggingInLabel1.Size = new System.Drawing.Size(133, 22);
            this.loggingInLabel1.TabIndex = 0;
            this.loggingInLabel1.Text = "Logging in =D";
            // 
            // loggingInLabel2
            // 
            this.loggingInLabel2.AutoSize = true;
            this.loggingInLabel2.Font = new System.Drawing.Font("Britannic Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loggingInLabel2.Location = new System.Drawing.Point(114, 47);
            this.loggingInLabel2.Name = "loggingInLabel2";
            this.loggingInLabel2.Size = new System.Drawing.Size(123, 22);
            this.loggingInLabel2.TabIndex = 1;
            this.loggingInLabel2.Text = "Logging in :)";
            // 
            // loggingInLabel3
            // 
            this.loggingInLabel3.AutoSize = true;
            this.loggingInLabel3.Font = new System.Drawing.Font("Britannic Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loggingInLabel3.Location = new System.Drawing.Point(114, 47);
            this.loggingInLabel3.Name = "loggingInLabel3";
            this.loggingInLabel3.Size = new System.Drawing.Size(129, 22);
            this.loggingInLabel3.TabIndex = 2;
            this.loggingInLabel3.Text = "Logging in :D";
            // 
            // LoggingInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(335, 115);
            this.Controls.Add(this.loggingInLabel3);
            this.Controls.Add(this.loggingInLabel2);
            this.Controls.Add(this.loggingInLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoggingInForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Logging In";
            this.Load += new System.EventHandler(this.LoggingInForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label loggingInLabel1;
        private System.Windows.Forms.Label loggingInLabel2;
        private System.Windows.Forms.Label loggingInLabel3;
    }
}