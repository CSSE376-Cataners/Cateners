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
using Microsoft.QualityTools.Testing.Fakes;

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

        [Test]
        public void testProcesssMessageLogin()
        {
            FakeClient client = new FakeClient();
            Login goodLogin = new Login("Good", "Password");

            catanersDataSet.checkUserDataTableDataTable table = new catanersDataSet.checkUserDataTableDataTable();
            table.AddcheckUserDataTableRow(table.NewcheckUserDataTableRow());

            catanersDataSet.checkUserDataTableRow row = (catanersDataSet.checkUserDataTableRow)table.Rows[0];
            row.UID = 1;
            row.Username = "Good";

            IDatabase sDB = mocks.DynamicMock<IDatabase>();

            sDB.Stub(call => call.getUser(Arg<Login>.Is.Anything)).Return(row);
            mocks.ReplayAll();

            Database.INSTANCE = sDB;


            // Good Login
            String jsonString = "{\"type\":0,\"message\":\"{\\\"username\\\":\\\"Good\\\",\\\"password\\\":\\\"Password\\\",\\\"register\\\":false}\"}";
            client.processesMessage(jsonString);
            Assert.AreEqual("{\"type\":0,\"message\":\"1\"}", client.lastCall);
            Assert.AreEqual("Good", client.userName);
            Assert.AreEqual(1, client.userID);

            sDB.Stub(call => call.getUser(Arg<Login>.Is.Anything)).Return(null);
            mocks.ReplayAll();
            jsonString = "{\"type\":0,\"message\":\"{\\\"username\\\":\\\"Good\\\",\\\"password\\\":\\\"Password\\\",\\\"register\\\":false}\"}";
            client.processesMessage(jsonString);
            Assert.AreEqual("{\"type\":0,\"message\":\"-1\"}", client.lastCall);

        }

        [Test]
        public void testProcessMessageRegister()
        {
            FakeClient client = new FakeClient();
            Login goodLogin = new Login("Good", "Password");

            catanersDataSet.checkUserDataTableDataTable table = new catanersDataSet.checkUserDataTableDataTable();
            table.AddcheckUserDataTableRow(table.NewcheckUserDataTableRow());

            catanersDataSet.checkUserDataTableRow row = (catanersDataSet.checkUserDataTableRow)table.Rows[0];
            row.UID = 1;
            row.Username = "Good";

            IDatabase sDB = mocks.DynamicMock<IDatabase>();

            sDB.Stub(call => call.registerUser(Arg<Login>.Is.Anything)).Return(1);
            mocks.ReplayAll();

            Database.INSTANCE = sDB;

            String jsonString = "{\"type\":1,\"message\":\"{\\\"username\\\":\\\"Good\\\",\\\"password\\\":\\\"Password\\\",\\\"register\\\":false}\"}";
            client.processesMessage(jsonString);
            Assert.AreEqual("{\"type\":1,\"message\":\"1\"}", client.lastCall);
        }

        public class FakeClient : Client
        {

            public FakeClient()
            {

            }

            public String lastCall = null;

            public override void sendToClient(String msg)
            {
                lastCall = msg;
            }
        }
    }
}