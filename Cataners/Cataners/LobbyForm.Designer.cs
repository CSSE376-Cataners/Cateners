namespace Cataners
{
    partial class LobbyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LobbyForm));
            this.playersDataGridView = new System.Windows.Forms.DataGridView();
            this.playerNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lobbyNameLabel = new System.Windows.Forms.Label();
            this.readyButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.playersDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // playersDataGridView
            // 
            this.playersDataGridView.AllowUserToAddRows = false;
            this.playersDataGridView.AllowUserToDeleteRows = false;
            this.playersDataGridView.AllowUserToOrderColumns = true;
            this.playersDataGridView.AllowUserToResizeColumns = false;
            this.playersDataGridView.AllowUserToResizeRows = false;
            this.playersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.playersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.playerNameColumn,
            this.statusColumn});
            this.playersDataGridView.Location = new System.Drawing.Point(123, 166);
            this.playersDataGridView.Name = "playersDataGridView";
            this.playersDataGridView.RowHeadersVisible = false;
            this.playersDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.playersDataGridView.RowTemplate.Height = 24;
            this.playersDataGridView.Size = new System.Drawing.Size(603, 195);
            this.playersDataGridView.TabIndex = 0;
            // 
            // playerNameColumn
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.playerNameColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.playerNameColumn.DividerWidth = 3;
            this.playerNameColumn.HeaderText = "Player Name";
            this.playerNameColumn.Name = "playerNameColumn";
            this.playerNameColumn.ReadOnly = true;
            this.playerNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.playerNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.playerNameColumn.Width = 300;
            // 
            // statusColumn
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.statusColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.statusColumn.DividerWidth = 3;
            this.statusColumn.HeaderText = "Status";
            this.statusColumn.Name = "statusColumn";
            this.statusColumn.ReadOnly = true;
            this.statusColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.statusColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.statusColumn.Width = 300;
            // 
            // lobbyNameLabel
            // 
            this.lobbyNameLabel.AutoSize = true;
            this.lobbyNameLabel.Font = new System.Drawing.Font("Britannic Bold", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lobbyNameLabel.Location = new System.Drawing.Point(282, 71);
            this.lobbyNameLabel.Name = "lobbyNameLabel";
            this.lobbyNameLabel.Size = new System.Drawing.Size(254, 37);
            this.lobbyNameLabel.TabIndex = 1;
            this.lobbyNameLabel.Text = "<Owner>\'s Lobby";
            // 
            // readyButton
            // 
            this.readyButton.Location = new System.Drawing.Point(181, 423);
            this.readyButton.Name = "readyButton";
            this.readyButton.Size = new System.Drawing.Size(165, 83);
            this.readyButton.TabIndex = 2;
            this.readyButton.Text = "Ready Up";
            this.readyButton.UseVisualStyleBackColor = true;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(450, 423);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(165, 83);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Start Game";
            this.startButton.UseVisualStyleBackColor = true;
            // 
            // LobbyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(867, 575);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.readyButton);
            this.Controls.Add(this.lobbyNameLabel);
            this.Controls.Add(this.playersDataGridView);
            this.Font = new System.Drawing.Font("Britannic Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "LobbyForm";
            this.Text = "Game Lobby";
            ((System.ComponentModel.ISupportInitialize)(this.playersDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView playersDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
        private System.Windows.Forms.Label lobbyNameLabel;
        private System.Windows.Forms.Button readyButton;
        private System.Windows.Forms.Button startButton;
    }
}