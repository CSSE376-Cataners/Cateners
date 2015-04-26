using System;
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
    public partial class LobbyForm : Form
    {
        public static LobbyForm INSTANCE;
        public delegate void refresher();
        private bool ready;
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
                Application.Exit();
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

        private void refreshLobby()
        {

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
            }

                playersDataGridView.Show();
            playersDataGridView.Refresh();
        }

        public void invokedRefresh()
        {
            this.Invoke(new refresher(refreshLobby));
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            CommunicationClient.Instance.sendToServer(new CatanersShared.Message("", Translation.TYPE.UpdateLobby).toJson());
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
            //send message to the server saying that we left
            JoinGameForm joinForm = new JoinGameForm();
            joinForm.Show();
        }
    }
}
