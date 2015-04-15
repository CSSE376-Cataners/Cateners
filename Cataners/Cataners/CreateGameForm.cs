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
    public partial class CreateGameForm : Form
    {
        public CreateGameForm()
        {
            InitializeComponent();
        }

        private void createGameButton_Click(object sender, EventArgs e)
        {
            switch(maxTimeComboBox.SelectedIndex){
                case 0:
                    Lobby newLobby = new Lobby(gameNameTextBox.Text, 5, null);
                    break;
                case 1:
                    Lobby newLobby1 = new Lobby(gameNameTextBox.Text, 10, null);
                    break;
                case 2:
                    Lobby newLobby2 = new Lobby(gameNameTextBox.Text, 15, null);
                    break;
                case 3:
                    Lobby newLobby3 = new Lobby(gameNameTextBox.Text, -1, null);
                    break;
            }
            
        }
    }
}
