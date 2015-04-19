using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CatanersShared;
using CatenersServer;
using System.Net.Sockets;
using System.Collections.Concurrent;
using System.Collections;
using System.IO;
using System.Threading;
using Rhino.Mocks;
using Newtonsoft.Json.Converters;
using CatanersShared;

namespace CatanersTest
{
    [TestFixture()]
    class ClientTesting
    {

        private MockRepository mocks = new MockRepository();

        [Test]
        public void testprocessMessage()
        {
            Message newMessage = new Message(new Login("steve38", "helloH38").toJson(), Translation.TYPE.Login);
            TcpClient newTcp = mocks.DynamicMock<TcpClient>();
            NetworkStream newStream = mocks.DynamicMock<NetworkStream>();
            Expect.Call(newTcp.Connected).PropertyBehavior();
            //newTcp.Connected = true;
            Expect.Call(newTcp.GetStream()).Return(newStream);
            mocks.ReplayAll();
            Client target = new Client(newTcp);
            target.processesMessage(newMessage.toJson());
            mocks.VerifyAll();
        }
    }
}