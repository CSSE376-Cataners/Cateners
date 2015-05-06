using CatanersShared;
using System;
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
    public partial class TradeForm : Form
    {
        public static TradeForm INSTANCE;
        private GamePlayer currentTrader;
        private bool brickCheck;
        public TradeForm()
        {
            InitializeComponent();
            INSTANCE = this;
            currentTrader = null;
        }

        public void initializeValues(){
            //initialize players in the target box
            for (int i = 0; i < ((GameLobby)Data.currentLobby).gamePlayers.Count; i++ ) {
                if (!((GameLobby)Data.currentLobby).gamePlayers[i].Username.Equals(Data.username))
                {
                    targetOfTradeComboBox.Items.Add(((GameLobby)Data.currentLobby).gamePlayers[i].Username);
                }
            }
            
        }

        private void giveBrickTextBox_TextChanged(object sender, EventArgs e)
        {
            CheckBrickQuantity();
        }

        private void CheckBrickQuantity()
        {

            /*int val = Int32.Parse(giveBrickTextBox.Text);
            if (val > .resources[Resource.TYPE.Brick])
            {
                brickCheck = false;
            }
            else
            {
                brickCheck = true;
            }*/

        }

    }
}
