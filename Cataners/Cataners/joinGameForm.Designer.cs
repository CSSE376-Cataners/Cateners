namespace Cataners
{
    partial class JoinGameForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JoinGameForm));
            this.joinGameLabel = new System.Windows.Forms.Label();
            this.gameTable = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.joinGameButton = new System.Windows.Forms.Button();
            this.gameNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gameCreatorColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numberPlayersColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxTimePerTurnColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gameTable)).BeginInit();
            this.SuspendLayout();
            // 
            // joinGameLabel
            // 
            this.joinGameLabel.AutoSize = true;
            this.joinGameLabel.Font = new System.Drawing.Font("Britannic Bold", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.joinGameLabel.Location = new System.Drawing.Point(386, 42);
            this.joinGameLabel.Name = "joinGameLabel";
            this.joinGameLabel.Size = new System.Drawing.Size(340, 37);
            this.joinGameLabel.TabIndex = 0;
            this.joinGameLabel.Text = "Join an Existing Game";
            // 
            // gameTable
            // 
            this.gameTable.AllowUserToAddRows = false;
            this.gameTable.AllowUserToDeleteRows = false;
            this.gameTable.AllowUserToResizeColumns = false;
            this.gameTable.AllowUserToResizeRows = false;
            this.gameTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gameTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gameTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gameTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gameNameColumn,
            this.gameCreatorColumn,
            this.numberPlayersColumn,
            this.maxTimePerTurnColumn});
            this.gameTable.Location = new System.Drawing.Point(21, 82);
            this.gameTable.MultiSelect = false;
            this.gameTable.Name = "gameTable";
            this.gameTable.ReadOnly = true;
            this.gameTable.RowHeadersVisible = false;
            this.gameTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gameTable.RowTemplate.Height = 24;
            this.gameTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gameTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gameTable.Size = new System.Drawing.Size(957, 437);
            this.gameTable.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(962, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 36);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(984, 276);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(111, 34);
            this.refreshButton.TabIndex = 3;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // joinGameButton
            // 
            this.joinGameButton.Location = new System.Drawing.Point(891, 552);
            this.joinGameButton.Name = "joinGameButton";
            this.joinGameButton.Size = new System.Drawing.Size(193, 63);
            this.joinGameButton.TabIndex = 4;
            this.joinGameButton.Text = "Join Game";
            this.joinGameButton.UseVisualStyleBackColor = true;
            this.joinGameButton.Click += new System.EventHandler(this.joinGameButton_Click);
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
            // 
            // numberPlayersColumn
            // 
            this.numberPlayersColumn.DataPropertyName = "playerCount";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.numberPlayersColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.numberPlayersColumn.DividerWidth = 3;
            this.numberPlayersColumn.HeaderText = "Number of Players";
            this.numberPlayersColumn.Name = "numberPlayersColumn";
            this.numberPlayersColumn.ReadOnly = true;
            this.numberPlayersColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.numberPlayersColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // maxTimePerTurnColumn
            // 
            this.maxTimePerTurnColumn.DataPropertyName = "maxTimePerTurn";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.maxTimePerTurnColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.maxTimePerTurnColumn.HeaderText = "Max Time Per Turn";
            this.maxTimePerTurnColumn.Name = "maxTimePerTurnColumn";
            this.maxTimePerTurnColumn.ReadOnly = true;

            // 
            // JoinGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LawnGreen;
            this.ClientSize = new System.Drawing.Size(1107, 638);
            this.Controls.Add(this.joinGameButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gameTable);
            this.Controls.Add(this.joinGameLabel);
            this.Font = new System.Drawing.Font("Britannic Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "JoinGameForm";
            this.Text = "Join Game";
            ((System.ComponentModel.ISupportInitialize)(this.gameTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label joinGameLabel;
        private System.Windows.Forms.DataGridView gameTable;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button joinGameButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn gameNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gameCreatorColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberPlayersColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxTimePerTurnColumn;

    }
}