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
        private int offeredResourceCount;
        private int desiredResourceCount;

        GamePlayer target;
        public Dictionary<Resource.TYPE, int> offered;
        public Dictionary<Resource.TYPE, int> wanted;

        int val;
        public TradeForm()
        {
            this.FormClosing += closing;
            InitializeComponent();
            INSTANCE = this;
            currentTrader = null;
        }

        private void closing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        public void initializeValues(){
            //initialize players in the target box
            targetOfTradeComboBox.Items.Clear();
            tradeWithBankLabel.Visible = false;
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

            targetOfTradeComboBox.Items.Add("Bank");

            giveBrickTextBox.Text = "0";
            giveOreTextBox.Text = "0";
            giveSheepTextBox.Text = "0";
            giveWheatTextBox.Text = "0";
            giveWoodTextBox.Text = "0";
            recvBrickTextBox.Text = "0";
            recvOreTextBox.Text = "0";
            recvSheepTextBox.Text = "0";
            recvWheatTextBox.Text = "0";
            recvWoodTextBox.Text = "0";

            
        }

        public bool CheckBrickQuantity()
        {
            String txt = giveBrickTextBox.Text;
            try
            {
                val = Int32.Parse(txt);
                offeredBrick = val;
                offeredResourceCount += val;
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
                offeredResourceCount += val;
                
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
                offeredResourceCount += val;

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
                offeredResourceCount += val;

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
                offeredResourceCount += val;

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
            desiredResourceCount = 0;
            offeredResourceCount = 0;
            if (targetOfTradeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select someone to trade with first");
                return;
            }
            if(targetOfTradeComboBox.SelectedItem.ToString().Equals("Bank")){
                TradeWithBank();
            }else if (brickCheck & oreCheck & sheepCheck & wheatCheck & woodCheck)
            {
                target = new GamePlayer(targetOfTradeComboBox.SelectedItem.ToString());
                InitializeDictionaries();
                Trade tradeobj = new Trade(currentTrader, target, offered, wanted);
                CommunicationClient.Instance.sendToServer(new CatanersShared.Message(tradeobj.toJson(),Translation.TYPE.StartTrade).toJson());
                this.Hide();
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
            if (targetOfTradeComboBox.SelectedItem.ToString().Equals("Bank"))
            {
                tradeWithBankLabel.Visible = true;
            }
            else
            {
                tradeWithBankLabel.Visible = false;
            }
        }

        private void recvBrickTextBox_TextChanged(object sender, EventArgs e)
        {
            String txt = recvBrickTextBox.Text;
            if (txt.Equals(""))
            {
                return;
            }
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
            if (txt.Equals(""))
            {
                return;
            }
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
            if (txt.Equals(""))
            {
                return;
            }
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
            if (txt.Equals(""))
            {
                return;
            }
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
            if (txt.Equals(""))
            {
                return;
            }
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

        private void TradeWithBank()
        {
            InitializeDictionaries();
            if (bankCheck())
            {
                Trade tradeobj = new Trade(currentTrader, new GamePlayer("Bank"), offered, wanted);
                CommunicationClient.Instance.sendToServer(new CatanersShared.Message(tradeobj.toJson(), Translation.TYPE.StartTrade).toJson());
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please pick 4 of one resource to get rid of in exchange for 1 resource from the bank");
            }

        }

        public bool bankCheck(){
            foreach (Resource.TYPE t in Enum.GetValues(typeof(Resource.TYPE)))
            {
                if (offered.ContainsKey(t) && offered[t] >= 0)
                {
                    if (offered[t] > currentTrader.resources[t])
                    {
                        return false;
                    }
                    if (offered[t] % 4 != 0)
                    {
                        return false;
                    }
                    else
                    {
                        offeredResourceCount += offered[t];
                    }
                }
                if (wanted.ContainsKey(t))
                {
                    desiredResourceCount += wanted[t];
                }
            }
            if (offeredResourceCount / 4 != desiredResourceCount)
            {
                return false;
            }
            return true;
        }



    }
}
