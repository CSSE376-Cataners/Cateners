using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;
using CatenersServer;
using NUnit.Framework;
using Rhino.Mocks;

[TestFixture()]
class SettlementTesting
{
    private MockRepository mocks = new MockRepository();

    [SetUp]
    public void SettlementTestingSetup()
    {
    }

    [Test]
    public void testAvailabilityFalseResources()
    {
        GamePlayer newPlayer = new GamePlayer("Stentopher");
        newPlayer.resources[Resource.TYPE.Sheep] += 3;
        newPlayer.resources[Resource.TYPE.Brick] += 1;
        newPlayer.resources[Resource.TYPE.Wheat] += 1;
        newPlayer.resources[Resource.TYPE.Wood] = 0;
        ServerLogic testLogic = new ServerLogic(new Lobby());
        Assert.False(testLogic.determineSettlementAvailability());
    }
}