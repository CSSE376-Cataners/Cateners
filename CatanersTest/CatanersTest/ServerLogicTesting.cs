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
            ServerLogic logic = new ServerLogic(new Lobby("Basketball", 100, new Player("Michael Jordan"), 10));
            logic.playerTurn = 1;
            logic.updateTurn();
            Assert.AreEqual(2, logic.playerTurn);
        }
        [Test]
        public void testThatPlayerTurnSwitches2()
        {
            ServerLogic logic = new ServerLogic(new Lobby("Basketball", 100, new Player("Michael Jordan"), 10));
            logic.playerTurn = 0;
            logic.updateTurn();
            Assert.AreEqual(1, logic.playerTurn);
        }
        [Test]
        public void testAllPlayerSwitchesWork()
        {
            ServerLogic logic = new ServerLogic(new Lobby("Basketball", 100, new Player("Michael Jordan"), 10));
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

        /*public void TestResourceConstructor()
        {
            Resource wheat = new Resource("wheat");
            Assert.NotNull(wheat);
        }*/
    }
}
