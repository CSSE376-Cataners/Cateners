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
        public List<Lobby> lobbies = new List<Lobby>();
        public JoinGameForm()
        {
            InitializeComponent();
            gameTable.DataSource = lobbies;
            CommunicationClient.Instance.sendToServer(new CatanersShared.Message(null, Translation.TYPE.RequestLobbies).toJson());
        }

    }
}
