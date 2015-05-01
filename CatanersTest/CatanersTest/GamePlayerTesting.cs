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
    }
}
