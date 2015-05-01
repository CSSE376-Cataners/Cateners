using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Cataners;
using CatanersShared;
using System.Reflection;
using WaveEngineGameProject;

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

        [Test]
        public void testProcessMessageBadMessage()
        {
            FakeClient client = new FakeClient();

            client.processesMessage("LALALA");

            Assert.Null(client.lastCall);
        }

        [Test]
        public void testProcessMessageLogin()
        {
            FakeClient client = new FakeClient();

            client.processesMessage(new Message("Username",Translation.TYPE.Login).toJson());
            Assert.AreEqual("Username", Data.username);

            client.processesMessage(new Message("-1", Translation.TYPE.Login).toJson());
            Assert.AreEqual("Username", Data.username);

            object o = client.queues[Translation.TYPE.Login].Take();
            Assert.AreEqual("Username", o);
            o = client.queues[Translation.TYPE.Login].Take();
            Assert.AreEqual("-1", o);
            
        }

        [Test]
        public void testProcessMessageRegister()
        {
            FakeClient client = new FakeClient();

            client.processesMessage(new Message("Register Message", Translation.TYPE.Register).toJson());

            Assert.AreEqual("Register Message", client.queues[Translation.TYPE.Register].Take());
        }

        [Test]
        public void testProccessMessageHexMessage()
        {
            FakeClient client = new FakeClient();

            int[][] array = new int[][] {new int[] {1 ,2}, new int[] {3, 4}};
            //client.processesMessage(new Message(Translation.intArraytwotoJson(array)))
        }

        public class FakeClient : CommunicationClient
        {
            public FakeClient()
            {

            }

            public String lastCall = null;

            public override void sendToServer(String msg)
            {
                lastCall = msg;
            }
        }
    }
}
