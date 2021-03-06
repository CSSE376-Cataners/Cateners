﻿using CatanersShared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cataners;

namespace Cataners
{
    public partial class JoinGameForm : Form
    {
        public static JoinGameForm INSTANCE;
        public delegate void refresher(object sender, EventArgs e);
        private BindingSource bs;
        private bool joinGame;
        public JoinGameForm()
        {
            InitializeComponent();
            gameTable.CellFormatting += noTimeLimit;
            INSTANCE = this;
            joinGame = false;
            this.FormClosing += closing;
            var bs = new BindingSource();
            bs.DataSource = Data.Lobbies;
            gameTable.DataSource = bs;
            CommunicationClient.Instance.sendToServer(new CatanersShared.Message(null, Translation.TYPE.RequestLobbies).toJson());
        }

        [ExcludeFromCodeCoverage]
        private void closing(object sender, FormClosingEventArgs e)
        {
            JoinGameForm.INSTANCE = null;
            if(!joinGame)
            MainGui.INSTANCE.Show();
        }

        [ExcludeFromCodeCoverage]
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.bs = new BindingSource();
                this.bs.DataSource = Data.Lobbies;
                gameTable.DataSource = bs;

                gameTable.Show();
                gameTable.Refresh();
            }
        }

        [ExcludeFromCodeCoverage]
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            CommunicationClient.Instance.sendToServer(new CatanersShared.Message(null, Translation.TYPE.RequestLobbies).toJson());
            //bs.DataSource = Data.Lobbies;
            //gameTable.Refresh();
        }

        [ExcludeFromCodeCoverage]
        public void invokedRefresh()
        {
            if (this.Visible)
            {
                this.Invoke(new refresher(button1_Click), new object[] { null, null });
            }
        }

        [ExcludeFromCodeCoverage]
        private void joinGameButton_Click(object sender, EventArgs e)
        {

            DataGridViewSelectedRowCollection selectedRow = gameTable.SelectedRows;
            if (selectedRow.Count < 1)
            {
                MessageBox.Show("Please actually select a game first before you join");
            }
            else
            {
                if (Data.Lobbies[gameTable.Rows.IndexOf(selectedRow[0])].PlayerCount < 4)
                {
                    JoinGameForm.INSTANCE = null;
                    joinGame = true;
                    CommunicationClient.Instance.sendToServer(new CatanersShared.Message(Data.Lobbies[gameTable.Rows.IndexOf(selectedRow[0])].lobbyID.ToString(), Translation.TYPE.JoinLobby).toJson());
                    this.Close();
                    new LobbyForm(Data.Lobbies[gameTable.Rows.IndexOf(selectedRow[0])].GameName).Show();
                }
                else
                {
                    MessageBox.Show("Sorry, This lobby is full. Please try another.");
                }
            }

        }

        [ExcludeFromCodeCoverage]
        private void noTimeLimit(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == gameTable.Columns["maxTimePerTurnColumn"].Index && e.Value.ToString().Equals("-1"))
            {
                e.Value = "No Time Limit";
                e.FormattingApplied = true;
            }
        }

        [ExcludeFromCodeCoverage]
        private void backToMainButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
