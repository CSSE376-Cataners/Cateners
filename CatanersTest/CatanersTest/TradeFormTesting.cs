using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Mocks;
using Cataners;
using CatanersShared;
using System.Reflection;
using System.Windows.Forms;

namespace CatanersTest
{
    [TestFixture()]
    class TradeFormTesting
    {
        TradeForm trade;
        [SetUp]
        public void setUp()
        {
            trade = new TradeForm();
        }

       [Test]
       public void TestThatOwnerNotInComboBox()
       {
           Player p1 = new Player("Bobby Tables");
           GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
           Data.currentLobby = lobby;
           Data.username = p1.Username;
           GamePlayer p2 = new GamePlayer("Rocky");
           ((GameLobby)Data.currentLobby).gamePlayers.Add(p2);
           trade.initializeValues();
           BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
           FieldInfo info = (typeof(TradeForm).GetField("targetOfTradeComboBox", flags));
           ComboBox box = (ComboBox)info.GetValue(trade);

           Assert.AreEqual(p2.Username, box.Items[0]);

       }

        [Test]
       public void TestBrickCheckFalseIfNotEnoughResources()
       {
           bool check = true;
           Player p1 = new Player("Bobby Tables");
           GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
           ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Brick] = 1;
           Data.currentLobby = lobby;
           Data.username = p1.Username;
           GamePlayer p2 = new GamePlayer("jimmy");
          ((GameLobby)lobby).gamePlayers.Add(p2);
           trade.initializeValues();

           BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
           FieldInfo info = (typeof(TradeForm).GetField("giveBrickTextBox", flags));
           TextBox box = (TextBox)info.GetValue(trade);
           box.Text = "1000";
           check = trade.CheckBrickQuantity();
           Assert.IsFalse(check);
           
       }

        [Test]
        public void TestBrickCheckTrueIfEnoughResources()
        {
            bool check = true;
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Brick] = 100;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("giveBrickTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "10";
            check = trade.CheckBrickQuantity();
            Assert.IsTrue(check);

        }

        [Test]
        public void TestBrickCheckFalseIfInvalidResources()
        {
            bool check = true;
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Brick] = 1;
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("giveBrickTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "Q";
            check = trade.CheckBrickQuantity();
            Assert.IsFalse(check);

        }

        [Test]
        public void TestOreCheckFalseIfNotEnoughResources()
        {
            bool check = true;
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Ore] = 1;
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("giveOreTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "1000";
            check = trade.CheckOreQuantity();
            Assert.IsFalse(check);

        }

        [Test]
        public void TestOreCheckTrueIfEnoughResources()
        {
            bool check = true;
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Ore] = 100;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("giveOreTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "10";
            check = trade.CheckOreQuantity();
            Assert.IsTrue(check);

        }

        [Test]
        public void TestOreCheckFalseIfInvalidResources()
        {
            bool check = true;
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Ore] = 1;
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("giveOreTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "Q";
            check = trade.CheckOreQuantity();
            Assert.IsFalse(check);

        }

        [Test]
        public void TestSheepCheckFalseIfNotEnoughResources()
        {
            bool check = true;
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Sheep] = 1;
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("giveSheepTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "1000";
            check = trade.CheckSheepQuantity();
            Assert.IsFalse(check);

        }

        [Test]
        public void TestSheepCheckTrueIfEnoughResources()
        {
            bool check = true;
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Sheep] = 100;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("giveSheepTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "10";
            check = trade.CheckSheepQuantity();
            Assert.IsTrue(check);

        }

        [Test]
        public void TestSheepCheckFalseIfInvalidResources()
        {
            bool check = true;
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Sheep] = 1;
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("giveSheepTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "Q";
            check = trade.CheckSheepQuantity();
            Assert.IsFalse(check);

        }

        [Test]
        public void TestWheatCheckFalseIfNotEnoughResources()
        {
            bool check = true;
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Wheat] = 1;
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("giveWheatTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "1000";
            check = trade.CheckWheatQuantity();
            Assert.IsFalse(check);

        }

        [Test]
        public void TestWheatCheckTrueIfEnoughResources()
        {
            bool check = true;
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Wheat] = 100;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("giveWheatTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "10";
            check = trade.CheckWheatQuantity();
            Assert.IsTrue(check);

        }

        [Test]
        public void TestWheatCheckFalseIfInvalidResources()
        {
            bool check = true;
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Wheat] = 1;
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("giveWheatTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "Q";
            check = trade.CheckWheatQuantity();
            Assert.IsFalse(check);

        }

        [Test]
        public void TestWoodCheckFalseIfNotEnoughResources()
        {
            bool check = true;
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Wood] = 1;
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("giveWoodTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "1000";
            check = trade.CheckWoodQuantity();
            Assert.IsFalse(check);

        }

        [Test]
        public void TestWoodCheckTrueIfEnoughResources()
        {
            bool check = true;
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Wood] = 100;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("giveWoodTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "10";
            check = trade.CheckWoodQuantity();
            Assert.IsTrue(check);

        }

        [Test]
        public void TestWoodCheckFalseIfInvalidResources()
        {
            bool check = true;
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Wood] = 1;
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("giveWoodTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "Q";
            check = trade.CheckWoodQuantity();
            Assert.IsFalse(check);

        }

        [Test]
        public void testErrorMessageBrokenBrick()
        {
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Brick] = 1;
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("giveBrickTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "100";

            String mystring = trade.printWrongResources();
            Assert.AreEqual("The following boxes are either invalid or greater than your current resources: Brick",mystring);
            
        }
        
    }
}
