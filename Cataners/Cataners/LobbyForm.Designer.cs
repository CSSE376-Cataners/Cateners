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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.playerNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lobbyNameLabel = new System.Windows.Forms.Label();
            this.readyButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.playerNameColumn,
            this.statusColumn});
            this.dataGridView1.Location = new System.Drawing.Point(123, 166);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(603, 195);
            this.dataGridView1.TabIndex = 0;
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(450, 423);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(165, 83);
            this.button1.TabIndex = 3;
            this.button1.Text = "Start Game";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // LobbyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(867, 575);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.readyButton);
            this.Controls.Add(this.lobbyNameLabel);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Britannic Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "LobbyForm";
            this.Text = "Game Lobby";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
        private System.Windows.Forms.Label lobbyNameLabel;
        private System.Windows.Forms.Button readyButton;
        private System.Windows.Forms.Button button1;
    }
}