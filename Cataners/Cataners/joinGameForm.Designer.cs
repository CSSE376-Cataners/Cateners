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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.joinGameLabel = new System.Windows.Forms.Label();
            this.gameTable = new System.Windows.Forms.DataGridView();
            this.gameNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gameCreatorColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numberPlayersColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.joinColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gameTable)).BeginInit();
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
            // gameTable
            // 
            this.gameTable.AllowUserToAddRows = false;
            this.gameTable.AllowUserToDeleteRows = false;
            this.gameTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gameTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gameNameColumn,
            this.gameCreatorColumn,
            this.numberPlayersColumn,
            this.joinColumn});
            this.gameTable.Location = new System.Drawing.Point(94, 98);
            this.gameTable.Name = "gameTable";
            this.gameTable.ReadOnly = true;
            this.gameTable.RowHeadersVisible = false;
            this.gameTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gameTable.RowTemplate.Height = 24;
            this.gameTable.Size = new System.Drawing.Size(742, 360);
            this.gameTable.TabIndex = 1;
            // 
            // gameNameColumn
            // 
            this.gameNameColumn.DataPropertyName = "GameName";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.gameNameColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.gameNameColumn.DividerWidth = 3;
            this.gameNameColumn.HeaderText = "Game Name";
            this.gameNameColumn.Name = "gameNameColumn";
            this.gameNameColumn.ReadOnly = true;
            this.gameNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gameNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.gameNameColumn.Width = 180;
            // 
            // gameCreatorColumn
            // 
            this.gameCreatorColumn.DataPropertyName = "Owner";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.gameCreatorColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.gameCreatorColumn.DividerWidth = 3;
            this.gameCreatorColumn.HeaderText = "Game Creator";
            this.gameCreatorColumn.Name = "gameCreatorColumn";
            this.gameCreatorColumn.ReadOnly = true;
            this.gameCreatorColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gameCreatorColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.gameCreatorColumn.Width = 200;
            // 
            // numberPlayersColumn
            // 
            this.numberPlayersColumn.DataPropertyName = "PlayerCount";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.numberPlayersColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.numberPlayersColumn.DividerWidth = 3;
            this.numberPlayersColumn.HeaderText = "Number of Players";
            this.numberPlayersColumn.Name = "numberPlayersColumn";
            this.numberPlayersColumn.ReadOnly = true;
            this.numberPlayersColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.numberPlayersColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.numberPlayersColumn.Width = 260;
            // 
            // joinColumn
            // 
            this.joinColumn.DividerWidth = 3;
            this.joinColumn.HeaderText = "Join";
            this.joinColumn.Name = "joinColumn";
            this.joinColumn.ReadOnly = true;
            this.joinColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.joinColumn.Text = "Join";
            this.joinColumn.Width = 98;
            // 
            // joinGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LawnGreen;
            this.ClientSize = new System.Drawing.Size(947, 503);
            this.Controls.Add(this.gameTable);
            this.Controls.Add(this.joinGameLabel);
            this.Font = new System.Drawing.Font("Britannic Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "joinGameForm";
            this.Text = "Join Game";
            ((System.ComponentModel.ISupportInitialize)(this.gameTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label joinGameLabel;
        private System.Windows.Forms.DataGridView gameTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn gameNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gameCreatorColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberPlayersColumn;
        private System.Windows.Forms.DataGridViewButtonColumn joinColumn;
    }
}