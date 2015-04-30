using System;
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
            Assert.AreEqual("{\"lobbyID\":2,\"Players\":[{\"Ready\":false,\"Username\":\"Steve\"}],\"GameName\":\"Test\",\"MaxTimePerTurn\":3,\"Owner\":{\"Ready\":false,\"Username\":\"Steve\"},\"PlayerCount\":1}", newLobby.toJson());
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
        [Test]
        public void testAddPlayerToLobby()
        {
            Lobby target = new Lobby("game", 10, new Player("BobbyTables"), 5);
            Player bobbyJr = new Player("BobbyTablesJr");
            target.addPlayer(bobbyJr);
            Assert.AreEqual(bobbyJr, target.Players[1]);
        }

        [Test]
        public void testRemovePlayerFromLobby()
        {
            Lobby target = new Lobby("game", 10, new Player("BobbyTables"), 5);
            target.removePlayer(target.Players[0]);
            Assert.AreEqual(0, target.PlayerCount);
        }

        [Test]
        public void testRemoveOwnerFromLobby()
        {
            Lobby target = new Lobby("game", 10, new Player("BobbyTables"), 5);
            target.addPlayer(new Player("JimBob"));
            target.addPlayer(new Player("BobbyJr"));

            target.removeAll();
            Assert.AreEqual(0, target.PlayerCount);

        }

        [Test]
        public void testGetHashCode()
        {
            Lobby lobby1 = new Lobby("GameName", 10, new Player("Username"), 1);
            Lobby lobby2 = new Lobby("GameName", 10, new Player("Username"), 1);

            Assert.AreEqual(lobby1.GetHashCode(), lobby2.GetHashCode());
        }

        [Test]
        public void testJsonToLobbyList()
        {
            Lobby lobby = new Lobby("Gamename", 10, new Player("Owner"), 1);
            Lobby lobby2 = new Lobby("Gamename2" ,10 , new Player("Owner2"), 2);

            List<Lobby> list = new List<Lobby>();
            list.Add(lobby);
            list.Add(lobby2);

            String lobbyText =  Newtonsoft.Json.JsonConvert.SerializeObject(list);

            CollectionAssert.AreEqual(list, Lobby.jsonToLobbyList(lobbyText));
        }
    }
}
