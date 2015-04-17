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

namespace CatanersTest
{
    [TestFixture()]
    class ClientTesting
    {

        private MockRepository mocks = new MockRepository();

        /*[Test]
        public void testprocessMessage()
        {
            Message newMessage = new Message("hello", Translation.TYPE.Login);
            String stringToPass = newMessage.ToString();
            TcpClient mockClient = mocks.StrictMock<TcpClient>();
            Socket mockSocket = mocks.StrictMock<Socket>();
            NetworkStream newStream = new NetworkStream(mockSocket);
            Expect.Call(mockClient.GetStream()).Return(newStream);
            mocks.ReplayAll();
            Client target = new Client(mockClient);
            target.processesMessage(stringToPass);
            mocks.VerifyAll();
        }*/
    }
}