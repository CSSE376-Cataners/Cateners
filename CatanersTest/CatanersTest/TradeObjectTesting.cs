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


    }
}
