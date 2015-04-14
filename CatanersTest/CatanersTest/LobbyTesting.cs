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
            Lobby newLobby = new Lobby("Test", 3);
            Assert.False(newLobby.Equals(null));
        }

        [Test]
        public void testStringMatch()
        {
            Lobby newLobby = new Lobby("Test", 3);
            Assert.AreEqual(newLobby.getGameName(), "Test");
        }
    }
}
