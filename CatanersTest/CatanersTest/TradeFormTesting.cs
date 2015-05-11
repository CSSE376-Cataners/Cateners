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
           Assert.AreEqual("Bank", box.Items[1]);

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

        [Test]
        public void testErrorMessageBrokenOre()
        {
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
            box.Text = "100";

            String mystring = trade.printWrongResources();
            Assert.AreEqual("The following boxes are either invalid or greater than your current resources: Ore", mystring);

        }

        [Test]
        public void testErrorMessageBrokenSheep()
        {
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
            box.Text = "100";

            String mystring = trade.printWrongResources();
            Assert.AreEqual("The following boxes are either invalid or greater than your current resources: Sheep", mystring);

        }

        [Test]
        public void testMultipleBrokenParts()
        {
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Sheep] = 1;
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("giveBrickTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "100";

            FieldInfo info2 = (typeof(TradeForm).GetField("giveOreTextBox", flags));
            TextBox box2 = (TextBox)info2.GetValue(trade);
            box2.Text = "100";

            FieldInfo info3 = (typeof(TradeForm).GetField("giveSheepTextBox", flags));
            TextBox box3 = (TextBox)info3.GetValue(trade);
            box3.Text = "100";

            FieldInfo info4 = (typeof(TradeForm).GetField("giveWheatTextBox", flags));
            TextBox box4 = (TextBox)info4.GetValue(trade);
            box4.Text = "100";

            FieldInfo info5 = (typeof(TradeForm).GetField("giveWoodTextBox", flags));
            TextBox box5 = (TextBox)info5.GetValue(trade);
            box5.Text = "100";


            String mystring = trade.printWrongResources();
            Assert.AreEqual("The following boxes are either invalid or greater than your current resources: Brick, Ore, Sheep, Wheat, Wood", mystring);

        }
        [Test]
        public void TestOfferedDictionaryInitialization(){
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Brick] = 1;
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Ore] = 2;
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Sheep] = 3;
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Wheat] = 4;
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Wood] = 5;
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("giveBrickTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "1";

            FieldInfo info2 = (typeof(TradeForm).GetField("giveOreTextBox", flags));
            TextBox box2 = (TextBox)info2.GetValue(trade);
            box2.Text = "2";

            FieldInfo info3 = (typeof(TradeForm).GetField("giveSheepTextBox", flags));
            TextBox box3 = (TextBox)info3.GetValue(trade);
            box3.Text = "3";

            FieldInfo info4 = (typeof(TradeForm).GetField("giveWheatTextBox", flags));
            TextBox box4 = (TextBox)info4.GetValue(trade);
            box4.Text = "4";

            FieldInfo info5 = (typeof(TradeForm).GetField("giveWoodTextBox", flags));
            TextBox box5 = (TextBox)info5.GetValue(trade);
            box5.Text = "5";

            trade.InitializeDictionaries();
            Assert.AreEqual(trade.offered[Resource.TYPE.Brick], 1);
            Assert.AreEqual(trade.offered[Resource.TYPE.Ore], 2);
            Assert.AreEqual(trade.offered[Resource.TYPE.Sheep], 3);
            Assert.AreEqual(trade.offered[Resource.TYPE.Wheat], 4);
            Assert.AreEqual(trade.offered[Resource.TYPE.Wood], 5);


        }

        [Test]
        public void TestOfferedDictionaryInitializationWithDifferentVals()
        {
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Brick] = 5;
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Ore] = 4;
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Sheep] = 3;
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Wheat] = 2;
            ((GameLobby)lobby).gamePlayers[0].resources[Resource.TYPE.Wood] = 1;
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("giveBrickTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "5";

            FieldInfo info2 = (typeof(TradeForm).GetField("giveOreTextBox", flags));
            TextBox box2 = (TextBox)info2.GetValue(trade);
            box2.Text = "4";

            FieldInfo info3 = (typeof(TradeForm).GetField("giveSheepTextBox", flags));
            TextBox box3 = (TextBox)info3.GetValue(trade);
            box3.Text = "3";

            FieldInfo info4 = (typeof(TradeForm).GetField("giveWheatTextBox", flags));
            TextBox box4 = (TextBox)info4.GetValue(trade);
            box4.Text = "2";

            FieldInfo info5 = (typeof(TradeForm).GetField("giveWoodTextBox", flags));
            TextBox box5 = (TextBox)info5.GetValue(trade);
            box5.Text = "1";

            trade.InitializeDictionaries();
            Assert.AreEqual(trade.offered[Resource.TYPE.Brick], 5);
            Assert.AreEqual(trade.offered[Resource.TYPE.Ore], 4);
            Assert.AreEqual(trade.offered[Resource.TYPE.Sheep], 3);
            Assert.AreEqual(trade.offered[Resource.TYPE.Wheat], 2);
            Assert.AreEqual(trade.offered[Resource.TYPE.Wood], 1);

        }

        [Test]
        public void TestWantedDictionaryInitialization()
        {
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("recvBrickTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "5";

            FieldInfo info2 = (typeof(TradeForm).GetField("recvOreTextBox", flags));
            TextBox box2 = (TextBox)info2.GetValue(trade);
            box2.Text = "4";

            FieldInfo info3 = (typeof(TradeForm).GetField("recvSheepTextBox", flags));
            TextBox box3 = (TextBox)info3.GetValue(trade);
            box3.Text = "3";

            FieldInfo info4 = (typeof(TradeForm).GetField("recvWheatTextBox", flags));
            TextBox box4 = (TextBox)info4.GetValue(trade);
            box4.Text = "2";

            FieldInfo info5 = (typeof(TradeForm).GetField("recvWoodTextBox", flags));
            TextBox box5 = (TextBox)info5.GetValue(trade);
            box5.Text = "1";

            trade.InitializeDictionaries();
            Assert.AreEqual(trade.wanted[Resource.TYPE.Brick], 5);
            Assert.AreEqual(trade.wanted[Resource.TYPE.Ore], 4);
            Assert.AreEqual(trade.wanted[Resource.TYPE.Sheep], 3);
            Assert.AreEqual(trade.wanted[Resource.TYPE.Wheat], 2);
            Assert.AreEqual(trade.wanted[Resource.TYPE.Wood], 1);

        }

        [Test]
        public void TestWantedDictionaryInitializationWithDiffVals()
        {
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("recvBrickTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "1";

            FieldInfo info2 = (typeof(TradeForm).GetField("recvOreTextBox", flags));
            TextBox box2 = (TextBox)info2.GetValue(trade);
            box2.Text = "2";

            FieldInfo info3 = (typeof(TradeForm).GetField("recvSheepTextBox", flags));
            TextBox box3 = (TextBox)info3.GetValue(trade);
            box3.Text = "3";

            FieldInfo info4 = (typeof(TradeForm).GetField("recvWheatTextBox", flags));
            TextBox box4 = (TextBox)info4.GetValue(trade);
            box4.Text = "4";

            FieldInfo info5 = (typeof(TradeForm).GetField("recvWoodTextBox", flags));
            TextBox box5 = (TextBox)info5.GetValue(trade);
            box5.Text = "5";

            trade.InitializeDictionaries();
            Assert.AreEqual(trade.wanted[Resource.TYPE.Brick], 1);
            Assert.AreEqual(trade.wanted[Resource.TYPE.Ore], 2);
            Assert.AreEqual(trade.wanted[Resource.TYPE.Sheep], 3);
            Assert.AreEqual(trade.wanted[Resource.TYPE.Wheat], 4);
            Assert.AreEqual(trade.wanted[Resource.TYPE.Wood], 5);

        }

        [Test]
        public void TestTradingBrickWithBank()
        {
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            lobby.gamePlayers[0].resources[Resource.TYPE.Brick] = 10;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info0 = (typeof(TradeForm).GetField("targetOfTradeComboBox", flags));
            ComboBox box0 = (ComboBox)info0.GetValue(trade);
            box0.Text = "Bank";

            
            FieldInfo info = (typeof(TradeForm).GetField("recvBrickTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "1";

            FieldInfo info2 = (typeof(TradeForm).GetField("giveBrickTextBox", flags));
            TextBox box2 = (TextBox)info2.GetValue(trade);
            box2.Text = "4";

            trade.InitializeDictionaries();
            Assert.IsTrue(trade.bankCheck());

        }
        [Test]
        public void TestTradingOreWithBank()
        {
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            lobby.gamePlayers[0].resources[Resource.TYPE.Ore] = 10;
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info0 = (typeof(TradeForm).GetField("targetOfTradeComboBox", flags));
            ComboBox box0 = (ComboBox)info0.GetValue(trade);
            box0.Text = "Bank";


            FieldInfo info = (typeof(TradeForm).GetField("recvBrickTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "2";

            FieldInfo info2 = (typeof(TradeForm).GetField("giveOreTextBox", flags));
            TextBox box2 = (TextBox)info2.GetValue(trade);
            box2.Text = "8";

            trade.InitializeDictionaries();
            Assert.IsTrue(trade.bankCheck());

        }
        [Test]
        public void TestTradingSheepWithBank()
        {
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            lobby.gamePlayers[0].resources[Resource.TYPE.Sheep] = 10;
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info0 = (typeof(TradeForm).GetField("targetOfTradeComboBox", flags));
            ComboBox box0 = (ComboBox)info0.GetValue(trade);
            box0.Text = "Bank";


            FieldInfo info = (typeof(TradeForm).GetField("recvBrickTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "1";

            FieldInfo info2 = (typeof(TradeForm).GetField("giveSheepTextBox", flags));
            TextBox box2 = (TextBox)info2.GetValue(trade);
            box2.Text = "4";

            trade.InitializeDictionaries();
            Assert.IsTrue(trade.bankCheck());

        }
        [Test]
        public void TestTradingWheatWithBank()
        {
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            lobby.gamePlayers[0].resources[Resource.TYPE.Wheat] = 10;
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info0 = (typeof(TradeForm).GetField("targetOfTradeComboBox", flags));
            ComboBox box0 = (ComboBox)info0.GetValue(trade);
            box0.Text = "Bank";


            FieldInfo info = (typeof(TradeForm).GetField("recvBrickTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "1";

            FieldInfo info2 = (typeof(TradeForm).GetField("giveWheatTextBox", flags));
            TextBox box2 = (TextBox)info2.GetValue(trade);
            box2.Text = "4";

            trade.InitializeDictionaries();
            Assert.IsTrue(trade.bankCheck());

        }
        [Test]
        public void TestTradingWoodWithBank()
        {
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            lobby.gamePlayers[0].resources[Resource.TYPE.Wood] = 10;
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info0 = (typeof(TradeForm).GetField("targetOfTradeComboBox", flags));
            ComboBox box0 = (ComboBox)info0.GetValue(trade);
            box0.Text = "Bank";


            FieldInfo info = (typeof(TradeForm).GetField("recvBrickTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "1";

            FieldInfo info2 = (typeof(TradeForm).GetField("giveWoodTextBox", flags));
            TextBox box2 = (TextBox)info2.GetValue(trade);
            box2.Text = "4";

            trade.InitializeDictionaries();
            Assert.IsTrue(trade.bankCheck());

        }
        [Test]
        public void TestWantingTooMuchFromBank()
        {
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            lobby.gamePlayers[0].resources[Resource.TYPE.Wood] = 3;
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info0 = (typeof(TradeForm).GetField("targetOfTradeComboBox", flags));
            ComboBox box0 = (ComboBox)info0.GetValue(trade);
            box0.Text = "Bank";


            FieldInfo info = (typeof(TradeForm).GetField("recvBrickTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "3";

            FieldInfo info2 = (typeof(TradeForm).GetField("giveWoodTextBox", flags));
            TextBox box2 = (TextBox)info2.GetValue(trade);
            box2.Text = "4";

            trade.InitializeDictionaries();
            Assert.IsFalse(trade.bankCheck());

        }
        [Test]
        public void TestGivingNothingToBank()
        {
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            lobby.gamePlayers[0].resources[Resource.TYPE.Wood] = 10;
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info0 = (typeof(TradeForm).GetField("targetOfTradeComboBox", flags));
            ComboBox box0 = (ComboBox)info0.GetValue(trade);
            box0.Text = "Bank";


            FieldInfo info = (typeof(TradeForm).GetField("recvBrickTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "3";

            FieldInfo info2 = (typeof(TradeForm).GetField("giveWoodTextBox", flags));
            TextBox box2 = (TextBox)info2.GetValue(trade);
            box2.Text = "0";

            trade.InitializeDictionaries();
            Assert.IsFalse(trade.bankCheck());

        }


        // Manual Test
        //[Test]
        public void TestWantedDictionaryInitializationWithInvalidEntries()
        {
            Player p1 = new Player("Bobby Tables");
            GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
            Data.currentLobby = lobby;
            Data.username = p1.Username;
            GamePlayer p2 = new GamePlayer("jimmy");
            ((GameLobby)lobby).gamePlayers.Add(p2);
            trade.initializeValues();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = (typeof(TradeForm).GetField("recvBrickTextBox", flags));
            TextBox box = (TextBox)info.GetValue(trade);
            box.Text = "q";

            FieldInfo info2 = (typeof(TradeForm).GetField("recvOreTextBox", flags));
            TextBox box2 = (TextBox)info2.GetValue(trade);
            box2.Text = "a";

            FieldInfo info3 = (typeof(TradeForm).GetField("recvSheepTextBox", flags));
            TextBox box3 = (TextBox)info3.GetValue(trade);
            box3.Text = "w";

            FieldInfo info4 = (typeof(TradeForm).GetField("recvWheatTextBox", flags));
            TextBox box4 = (TextBox)info4.GetValue(trade);
            box4.Text = "r";

            FieldInfo info5 = (typeof(TradeForm).GetField("recvWoodTextBox", flags));
            TextBox box5 = (TextBox)info5.GetValue(trade);
            box5.Text = "z";

            trade.InitializeDictionaries();
            Assert.AreEqual(trade.wanted[Resource.TYPE.Brick], 0);
            Assert.AreEqual(trade.wanted[Resource.TYPE.Ore], 0);
            Assert.AreEqual(trade.wanted[Resource.TYPE.Sheep], 0);
            Assert.AreEqual(trade.wanted[Resource.TYPE.Wheat], 0);
            Assert.AreEqual(trade.wanted[Resource.TYPE.Wood], 0);


        }
        
    }
}
