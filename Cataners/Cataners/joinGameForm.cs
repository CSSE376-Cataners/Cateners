using CatanersShared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cataners
{
    public partial class JoinGameForm : Form
    {
        public static JoinGameForm INSTANCE;
        private BindingSource bs;
        public JoinGameForm()
        {
            InitializeComponent();
            var bs = new BindingSource();
            bs.DataSource = Data.Lobbies;
            gameTable.DataSource = bs;
            CommunicationClient.Instance.sendToServer(new CatanersShared.Message(null, Translation.TYPE.RequestLobbies).toJson());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.bs = new BindingSource();
            this.bs.DataSource = Data.Lobbies;
            gameTable.DataSource = bs;
            gameTable.Show();
            gameTable.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CommunicationClient.Instance.sendToServer(new CatanersShared.Message(null, Translation.TYPE.RequestLobbies).toJson());
            bs.DataSource = Data.Lobbies;
            gameTable.Refresh();
        }

    }
}
