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
            Lobby newLobby = new Lobby("Test", 3, new Player("Steve"));
            Assert.False(newLobby.Equals(null));
        }

        [Test]
        public void testStringMatch()
        {
            Lobby newLobby = new Lobby("Test", 3, new Player("Steve"));
            Assert.AreEqual(newLobby.GameName, "Test");
        }


        [Test]
        public void testLobbyToJson()
        {
            Lobby newLobby = new Lobby("Test", 3, new Player("Steve"));
            Assert.AreEqual("{\"players\":[{\"Username\":\"Steve\"}],\"Players\":[{\"Username\":\"Steve\"}],\"GameName\":\"Test\",\"MaxTimePerTurn\":3,\"Owner\":{\"Username\":\"Steve\"}}", newLobby.toJson());
        }
    }
}
