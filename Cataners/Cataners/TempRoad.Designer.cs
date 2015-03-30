using System.Drawing;
namespace Cataners
{
    partial class TempRoad
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
            this.road1 = new Cataners.Road();
            ((System.ComponentModel.ISupportInitialize)(this.road1)).BeginInit();
            this.SuspendLayout();
            // 
            // road1
            // 
            this.road1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.road1.Location = new System.Drawing.Point(375, 144);
            this.road1.Name = "road1";
            this.road1.Size = new System.Drawing.Size(100, 50);
            this.road1.TabIndex = 0;
            this.road1.TabStop = false;
            this.road1.Click += new System.EventHandler(this.road1_Click_1);
            // 
            // TempRoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 512);
            this.Controls.Add(this.road1);
            this.Name = "TempRoad";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.TempRoad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.road1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Road road1;


    }
}