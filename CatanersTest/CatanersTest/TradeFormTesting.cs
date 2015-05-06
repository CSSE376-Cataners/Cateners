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
           trade.initializeValues(gp1);
           BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
           FieldInfo info = (typeof(TradeForm).GetField("targetOfTradeComboBox", flags));
           ComboBox box = (ComboBox)info.GetValue(trade);
           

           Assert.AreEqual(p1.Username, box.Items[0]);

        }

        [Test]
       public void TestBrickCheck()
       {
           
       }
        
    }
}
