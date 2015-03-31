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
        CommunicationServer server;
        [SetUp]
        public void serverSetup()
        {
            server = new CommunicationServer();
        }

        [Test]
        public void testTCPIntializes()
        {
            Assert.NotNull(server.listener);
        }


    }
}
