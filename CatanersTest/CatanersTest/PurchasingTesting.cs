using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using CatanersShared;
using CatenersServer;

namespace CatanersTest
{
    [TestFixture()]
    public class PurchasingTesting
    {
        [Test]
        public void testAvailabilityFalseResources()
        {
            GamePlayer newPlayer = new GamePlayer("Stentopher");
            newPlayer.resources[Resource.TYPE.Sheep] += 3;
            newPlayer.resources[Resource.TYPE.Brick] += 1;
            newPlayer.resources[Resource.TYPE.Wheat] += 1;
            newPlayer.resources[Resource.TYPE.Wood] = 0;
            ServerLogic testLogic = new ServerLogic(new Lobby("testGame1", 10, new Player("TrottaSN"), 43));
            Assert.False(testLogic.determineSettlementAvailability());
        }
    }
}
