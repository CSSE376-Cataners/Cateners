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

            Assert.AreEqual("{\"source\":{\"resources\":{\"Brick\":0,\"Ore\":0,\"Sheep\":0,\"Wheat\":0,\"Wood\":0},\"Ready\":false,\"resourceCount\":0,\"Username\":\"Sender\"},\"target\":{\"resources\":{\"Brick\":0,\"Ore\":0,\"Sheep\":0,\"Wheat\":0,\"Wood\":0},\"Ready\":false,\"resourceCount\":0,\"Username\":\"Reciver\"},\"offeredResources\":{\"Wheat\":10,\"Sheep\":11,\"Brick\":12,\"Ore\":13,\"Wood\":14,\"Desert\":15},\"wantedResources\":{\"Wheat\":10,\"Sheep\":11,\"Brick\":12,\"Ore\":13,\"Wood\":14,\"Desert\":15}}", json);
        }

        [Test]
        public void testFromJson()
        {
            Dictionary<Resource.TYPE, int> offer = new Dictionary<Resource.TYPE, int>();
            Dictionary<Resource.TYPE, int> request = new Dictionary<Resource.TYPE, int>();

            int x = 0;
            foreach (Resource.TYPE t in Enum.GetValues(typeof(Resource.TYPE)))
            {
                offer[t] = 10 + x;
                request[t] = 10 + x++;
            }

            Trade trade = new Trade(new GamePlayer("Sender"), new GamePlayer("Reciver"), offer, request);

            String json = trade.toJson();

            Trade reverse = Trade.fromJson(json);

            Assert.AreEqual(trade, reverse);
        }

        [Test]
        public void testEquals()
        {
            Dictionary<Resource.TYPE, int> offer = new Dictionary<Resource.TYPE, int>();
            Dictionary<Resource.TYPE, int> request = new Dictionary<Resource.TYPE, int>();

            int x = 0;
            foreach (Resource.TYPE t in Enum.GetValues(typeof(Resource.TYPE)))
            {
                offer[t] = 10 + x;
                request[t] = 10 + x++;
            }

            Trade trade1 = new Trade(new GamePlayer("Sender"), new GamePlayer("Reciver"), offer, request);

            Dictionary<Resource.TYPE, int> offer2 = new Dictionary<Resource.TYPE, int>();
            Dictionary<Resource.TYPE, int> request2 = new Dictionary<Resource.TYPE, int>();

            x = 5;
            foreach (Resource.TYPE t in Enum.GetValues(typeof(Resource.TYPE)))
            {
                offer2[t] = 10 + x;
                request2[t] = 10 + x++;
            }

            Trade trade = new Trade(new GamePlayer("Sender"), new GamePlayer("Reciver"), offer2, request2);

            Dictionary<Resource.TYPE, int> offera = new Dictionary<Resource.TYPE, int>();
            Dictionary<Resource.TYPE, int> requesta = new Dictionary<Resource.TYPE, int>();

            x = 0;
            foreach (Resource.TYPE t in Enum.GetValues(typeof(Resource.TYPE)))
            {
                offera[t] = 10 + x;
                requesta[t] = 10 + x++;
            }

            Trade tradea = new Trade(new GamePlayer("Sender"), new GamePlayer("Reciver"), offera, requesta);


            Assert.True(trade.Equals(trade));
            Assert.True(trade1.Equals(tradea));
            Assert.False(trade.Equals(trade1));
            Assert.False(trade.Equals(null));
            Assert.False(trade.Equals(""));


            Trade trade3 = new Trade(new GamePlayer("Diffrent"), new GamePlayer("Reciver"), offer, request);
            Trade trade4 = new Trade(new GamePlayer("Sender"), new GamePlayer("Diffrent"), offer, request);


            Assert.False(trade.Equals(trade3));
            Assert.False(trade.Equals(trade4));



        }

        [Test]
        public void testHashCode()
        {
            Dictionary<Resource.TYPE, int> offer = new Dictionary<Resource.TYPE, int>();
            Dictionary<Resource.TYPE, int> request = new Dictionary<Resource.TYPE, int>();

            int x = 0;
            foreach (Resource.TYPE t in Enum.GetValues(typeof(Resource.TYPE)))
            {
                offer[t] = 10 + x;
                request[t] = 10 + x++;
            }

            Trade trade1 = new Trade(new GamePlayer("Sender"), new GamePlayer("Reciver"), offer, request);

            Dictionary<Resource.TYPE, int> offer2 = new Dictionary<Resource.TYPE, int>();
            Dictionary<Resource.TYPE, int> request2 = new Dictionary<Resource.TYPE, int>();

            x = 5;
            foreach (Resource.TYPE t in Enum.GetValues(typeof(Resource.TYPE)))
            {
                offer2[t] = 10 + x;
                request2[t] = 10 + x++;
            }

            Trade trade = new Trade(new GamePlayer("Sender"), new GamePlayer("Reciver"), offer2, request2);

            Dictionary<Resource.TYPE, int> offera = new Dictionary<Resource.TYPE, int>();
            Dictionary<Resource.TYPE, int> requesta = new Dictionary<Resource.TYPE, int>();

            x = 0;
            foreach (Resource.TYPE t in Enum.GetValues(typeof(Resource.TYPE)))
            {
                offera[t] = 10 + x;
                requesta[t] = 10 + x++;
            }

            Trade tradea = new Trade(new GamePlayer("Sender"), new GamePlayer("Reciver"), offera, requesta);

            int a = Trade.getResourcesHash(request);
            int b = Trade.getResourcesHash(requesta);

            Assert.AreEqual(trade.GetHashCode(),trade.GetHashCode());
            Assert.AreEqual(trade1.GetHashCode(), tradea.GetHashCode());
            Assert.AreNotEqual(trade.GetHashCode(), trade1.GetHashCode());


            Trade trade3 = new Trade(new GamePlayer("Diffrent"), new GamePlayer("Reciver"), offer, request);
            Trade trade4 = new Trade(new GamePlayer("Sender"), new GamePlayer("Diffrent"), offer, request);


            Assert.AreNotEqual(trade.GetHashCode(), trade3.GetHashCode());
            Assert.AreNotEqual(trade.GetHashCode(),trade4.GetHashCode());
        }


    }
}
