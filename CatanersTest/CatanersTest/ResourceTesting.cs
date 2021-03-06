﻿using NUnit.Framework;
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

        [Test]
        public void ResourceTestToJson()
        {

            Player player = new Player("Bobby Tables");
            AddResource resource = new AddResource(player, Resource.TYPE.Brick, 3);
            Console.WriteLine(resource.toJson());
            Assert.AreEqual("{\"player\":{\"Ready\":false,\"Username\":\"Bobby Tables\"},\"resourceType\":2,\"number\":3}", resource.toJson());

        }

        [Test]
        public void ResourceTestFromJson()
        {

            Player player = new Player("Bobby Tables");
            AddResource resource = new AddResource(player, Resource.TYPE.Brick, 3);
            Console.WriteLine(resource);
            AddResource resource2 = AddResource.fromJson("{\"player\":{\"Ready\":false,\"Username\":\"Bobby Tables\"},\"resourceType\":2,\"number\":3}");

            Assert.True(resource.Equals(resource2));

        }

        [Test]
        public void testEquals()
        {
            AddResource r1 = new AddResource(new Player("Test"), Resource.TYPE.Brick, 5);
            AddResource r2 = new AddResource(new Player("Test"), Resource.TYPE.Brick, 6);
            AddResource r3 = new AddResource(new Player("Test"), Resource.TYPE.Desert, 5);

            Assert.True(r1.Equals(r1));
            Assert.False(r1.Equals(null));
            Assert.False(r1.Equals(""));
            Assert.False(r1.Equals(r2));
            Assert.False(r1.Equals(r3));
        }
    }
}
