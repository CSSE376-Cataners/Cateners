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
        private int offeredBrick;
        private int offeredOre;
        private int offeredSheep;
        private int offeredWheat;
        private int offeredWood;
        private int wantedBrick;
        private int wantedOre;
        private int wantedSheep;
        private int wantedWheat;
        private int wantedWood;

        GamePlayer target;
        public Dictionary<Resource.TYPE, int> offered;
        public Dictionary<Resource.TYPE, int> wanted;

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

            giveBrickTextBox.Text = "0";
            giveOreTextBox.Text = "0";
            giveSheepTextBox.Text = "0";
            giveWheatTextBox.Text = "0";
            giveWoodTextBox.Text = "0";
            
        }

        public bool CheckBrickQuantity()
        {
            String txt = giveBrickTextBox.Text;
            try
            {
                val = Int32.Parse(txt);
                offeredBrick = val;
            }
            catch {
                return false;
            }
            return (currentTrader.resources[Resource.TYPE.Brick] >= val && val >=0) ? true : false;
            
        }

        public bool CheckOreQuantity()
        {
            String txt = giveOreTextBox.Text;
            try
            {
                val = Int32.Parse(txt);
                offeredOre = val;
                
            }
            catch
            {
                return false;
            }
            return (currentTrader.resources[Resource.TYPE.Ore] >= val && val >= 0) ? true : false;
            
        }

        public bool CheckSheepQuantity()
        {
            String txt = giveSheepTextBox.Text;
            try
            {
                val = Int32.Parse(txt);
                offeredSheep = val;

            }
            catch
            {
                return false;
            }
            return (currentTrader.resources[Resource.TYPE.Sheep] >= val && val >= 0) ? true : false;
        }

        public bool CheckWheatQuantity()
        {
            String txt = giveWheatTextBox.Text;
            try
            {
                val = Int32.Parse(txt);
                offeredWheat = val;

            }
            catch
            {
                return false;
            }
            return (currentTrader.resources[Resource.TYPE.Wheat] >= val && val >= 0) ? true : false;
        }

        public bool CheckWoodQuantity()
        {
            String txt = giveWoodTextBox.Text;
            try
            {
                val = Int32.Parse(txt);
                offeredWood = val;

            }
            catch
            {
                return false;
            }
            return (currentTrader.resources[Resource.TYPE.Wood] >= val && val >= 0) ? true : false;
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

        public String printWrongResources()
        {
            List<String> broken = new List<String>();
            if (!brickCheck)
            {
                broken.Add("Brick");
            }
            if (!oreCheck)
            {
                broken.Add("Ore");
            }
            if (!sheepCheck)
            {
                broken.Add("Sheep");
            }
            if (!wheatCheck)
            {
                broken.Add("Wheat");
            }
            if (!woodCheck)
            {
                broken.Add("Wood");
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("The following boxes are either invalid or greater than your current resources: ");
            for (int i = 0; i < broken.Count - 1; i++)
            {
                sb.Append(broken[i] + ", ");
            }
            sb.Append(broken[broken.Count - 1]);

            return sb.ToString();

        }
        public void InitializeDictionaries()
        {
            offered = new Dictionary<Resource.TYPE, int>();
            wanted = new Dictionary<Resource.TYPE, int>();

            offered.Add(Resource.TYPE.Brick, offeredBrick);
            offered.Add(Resource.TYPE.Ore, offeredOre);
            offered.Add(Resource.TYPE.Sheep, offeredSheep);
            offered.Add(Resource.TYPE.Wheat, offeredWheat);
            offered.Add(Resource.TYPE.Wood, offeredWood);

            String txt = recvBrickTextBox.Text;
            try
            {
                wantedBrick = Int32.Parse(txt);
            }
            catch
            {
                wantedBrick = 0;
            }
            wanted.Add(Resource.TYPE.Brick, wantedBrick);

            txt = recvOreTextBox.Text;
            try
            {
                wantedOre = Int32.Parse(txt);
            }
            catch
            {
                wantedOre = 0;
            }
            wanted.Add(Resource.TYPE.Ore, wantedOre);

            txt = recvSheepTextBox.Text;
            try
            {
                wantedSheep = Int32.Parse(txt);
            }
            catch
            {
                wantedSheep = 0;
            }
            wanted.Add(Resource.TYPE.Sheep, wantedSheep);

            txt = recvWheatTextBox.Text;
            try
            {
                wantedWheat = Int32.Parse(txt);
            }
            catch
            {
                wantedWheat = 0;
            }
            wanted.Add(Resource.TYPE.Wheat, wantedWheat);

            txt = recvWoodTextBox.Text;
            try
            {
                wantedWood = Int32.Parse(txt);
            }
            catch
            {
                wantedWood = 0;
            }
            wanted.Add(Resource.TYPE.Wood, wantedWood);

        }

        private void tradeButton_Click(object sender, EventArgs e)
        {
            if (brickCheck & oreCheck & sheepCheck & wheatCheck & woodCheck)
            {
                target = (GamePlayer)targetOfTradeComboBox.SelectedValue;
                InitializeDictionaries();
                Trade tradeobj = new Trade(currentTrader, target, offered, wanted);
            }
            else
            {
                String error = printWrongResources();
                MessageBox.Show(error);
            }
        }

        private void targetOfTradeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            tradeButton.Enabled = true;
        }

        private void recvBrickTextBox_TextChanged(object sender, EventArgs e)
        {
            String txt = recvBrickTextBox.Text;
            try
            {
                val = Int32.Parse(txt);
                if (val < 0)
                {
                    MessageBox.Show("Please do not enter a negative number");
                }
            }
            catch
            {
                MessageBox.Show("Please enter a positive integer");
            }
        }

        private void recvOreTextBox_TextChanged(object sender, EventArgs e)
        {
            String txt = recvOreTextBox.Text;
            try
            {
                val = Int32.Parse(txt);
                if (val < 0)
                {
                    MessageBox.Show("Please do not enter a negative number");
                }
            }
            catch
            {
                MessageBox.Show("Please enter a positive integer");
            }
        }

        private void recvSheepTextBox_TextChanged(object sender, EventArgs e)
        {
            String txt = recvSheepTextBox.Text;
            try
            {
                val = Int32.Parse(txt);
                if (val < 0)
                {
                    MessageBox.Show("Please do not enter a negative number");
                }
            }
            catch
            {
                MessageBox.Show("Please enter a positive integer");
            }
        }

        private void recvWheatTextBox_TextChanged(object sender, EventArgs e)
        {
            String txt = recvWheatTextBox.Text;
            try
            {
                val = Int32.Parse(txt);
                if (val < 0)
                {
                    MessageBox.Show("Please do not enter a negative number");
                }
            }
            catch
            {
                MessageBox.Show("Please enter a positive integer");
            }
        }

        private void recvWoodTextBox_TextChanged(object sender, EventArgs e)
        {
            String txt = recvWoodTextBox.Text;
            try
            {
                val = Int32.Parse(txt);
                if (val < 0)
                {
                    MessageBox.Show("Please do not enter a negative number");
                }
            }
            catch
            {
                MessageBox.Show("Please enter a positive integer");
            }
        }

    }
}
