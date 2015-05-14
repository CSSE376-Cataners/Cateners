using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CatanersShared;
using CatenersServer;
using System.Net.Sockets;
using System.Collections.Concurrent;
using System.Collections;
using System.IO;
using System.Threading;
using Rhino.Mocks;
using Newtonsoft.Json.Converters;
using Microsoft.QualityTools.Testing.Fakes;
using System.Reflection;
using WaveEngine;
using Cataners;
using WaveEngine.Framework;

namespace CatanersTest
{
    [TestFixture()]
    class ServerLogicTesting
    {
        private MockRepository mocks = new MockRepository();

        [SetUp]
        public void ServerLogicSetup()
        {
        }

        [Test]
        public void testHexToShadow()
        {
            ServerLogic logic = new ServerLogic(new Lobby("Basketball", 100, new Player("Michael Jordan"), 10));
            HexServer targetHolder = new HexServer(2);
            targetHolder.setPlacementNumber(3);
            targetHolder.setRollNumber(3);
            SettlementServer[] setArray = new SettlementServer[6];
            for (int k = 0; k < 6; k++)
            {
                setArray[k] = new SettlementServer(1, k);
            }
            targetHolder.setSettlementArray(setArray);
            Assert.AreEqual(targetHolder.toShadow(), new int[] { 2, 3, 3, 0, 1, 2, 3, 4, 5 });
        }

        [Test]
        public void testThatPlayerTurnSwitches()
        {
            Player p0 = new Player("Michael Jordan");
            Lobby lobby = new Lobby("Basketball", 100, p0, 10);
            lobby.addPlayer(new Player("p1"));
            lobby.addPlayer(new Player("p2"));
            ServerLogic logic = new ServerLogic(lobby);
            logic.playerTurn = 1;
            logic.updateTurn();
            Assert.AreEqual(2, logic.playerTurn);
        }
        [Test]
        public void testThatPlayerTurnSwitches2()
        {
            Player p0 = new Player("Michael Jordan");
            Lobby lobby = new Lobby("Basketball", 100, p0, 10);
            lobby.addPlayer(new Player("p1"));
            lobby.addPlayer(new Player("p2"));
            lobby.addPlayer(new Player("p3"));
            ServerLogic logic = new ServerLogic(lobby);
            logic.playerTurn = 0;
            logic.updateTurn();
            Assert.AreEqual(1, logic.playerTurn);
        }
        [Test]
        public void testAllPlayerSwitchesWork()
        {
            Player p0 = new Player("Michael Jordan");
            Lobby lobby = new Lobby("Basketball", 100, p0, 10);
            lobby.addPlayer(new Player("p1"));
            lobby.addPlayer(new Player("p2"));
            lobby.addPlayer(new Player("p3"));
            ServerLogic logic = new ServerLogic(lobby);
            logic.playerTurn = 0;
            logic.updateTurn();
            Assert.AreEqual(1, logic.playerTurn);
            logic.updateTurn();
            Assert.AreEqual(2, logic.playerTurn);
            logic.updateTurn();
            Assert.AreEqual(3, logic.playerTurn);
            logic.updateTurn();
            Assert.AreEqual(0, logic.playerTurn);
            
        }

        [Test]
        public void testThatboardHexesAreMade()
        {
            ServerLogic logic = new ServerLogic(new Lobby("Basketball", 100, new Player("Michael Jordan"), 10));
            int[][] board = logic.gethexArray();
            for (int i = 0; i < 19; i++)
            {
                //0 = type    1 = placement number        2 = die roll
                Assert.AreEqual((int)logic.board.hexes[board[i][1]].dice,board[i][2]);
                Assert.AreEqual((int)logic.board.hexes[board[i][1]].type, board[i][0]);
            }
        }


        [Test]
        public void testThatOwnerGetsAddedToObjectModel()
        {
            Player player = new Player("IDK Who I am");
            ServerLogic logic = new ServerLogic(new Lobby("Basketball", 100, player, 10));

            foreach (Resource.TYPE t in Enum.GetValues(typeof(Resource.TYPE)))
            {
                logic.gameLobby.gamePlayers[0].resources[t] = 10;
            }

            logic.determineSettlementAvailability(player.Username, 10);

            Assert.AreEqual(player.Username, logic.board.buildings[10].owner.Username);


        }

        [Test]
        public void testThatDieRollGivesResources()
        {
            Player player = new Player("IDK Who I am");
            ServerLogic logic = new ServerLogic(new Lobby("Basketball", 100, player, 10));

            int oldBrick = logic.gameLobby.gamePlayers[0].resources[Resource.TYPE.Brick];
            
            logic.board.hexes[0].dice = 10;
            logic.board.hexes[0].type = Resource.TYPE.Brick;

            logic.dice = 10;

            logic.board.hexes[0].buildings[0].owner = logic.gameLobby.gamePlayers[0];

            logic.diceRolled();

            Assert.True(oldBrick < logic.gameLobby.gamePlayers[0].resources[Resource.TYPE.Brick]);
        }
        [Test]
        public void updateTurnStartPhase1()
        {
            Player p0 = new Player("Michael Jordan");
            Lobby lobby = new Lobby("Basketball", 100, p0, 10);
            lobby.addPlayer(new Player("p1"));
            lobby.addPlayer(new Player("p2"));
            lobby.addPlayer(new Player("p3"));
            ServerLogic logic = new ServerLogic(lobby);
            logic.playerTurn = 0;
            logic.updateTurnStartPhase1();
            Assert.AreEqual(1, logic.playerTurn);
        }


        /*public void TestResourceConstructor()
        {
            Resource wheat = new Resource("wheat");
            Assert.NotNull(wheat);
        }*/
    }
}
