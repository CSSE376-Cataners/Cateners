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
        public LobbyForm()
        {
            InitializeComponent();
            this.FormClosing += closing;
            INSTANCE = this;
            ready = false;
            var bs = new BindingSource();
            bs.DataSource = Data.Lobbies;
            playersDataGridView.DataSource = bs;
            
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
            }
            else
            {
                CommunicationClient.Instance.sendToServer(new CatanersShared.Message(true.ToString(), Translation.TYPE.ChangeReadyStatus).toJson());
            }

            
        }

        private void refreshLobby()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = Data.Lobbies;
            playersDataGridView.DataSource = bs;
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
    }
}
