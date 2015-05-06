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
        public void TestThatPlayerAddedInComboBox()
        {
           Player p1 = new Player("Bobby Tables");
           GameLobby lobby = new GameLobby(new Lobby("game", 100, p1, 10));
           Data.currentLobby = lobby;
           GamePlayer gp1 = new GamePlayer("Bobby Tables");
           trade.initializeValues();
           BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
           FieldInfo info = (typeof(TradeForm).GetField("targetOfTradeComboBox", flags));
           ComboBox box = (ComboBox)info.GetValue(trade);
           
           Assert.AreEqual(p1.Username, box.Items[0]);

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
        
    }
}
