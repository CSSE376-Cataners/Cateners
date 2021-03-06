﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CatanersShared;
using Cataners;
using System.Diagnostics.CodeAnalysis;

namespace Cataners
{
    public partial class LobbyForm : Form
    {
        public static LobbyForm INSTANCE;
        public delegate void refresher();
        public delegate void closer(bool start);
        public bool ready;
        public bool startButtonClose;
        public LobbyForm(String gameName)
        {
            InitializeComponent();
            lobbyNameLabel.Text = gameName;
            this.FormClosing += closing;
            INSTANCE = this;
            ready = false;
            var bs = new BindingSource();
            bs.DataSource = Data.currentLobby.Players;
            playersDataGridView.DataSource = bs;
            CommunicationClient.Instance.sendToServer(new CatanersShared.Message("", Translation.TYPE.UpdateLobby).toJson());

        }

        private void closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                backButton_Click(sender, e);
            }
        }

        private void readyButton_Click(object sender, EventArgs e)
        {
            if (ready) {
                CommunicationClient.Instance.sendToServer(new CatanersShared.Message(false.ToString(), Translation.TYPE.ChangeReadyStatus).toJson());
                ready = false;
                readyButton.Text = "Ready Up";
            }
            else
            {
                CommunicationClient.Instance.sendToServer(new CatanersShared.Message(true.ToString(), Translation.TYPE.ChangeReadyStatus).toJson());
                ready = true;
                readyButton.Text = "Not Ready";
            }

            
        }

        [ExcludeFromCodeCoverage]
        private void refreshLobby()
        {
            if (Data.currentLobby == null)
            {
                this.Close();
                JoinGameForm joinForm = new JoinGameForm();
                joinForm.Show();
            }

            BindingSource bs = new BindingSource();
            bs.DataSource = Data.currentLobby.Players;
            playersDataGridView.DataSource = bs;

            for (int i = 0; i < playersDataGridView.Rows.Count; i++)
            {
                if (Data.currentLobby.Players[i].Ready)
                {
                    playersDataGridView.Rows[i].Cells[0].Value = "Ready";
                }
                else
                {
                    playersDataGridView.Rows[i].Cells[0].Value = "Not Ready";
                }

                if (Data.username.Equals(Data.currentLobby.Owner.Username))
                {
                    startButton.Visible = true;
                }
                else
                {
                    waitingButton.Visible = true;
                }
            }

                playersDataGridView.Show();
            playersDataGridView.Refresh();
        }

        [ExcludeFromCodeCoverage]
        public void invokedRefresh()
        {
            if (this.Visible && !this.IsDisposed && !this.Disposing)
            {
                this.Invoke(new refresher(refreshLobby));
            }
        }

        [ExcludeFromCodeCoverage]
        public void InvokedClose(bool start)
        {
            this.Invoke(new closer(CloseLobby),new object[]{start});
        }
        public void CloseLobby(bool start){
            if (!start)
            {
                MessageBox.Show("The host has left the building.");
                this.Close();
                MainGui.INSTANCE.Show();
            }
            else
            {
                this.Close();
                MainGui.INSTANCE.Hide();
            }
            
            
        }

        [ExcludeFromCodeCoverage]
        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (Visible)
            {
                CommunicationClient.Instance.sendToServer(new CatanersShared.Message("", Translation.TYPE.UpdateLobby).toJson());
       
            }
        }

        [ExcludeFromCodeCoverage]
        private void backButton_Click(object sender, EventArgs e)
        {
            if (!startButtonClose)
            {
                CommunicationClient.Instance.sendToServer(new CatanersShared.Message("", Translation.TYPE.LeaveLobby).toJson());
            }
            this.Hide();
            MainGui.INSTANCE.Show();
      
        }

        [ExcludeFromCodeCoverage]
        private void startButton_Click(object sender, EventArgs e)
        {
            CommunicationClient.Instance.sendToServer(new CatanersShared.Message("", Translation.TYPE.StartGame).toJson());
            //this.Close();
            //MainGui.INSTANCE.Hide();
            /*if (Data.currentLobby.Players.Count == 4)
            {
                CommunicationClient.Instance.sendToServer(new CatanersShared.Message("", Translation.TYPE.StartGame).toJson());
            }
            else
            {
                MessageBox.Show("Please wait until four people have joined your game");
            }*/
        }
    }
}
