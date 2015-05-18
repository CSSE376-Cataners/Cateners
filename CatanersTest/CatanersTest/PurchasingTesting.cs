using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using CatanersShared;
using System.Net.Sockets;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using CatenersServer;

namespace CatanersTest
{
    [TestFixture()]
    public class PurchasingTesting
    {
        private Lobby newLobby;
        private ServerPlayer newPlayer1;
        private ServerPlayer newPlayer2;
        private ServerPlayer newPlayer3;
        private int[] neighborArray;
        private MockRepository mocks = new MockRepository();
        [SetUp]
        public void PurchasingTestingSetup()
        {
            ClientTesting.FakeClient fakeClient = new ClientTesting.FakeClient();
            ServerPlayer player0 = new ServerPlayer("TrottaSN", fakeClient);
            this.newPlayer1 = new ServerPlayer("Stentopher", fakeClient);
            this.newPlayer2 = new ServerPlayer("Hays", fakeClient);
            this.newPlayer3 = new ServerPlayer("Mellor", fakeClient);
            this.newLobby = new Lobby("testGame1", 10, player0, 43);
            this.newLobby.addPlayer(this.newPlayer1);
            this.newLobby.addPlayer(this.newPlayer2);
            this.newLobby.addPlayer(this.newPlayer3);
            this.neighborArray = new int[0];

            this.newPlayer1.client.currentLobby = newLobby;
        }

        [Test]
        public void testAvailabilityTrueResources()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Sheep] = 1;
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wheat] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            Assert.True(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testAvailabilityFalseResourcesSheep()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Sheep] = 0;
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wheat] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            testLogic.isStartPhase1 = false;
            testLogic.isStartPhase2 = false;
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testAvailabilityFalseResourcesBrick()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Sheep] = 1;
            player.resources[Resource.TYPE.Brick] = 0;
            player.resources[Resource.TYPE.Wheat] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            testLogic.isStartPhase1 = false;
            testLogic.isStartPhase2 = false;
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testAvailabilityFalseResourcesWheat()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Sheep] = 1;
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wheat] = 0;
            player.resources[Resource.TYPE.Wood] = 1;
            testLogic.isStartPhase1 = false;
            testLogic.isStartPhase2 = false;
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testAvailabilityFalseResourcesWood()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Sheep] = 1;
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wheat] = 1;
            player.resources[Resource.TYPE.Wood] = 0;
            testLogic.isStartPhase1 = false;
            testLogic.isStartPhase2 = false;
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testAvailabilityFalseActivityNeighbor()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Sheep] = 1;
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wheat] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            testLogic.setSettlementActivity(4, "Stentopher");
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testAvailabilityTrueBoth()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Sheep] = 1;
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wheat] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            this.neighborArray = new int[1] { 2 };
            Assert.True(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testAvailabilityFalseBoth()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Sheep] = 1;
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wheat] = 1;
            player.resources[Resource.TYPE.Wood] = 0;
            testLogic.setSettlementActivity(4, "Stentopher");
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testAvailabilityTrueActivityFalseResource()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Sheep] = 1;
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wheat] = 1;
            player.resources[Resource.TYPE.Wood] = 0;
            testLogic.isStartPhase1 = false;
            testLogic.isStartPhase2 = false;
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testAvailabilitySubtraction()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Sheep] = 1;
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wheat] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            Assert.True(testLogic.determineSettlementAvailability("Stentopher", 1));
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testResourceSubtraction()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Sheep] = 1;
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wheat] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            testLogic.isStartPhase1 = false;
            testLogic.isStartPhase2 = false;
            Assert.True(testLogic.determineSettlementAvailability("Stentopher", 1));
            Assert.AreEqual(0, player.resources[Resource.TYPE.Sheep]);
            Assert.AreEqual(0, player.resources[Resource.TYPE.Brick]);
            Assert.AreEqual(0, player.resources[Resource.TYPE.Wheat]);
            Assert.AreEqual(0, player.resources[Resource.TYPE.Wood]);
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 1));
        }

        [Test]
        public void testRoadResourceWoodFalse()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wood] = 0;
            Assert.False(testLogic.determineRoadAvailability("Stentopher", 1));
        }

        [Test]
        public void testRoadResourceBrickFalse()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Brick] = 0;
            player.resources[Resource.TYPE.Wood] = 1;
            Assert.False(testLogic.determineRoadAvailability("Stentopher", 1));
        }

        [Test]
        public void testRoadResourceTrue()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            testLogic.playerKeepers["Stentopher"].addToSettlements(4);
            testLogic.playerKeepers["Stentopher"].addToRoads(7, testLogic.getRoadList()[7].getNeighbors());
            testLogic.setRoadActivity(7, "Stentopher");
            testLogic.setSettlementActivity(4, "Stentopher");
            Assert.True(testLogic.determineRoadAvailability("Stentopher", 1));
        }

        [Test]
        public void testRoadActivityFalseOnetoFour()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            Assert.False(testLogic.determineRoadAvailability("Stentopher", 1));
        }

        [Test]
        public void testRoadActivityTrueOnetoFour()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            testLogic.setSettlementActivity(4, "Stentopher");
            Assert.True(testLogic.determineRoadAvailability("Stentopher", 1));
        }

        [Test]
        public void testNeighborActiveSettlementFalse()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Sheep] = 1;
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wheat] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            testLogic.setSettlementActivity(3, "Stentopher");
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 0));
        }

        [Test]
        public void testNeighborActiveSettlementTrue()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Sheep] = 1;
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wheat] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            Assert.True(testLogic.determineSettlementAvailability("Stentopher", 0));
        }

        [Test]
        public void testAlreadyActiveFalse()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            testLogic.setSettlementActivity(0, "Stentopher");
            testLogic.board.buildings[0].owner = player;
            player.resources[Resource.TYPE.Sheep] = 1;
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wheat] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 0));
        }

        [Test]
        public void testNoRoadsNotBelowTwo()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            testLogic.setSettlementActivity(0, "Stentopher");
            testLogic.setSettlementActivity(53, "Stentopher");
            player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Sheep] = 1;
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wheat] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            Assert.False(testLogic.determineSettlementAvailability("Stentopher", 20));
        }

        [Test]
        public void testCanPlacePastTwoIfRoads()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            testLogic.setSettlementActivity(0, "Stentopher");
            testLogic.setSettlementActivity(53, "Stentopher");
            player.resources[Resource.TYPE.Sheep] = 1;
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wheat] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            testLogic.isStartPhase1 = false;
            testLogic.isStartPhase2 = false;
            testLogic.setRoadActivity(4, "Stentopher");
            Assert.True(testLogic.determineSettlementAvailability("Stentopher", 2));
        }

        [Test]
        public void testCanPlaceIfNeighborRoadSet()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            testLogic.isStartPhase1 = false;
            testLogic.isStartPhase2 = false;
            testLogic.setRoadActivity(0, "Stentopher");
            Assert.True(testLogic.determineRoadAvailability("Stentopher", 1));
        }

        [Test]
        public void testCanPlaceIfNeighborSettlementSet()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            testLogic.setSettlementActivity(0, "Stentopher");
            Assert.True(testLogic.determineRoadAvailability("Stentopher", 1));
        }

        [Test]
        public void testCantPlaceIfNeighborRoadSetButNoOwnership()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            testLogic.setRoadActivity(0, "TrottaSN");
            Assert.False(testLogic.determineRoadAvailability("Stentopher", 1));
        }

        [Test]
        public void testLongestRoadAfterFourthRoadPurchased()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            testLogic.setRoadActivity(4, "Stentopher");
            testLogic.setRoadActivity(3, "Stentopher");
            testLogic.setRoadActivity(2, "Stentopher");
            Assert.True(testLogic.determineRoadAvailability("Stentopher", 1));
            Assert.AreEqual("Stentopher", Data.INSTANCE.UserWithLongestRoad);
        }
    }
}
