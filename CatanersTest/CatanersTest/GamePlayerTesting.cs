using CatanersShared;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersTest
{
    [TestFixture]
    class GamePlayerTesting
    {
        [Test]
        public void TestConstructor()
        {
            Player player = new Player("Subway");
            GamePlayer gamePlayer = new GamePlayer(player);

            Assert.AreEqual(gamePlayer.Username, player.Username);
        }

        [Test]
        public void TestThatAllResourcesStartAtZero()
        {
            Player player = new Player("Frodo");
            GamePlayer gameplayer = new GamePlayer(player);

            Assert.IsTrue(gameplayer.resources.ContainsKey(Resource.TYPE.Brick));
            Assert.IsTrue(gameplayer.resources.ContainsKey(Resource.TYPE.Ore));
            Assert.IsTrue(gameplayer.resources.ContainsKey(Resource.TYPE.Sheep));
            Assert.IsTrue(gameplayer.resources.ContainsKey(Resource.TYPE.Wheat));
            Assert.IsTrue(gameplayer.resources.ContainsKey(Resource.TYPE.Wood));

            Assert.AreEqual(0, gameplayer.resources[Resource.TYPE.Brick]);
            Assert.AreEqual(0, gameplayer.resources[Resource.TYPE.Ore]);
            Assert.AreEqual(0, gameplayer.resources[Resource.TYPE.Sheep]);
            Assert.AreEqual(0, gameplayer.resources[Resource.TYPE.Wheat]);
            Assert.AreEqual(0, gameplayer.resources[Resource.TYPE.Wood]);
        }
    }
}
