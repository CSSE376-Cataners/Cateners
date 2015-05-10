namespace Cataners
{
    partial class TradeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TradeForm));
            this.tradeLabel = new System.Windows.Forms.Label();
            this.tradeButton = new System.Windows.Forms.Button();
            this.targetOfTradeComboBox = new System.Windows.Forms.ComboBox();
            this.giveBrickTextBox = new System.Windows.Forms.TextBox();
            this.giveOreTextBox = new System.Windows.Forms.TextBox();
            this.giveSheepTextBox = new System.Windows.Forms.TextBox();
            this.giveWheatTextBox = new System.Windows.Forms.TextBox();
            this.giveWoodTextBox = new System.Windows.Forms.TextBox();
            this.targetOfTradeLabel = new System.Windows.Forms.Label();
            this.brickLabel = new System.Windows.Forms.Label();
            this.oreLabel = new System.Windows.Forms.Label();
            this.sheepLabel = new System.Windows.Forms.Label();
            this.wheatLabel = new System.Windows.Forms.Label();
            this.woodLabel = new System.Windows.Forms.Label();
            this.woodLabel2 = new System.Windows.Forms.Label();
            this.wheatLabel2 = new System.Windows.Forms.Label();
            this.sheepLabel2 = new System.Windows.Forms.Label();
            this.oreLabel2 = new System.Windows.Forms.Label();
            this.brickLabel2 = new System.Windows.Forms.Label();
            this.recvWoodTextBox = new System.Windows.Forms.TextBox();
            this.recvWheatTextBox = new System.Windows.Forms.TextBox();
            this.recvSheepTextBox = new System.Windows.Forms.TextBox();
            this.recvOreTextBox = new System.Windows.Forms.TextBox();
            this.recvBrickTextBox = new System.Windows.Forms.TextBox();
            this.giveLabel = new System.Windows.Forms.Label();
            this.recvLabel = new System.Windows.Forms.Label();
            this.tradeWithBankLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tradeLabel
            // 
            this.tradeLabel.AutoSize = true;
            this.tradeLabel.Font = new System.Drawing.Font("Britannic Bold", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tradeLabel.Location = new System.Drawing.Point(435, 28);
            this.tradeLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.tradeLabel.Name = "tradeLabel";
            this.tradeLabel.Size = new System.Drawing.Size(121, 44);
            this.tradeLabel.TabIndex = 0;
            this.tradeLabel.Text = "Trade";
            // 
            // tradeButton
            // 
            this.tradeButton.Enabled = false;
            this.tradeButton.Location = new System.Drawing.Point(808, 344);
            this.tradeButton.Name = "tradeButton";
            this.tradeButton.Size = new System.Drawing.Size(106, 46);
            this.tradeButton.TabIndex = 1;
            this.tradeButton.Text = "Trade";
            this.tradeButton.UseVisualStyleBackColor = true;
            this.tradeButton.Click += new System.EventHandler(this.tradeButton_Click);
            // 
            // targetOfTradeComboBox
            // 
            this.targetOfTradeComboBox.FormattingEnabled = true;
            this.targetOfTradeComboBox.Location = new System.Drawing.Point(483, 96);
            this.targetOfTradeComboBox.Name = "targetOfTradeComboBox";
            this.targetOfTradeComboBox.Size = new System.Drawing.Size(153, 30);
            this.targetOfTradeComboBox.TabIndex = 2;
            this.targetOfTradeComboBox.SelectedIndexChanged += new System.EventHandler(this.targetOfTradeComboBox_SelectedIndexChanged);
            // 
            // giveBrickTextBox
            // 
            this.giveBrickTextBox.Location = new System.Drawing.Point(164, 207);
            this.giveBrickTextBox.Name = "giveBrickTextBox";
            this.giveBrickTextBox.Size = new System.Drawing.Size(59, 30);
            this.giveBrickTextBox.TabIndex = 3;
            this.giveBrickTextBox.TextChanged += new System.EventHandler(this.giveBrickTextBox_TextChanged);
            // 
            // giveOreTextBox
            // 
            this.giveOreTextBox.Location = new System.Drawing.Point(164, 276);
            this.giveOreTextBox.Name = "giveOreTextBox";
            this.giveOreTextBox.Size = new System.Drawing.Size(59, 30);
            this.giveOreTextBox.TabIndex = 4;
            this.giveOreTextBox.TextChanged += new System.EventHandler(this.giveOreTextBox_TextChanged);
            // 
            // giveSheepTextBox
            // 
            this.giveSheepTextBox.Location = new System.Drawing.Point(164, 341);
            this.giveSheepTextBox.Name = "giveSheepTextBox";
            this.giveSheepTextBox.Size = new System.Drawing.Size(59, 30);
            this.giveSheepTextBox.TabIndex = 5;
            this.giveSheepTextBox.TextChanged += new System.EventHandler(this.giveSheepTextBox_TextChanged);
            // 
            // giveWheatTextBox
            // 
            this.giveWheatTextBox.Location = new System.Drawing.Point(331, 207);
            this.giveWheatTextBox.Name = "giveWheatTextBox";
            this.giveWheatTextBox.Size = new System.Drawing.Size(59, 30);
            this.giveWheatTextBox.TabIndex = 6;
            this.giveWheatTextBox.TextChanged += new System.EventHandler(this.giveWheatTextBox_TextChanged);
            // 
            // giveWoodTextBox
            // 
            this.giveWoodTextBox.Location = new System.Drawing.Point(331, 276);
            this.giveWoodTextBox.Name = "giveWoodTextBox";
            this.giveWoodTextBox.Size = new System.Drawing.Size(59, 30);
            this.giveWoodTextBox.TabIndex = 7;
            this.giveWoodTextBox.TextChanged += new System.EventHandler(this.giveWoodTextBox_TextChanged);
            // 
            // targetOfTradeLabel
            // 
            this.targetOfTradeLabel.AutoSize = true;
            this.targetOfTradeLabel.Location = new System.Drawing.Point(327, 99);
            this.targetOfTradeLabel.Name = "targetOfTradeLabel";
            this.targetOfTradeLabel.Size = new System.Drawing.Size(153, 22);
            this.targetOfTradeLabel.TabIndex = 8;
            this.targetOfTradeLabel.Text = "Target of Trade:";
            // 
            // brickLabel
            // 
            this.brickLabel.AutoSize = true;
            this.brickLabel.Location = new System.Drawing.Point(78, 210);
            this.brickLabel.Name = "brickLabel";
            this.brickLabel.Size = new System.Drawing.Size(65, 22);
            this.brickLabel.TabIndex = 9;
            this.brickLabel.Text = "Brick:";
            // 
            // oreLabel
            // 
            this.oreLabel.AutoSize = true;
            this.oreLabel.Location = new System.Drawing.Point(95, 279);
            this.oreLabel.Name = "oreLabel";
            this.oreLabel.Size = new System.Drawing.Size(48, 22);
            this.oreLabel.TabIndex = 10;
            this.oreLabel.Text = "Ore:";
            // 
            // sheepLabel
            // 
            this.sheepLabel.AutoSize = true;
            this.sheepLabel.Location = new System.Drawing.Point(78, 344);
            this.sheepLabel.Name = "sheepLabel";
            this.sheepLabel.Size = new System.Drawing.Size(69, 22);
            this.sheepLabel.TabIndex = 11;
            this.sheepLabel.Text = "Sheep:";
            // 
            // wheatLabel
            // 
            this.wheatLabel.AutoSize = true;
            this.wheatLabel.Location = new System.Drawing.Point(252, 210);
            this.wheatLabel.Name = "wheatLabel";
            this.wheatLabel.Size = new System.Drawing.Size(70, 22);
            this.wheatLabel.TabIndex = 12;
            this.wheatLabel.Text = "Wheat:";
            // 
            // woodLabel
            // 
            this.woodLabel.AutoSize = true;
            this.woodLabel.Location = new System.Drawing.Point(260, 279);
            this.woodLabel.Name = "woodLabel";
            this.woodLabel.Size = new System.Drawing.Size(65, 22);
            this.woodLabel.TabIndex = 13;
            this.woodLabel.Text = "Wood:";
            // 
            // woodLabel2
            // 
            this.woodLabel2.AutoSize = true;
            this.woodLabel2.Location = new System.Drawing.Point(673, 279);
            this.woodLabel2.Name = "woodLabel2";
            this.woodLabel2.Size = new System.Drawing.Size(65, 22);
            this.woodLabel2.TabIndex = 23;
            this.woodLabel2.Text = "Wood:";
            // 
            // wheatLabel2
            // 
            this.wheatLabel2.AutoSize = true;
            this.wheatLabel2.Location = new System.Drawing.Point(665, 210);
            this.wheatLabel2.Name = "wheatLabel2";
            this.wheatLabel2.Size = new System.Drawing.Size(70, 22);
            this.wheatLabel2.TabIndex = 22;
            this.wheatLabel2.Text = "Wheat:";
            // 
            // sheepLabel2
            // 
            this.sheepLabel2.AutoSize = true;
            this.sheepLabel2.Location = new System.Drawing.Point(491, 344);
            this.sheepLabel2.Name = "sheepLabel2";
            this.sheepLabel2.Size = new System.Drawing.Size(69, 22);
            this.sheepLabel2.TabIndex = 21;
            this.sheepLabel2.Text = "Sheep:";
            // 
            // oreLabel2
            // 
            this.oreLabel2.AutoSize = true;
            this.oreLabel2.Location = new System.Drawing.Point(508, 279);
            this.oreLabel2.Name = "oreLabel2";
            this.oreLabel2.Size = new System.Drawing.Size(48, 22);
            this.oreLabel2.TabIndex = 20;
            this.oreLabel2.Text = "Ore:";
            // 
            // brickLabel2
            // 
            this.brickLabel2.AutoSize = true;
            this.brickLabel2.Location = new System.Drawing.Point(491, 210);
            this.brickLabel2.Name = "brickLabel2";
            this.brickLabel2.Size = new System.Drawing.Size(65, 22);
            this.brickLabel2.TabIndex = 19;
            this.brickLabel2.Text = "Brick:";
            // 
            // recvWoodTextBox
            // 
            this.recvWoodTextBox.Location = new System.Drawing.Point(744, 276);
            this.recvWoodTextBox.Name = "recvWoodTextBox";
            this.recvWoodTextBox.Size = new System.Drawing.Size(59, 30);
            this.recvWoodTextBox.TabIndex = 18;
            this.recvWoodTextBox.TextChanged += new System.EventHandler(this.recvWoodTextBox_TextChanged);
            // 
            // recvWheatTextBox
            // 
            this.recvWheatTextBox.Location = new System.Drawing.Point(744, 207);
            this.recvWheatTextBox.Name = "recvWheatTextBox";
            this.recvWheatTextBox.Size = new System.Drawing.Size(59, 30);
            this.recvWheatTextBox.TabIndex = 17;
            this.recvWheatTextBox.TextChanged += new System.EventHandler(this.recvWheatTextBox_TextChanged);
            // 
            // recvSheepTextBox
            // 
            this.recvSheepTextBox.Location = new System.Drawing.Point(577, 341);
            this.recvSheepTextBox.Name = "recvSheepTextBox";
            this.recvSheepTextBox.Size = new System.Drawing.Size(59, 30);
            this.recvSheepTextBox.TabIndex = 16;
            this.recvSheepTextBox.TextChanged += new System.EventHandler(this.recvSheepTextBox_TextChanged);
            // 
            // recvOreTextBox
            // 
            this.recvOreTextBox.Location = new System.Drawing.Point(577, 276);
            this.recvOreTextBox.Name = "recvOreTextBox";
            this.recvOreTextBox.Size = new System.Drawing.Size(59, 30);
            this.recvOreTextBox.TabIndex = 15;
            this.recvOreTextBox.TextChanged += new System.EventHandler(this.recvOreTextBox_TextChanged);
            // 
            // recvBrickTextBox
            // 
            this.recvBrickTextBox.Location = new System.Drawing.Point(577, 207);
            this.recvBrickTextBox.Name = "recvBrickTextBox";
            this.recvBrickTextBox.Size = new System.Drawing.Size(59, 30);
            this.recvBrickTextBox.TabIndex = 14;
            this.recvBrickTextBox.TextChanged += new System.EventHandler(this.recvBrickTextBox_TextChanged);
            // 
            // giveLabel
            // 
            this.giveLabel.AutoSize = true;
            this.giveLabel.Location = new System.Drawing.Point(216, 153);
            this.giveLabel.Name = "giveLabel";
            this.giveLabel.Size = new System.Drawing.Size(54, 22);
            this.giveLabel.TabIndex = 24;
            this.giveLabel.Text = "Give:";
            // 
            // recvLabel
            // 
            this.recvLabel.AutoSize = true;
            this.recvLabel.Location = new System.Drawing.Point(613, 153);
            this.recvLabel.Name = "recvLabel";
            this.recvLabel.Size = new System.Drawing.Size(84, 22);
            this.recvLabel.TabIndex = 25;
            this.recvLabel.Text = "Receive:";
            // 
            // tradeWithBankLabel
            // 
            this.tradeWithBankLabel.AutoSize = true;
            this.tradeWithBankLabel.Location = new System.Drawing.Point(234, 131);
            this.tradeWithBankLabel.Name = "tradeWithBankLabel";
            this.tradeWithBankLabel.Size = new System.Drawing.Size(475, 22);
            this.tradeWithBankLabel.TabIndex = 26;
            this.tradeWithBankLabel.Text = "Trade 4 resources in exchange for 1 from the bank!";
            this.tradeWithBankLabel.Visible = false;
            // 
            // TradeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(988, 411);
            this.Controls.Add(this.tradeWithBankLabel);
            this.Controls.Add(this.recvLabel);
            this.Controls.Add(this.giveLabel);
            this.Controls.Add(this.woodLabel2);
            this.Controls.Add(this.wheatLabel2);
            this.Controls.Add(this.sheepLabel2);
            this.Controls.Add(this.oreLabel2);
            this.Controls.Add(this.brickLabel2);
            this.Controls.Add(this.recvWoodTextBox);
            this.Controls.Add(this.recvWheatTextBox);
            this.Controls.Add(this.recvSheepTextBox);
            this.Controls.Add(this.recvOreTextBox);
            this.Controls.Add(this.recvBrickTextBox);
            this.Controls.Add(this.woodLabel);
            this.Controls.Add(this.wheatLabel);
            this.Controls.Add(this.sheepLabel);
            this.Controls.Add(this.oreLabel);
            this.Controls.Add(this.brickLabel);
            this.Controls.Add(this.targetOfTradeLabel);
            this.Controls.Add(this.giveWoodTextBox);
            this.Controls.Add(this.giveWheatTextBox);
            this.Controls.Add(this.giveSheepTextBox);
            this.Controls.Add(this.giveOreTextBox);
            this.Controls.Add(this.giveBrickTextBox);
            this.Controls.Add(this.targetOfTradeComboBox);
            this.Controls.Add(this.tradeButton);
            this.Controls.Add(this.tradeLabel);
            this.Font = new System.Drawing.Font("Britannic Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TradeForm";
            this.Text = "Trade";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tradeLabel;
        private System.Windows.Forms.Button tradeButton;
        private System.Windows.Forms.ComboBox targetOfTradeComboBox;
        private System.Windows.Forms.TextBox giveBrickTextBox;
        private System.Windows.Forms.TextBox giveOreTextBox;
        private System.Windows.Forms.TextBox giveSheepTextBox;
        private System.Windows.Forms.TextBox giveWheatTextBox;
        private System.Windows.Forms.TextBox giveWoodTextBox;
        private System.Windows.Forms.Label targetOfTradeLabel;
        private System.Windows.Forms.Label brickLabel;
        private System.Windows.Forms.Label oreLabel;
        private System.Windows.Forms.Label sheepLabel;
        private System.Windows.Forms.Label wheatLabel;
        private System.Windows.Forms.Label woodLabel;
        private System.Windows.Forms.Label woodLabel2;
        private System.Windows.Forms.Label wheatLabel2;
        private System.Windows.Forms.Label sheepLabel2;
        private System.Windows.Forms.Label oreLabel2;
        private System.Windows.Forms.Label brickLabel2;
        private System.Windows.Forms.TextBox recvWoodTextBox;
        private System.Windows.Forms.TextBox recvWheatTextBox;
        private System.Windows.Forms.TextBox recvSheepTextBox;
        private System.Windows.Forms.TextBox recvOreTextBox;
        private System.Windows.Forms.TextBox recvBrickTextBox;
        private System.Windows.Forms.Label giveLabel;
        private System.Windows.Forms.Label recvLabel;
        private System.Windows.Forms.Label tradeWithBankLabel;
    }
}