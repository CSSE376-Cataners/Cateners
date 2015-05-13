using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Cataners;
using CatanersShared;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

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

        [Test]
        public void testProcessMessageRequestLobbies()
        {
            FakeClient client = new FakeClient();

            List<Lobby> list = new List<Lobby>();

            list.Add(new Lobby("Gamename1", 10, new Player("One"), 1));
            list.Add(new Lobby("Gamename2", 10, new Player("One"), 2));
            list.Add(new Lobby("Gamename3", 10, new Player("One"), 3));
            list.Add(new Lobby("Gamename4", 10, new Player("One"), 4));

            client.processesMessage(new Message(Newtonsoft.Json.JsonConvert.SerializeObject(list), Translation.TYPE.RequestLobbies).toJson());

            CollectionAssert.AreEqual(list, Data.Lobbies);
        }


        [Test]
        public void testProcessMessageLeaveLobby()
        {
            FakeClient client = new FakeClient();
            client.processesMessage(new Message("", Translation.TYPE.LeaveLobby).toJson());
        }

        [Test]
        public void testProcessMessageStartGame()
        {
            FakeClient client = new FakeClient();
            // Starts up wave lobby
            //client.processesMessage(new Message("", Translation.TYPE.StartGame).toJson());
        }

        [Test]
        public void testProcessMessageGetGameLobby()
        {
            GameLobby lobby = new GameLobby(new Lobby("Gamename", 10, new Player("Owner"), 1));

            FakeClient client = new FakeClient();

            Message s = new Message(lobby.toJson(), Translation.TYPE.GetGameLobby);

            client.processesMessage(s.toJson());

            Assert.AreEqual(lobby,Data.currentLobby);
        }
       
        [Test]
        public void TestThatPlayerResourceGetsAdded()
        {
            Player player = new Player("Owner");
            Lobby lobby = new Lobby("Game", 100, player, 10);
            GamePlayer gamePlayer = new GamePlayer("Owner");
            GameLobby gameLobby = new GameLobby(lobby);
            FakeClient client = new FakeClient();

            Message s = new Message(gameLobby.toJson(), Translation.TYPE.GetGameLobby);
            client.processesMessage(s.toJson());

            Assert.AreEqual(gameLobby, Data.currentLobby);
            
            AddResource addWheat = new AddResource(gamePlayer,Resource.TYPE.Wheat,1);
            Message msg = new Message(addWheat.toJson(), Translation.TYPE.addResource);
            client.processesMessage(msg.toJson());


            Assert.AreEqual(1, ((GameLobby)Data.currentLobby).gamePlayers[0].resources[Resource.TYPE.Wheat]);



        }

        [Test]
        public void TestThatMultipleResourcesGetAdded()
        {
            Player player = new Player("Owner");
            Lobby lobby = new Lobby("Game", 100, player, 10);
            GamePlayer gamePlayer = new GamePlayer("Owner");
            GameLobby gameLobby = new GameLobby(lobby);
            FakeClient client = new FakeClient();

            Message s = new Message(gameLobby.toJson(), Translation.TYPE.GetGameLobby);
            client.processesMessage(s.toJson());

            Assert.AreEqual(gameLobby, Data.currentLobby);

            AddResource addWheat = new AddResource(gamePlayer, Resource.TYPE.Wheat, 1);
            Message msg = new Message(addWheat.toJson(), Translation.TYPE.addResource);
            AddResource addSheep = new AddResource(gamePlayer, Resource.TYPE.Sheep, 10000);
            Message newmsg = new Message(addSheep.toJson(), Translation.TYPE.addResource);
            client.processesMessage(msg.toJson());
            client.processesMessage(newmsg.toJson());

            Assert.AreEqual(1, ((GameLobby)Data.currentLobby).gamePlayers[0].resources[Resource.TYPE.Wheat]);
            Assert.AreEqual(10000, ((GameLobby)Data.currentLobby).gamePlayers[0].resources[Resource.TYPE.Sheep]);

            client.processesMessage(msg.toJson()); 
            Assert.AreEqual(2, ((GameLobby)Data.currentLobby).gamePlayers[0].resources[Resource.TYPE.Wheat]);

        }


        [Test]
        public void testChatMessageRecived()
        {
            FakeClient client = new FakeClient();

            Chat chat1 = new Chat("I am Player1", Chat.TYPE.Normal, "Player1");
            CatanersShared.Message message1 = new CatanersShared.Message(chat1.toJson(), Translation.TYPE.Chat);
            client.processesMessage(message1.toJson());

            chat1 = new Chat("I am Player1!!!! Respond", Chat.TYPE.Normal, "Player1");
            message1 = new CatanersShared.Message(chat1.toJson(), Translation.TYPE.Chat);
            ChatBox box = new ChatBox();
            box.Show();
            client.processesMessage(message1.toJson());

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo info = typeof(ChatBox).GetField("richTextBox", flags);

            System.Windows.Forms.RichTextBox rtb = (System.Windows.Forms.RichTextBox)info.GetValue(box);
            int length = rtb.Text.Length;
            client.processesMessage(message1.toJson());
            Assert.True(rtb.Text.Length > length);
            box.Close();

        }

        // Commented Out as messes with automatic testing.
        //[Test]
        public void testTradeResponce()
        {
            Dictionary<Resource.TYPE,int> offer = new Dictionary<Resource.TYPE,int>();
            Dictionary<Resource.TYPE,int> request = new Dictionary<Resource.TYPE,int>();

            Trade trade = new Trade(new GamePlayer("Sender"), new GamePlayer("Reciver"), offer, request);
            CatanersShared.Message msg = new CatanersShared.Message("trade.toJson()", Translation.TYPE.StartTrade);

            FakeClient client = new FakeClient();

            client.processesMessage(msg.toJson());


        }

        [Test]
        public void testGetGameLobby2()
        {
            FakeClient client = new FakeClient();

            Player p1 = new Player("Owner");
            Player p2 = new Player("p1");
            Player p3 = new Player("p2");
            Player p4 = new Player("p3");

            Lobby lob = new Lobby("GameName", 10, p1,1);
            lob.addPlayer(p2);
            lob.addPlayer(p3);
            lob.addPlayer(p4);
            GameLobby gLob = new GameLobby(lob);

            Message setup = new Message(gLob.toJson(), Translation.TYPE.GetGameLobby);
            client.processesMessage(setup.toJson());

            Assert.AreEqual(gLob, Data.currentGameLobby);
            foreach (GamePlayer p in gLob.gamePlayers)
            {
                p.resources[Resource.TYPE.Wheat] = 10;
            }

            Message after = new Message(gLob.toJson(), Translation.TYPE.GetGameLobby);
            client.processesMessage(after.toJson());

            Assert.AreEqual(gLob, Data.currentGameLobby);
        }

        [Test]
        public void testUpdateResources()
        {
            FakeClient client = new FakeClient();

            Player p1 = new Player("Owner");
            Player p2 = new Player("p1");
            Player p3 = new Player("p2");
            Player p4 = new Player("p3");

            Lobby lob = new Lobby("GameName", 10, p1, 1);
            lob.addPlayer(p2);
            lob.addPlayer(p3);
            lob.addPlayer(p4);
            GameLobby gLob = new GameLobby(lob);

            Message setup = new Message(gLob.toJson(), Translation.TYPE.GetGameLobby);
            client.processesMessage(setup.toJson());

            Assert.AreEqual(gLob, Data.currentGameLobby);
            foreach (GamePlayer p in gLob.gamePlayers)
            {
                p.resources[Resource.TYPE.Wheat] = 10;
            }

            Message after = new Message(Newtonsoft.Json.JsonConvert.SerializeObject(gLob.gamePlayers), Translation.TYPE.UpdateResources);
            client.processesMessage(after.toJson());

            Assert.AreEqual(gLob.gamePlayers[0].resources[Resource.TYPE.Wheat], Data.currentGameLobby.gamePlayers[0].resources[Resource.TYPE.Wheat]);
        }
        [Test]
        public void testEndTurnNotEnoughPlayers()
        {
            FakeClient client = new FakeClient();
            Player p1 = new Player("Owner");
            Player p2 = new Player("p1");

            Lobby lob = new Lobby("GameName", 10, p1, 1);
            lob.addPlayer(p2);
            GameLobby gLob = new GameLobby(lob);
            Message setup = new Message(gLob.toJson(), Translation.TYPE.GetGameLobby);
            client.processesMessage(setup.toJson());

            Assert.AreEqual(gLob, Data.currentGameLobby);

            Data.isMyTurn = true;
            Data.username = "p1";
            Message endTurn= new Message("2", Translation.TYPE.EndTurn);
            client.processesMessage(endTurn.toJson());
            Assert.AreEqual(true, Data.isMyTurn);
            Data.isMyTurn = false;
            Data.username = "Owner";
            client.processesMessage(endTurn.toJson());
            Assert.AreEqual(false,Data.isMyTurn);

        }

        [Test]
        public void testEndTurnNextPlayerGetsTurn()
        {
            FakeClient client = new FakeClient();
            Player p1 = new Player("Owner");
            Player p2 = new Player("p1");

            Lobby lob = new Lobby("GameName", 10, p1, 1);
            lob.addPlayer(p2);
            GameLobby gLob = new GameLobby(lob);
            Message setup = new Message(gLob.toJson(), Translation.TYPE.GetGameLobby);
            client.processesMessage(setup.toJson());

            Assert.AreEqual(gLob, Data.currentGameLobby);

            Data.currentGameLobby.gamePlayers[0].isMyTurn = true;
            Data.username = "p1";
            Message endTurn = new Message("1", Translation.TYPE.EndTurn);
            client.processesMessage(endTurn.toJson());
            Assert.AreEqual(true, Data.isMyTurn);
        }


        [Test]
        public void testEndTurn3rdPlayerGetsTurn()
        {
            FakeClient client = new FakeClient();
            Player p1 = new Player("Owner");
            Player p2 = new Player("p1");
            Player p3 = new Player("p2");

            Lobby lob = new Lobby("GameName", 10, p1, 1);
            lob.addPlayer(p2);
            lob.addPlayer(p3);
            GameLobby gLob = new GameLobby(lob);
            Message setup = new Message(gLob.toJson(), Translation.TYPE.GetGameLobby);
            client.processesMessage(setup.toJson());

            Assert.AreEqual(gLob, Data.currentGameLobby);
            Data.username = "p2";
            Message endTurn = new Message("2", Translation.TYPE.EndTurn);
            client.processesMessage(endTurn.toJson());
            Assert.AreEqual(true, Data.isMyTurn);
        }

        [Test]
        public void testEndTurnMakesOtherPlayerTurnFalse()
        {
            FakeClient client = new FakeClient();
            FakeClient client2 = new FakeClient();
            Player p1 = new Player("Owner");
            Player p2 = new Player("p1");
            Lobby lob = new Lobby("GameName", 10, p1, 1);
            lob.addPlayer(p2);
            GameLobby gLob = new GameLobby(lob);
            Message setup = new Message(gLob.toJson(), Translation.TYPE.GetGameLobby);
            client.processesMessage(setup.toJson());
            Assert.AreEqual(gLob, Data.currentGameLobby);
            //process message for p1
            Data.username = "p1";
            Data.isMyTurn = false;
            Message endTurn = new Message("1", Translation.TYPE.EndTurn);
            client.processesMessage(endTurn.toJson());
            Assert.AreEqual(true, Data.isMyTurn);
            //process message for owner
            Data.username = "owner";
            Data.isMyTurn = true;
            client.processesMessage(endTurn.toJson());
            Assert.AreEqual(false, Data.isMyTurn);
        }


        [Test]
        public void testThatOwnerHasFirstTurn()
        {
            FakeClient client = new FakeClient();
            GameLobby lobby = new GameLobby(new Lobby("Gamename", 10, new Player("Owner"), 1));
            lobby.addPlayer(new Player("p2"));
            Data.username = "Owner";

            Message s = new Message(lobby.toJson(), Translation.TYPE.GetGameLobby);
            client.processesMessage(s.toJson());
            Assert.AreEqual(true, Data.isMyTurn);
            Data.username = "p2";
            client.processesMessage(s.toJson());
            Assert.AreEqual(false, Data.isMyTurn);
        }

        [ExcludeFromCodeCoverage]
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
