using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using CatanersShared;
using CatenersServer;

namespace CatanersTest
{
    [TestFixture()]
    public class PurchasingTesting
    {
        private Lobby newLobby;
        private GamePlayer newPlayer1;
        private GamePlayer newPlayer2;
        private GamePlayer newPlayer3;
        private GamePlayer newPlayer4;
        private MockRepository mocks = new MockRepository();
        [SetUp]
        public void PurchasingTestingSetup()
        {
            this.newPlayer1 = new GamePlayer("Stentopher");
            this.newPlayer2 = new GamePlayer("Hays");
            this.newPlayer3 = new GamePlayer("Mellor");
            this.newPlayer4 = new GamePlayer("Laxer");
            this.newLobby = new Lobby("testGame1", 10, new Player("TrottaSN"), 43);
            newLobby.addPlayer(this.newPlayer1);
            newLobby.addPlayer(this.newPlayer2);
            newLobby.addPlayer(this.newPlayer3);
            newLobby.addPlayer(this.newPlayer4);
        }

        [Test]
        public void testAvailabilityFalseResources()
        {
            this.newPlayer1.resources[Resource.TYPE.Sheep] += 3;
            this.newPlayer1.resources[Resource.TYPE.Brick] += 1;
            this.newPlayer1.resources[Resource.TYPE.Wheat] += 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 0;
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            Assert.False(testLogic.determineSettlementAvailability("Stentopher"));
        }
    }
}
