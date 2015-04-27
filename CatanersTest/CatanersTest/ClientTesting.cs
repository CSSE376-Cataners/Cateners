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
using System.Reflection;

namespace CatanersTest
{
    [TestFixture()]
    class ClientTesting
    {

        private MockRepository mocks = new MockRepository();

        [Test]
        public void testProcesssMessageLogin()
        {
            FakeClient client = new FakeClient();
            Login goodLogin = new Login("Good", "Password");

            catanersDataSet.checkUserDataTableDataTable table = new catanersDataSet.checkUserDataTableDataTable();
            table.AddcheckUserDataTableRow(table.NewcheckUserDataTableRow());

            catanersDataSet.checkUserDataTableRow row = (catanersDataSet.checkUserDataTableRow)table.Rows[0];
            row.UID = 1;
            row.Username = goodLogin.username;

            IDatabase sDB = mocks.DynamicMock<IDatabase>();

            sDB.Stub(call => call.getUser(Arg<Login>.Is.Anything)).Return(row);
            mocks.ReplayAll();

            Database.INSTANCE = sDB;


            // Good Login
            String jsonString = new Message(goodLogin.toJson(), Translation.TYPE.Login).toJson();
            
            client.processesMessage(jsonString);

            Assert.AreEqual(new Message(goodLogin.username, Translation.TYPE.Login).toJson(), client.lastCall);
            Assert.AreEqual(goodLogin.username, client.userName);
            Assert.AreEqual(1, client.userID);


            IDatabase sDB2 = mocks.DynamicMock<IDatabase>();

            sDB2.Stub(call => call.getUser(Arg<Login>.Is.Anything)).Return(null);
            mocks.ReplayAll();

            Database.INSTANCE = sDB2;
            mocks.ReplayAll();
            client.processesMessage(jsonString);

            Assert.AreEqual(new Message("-1", Translation.TYPE.Login).toJson(), client.lastCall);


            jsonString = new Message("",Translation.TYPE.Login).toJson();
            client.processesMessage(jsonString);
            Assert.AreEqual(new Message("-1", Translation.TYPE.Login).toJson(), client.lastCall);

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

            String jsonString = new Message(goodLogin.toJson(), Translation.TYPE.Register).toJson();
            client.processesMessage(jsonString);
            Assert.AreEqual(new Message("1",Translation.TYPE.Register).toJson(), client.lastCall);

            IDatabase sDB2 = mocks.DynamicMock<IDatabase>();

            sDB2.Stub(call => call.registerUser(Arg<Login>.Is.Anything)).Return(-1);
            mocks.ReplayAll();

            Database.INSTANCE = sDB2;

            client.processesMessage(jsonString);
            Assert.AreEqual(new Message("-1", Translation.TYPE.Register).toJson(), client.lastCall);
        }

        [Test]
        public void testProcessMessageRequestLobbies()
        {
            FakeClient client = new FakeClient();

            List<Lobby> lobbies = new List<Lobby>();
            lobbies.Add(new Lobby("Game1", 10, new Player("Owner One"),Data.INSTANCE.nextLobbyID++));
            lobbies.Add(new Lobby("Game2", 1, new Player("Owner Two"),Data.INSTANCE.nextLobbyID++));
            lobbies.Add(new Lobby("Game3", -1, new Player("Owner Three"),Data.INSTANCE.nextLobbyID++));

            Data.INSTANCE.Lobbies.Clear();
            Data.INSTANCE.Lobbies.AddRange(lobbies);

            String jsonString = "{\"type\":2,\"message\":\"\"}";
            client.processesMessage(jsonString);
            Assert.AreEqual(new Message(Newtonsoft.Json.JsonConvert.SerializeObject(Data.INSTANCE.Lobbies), Translation.TYPE.RequestLobbies).toJson(), client.lastCall);
        }

        [Test]
        public void testProcessMessageCreateLobby()
        {
            FakeClient client = new FakeClient();
            client.userName = "TestUserName";

            String jsonString = new Message(new Lobby("GameName", 10, new Player("BadUserName"), 100).toJson(),Translation.TYPE.CreateLobby).toJson();


            int count = Data.INSTANCE.Lobbies.Count;
            int oldID = Data.INSTANCE.nextLobbyID;
            client.processesMessage(jsonString);
            
            Assert.True(count < Data.INSTANCE.Lobbies.Count);
            Assert.True(oldID < Data.INSTANCE.nextLobbyID);

            Assert.AreEqual(new Lobby("GameName",10,new Player("TestUserName"),oldID),Data.INSTANCE.Lobbies[count]);
        }

        [Test]
        public void testProcessMessageChangeReadyStatus()
        {
            FakeClient client = new FakeClient();

            String readyString = new Message(true.ToString(), Translation.TYPE.ChangeReadyStatus).toJson();
            String falseString = new Message(false.ToString(), Translation.TYPE.ChangeReadyStatus).toJson();
            client.processesMessage(readyString);

            Assert.Null(client.lastCall);

            client.userName = "GoodUser";
            client.currentLobby = new Lobby("GameName", 10, new Player("GoodUser"),10);

            Assert.False(client.currentLobby.Players[0].Ready);

            client.processesMessage(readyString);
            Assert.True(client.currentLobby.Players[0].Ready);
            client.processesMessage(readyString);
            Assert.True(client.currentLobby.Players[0].Ready);
            client.processesMessage(falseString);
            Assert.False(client.currentLobby.Players[0].Ready);

            client.currentLobby.Players.Insert(0, new Player("OtherUser"));
            client.processesMessage(readyString);
            Assert.True(client.currentLobby.Players[1].Ready);

            // Force Zero Case. Not applicable with out forced Buissnesss Logic, but still want to test for mutation.
            client.currentLobby.Players.Clear();
            client.processesMessage(readyString);

        }

        [Test]
        public void testProcessMessageJoinLobby()
        {
            FakeClient client = new FakeClient();

            int id = 10;
            String joinGame = new Message(id.ToString(), Translation.TYPE.JoinLobby).toJson();

            client.processesMessage(joinGame);

            Assert.Null(client.currentLobby);

            Lobby lobby = new Lobby("Gamename", -1, new Player("Owner"), 10);
            Data.INSTANCE.Lobbies.Add(lobby);

            client.processesMessage(joinGame);

            Assert.AreEqual(lobby, client.currentLobby);
        }

        [Test]
        public void testProcessMessageLeaveLobbyThatICantJoinFullGame()
        {
            FakeClient client = new FakeClient();

            int id = 10;
            String joinGame = new Message(id.ToString(), Translation.TYPE.JoinLobby).toJson();
            client.userName = "Trent";
            Player owner = new Player("Owner");
            Lobby lobby = new Lobby("Gamename", -1, owner, 10);
            
            lobby.addPlayer(new Player("player2"));
            lobby.addPlayer(new Player("player3"));
            lobby.addPlayer(new Player("player4"));

            Data.INSTANCE.Lobbies.Add(lobby);

            client.processesMessage(joinGame);

            for (int i = 0; i < lobby.PlayerCount; i++)
            {
                Assert.AreNotEqual(lobby.Players[i].Username, client.userName);
            }
        }

        [Test]
        public void testProcessMessageUpdateLobby()
        {
            FakeClient client = new FakeClient();
            String updateMessage = new Message("", Translation.TYPE.UpdateLobby).toJson();

            client.processesMessage(updateMessage);
            Assert.Null(client.lastCall);

            client.currentLobby = new Lobby("GameName", 10, new Player("Owner"), 1);
            client.processesMessage(updateMessage);

            Assert.AreEqual(new Message(client.currentLobby.toJson(), Translation.TYPE.UpdateLobby).toJson(), client.lastCall);
        }

        [Test]
        public void testProcessMessageLeaveLobbyThatIHaveNoLobby()
        {
            FakeClient client = new FakeClient();
            int id = 100;
            String leaveGame = new Message("", Translation.TYPE.LeaveLobby).toJson();
            client.processesMessage(leaveGame);
            Assert.Null(client.currentLobby);

            Lobby lobby = new Lobby("game", -1, new Player("owner"), id);

            Player player = new Player("User1");
            client.userName = player.Username;

            lobby.addPlayer(player);
            client.currentLobby = lobby;

            client.processesMessage(leaveGame);
            Assert.Null(client.currentLobby);

        }
        
        [Test]
        public void testProcessMessageLeaveLobbyThatImNotInLobby()
        {
            FakeClient client = new FakeClient();
            client.userName = "Bobby";
            int id = 100;
            String leaveGame = new Message("", Translation.TYPE.LeaveLobby).toJson();
            client.processesMessage(leaveGame);
            Assert.Null(client.currentLobby);

            Lobby lobby = new Lobby("game", -1, new Player("owner"), id);
            
            lobby.addPlayer(new Player("Not Me"));
            client.currentLobby = lobby;
            client.processesMessage(leaveGame);
            for (int i = 0; i < lobby.PlayerCount; i++)
            {
                Assert.AreNotEqual(client.userName, lobby.Players[i].Username);
            }

        }

        [Test]
        public void testProcessMessageLeaveLobbyThatOwnerLeavingKicksEveryone()
        {
            FakeClient client = new FakeClient();
            client.userName = "Trent";
            int id = 100;
            String leaveGame = new Message("", Translation.TYPE.LeaveLobby).toJson();
            client.processesMessage(leaveGame);
            Assert.Null(client.currentLobby);

            Lobby lobby = new Lobby("game", -1, new Player(client.userName), id);

            
            lobby.addPlayer(new Player("JimBob"));
            lobby.addPlayer(new Player("BobbyTables"));
            client.currentLobby = lobby;
            client.processesMessage(leaveGame);

            Assert.AreEqual(0, lobby.PlayerCount);
        }

        [Test]

        public void testProcessMessageLeaveLobbyThatOwnerLeavingRemovesGame()
        {
            FakeClient client = new FakeClient();
            client.userName = "Trent";
            int id = 100;
            String leaveGame = new Message("", Translation.TYPE.LeaveLobby).toJson();
            client.processesMessage(leaveGame);
            Assert.Null(client.currentLobby);

            Lobby lobby = new Lobby("game", -1, new Player(client.userName), id);
            Lobby differentlobby = new Lobby("game2", -1, new Player("CJ"), 150);
            Data.INSTANCE.Lobbies.Add(lobby);
            Data.INSTANCE.Lobbies.Add(differentlobby);


            client.currentLobby = lobby;
            client.processesMessage(leaveGame);

            for(int i = 0; i< Data.INSTANCE.Lobbies.Count; i++){
                Assert.AreNotEqual(Data.INSTANCE.Lobbies[i].lobbyID, id);
            }
        }

        [Test]
        public void testProcessMessageDefaultCase()
        {
            FakeClient client = new FakeClient();

            String defaultCase = new Message("", Translation.TYPE.Unknown).toJson();

            // Makeing sure no Exceptions.
            client.processesMessage(defaultCase);
        }

        [Test]
        public void testSendToClient()
        {
            Client client = new Client();

            MemoryStream memStream = new MemoryStream(1024);
            StreamWriter fakeStream = new StreamWriter(memStream, Encoding.Unicode);

            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            FieldInfo field = typeof(Client).GetField("writer", bindFlags);
            Console.WriteLine(field.ToString());
            field.SetValue(client, fakeStream);

            String testMessage = "Test Message abc123";
            client.sendToClient(testMessage);

            String s = Encoding.Unicode.GetString(memStream.ToArray());
            Assert.Less(2, s.Length);
            Assert.AreEqual(testMessage + "\r\n",  s.Substring(1));
        }

        [Test]
        public async void testNormalConstructor()
        {
            TcpListener listener = new TcpListener(System.Net.IPAddress.Any ,9999);
            TcpClient tcpClient = new TcpClient("127.0.0.1",9999);

            listener.Start();
            tcpClient.ConnectAsync("127.0.0.1",9999);

            TcpClient serverSide = await listener.AcceptTcpClientAsync();

            Client client = new Client(serverSide);

            Assert.True(client.Enabled);
            Assert.AreEqual(-1, client.userID);
            Assert.Null(client.userName);
            Assert.Null(client.currentLobby);

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo fieldTCP = typeof(FakeClient).GetField("tcp", flags);
            FieldInfo fieldReader = typeof(FakeClient).GetField("reader", flags);
            FieldInfo fieldWriter = typeof(FakeClient).GetField("writer", flags);

            Assert.AreEqual(serverSide, fieldTCP.GetValue(serverSide));

            Assert.NotNull(fieldReader.GetValue(client));
            Assert.NotNull(fieldWriter.GetValue(client));
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