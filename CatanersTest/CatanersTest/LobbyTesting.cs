﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using CatanersShared;

namespace CatanersTest
{
    [TestFixture()]
    class LobbyTesting
    {

        [Test]
        public void testConstructor()
        {
            Lobby newLobby = new Lobby("Test", 3, new Player("Steve"), 2);
            Assert.False(newLobby.Equals(null));
        }

        [Test]
        public void testStringMatch()
        {
            Lobby newLobby = new Lobby("Test", 3, new Player("Steve"), 2);
            Assert.AreEqual(newLobby.GameName, "Test");
        }


        [Test]
        public void testLobbyToJson()
        {
            Lobby newLobby = new Lobby("Test", 3, new Player("Steve"), 2);
            Assert.AreEqual("{\"players\":[{\"Username\":\"Steve\"}],\"Players\":[{\"Username\":\"Steve\"}],\"GameName\":\"Test\",\"MaxTimePerTurn\":3,\"Owner\":{\"Username\":\"Steve\"}}", newLobby.toJson());
        }

        [Test]
        public void testLobbyFromJson()
        {
            Lobby testLobby = new Lobby("Test", 3, new Player("Steve"), 2);
            Console.WriteLine(testLobby);
            Lobby newLobby = Lobby.fromJson("{\"players\":[{\"Username\":\"Steve\"}],\"Players\":[{\"Username\":\"Steve\"}],\"GameName\":\"Test\",\"MaxTimePerTurn\":3,\"Owner\":{\"Username\":\"Steve\"}}");
            Console.WriteLine(newLobby);
            Assert.True(testLobby.Equals(newLobby));
        }

        [Test]
        public void testLobbyEqualsIsEqual()
        {
            Lobby newLobby = new Lobby("game", 3, new Player("name"), 2);
            Lobby target = new Lobby("game", 3, new Player("name"), 2);
            Assert.True(target.Equals(newLobby));
        }

        [Test]
        public void testLobbyEqualsNullObject()
        {
            Lobby newLobby = null;
            Lobby target = new Lobby("game", 3, new Player("name"), 2);
            Assert.False(target.Equals(newLobby));
        }

        [Test]
        public void testLobbyEqualsNotNullWrongType()
        {
            int newLobby = 0;
            Lobby target = new Lobby("game", 3, new Player("name"), 2);
            Assert.False(target.Equals(newLobby));
        }
    }
}
