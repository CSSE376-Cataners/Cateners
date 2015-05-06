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
           ((GamePlayer)p1).resources[Resource.TYPE.Brick] = 1;

           BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
           FieldInfo info = (typeof(TradeForm).GetField("giveBrickTextBox", flags));
           TextBox box = (TextBox)info.GetValue(info);
           box.Text = "1000";
           check = trade.CheckBrickQuantity();
           trade.CheckBrickQuantity();
           Assert.IsFalse(check);
           
       }
        
    }
}
