using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;

namespace CatanersTest
{
    [TestFixture]
    class ResourceTesting
    {

        [Test]
        public void ResourceTestInitialize()
        {
            Player player = new Player("Bobby Tables");
            AddResource resource = new AddResource(player, Resource.TYPE.Brick,3);

            Assert.AreEqual(player, resource.player);
            Assert.AreEqual(Resource.TYPE.Brick, resource.resourceType);
            Assert.AreEqual(3, resource.number);

         

        }

    }
}
