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
        private int[] neighborArray;
        private MockRepository mocks = new MockRepository();
        [SetUp]
        public void PurchasingTestingSetup()
        {
            this.newPlayer1 = new GamePlayer("Stentopher");
            this.newPlayer2 = new GamePlayer("Hays");
            this.newPlayer3 = new GamePlayer("Mellor");
            this.newLobby = new Lobby("testGame1", 10, new Player("TrottaSN"), 43);
            this.newLobby.addPlayer(this.newPlayer1);
            this.newLobby.addPlayer(this.newPlayer2);
            this.newLobby.addPlayer(this.newPlayer3);
            this.neighborArray = new int[0];
        }

        [Test]
        public void testAvailabilityTrueResources()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            this.newPlayer1.resources[Resource.TYPE.Sheep] = 1;
            this.newPlayer1.resources[Resource.TYPE.Brick] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wheat] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 1;
            Assert.True(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testAvailabilityFalseResourcesSheep()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            this.newPlayer1.resources[Resource.TYPE.Sheep] = 0;
            this.newPlayer1.resources[Resource.TYPE.Brick] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wheat] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 1;
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testAvailabilityFalseResourcesBrick()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            this.newPlayer1.resources[Resource.TYPE.Sheep] = 1;
            this.newPlayer1.resources[Resource.TYPE.Brick] = 0;
            this.newPlayer1.resources[Resource.TYPE.Wheat] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 1;
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testAvailabilityFalseResourcesWheat()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            this.newPlayer1.resources[Resource.TYPE.Sheep] = 1;
            this.newPlayer1.resources[Resource.TYPE.Brick] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wheat] = 0;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 1;
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testAvailabilityFalseResourcesWood()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            this.newPlayer1.resources[Resource.TYPE.Sheep] = 1;
            this.newPlayer1.resources[Resource.TYPE.Brick] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wheat] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 0;
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testAvailabilityFalseActivityNeighbor()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            this.newPlayer1.resources[Resource.TYPE.Sheep] = 1;
            this.newPlayer1.resources[Resource.TYPE.Brick] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wheat] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 1;
            testLogic.setSettlementActivity(4, "Stentopher");
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testAvailabilityTrueBoth()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            this.newPlayer1.resources[Resource.TYPE.Sheep] = 1;
            this.newPlayer1.resources[Resource.TYPE.Brick] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wheat] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 1;
            this.neighborArray = new int[1] { 2 };
            Assert.True(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testAvailabilityFalseBoth()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            this.newPlayer1.resources[Resource.TYPE.Sheep] = 1;
            this.newPlayer1.resources[Resource.TYPE.Brick] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wheat] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 0;
            testLogic.setSettlementActivity(4, "Stentopher");
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testAvailabilityTrueActivityFalseResource()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            this.newPlayer1.resources[Resource.TYPE.Sheep] = 1;
            this.newPlayer1.resources[Resource.TYPE.Brick] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wheat] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 0;
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testAvailabilitySubtraction()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            this.newPlayer1.resources[Resource.TYPE.Sheep] = 1;
            this.newPlayer1.resources[Resource.TYPE.Brick] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wheat] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 1;
            Assert.True(testLogic.determineSettlementAvailability("Stentopher", 1));
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testResourceSubtraction()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            this.newPlayer1.resources[Resource.TYPE.Sheep] = 1;
            this.newPlayer1.resources[Resource.TYPE.Brick] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wheat] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 1;
            Assert.True(testLogic.determineSettlementAvailability("Stentopher", 1));
            Assert.AreEqual(0, this.newPlayer1.resources[Resource.TYPE.Sheep]);
            Assert.AreEqual(0, this.newPlayer1.resources[Resource.TYPE.Brick]);
            Assert.AreEqual(0, this.newPlayer1.resources[Resource.TYPE.Wheat]);
            Assert.AreEqual(0, this.newPlayer1.resources[Resource.TYPE.Wood]);
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testRoadResourceWoodFalse()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            this.newPlayer1.resources[Resource.TYPE.Brick] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 0;
            Assert.False(testLogic.determineRoadAvailability("Stentopher", 1));
        }

        [Test]
        public void testRoadResourceBrickFalse()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            this.newPlayer1.resources[Resource.TYPE.Brick] = 0;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 1;
            Assert.False(testLogic.determineRoadAvailability("Stentopher", 1));
        }

        [Test]
        public void testRoadResourceTrue()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            this.newPlayer1.resources[Resource.TYPE.Brick] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 1;
            testLogic.playerKeepers["Stentopher"].addToSettlements(4);
            testLogic.playerKeepers["Stentopher"].addToRoads(7);
            testLogic.setRoadActivity(7, "Stentopher");
            testLogic.setSettlementActivity(4, "Stentopher");
            Assert.True(testLogic.determineRoadAvailability("Stentopher", 1));
        }

        [Test]
        public void testRoadActivityFalseOnetoSix()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            this.newPlayer1.resources[Resource.TYPE.Brick] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 1;
            Assert.False(testLogic.determineRoadAvailability("Stentopher", 1));
        }

        [Test]
        public void testRoadActivityTrueOnetoSix()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            this.newPlayer1.resources[Resource.TYPE.Brick] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 1;
            testLogic.setSettlementActivity(4, "Stentopher");
            Assert.True(testLogic.determineRoadAvailability("Stentopher", 1));
        }

        [Test]
        public void testNeighborActiveSettlementFalse()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            this.newPlayer1.resources[Resource.TYPE.Sheep] = 1;
            this.newPlayer1.resources[Resource.TYPE.Brick] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wheat] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 1;
            testLogic.setSettlementActivity(3, "Stentopher");
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 0));
        }

        [Test]
        public void testNeighborActiveSettlementTrue()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            this.newPlayer1.resources[Resource.TYPE.Sheep] = 1;
            this.newPlayer1.resources[Resource.TYPE.Brick] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wheat] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 1;
            Assert.True(testLogic.determineSettlementAvailability("Stentopher", 0));
        }

        [Test]
        public void testAlreadyActiveFalse()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            testLogic.setSettlementActivity(0, "Stentopher");
            this.newPlayer1.resources[Resource.TYPE.Sheep] = 1;
            this.newPlayer1.resources[Resource.TYPE.Brick] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wheat] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 1;
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 0));
        }

        [Test]
        public void testNoRoadsNotBelowTwo()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            testLogic.setSettlementActivity(0, "Stentopher");
            testLogic.setSettlementActivity(53, "Stentopher");
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            this.newPlayer1.resources[Resource.TYPE.Sheep] = 1;
            this.newPlayer1.resources[Resource.TYPE.Brick] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wheat] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 1;
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 20));
        }

        [Test]
        public void testCanPlacePastTwoIfRoads()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            this.newPlayer1 = testLogic.gameLobby.gamePlayers[1];
            testLogic.setSettlementActivity(0, "Stentopher");
            testLogic.setSettlementActivity(53, "Stentopher");
            this.newPlayer1.resources[Resource.TYPE.Sheep] = 1;
            this.newPlayer1.resources[Resource.TYPE.Brick] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wheat] = 1;
            this.newPlayer1.resources[Resource.TYPE.Wood] = 1;
            testLogic.setRoadActivity(4, "Stentopher");
            Assert.True(testLogic.determineSettlementAvailability("Stentopher", 2));
        }
    }
}
