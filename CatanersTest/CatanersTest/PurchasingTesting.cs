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
            testLogic.canRegen = false;
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
            testLogic.canRegen = false;
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
            testLogic.canRegen = false;
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
            testLogic.canRegen = false;
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
            testLogic.canRegen = false;
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wood] = 0;
            Assert.False(testLogic.determineRoadAvailability("Stentopher", 1));
        }

        [Test]
        public void testRoadResourceBrickFalse()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            testLogic.canRegen = false;
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
            testLogic.canRegen = false;
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
            testLogic.canRegen = false;
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
            Assert.AreEqual("Stentopher", testLogic.UserWithLongestRoad);
        }

        [Test]
        public void testLongestRoadAfterFourthRoadPurchasedFalse()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            testLogic.LongestRoadCount = 5;
            testLogic.UserWithLongestRoad = "TrottaSN";
            testLogic.setRoadActivity(4, "Stentopher");
            testLogic.setRoadActivity(3, "Stentopher");
            testLogic.setRoadActivity(2, "Stentopher");
            Assert.True(testLogic.determineRoadAvailability("Stentopher", 1));
            Assert.AreEqual("TrottaSN", testLogic.UserWithLongestRoad);
        }

        [Test]
        public void testLongestRoadTwo()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            player.resources[Resource.TYPE.Brick] = 1;
            player.resources[Resource.TYPE.Wood] = 1;
            testLogic.LongestRoadCount = 5;
            testLogic.UserWithLongestRoad = "TrottaSN";
            testLogic.setRoadActivity(0, "Stentopher");
            RoadPath testedPath = testLogic.setRoadActivity(1, "Stentopher");
            int size = testedPath.getSize();
            Assert.AreEqual("TrottaSN", testLogic.UserWithLongestRoad);
            Assert.AreEqual(5, testLogic.LongestRoadCount);
        }

        [Test]
        public void testLongestRoadSplit()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            testLogic.canRegen = false;
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            testLogic.setRoadActivity(0, "Stentopher");
            testLogic.setRoadActivity(1, "Stentopher");
            testLogic.setRoadActivity(7, "Stentopher");
            testLogic.setRoadActivity(12, "Stentopher");
            testLogic.setRoadActivity(13, "Stentopher");
            RoadPath testedPath = testLogic.setRoadActivity(20, "Stentopher");
            Console.WriteLine(String.Join(",", testedPath.getRoadIDs()));
            Assert.AreEqual("Stentopher", testLogic.UserWithLongestRoad);
            Assert.AreEqual(5, testLogic.LongestRoadCount);
        }

        [Test]
        public void testLongestRoadRejoint()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            testLogic.canRegen = false;
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            testLogic.setRoadActivity(0, "Stentopher");
            testLogic.setRoadActivity(1, "Stentopher");
            testLogic.setRoadActivity(7, "Stentopher");
            testLogic.setRoadActivity(12, "Stentopher");
            testLogic.setRoadActivity(13, "Stentopher");
            testLogic.setRoadActivity(20, "Stentopher");
            testLogic.setRoadActivity(27, "Stentopher");
            testLogic.setRoadActivity(26, "Stentopher");
            RoadPath testedPath = testLogic.setRoadActivity(19, "Stentopher");
            Console.WriteLine(String.Join(",", testedPath.getRoadIDs()));
            Assert.AreEqual(9, testLogic.LongestRoadCount);
        }

        [Test]
        public void testJoiningFrontFront()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            testLogic.canRegen = false;
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            RoadPath newPath = new RoadPath(3);
            newPath.generateNewPath(4, new int[] { 3 });
            newPath.generateNewPath(5, new int[] { 4 });
            newPath.generateNewPath(6, new int[] { 5 });
            //RoadPath result = newPath.joinFrontFront(new RoadPath (new int[] { 9, 10, 6 }));
            //Console.WriteLine(String.Join(",", result.getRoadIDs()));
            //Assert.AreEqual(new int[] { 3, 4, 5, 6, 10, 9 }, result.getRoadIDs());
        }

        [Test]
        public void testJoiningFrontBack()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            testLogic.canRegen = false;
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            RoadPath newPath = new RoadPath(3);
            newPath.generateNewPath(4, new int[] { 3 });
            newPath.generateNewPath(5, new int[] { 4 });
            newPath.generateNewPath(6, new int[] { 5 });
            //RoadPath result = newPath.joinFrontBack(new RoadPath(new int[] { 6, 9, 10 }));
            //Console.WriteLine(String.Join(",", result.getRoadIDs()));
            //Assert.AreEqual(new int[] { 3, 4, 5, 6, 9, 10 }, result.getRoadIDs());
        }

        [Test]
        public void testJoiningBackFront()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            testLogic.canRegen = false;
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            RoadPath newPath = new RoadPath(3);
            newPath.generateNewPath(4, new int[] { 3 });
            newPath.generateNewPath(5, new int[] { 4 });
            newPath.generateNewPath(6, new int[] { 5 });
            //RoadPath result = newPath.joinBackFront(new RoadPath(new int[] { 9, 10, 3 }));
            //Console.WriteLine(String.Join(",", result.getRoadIDs()));
            //Assert.AreEqual(new int[] { 9, 10, 3, 4, 5, 6 }, result.getRoadIDs());
        }

        [Test]
        public void testJoiningBackBack()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            testLogic.canRegen = false;
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            RoadPath newPath = new RoadPath(3);
            newPath.generateNewPath(4, new int[] { 3 });
            newPath.generateNewPath(5, new int[] { 4 });
            newPath.generateNewPath(6, new int[] { 5 });
            //RoadPath result = newPath.joinBackBack(new RoadPath(new int[] { 3, 9, 10 }));
            //Console.WriteLine(String.Join(",", result.getRoadIDs()));
            //Assert.AreEqual(new int[] { 10, 9, 3, 4, 5, 6 }, result.getRoadIDs());
        }

        [Test]
        public void moreRareDifficultCase12or7()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            testLogic.canRegen = false;
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            RoadPath newPath = new RoadPath(3);
            testLogic.setRoadActivity(0, "Stentopher");
            testLogic.setRoadActivity(1, "Stentopher");
            testLogic.setRoadActivity(7, "Stentopher");
            testLogic.setRoadActivity(12, "Stentopher");
            testLogic.setRoadActivity(2, "Stentopher");
            testLogic.setRoadActivity(3, "Stentopher");
            testLogic.setRoadActivity(8, "Stentopher");
            testLogic.setRoadActivity(14, "Stentopher");
            RoadPath testedPath = testLogic.setRoadActivity(13, "Stentopher");
            Console.WriteLine(String.Join(",", testedPath.getRoadIDs()));
            Assert.AreEqual(8, testLogic.LongestRoadCount);
        }

        [Test]
        public void testLongestRoadRareCase12or7Part2()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            testLogic.canRegen = false;
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            RoadPath newPath = new RoadPath(3);
            testLogic.setRoadActivity(0, "Stentopher");
            testLogic.setRoadActivity(1, "Stentopher");
            testLogic.setRoadActivity(7, "Stentopher");
            testLogic.setRoadActivity(12, "Stentopher");
            testLogic.setRoadActivity(2, "Stentopher");
            testLogic.setRoadActivity(3, "Stentopher");
            testLogic.setRoadActivity(8, "Stentopher");
            testLogic.setRoadActivity(14, "Stentopher");
            testLogic.setRoadActivity(13, "Stentopher");
            RoadPath testedPath = testLogic.setRoadActivity(11, "Stentopher");
            Console.WriteLine(String.Join(",", testedPath.getRoadIDs()));
            Assert.AreEqual(8, testLogic.LongestRoadCount);
        }

        [Test]
        public void testEvenRarerCase()
        {
            ServerLogic testLogic = new ServerLogic(this.newLobby);
            testLogic.canRegen = false;
            GamePlayer player = testLogic.gameLobby.gamePlayers[1];
            RoadPath newPath = new RoadPath(3);
            testLogic.setRoadActivity(0, "Stentopher");
            testLogic.setRoadActivity(1, "Stentopher");
            testLogic.setRoadActivity(7, "Stentopher");
            testLogic.setRoadActivity(6, "Stentopher");
            testLogic.setRoadActivity(11, "Stentopher");
            testLogic.setRoadActivity(10, "Stentopher");
            testLogic.setRoadActivity(18, "Stentopher");
            testLogic.setRoadActivity(24, "Stentopher");
            testLogic.setRoadActivity(25, "Stentopher");
            testLogic.setRoadActivity(19, "Stentopher");
            testLogic.setRoadActivity(26, "Stentopher");
            testLogic.setRoadActivity(27, "Stentopher");
            testLogic.setRoadActivity(20, "Stentopher");
            RoadPath testedPath = testLogic.setRoadActivity(13, "Stentopher");
            Console.WriteLine(String.Join(",", testedPath.getRoadIDs()));
            Assert.AreEqual(14, testLogic.LongestRoadCount);
        }
    }
}
