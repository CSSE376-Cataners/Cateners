using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Cataners;
using CatanersShared;
using System.Reflection;

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

        [Test]
        public void testDefaultFields() 
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo fieldEnabled = typeof(CommunicationClient).GetField("Enabled", flags);

            Assert.False((bool)fieldEnabled.GetValue(client));

            Assert.AreEqual(Enum.GetNames(typeof(Translation.TYPE)).Length, client.queues.Count);
        }

        [Test]
        public void testSingleTon()
        {
            Assert.NotNull(CommunicationClient.Instance);
        }

    }
}
