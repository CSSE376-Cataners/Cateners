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
    class TradeObjectTesting
    {
        [Test]
        public void TestConstrutor()
        {
            GamePlayer source = new GamePlayer("source");
            GamePlayer target = new GamePlayer("target");
            Dictionary<Resource.TYPE,int> offered = new Dictionary<Resource.TYPE,int>();
            Dictionary<Resource.TYPE,int> wanted = new Dictionary<Resource.TYPE,int>();

            Trade tradeobj = new Trade(source, target, offered, wanted);
            Assert.IsNotNull(tradeobj.source);
            Assert.IsNotNull(tradeobj.target);
            Assert.IsNotNull(tradeobj.offeredResources);
            Assert.IsNotNull(tradeobj.wantedResources);
        }

        [Test]
        public void testToJson()
        {
            Dictionary<Resource.TYPE,int> offer = new Dictionary<Resource.TYPE,int>();
            Dictionary<Resource.TYPE,int> request = new Dictionary<Resource.TYPE,int>();

            int x = 0;
            foreach (Resource.TYPE t in Enum.GetValues(typeof(Resource.TYPE)))
            {
                offer[t] = 10 + x;
                request[t] = 10 + x++;
            }

            Trade trade = new Trade(new GamePlayer("Sender"), new GamePlayer("Reciver"), offer, request);

            String json = trade.toJson();

            Console.WriteLine(json);

            Assert.AreEqual("{\"source\":{\"resources\":{\"Brick\":0,\"Ore\":0,\"Sheep\":0,\"Wheat\":0,\"Wood\":0},\"Ready\":false,\"Username\":\"Sender\"},\"target\":{\"resources\":{\"Brick\":0,\"Ore\":0,\"Sheep\":0,\"Wheat\":0,\"Wood\":0},\"Ready\":false,\"Username\":\"Reciver\"},\"offeredResources\":{\"Wheat\":10,\"Sheep\":11,\"Brick\":12,\"Ore\":13,\"Wood\":14},\"wantedResources\":{\"Wheat\":10,\"Sheep\":11,\"Brick\":12,\"Ore\":13,\"Wood\":14}}", json);
        }




    }
}
