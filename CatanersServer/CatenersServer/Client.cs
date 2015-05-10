﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Collections.Concurrent;
using CatanersShared;
using System.Collections;
using System.IO;
using System.Threading;

namespace CatenersServer
{
    public class Client
    {
        public TcpClient socket;
        public bool Enabled;

        public int userID;
        public String userName;

        public Lobby currentLobby;

        StreamWriter writer;
        StreamReader reader;

        public ServerPlayer player;
        public ServerLogic serverLogic;
        public GameLobby gameLobby;

        public Client(TcpClient tcp)
        {
            this.socket = tcp;
            writer = new StreamWriter(socket.GetStream(), Encoding.Unicode);
            reader = new StreamReader(socket.GetStream(), Encoding.Unicode);
            Enabled = true;
            userID = -1;
            userName = null;
            currentLobby = null;
            this.player = new ServerPlayer(userName, this);
        }

        public Client()
        {
            // For Testing
        }

        public async void queueMessagesAsync()
        {
            while(Enabled && socket.Connected) {
                string line;
                Task<String> task = reader.ReadLineAsync();
                try
                {
                    line = await task;
                    if (line == null)
                        continue;

                    //Console.WriteLine("Message:" + line);

                    Thread thread = new Thread(() => processesMessage(line));
                    thread.Start();

                }
                catch (System.IO.IOException)
                {
                    // Client caused exception just disconnect.
                    break;
                }
            }
            leaveLobby();
            Console.WriteLine("Client Closed: " + ((System.Net.IPEndPoint)socket.Client.RemoteEndPoint).Address.ToString());
        }

        public virtual void sendToClient(String msg)
        {
            writer.WriteLine(msg);
            writer.Flush();
        }

        public void processesMessage(String s)
        {
            Message msg = Message.fromJson(s);
            System.Diagnostics.Debug.WriteLine(String.Format("[{0}] {1}::TYPE: {2} :: Message: {3}", DateTime.Now.ToString("T"), this.userName, msg.type, msg.message));
            switch(msg.type) {
                case Translation.TYPE.Login:
                    Login login = Login.fromJson(msg.message);
                    // TODO verification of login symbols;
                    if (login == null)
                    {
                        sendToClient(new Message("-1", Translation.TYPE.Login).toJson());
                    }
                    catanersDataSet.checkUserDataTableRow user = Database.INSTANCE.getUser(login);
                    if (user == null)
                    {
                        sendToClient(new Message("-1", Translation.TYPE.Login).toJson());
                    }
                    else
                    {
                        sendToClient(new Message(user.Username.ToString(), Translation.TYPE.Login).toJson());
                        this.userID = user.UID;
                        this.userName = user.Username;
                        this.player.Username = user.Username;
                    }
                break;

                case Translation.TYPE.Register:
                    login = Login.fromJson(msg.message);
                    // TODO verification of login symbols;
                    int id = Database.INSTANCE.registerUser(login);
                    if(id < 0 )
                        sendToClient(new Message("-1", Translation.TYPE.Register).toJson());
                    else
                        sendToClient(new Message(id.ToString(), Translation.TYPE.Register).toJson());
                break;
                
                case Translation.TYPE.RequestLobbies:
                    Message toSend = new Message(Newtonsoft.Json.JsonConvert.SerializeObject(Data.INSTANCE.Lobbies), Translation.TYPE.RequestLobbies);
                    sendToClient(toSend.toJson());
                break;

                case Translation.TYPE.BuySettlement:
                    int parsedInt = int.Parse(msg.message);
                    bool test = serverLogic.determineSettlementAvailability(this.userName, parsedInt);
                    if (test == true)
                    {
                        Message settlementPurchased = new Message(parsedInt.ToString(), Translation.TYPE.BuySettlement);
                        foreach (ServerPlayer p in this.currentLobby.Players)
                        {
                            p.client.sendToClient(settlementPurchased.toJson());
                        }
                    }
                break;

                case  Translation.TYPE.CreateLobby:
                    Lobby lobby = Lobby.fromJson(msg.message);
                    Player owner = new Player(this.userName);

                    Lobby newLobby = new Lobby(lobby.GameName, lobby.MaxTimePerTurn, this.player, Data.INSTANCE.nextLobbyID++);
                    // TODO verify Lobby;
                    Data.INSTANCE.Lobbies.Add(newLobby);

                    this.currentLobby = newLobby;
                break;

                case Translation.TYPE.ChangeReadyStatus:
                    if (this.currentLobby == null || this.userName == null)
                    {
                        // Trying to change ready status while not in a group just going to ingore incase of race conditions
                        break;
                    }
                    for (int i = 0; i < this.currentLobby.Players.Count; i++)
                    {
                        if (this.currentLobby.Players[i].Username.Equals(this.userName))
                        {
                            this.currentLobby.Players[i].Ready = Boolean.Parse(msg.message);
                            break;
                        }
                    }
                break;
                case Translation.TYPE.JoinLobby:
                    int lobbyID = int.Parse(msg.message);
                    for (int i = 0; i < Data.INSTANCE.Lobbies.Count; i++)
                    {
                        if (Data.INSTANCE.Lobbies[i].lobbyID == lobbyID)
                        {
                            if (Data.INSTANCE.Lobbies[i].PlayerCount < 4)
                            {
                                this.currentLobby = Data.INSTANCE.Lobbies[i];
                                this.currentLobby.addPlayer(this.player);
                                break;
                            }
                        }
                    }
                break;

                case Translation.TYPE.LeaveLobby:
                    leaveLobby();
                break;

                case Translation.TYPE.UpdateLobby:
                    if(this.currentLobby != null)
                        sendToClient(new Message(this.currentLobby.toJson(),Translation.TYPE.UpdateLobby).toJson());
                break;

                case Translation.TYPE.RegenerateBoard:
                if (this.serverLogic != null)
                {
                    String getLobby = new CatanersShared.Message(serverLogic.gameLobby.toJson(), Translation.TYPE.GetGameLobby).toJson();
                    this.serverLogic.generatehexArray();
                    int[][] array = this.serverLogic.gethexArray();
                    String boardString = new Message(Translation.intArraytwotoJson(array), Translation.TYPE.HexMessage).toJson();
                    foreach (ServerPlayer p in this.currentLobby.Players)
                    {
                        p.client.sendToClient(boardString);
                        p.client.sendToClient(getLobby);
                    }
                }
                break;

                case Translation.TYPE.GetGameLobby:
                    if(this.gameLobby != null)
                    sendToClient(new Message(this.gameLobby.toJson(), Translation.TYPE.GetGameLobby).toJson());
                break;

                case Translation.TYPE.StartGame:
                if(currentLobby != null) {
                    if (checkReady())
                    {
                        ServerLogic newLogic = new ServerLogic(this.currentLobby);
                        this.serverLogic = newLogic;
                        this.serverLogic.generatehexArray();
                        gameLobby = newLogic.gameLobby;
                        string toPass = newLogic.sendGeneration();
                        String getLobby = new CatanersShared.Message(serverLogic.gameLobby.toJson(), Translation.TYPE.GetGameLobby).toJson();
                        
                        for (int i = 0; i < currentLobby.PlayerCount; i++)
                        {
                            ((ServerPlayer)currentLobby.Players[i]).client.sendToClient(getLobby);
                            ((ServerPlayer)currentLobby.Players[i]).client.sendToClient(new Message(toPass, Translation.TYPE.StartGame).toJson());
                            //((ServerPlayer)currentLobby.Players[i]).client.sendToClient(boardString);
                            ((ServerPlayer)currentLobby.Players[i]).client.serverLogic = newLogic;
                            ((ServerPlayer)currentLobby.Players[i]).client.gameLobby = gameLobby;
                        }
                    }
                }
                break;
                case Translation.TYPE.OpenTradeWindow:
                    // Not in a game
                if (gameLobby == null)
                {

                    return;
                }
                    if (serverLogic == null || serverLogic.onGoingTrade != null)
                        return;
                    Trade trade = Trade.fromJson(msg.message);

                    // Trying to trade with self.
                    if (trade.source.Username.Equals(trade.target.Username))
                        return;

                    GamePlayer sender = null;
                    GamePlayer target = null;
                    foreach (GamePlayer p in gameLobby.gamePlayers)
                    {
                        if (p.Username.Equals(this.userName))
                        {
                            sender = p;
                        }
                        if (p.Username.Equals(trade.target.Username))
                        {
                            target = p;
                        }
                    }
                    if (sender == null || target == null)
                        return;

                    Dictionary<Resource.TYPE, int> offer = trade.offeredResources;
                    Dictionary<Resource.TYPE, int> request = trade.wantedResources;

                    foreach (Resource.TYPE t in Enum.GetValues(typeof(Resource.TYPE)))
                    {
                        // if sender does not have enough
                        if (offer.ContainsKey(t) && offer[t] > sender.resources[t] && offer[t] >= 0)
                            return;
                        // if target does not have enough
                        if (request.ContainsKey(t) && request[t] > target.resources[t] && request[t] >= 0)
                            return;
                    }

                    foreach (ServerPlayer p in currentLobby.Players)
                    {
                        if (p.Username.Equals(target.Username))
                        {
                            serverLogic.onGoingTrade = trade;
                            p.client.sendToClient(s);
                            return;
                        }
                    }

                break;
                case Translation.TYPE.Chat:
                    if (currentLobby != null)
                    {
                        Chat revchat = Chat.fromJson(msg.message);
                        Chat sendChat = new Chat(revchat.Message, Chat.TYPE.Normal, this.userName);
                        Message toSendMsg = new Message(sendChat.toJson(), Translation.TYPE.Chat);
                        foreach (ServerPlayer player in currentLobby.Players)
                        {
                            if (!player.Username.Equals(this.userName))
                            {
                                player.client.sendToClient(toSendMsg.toJson());
                            }
                        }
                    }

                break;
                case Translation.TYPE.TradeResponce:
                    if (serverLogic != null && serverLogic.onGoingTrade != null)
                    {
                        if (serverLogic.onGoingTrade.target.Username.Equals(this.userName))
                        {
                            bool responce = Boolean.Parse(msg.message);
                            if (responce)
                            {
                                sender = null;
                                target = null;
                                foreach (GamePlayer p in gameLobby.gamePlayers)
                                {
                                    if (p.Username.Equals(serverLogic.onGoingTrade.source.Username))
                                    {
                                        sender = p;
                                    }
                                    if (p.Username.Equals(this.userName))
                                    {
                                        target = p;
                                    }
                                }

                                if (sender == null || target == null)
                                    return;
                                foreach (Resource.TYPE t in Enum.GetValues(typeof(Resource.TYPE)))
                                {
                                    if (serverLogic.onGoingTrade.offeredResources.ContainsKey(t))
                                    {
                                        sender.resources[t] -= serverLogic.onGoingTrade.offeredResources[t];
                                        target.resources[t] += serverLogic.onGoingTrade.offeredResources[t];
                                    }
                                    if (serverLogic.onGoingTrade.wantedResources.ContainsKey(t))
                                    {
                                        target.resources[t] -= serverLogic.onGoingTrade.wantedResources[t];
                                        sender.resources[t] += serverLogic.onGoingTrade.wantedResources[t];
                                    }
                                }
                                // TODO: Update resources message;
                            }
                            serverLogic.onGoingTrade = null;
                        }
                    }
                break;
                default:
                    // We Are just going to ignore it.
                break;
            }
        }

        public bool checkReady()
        {
            int readyCount=0;
            for (int i = 0; i < currentLobby.PlayerCount; i++)
            {
                if (currentLobby.Players[i].Ready)
                {
                    readyCount++;
                }
            }

            if (readyCount == currentLobby.PlayerCount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void leaveLobby()
        {
            if (this.currentLobby != null)
            {
                //if person that leaves is owner
                if (this.currentLobby.Owner.Equals(this.player))
                {
                    currentLobby.removePlayer(this.player);
                    for (int i = 0; i < currentLobby.PlayerCount; i++)
                    {
                        ((ServerPlayer)currentLobby.Players[i]).client.sendToClient(new Message("", Translation.TYPE.LeaveLobby).toJson());
                    }
                    currentLobby.removeAll();
                    Data.INSTANCE.Lobbies.Remove(currentLobby);
                }
                else
                {
                    currentLobby.removePlayer(this.player);
                }
            }


            this.currentLobby = null;
        }


        public void socketClosed()
        {
            leaveLobby();
        }
    }
}
