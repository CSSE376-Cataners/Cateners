using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Cataners;

namespace CatanersTest
{
    [TestFixture]
    class TestCommunicationClient
    {
        CommunicationClient client;

        [SetUp]
        public void setupClient()
        {
            client = new CommunicationClient();
        }
        
        [Test]
        public void testClientTCPIntializes()
        {
            Assert.NotNull(client.clientSocket);
        }
    }
}
