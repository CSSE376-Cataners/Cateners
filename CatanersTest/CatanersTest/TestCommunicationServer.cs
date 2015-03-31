using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CatenersServer;

namespace CatanersTest
{
    [TestFixture()]
    class TestCommunicationServer
    {

        [Test]
        public void testTCPIntializes()
        {
            CommunicationServer server = new CommunicationServer();
            Assert.NotNull(server.listener);
        }
    }
}
