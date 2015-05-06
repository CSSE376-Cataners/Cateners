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
        private bool oreCheck;
        private bool sheepCheck;
        private bool wheatCheck;
        private bool woodCheck;

        int val;
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
                else
                {
                    currentTrader = ((GameLobby)Data.currentLobby).gamePlayers[i];
                }
            }
            
        }

        public bool CheckBrickQuantity()
        {
            String txt = giveBrickTextBox.Text;
            try
            {
                val = Int32.Parse(txt);
            }
            catch {
                return false;
            }
            return (currentTrader.resources[Resource.TYPE.Brick] > val) ? true : false;
            
        }

        public bool CheckOreQuantity()
        {
            String txt = giveOreTextBox.Text;
            try
            {
                val = Int32.Parse(txt);
                
            }
            catch
            {
                return false;
            }
            return (currentTrader.resources[Resource.TYPE.Ore] > val) ? true : false;
            
        }

        public bool CheckSheepQuantity()
        {
            String txt = giveSheepTextBox.Text;
            try
            {
                val = Int32.Parse(txt);

            }
            catch
            {
                return false;
            }
            return (currentTrader.resources[Resource.TYPE.Sheep] > val) ? true : false;
        }

        public bool CheckWheatQuantity()
        {
            String txt = giveWheatTextBox.Text;
            try
            {
                val = Int32.Parse(txt);

            }
            catch
            {
                return false;
            }
            return (currentTrader.resources[Resource.TYPE.Wheat] > val) ? true : false;
        }

        public bool CheckWoodQuantity()
        {
            String txt = giveWoodTextBox.Text;
            try
            {
                val = Int32.Parse(txt);

            }
            catch
            {
                return false;
            }
            return (currentTrader.resources[Resource.TYPE.Wood] > val) ? true : false;
        }

        private void giveBrickTextBox_TextChanged(object sender, EventArgs e)
        {
            brickCheck = CheckBrickQuantity();
        }

        private void giveOreTextBox_TextChanged(object sender, EventArgs e)
        {
            oreCheck = CheckOreQuantity();
        }

        private void giveSheepTextBox_TextChanged(object sender, EventArgs e)
        {
            sheepCheck = CheckSheepQuantity();
        }

        private void giveWheatTextBox_TextChanged(object sender, EventArgs e)
        {
            wheatCheck = CheckWheatQuantity();
        }

        private void giveWoodTextBox_TextChanged(object sender, EventArgs e)
        {
            woodCheck = CheckWoodQuantity();
        }

    }
}
