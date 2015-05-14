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
            ServerPlayer player = new ServerPlayer("Good", client);
            client.player = player;
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

            Assert.AreEqual(oldID,Data.INSTANCE.Lobbies[count].lobbyID);
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

            Lobby lobbypre = new Lobby("Gamename2", - 1, new Player("YAYDA"), 1);
            Lobby lobby = new Lobby("Gamename", -1, new Player("Owner"), 10);
            Data.INSTANCE.Lobbies.Add(lobbypre);
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
            ServerPlayer player = new ServerPlayer("Trent", client);
            client.player = player;
            client.userName = player.Username;

            int id = 100;
            String leaveGame = new Message("", Translation.TYPE.LeaveLobby).toJson();
            client.processesMessage(leaveGame);
            Assert.Null(client.currentLobby);

            Lobby lobby = new Lobby("game", -1, client.player, id);

            
            lobby.addPlayer(new ServerPlayer("JimBob",new FakeClient()));
            lobby.addPlayer(new ServerPlayer("BobbyTables",new FakeClient()));
            client.currentLobby = lobby;
            client.processesMessage(leaveGame);

            Assert.AreEqual(0, lobby.PlayerCount);
        }

        [Test]

        public void testProcessMessageLeaveLobbyThatOwnerLeavingRemovesGame()
        {
            FakeClient client = new FakeClient();
            ServerPlayer player = new ServerPlayer("Trent", client);
            client.player = player;
            client.userName = player.Username;

            int id = 100;
            String leaveGame = new Message("", Translation.TYPE.LeaveLobby).toJson();
            client.processesMessage(leaveGame);
            Assert.Null(client.currentLobby);

            Lobby lobby = new Lobby("game", -1, client.player, id);
            Lobby differentlobby = new Lobby("game2", -1, new ServerPlayer("CJ", new FakeClient()), 150);
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
        public void testProcessMessageJoiningFullLobby()
        {
            FakeClient client = new FakeClient();

            Lobby lobby = new Lobby("GameName", 10, new Player("PlayerOwner"), 1);
            lobby.addPlayer(new Player("Player1"));
            lobby.addPlayer(new Player("Player2"));
            lobby.addPlayer(new Player("Player3"));

            client.processesMessage(new Message("1", Translation.TYPE.JoinLobby).toJson());

            Assert.Null(client.currentLobby);
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
            

            listener.Start();
            TcpClient tcpClient = new TcpClient("127.0.0.1", 9999);

            TcpClient serverSide = await listener.AcceptTcpClientAsync();

            Client client = new Client(serverSide);

            Assert.True(client.Enabled);
            Assert.AreEqual(-1, client.userID);
            Assert.Null(client.userName);
            Assert.Null(client.currentLobby);

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo fieldTCP = typeof(Client).GetField("socket", flags);
            FieldInfo fieldReader = typeof(Client).GetField("reader", flags);
            FieldInfo fieldWriter = typeof(Client).GetField("writer", flags);

            Assert.AreEqual(serverSide, client.socket);

            Assert.NotNull(fieldReader.GetValue(client));
            Assert.NotNull(fieldWriter.GetValue(client));
        }
        
        [Test]
        public void testLeaveLobbySendsMessageToOtherClients()
        {
            FakeClient client1 = new FakeClient();
            ServerPlayer player1 = new ServerPlayer("client1", client1);
            client1.player = player1;

            FakeClient client2 = new FakeClient();
            ServerPlayer player2 = new ServerPlayer("client2", client2);
            client2.player = player2;


            Lobby lobby = new Lobby("Gamename", 10, player1, 1);
            lobby.addPlayer(player2);
            Data.INSTANCE.Lobbies.Add(lobby);

            client1.currentLobby = lobby;
            client2.currentLobby = lobby;

            client1.processesMessage(new Message("", Translation.TYPE.LeaveLobby).toJson());

            Assert.AreEqual(new Message("", Translation.TYPE.LeaveLobby).toJson(), client2.lastCall);
        }

        [Test]
        public void testStartGameSendsMessageToOtherClients()
        {
            FakeClient client1 = new FakeClient();
            ServerPlayer player1 = new ServerPlayer("client1", client1);
            client1.player = player1;

            FakeClient client2 = new FakeClient();
            ServerPlayer player2 = new ServerPlayer("client2", client2);
            client2.player = player2;

            player1.Ready = true;
            player2.Ready = true;


            Lobby lobby = new Lobby("Gamename", 10, player1, 1);
            lobby.addPlayer(player2);
            Data.INSTANCE.Lobbies.Add(lobby);

            client1.currentLobby = lobby;
            client2.currentLobby = lobby;

            client1.processesMessage(new Message("", Translation.TYPE.StartGame).toJson());
            Message newmsg = Message.fromJson(client2.lastCall);


            Assert.AreEqual(Translation.TYPE.StartGame, newmsg.type);
        }

        [Test]
        public void testCheckReadyTrue()
        {

            FakeClient client1 = new FakeClient();
            FakeClient client2 = new FakeClient();
            FakeClient client3 = new FakeClient();

            ServerPlayer player1 = new ServerPlayer("p1", client1);
            ServerPlayer player2 = new ServerPlayer("p2", client2);
            ServerPlayer player3 = new ServerPlayer("p3", client3);

            client1.player = player1;
            client2.player = player2;
            client3.player = player3;

            Lobby lobsters = new Lobby("game", 1000, player1, 100);

            client1.currentLobby = lobsters;
            client2.currentLobby = lobsters;
            client3.currentLobby = lobsters;

            player1.Ready = true;
            player2.Ready = true;
            player3.Ready = true;

            Assert.IsTrue(client1.checkReady());

        }

        [Test]
        public void testCheckReadyFalseIfPlayersArentReady()
        {

            FakeClient client1 = new FakeClient();
            FakeClient client2 = new FakeClient();
            FakeClient client3 = new FakeClient();

            ServerPlayer player1 = new ServerPlayer("p1", client1);
            ServerPlayer player2 = new ServerPlayer("p2", client2);
            ServerPlayer player3 = new ServerPlayer("p3", client3);

            client1.player = player1;
            client2.player = player2;
            client3.player = player3;

            Lobby lobsters = new Lobby("game", 1000, player1, 100);

            client1.currentLobby = lobsters;
            client2.currentLobby = lobsters;
            client3.currentLobby = lobsters;

            player1.Ready = false;
            player2.Ready = true;
            player3.Ready = true;

            Assert.IsFalse(client1.checkReady());

        }

        [Test]
        public void TestThatClientGetsGameLobbyBack()
        {
            FakeClient client = new FakeClient();
            ServerPlayer p1 = new ServerPlayer("p1",client);
            client.player = p1;

            String getGameLobbyMessage = new Message("", Translation.TYPE.GetGameLobby).toJson();

            client.processesMessage(getGameLobbyMessage);
            Assert.Null(client.lastCall);

            
            Lobby lobsters = new Lobby("",10,p1,1);
            GameLobby game = new GameLobby(lobsters);
            client.gameLobby = game;
            client.processesMessage(getGameLobbyMessage);

            Assert.AreEqual(new Message(game.toJson(), Translation.TYPE.GetGameLobby).toJson(), client.lastCall);
        }

        [Test]
        public void testChatMessageToLobby()
        {
            FakeClient client1 = new FakeClient();
            client1.userName = "client1";
            ServerPlayer player1 = new ServerPlayer(client1.userName, client1);
            client1.player = player1;
            
            
            FakeClient client2 = new FakeClient();
            client2.userName = "client2";
            ServerPlayer player2 = new ServerPlayer(client2.userName, client2);
            client2.player = player2;

            FakeClient client3 = new FakeClient();
            client3.userName = "client3";
            ServerPlayer player3 = new ServerPlayer(client3.userName, client3);
            client3.player = player3;

            FakeClient client4 = new FakeClient();
            client4.userName = "client4";
            ServerPlayer player4 = new ServerPlayer(client4.userName, client4);
            client4.player = player4;

            Lobby lobby = new Lobby("GameName", 10, client1.player, 1);
            lobby.Players.Add(player2);
            lobby.Players.Add(player3);
            lobby.Players.Add(player4);
            client1.currentLobby = lobby;
            client2.currentLobby = lobby;
            client3.currentLobby = lobby;
            client4.currentLobby = lobby;
            // Done with setup

            Chat chat1 = new Chat("I am Player1", Chat.TYPE.Normal, null);
            Message message1 = new Message(chat1.toJson(), Translation.TYPE.Chat);
            Chat chat1R = new Chat(chat1.Message, Chat.TYPE.Normal, player1.Username);
            Message message1R = new Message(chat1R.toJson(), Translation.TYPE.Chat);
            
            
            client1.processesMessage(message1.toJson());
            Assert.Null(client1.lastCall);
            Assert.AreEqual(message1R.toJson(), client2.lastCall);
            Assert.AreEqual(message1R.toJson(), client3.lastCall);
            Assert.AreEqual(message1R.toJson(), client4.lastCall);


            Chat chat2 = new Chat("I am Player2", Chat.TYPE.Normal, null);
            Message message2 = new Message(chat2.toJson(), Translation.TYPE.Chat);
            Chat chat2R = new Chat(chat2.Message, Chat.TYPE.Normal, player2.Username);
            Message message2R = new Message(chat2R.toJson(), Translation.TYPE.Chat);


            client2.processesMessage(message2.toJson());
            Assert.AreEqual(message1R.toJson(), client2.lastCall); // Should be same as last value;
            Assert.AreEqual(message2R.toJson(), client1.lastCall);
            Assert.AreEqual(message2R.toJson(), client3.lastCall);
            Assert.AreEqual(message2R.toJson(), client4.lastCall);


            FakeClient client5 = new FakeClient();
            client5.processesMessage(message2.toJson());
            Assert.Null(client5.lastCall);

        }

        [Test]
        public void testSocketClosed()
        {
            FakeClient client = new FakeClient();

            // Make sure does not Error when nothing to clear;
            client.socketClosed();

            client.currentLobby = new Lobby("Random", 10, new Player("HERE"), 66);

            client.socketClosed();
            Assert.Null(client.currentLobby);
        }

        [Test]
        public void testOpenTrade()
        {
            Dictionary<Resource.TYPE, int> offer = new Dictionary<Resource.TYPE, int>();
            Dictionary<Resource.TYPE, int> request = new Dictionary<Resource.TYPE, int>();

            Trade trade = new Trade(new GamePlayer("Sender"), new GamePlayer("Reciver"), offer, request);
            CatanersShared.Message msg = new CatanersShared.Message(trade.toJson(), Translation.TYPE.StartTrade);

            FakeClient client = new FakeClient();
            client.userName = "Sender";
            FakeClient reciver = new FakeClient();
            reciver.userName = "Reciver";

            // Ignore, Dont Crash;
            client.processesMessage(msg.toJson());
            Assert.Null(reciver.lastCall);

            offer[Resource.TYPE.Wheat] = 10;
            
            ServerPlayer ptemp = new ServerPlayer("Sender",client);
            ServerPlayer ptemp2 = new ServerPlayer("Reciver",reciver);

            Lobby temp = new Lobby("Lobby",10,ptemp,1);
            temp.addPlayer(ptemp2);
            GameLobby gLobby = new GameLobby(temp);

            client.player  = ptemp;
            client.currentLobby = temp;
            client.gameLobby = gLobby;
            client.serverLogic = new ServerLogic(temp);

            msg = new CatanersShared.Message(trade.toJson(), Translation.TYPE.StartTrade);

            client.processesMessage(msg.toJson());
            Assert.Null(reciver.lastCall);
            Assert.Null(client.serverLogic.onGoingTrade);
            // Still dont do anything as both do not have resources


            client.gameLobby.gamePlayers[0].resources[Resource.TYPE.Wheat] = 10;

            client.processesMessage(msg.toJson());
            // Good
            Assert.AreEqual(msg.toJson(),reciver.lastCall);
            Assert.AreEqual(client.serverLogic.onGoingTrade, trade);


            request[Resource.TYPE.Wheat] = 10;
            client.gameLobby.gamePlayers[1].resources[Resource.TYPE.Wheat] = 10;

            // Reset trade
            client.serverLogic.onGoingTrade = null;

            msg = new CatanersShared.Message(trade.toJson(), Translation.TYPE.StartTrade);
            client.processesMessage(msg.toJson());

            Assert.AreEqual(client.serverLogic.onGoingTrade, trade);
            Assert.AreEqual(msg.toJson(), reciver.lastCall);

        }

        [Test]
        public void testTradeResponce() 
        {
            Dictionary<Resource.TYPE, int> offer = new Dictionary<Resource.TYPE, int>();
            Dictionary<Resource.TYPE, int> request = new Dictionary<Resource.TYPE, int>();

            Trade correctTrade = new Trade(new GamePlayer("Sender"), new GamePlayer("Reciver"), offer, request);
            Trade incorrectTrade = new Trade(new GamePlayer("OddSend"), new GamePlayer("OddReciver"), offer, request);
            String acceptTrade = new Message(true.ToString(), Translation.TYPE.TradeResponce).toJson();
            String declineTrade = new Message(false.ToString(), Translation.TYPE.TradeResponce).toJson();

            FakeClient clientR = new FakeClient();
            clientR.userName = "Reciver";
            clientR.player = new ServerPlayer(clientR.userName,clientR);

            FakeClient clientS = new FakeClient();
            clientS.userName = "Sender";
            clientS.player = new ServerPlayer(clientS.userName, clientS);

            Lobby lob = new Lobby("Gamename", 10, clientR.player, 1);
            lob.addPlayer(clientS.player);

            clientR.currentLobby = lob;
            clientS.currentLobby = lob;


            ServerLogic sl = new ServerLogic(lob);
            clientR.serverLogic = sl;
            clientS.serverLogic = sl;
            clientR.gameLobby = sl.gameLobby;
            clientS.gameLobby = sl.gameLobby;

            // Should Do Nothing to state because ongoingtrade is null
            clientR.processesMessage(acceptTrade);

            sl.onGoingTrade = correctTrade;

            clientR.processesMessage(declineTrade);

            Assert.Null(sl.onGoingTrade);

            sl.onGoingTrade = correctTrade;

            clientR.processesMessage(acceptTrade);
            Assert.Null(sl.onGoingTrade);

            clientS.serverLogic.gameLobby.gamePlayers[1].resources[Resource.TYPE.Sheep] = 10;
            offer[Resource.TYPE.Sheep] = 10;

            sl.onGoingTrade = correctTrade;
            clientR.processesMessage(acceptTrade);
            Assert.Null(sl.onGoingTrade);
            Assert.AreEqual(0, clientS.serverLogic.gameLobby.gamePlayers[1].resources[Resource.TYPE.Sheep]);
            Assert.AreEqual(10, clientR.serverLogic.gameLobby.gamePlayers[0].resources[Resource.TYPE.Sheep]);

            String gamePlayerList = Newtonsoft.Json.JsonConvert.SerializeObject(clientR.serverLogic.gameLobby.gamePlayers);

            Message correctReturn = new Message(gamePlayerList,Translation.TYPE.UpdateResources);

            CollectionAssert.AreEqual(correctReturn.toJson(), clientR.lastCall);
            CollectionAssert.AreEqual(correctReturn.toJson(), clientS.lastCall);
        }

        [Test]
        public void TestTradeWithBank()
        {
            Dictionary<Resource.TYPE, int> offer = new Dictionary<Resource.TYPE, int>();
            Dictionary<Resource.TYPE, int> request = new Dictionary<Resource.TYPE, int>();

            Trade trade = new Trade(new GamePlayer("Sender"), new GamePlayer("Bank"), offer, request);
            CatanersShared.Message msg = new CatanersShared.Message(trade.toJson(), Translation.TYPE.StartTrade);

            FakeClient client = new FakeClient();
            client.userName = "Sender";
            FakeClient client2 = new FakeClient();
            client2.userName = "bystander";

            ServerPlayer ptemp = new ServerPlayer("Sender", client);
            ServerPlayer ptemp2 = new ServerPlayer("bystander", client2);

            Lobby temp = new Lobby("Lobby", 10, ptemp, 1);
            temp.addPlayer(ptemp2);
            GameLobby gLobby = new GameLobby(temp);

            client.player = ptemp;
            client.currentLobby = temp;
            client.gameLobby = gLobby;
            client.serverLogic = new ServerLogic(temp);
            client2.player = ptemp2;
            client2.currentLobby = temp;
            client2.gameLobby = gLobby;
            client2.serverLogic = new ServerLogic(temp);

            String gamePlayerList = Newtonsoft.Json.JsonConvert.SerializeObject(client.serverLogic.gameLobby.gamePlayers);
            String gamePlayerList2 = Newtonsoft.Json.JsonConvert.SerializeObject(client2.serverLogic.gameLobby.gamePlayers);
            
            // Ignore, Dont Crash;
            client.processesMessage(msg.toJson());
            Assert.AreEqual(new CatanersShared.Message(gamePlayerList,CatanersShared.Translation.TYPE.UpdateResources).toJson(),client.lastCall);
            Assert.AreEqual(new CatanersShared.Message(gamePlayerList2, CatanersShared.Translation.TYPE.UpdateResources).toJson(), client2.lastCall);

            offer[Resource.TYPE.Wheat] = 4;

            msg = new CatanersShared.Message(trade.toJson(), Translation.TYPE.StartTrade);

            client.lastCall = null;
            client.processesMessage(msg.toJson());
            Assert.Null(client.lastCall);
            // Still dont do anything as both do not have resources

            client.gameLobby.gamePlayers[0].resources[Resource.TYPE.Wheat] = 4;
            request[Resource.TYPE.Brick] = 1;
            msg = new CatanersShared.Message(trade.toJson(), Translation.TYPE.StartTrade);

            client.processesMessage(msg.toJson());
            // Good
            Assert.AreEqual(new CatanersShared.Message(gamePlayerList, CatanersShared.Translation.TYPE.UpdateResources).toJson(), client.lastCall);
            Assert.AreEqual(new CatanersShared.Message(gamePlayerList2, CatanersShared.Translation.TYPE.UpdateResources).toJson(), client2.lastCall);
            
        }

        [Test]
        public void TestEndTurnMessage()
        {
            FakeClient client = new FakeClient();
            client.userName = "p1";
            FakeClient client2 = new FakeClient();
            client2.userName = "p2";

            ServerPlayer ptemp = new ServerPlayer("p1", client);
            ServerPlayer ptemp2 = new ServerPlayer("p2", client2);

            Lobby temp = new Lobby("Lobby", 10, ptemp, 1);
            temp.addPlayer(ptemp2);
            GameLobby gLobby = new GameLobby(temp);

            client.player = ptemp;
            client.currentLobby = temp;
            client.gameLobby = gLobby;
            client.serverLogic = new ServerLogic(temp);
            client2.player = ptemp2;
            client2.currentLobby = temp;
            client2.gameLobby = gLobby;
            client2.serverLogic = new ServerLogic(temp);
            client.serverLogic.playerTurn = 0;
            client2.serverLogic.playerTurn = 0;

            String endTurnMessage = new Message("", Translation.TYPE.EndTurn).toJson();

            client.processesMessage(endTurnMessage);
            client2.processesMessage(endTurnMessage);
            Assert.AreEqual(new Message("1",Translation.TYPE.EndTurn).toJson(),client.lastCall);
            Assert.AreEqual(new Message("1", Translation.TYPE.EndTurn).toJson(), client2.lastCall);

        }

        [Test]
        public void testDiceRoll()
        {
            FakeClient client = new FakeClient();
            client.userName = "p1";
            ServerPlayer ptemp = new ServerPlayer("p1", client);

            Lobby temp = new Lobby("Lobby", 10, ptemp, 1);
            GameLobby gLobby = new GameLobby(temp);

            client.player = ptemp;
            client.currentLobby = temp;
            client.gameLobby = gLobby;
            client.serverLogic = new ServerLogic(temp);

            String diceRollMessage = new Message("", Translation.TYPE.DiceRoll).toJson();
            client.processesMessage(diceRollMessage);

            Assert.AreEqual(Translation.TYPE.DiceRoll, Message.fromJson(client.lastCall).type);
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