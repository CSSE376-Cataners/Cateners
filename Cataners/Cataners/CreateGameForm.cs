﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CatanersShared;

namespace Cataners
{
    public partial class CreateGameForm : Form
    {
        private MainGui parent;
        public CreateGameForm(MainGui parent)
        {
            this.parent = parent;
            InitializeComponent();

        }

        private void createGameButton_Click(object sender, EventArgs e)
        {

            Lobby newLobby;
            switch(maxTimeComboBox.SelectedIndex){
                case 0:
                    newLobby = new Lobby(gameNameTextBox.Text, 5, null,-1);
                    break;
                case 1:
                    newLobby = new Lobby(gameNameTextBox.Text, 10, null, -1);
                    break;
                case 2:
                    newLobby = new Lobby(gameNameTextBox.Text, 15, null, -1);
                    break;
                case 3:
                    newLobby = new Lobby(gameNameTextBox.Text, -1, null, -1);
                    break;
                default: return;
            }
            CommunicationClient.Instance.sendToServer(new CatanersShared.Message(newLobby.toJson(), Translation.TYPE.CreateLobby).toJson());
            LobbyForm lobby = new LobbyForm(newLobby.GameName);
            this.Close();
            lobby.Show();
            this.parent.Close();
        }

    }
}
