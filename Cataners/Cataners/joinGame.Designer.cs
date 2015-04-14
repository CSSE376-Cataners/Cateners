namespace Cataners
{
    partial class joinGameForm
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
            this.joinGameLabel = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.gameNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gameCreatorColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numberPlayersColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.joinColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // joinGameLabel
            // 
            this.joinGameLabel.AutoSize = true;
            this.joinGameLabel.Font = new System.Drawing.Font("Britannic Bold", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.joinGameLabel.Location = new System.Drawing.Point(294, 45);
            this.joinGameLabel.Name = "joinGameLabel";
            this.joinGameLabel.Size = new System.Drawing.Size(340, 37);
            this.joinGameLabel.TabIndex = 0;
            this.joinGameLabel.Text = "Join an Existing Game";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gameNameColumn,
            this.gameCreatorColumn,
            this.numberPlayersColumn,
            this.joinColumn});
            this.dataGridView1.Location = new System.Drawing.Point(92, 131);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(742, 360);
            this.dataGridView1.TabIndex = 1;
            // 
            // gameNameColumn
            // 
            this.gameNameColumn.HeaderText = "Game Name";
            this.gameNameColumn.Name = "gameNameColumn";
            this.gameNameColumn.ReadOnly = true;
            this.gameNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gameNameColumn.Width = 168;
            // 
            // gameCreatorColumn
            // 
            this.gameCreatorColumn.HeaderText = "Game Creator";
            this.gameCreatorColumn.Name = "gameCreatorColumn";
            this.gameCreatorColumn.ReadOnly = true;
            // 
            // numberPlayersColumn
            // 
            this.numberPlayersColumn.HeaderText = "Number of Players";
            this.numberPlayersColumn.Name = "numberPlayersColumn";
            this.numberPlayersColumn.ReadOnly = true;
            // 
            // joinColumn
            // 
            this.joinColumn.HeaderText = "Join";
            this.joinColumn.Name = "joinColumn";
            this.joinColumn.ReadOnly = true;
            // 
            // joinGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LawnGreen;
            this.ClientSize = new System.Drawing.Size(947, 503);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.joinGameLabel);
            this.Font = new System.Drawing.Font("Britannic Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "joinGameForm";
            this.Text = "Join Game";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label joinGameLabel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn gameNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gameCreatorColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberPlayersColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn joinColumn;
    }
}