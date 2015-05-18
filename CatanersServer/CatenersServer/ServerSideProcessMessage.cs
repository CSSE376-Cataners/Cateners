using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;

namespace CatenersServer
{
    public partial class Client
    {
        public void PM_TradeResponce(Message msg)
        {
            if (serverLogic != null && serverLogic.onGoingTrade != null)
            {
                if (serverLogic.onGoingTrade.target.Username.Equals(this.userName))
                {
                    bool responce = Boolean.Parse(msg.message);
                    if (responce)
                    {
                        GamePlayer sender = null;
                        GamePlayer target = null;
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

                        String gamePlayerList = Newtonsoft.Json.JsonConvert.SerializeObject(this.serverLogic.gameLobby.gamePlayers);
                        String toReturn = new Message(gamePlayerList, Translation.TYPE.UpdateResources).toJson();
                        this.sendToLobby(toReturn);
                    }
                    serverLogic.onGoingTrade = null;
                }
            }
        }

        public void PM_Chat(Message msg)
        {
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
        }

        public void PM_NewLongestRoad(Message msg)
        {
            
        }

        public void PM_StartTrade(Message msg)
        {
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
            if (sender == null)
                return;

            Dictionary<Resource.TYPE, int> offer = trade.offeredResources;
            Dictionary<Resource.TYPE, int> request = trade.wantedResources;

            if (trade.target.Username.Equals("Bank"))
            {


                int sumOffer = 0;
                int sumRequest = 0;
                foreach (Resource.TYPE t in Enum.GetValues(typeof(Resource.TYPE)))
                {
                    if ((offer.ContainsKey(t) && offer[t] >= 0))
                    {
                        if (offer[t] > sender.resources[t])
                        {
                            return;
                        }
                        if (offer[t] % 4 != 0)
                        {
                            // TODO write popup message
                            //sendToClient(new Message())
                            return;
                        }
                        else
                        {
                            sumOffer += offer[t];
                        }
                    }
                    if (request.ContainsKey(t))
                        sumRequest += request[t];
                }
                if (sumOffer / 4 != sumRequest)
                {
                    // TODO write popup message
                    //sendToClient(new Message())
                    return;
                }
                // Good trade resources;

                foreach (Resource.TYPE t in Enum.GetValues(typeof(Resource.TYPE)))
                {
                    if (offer.ContainsKey(t))
                    {
                        sender.resources[t] -= offer[t];
                    }
                    if (request.ContainsKey(t))
                    {
                        sender.resources[t] += request[t];
                    }
                }

                String gamePlayerList = Newtonsoft.Json.JsonConvert.SerializeObject(this.serverLogic.gameLobby.gamePlayers);
                String toReturn = new Message(gamePlayerList, Translation.TYPE.UpdateResources).toJson();
                sendToLobby(toReturn);
                
            }
            else
            {
                if (target == null)
                {
                    return;
                }

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
                        p.client.sendToClient(msg.toJson());
                        return;
                    }
                }
            }

        }

        public void PM_StartGame(Message msg)
        {
            if (currentLobby != null)
            {
                if (checkReady())
                {
                    ServerLogic newLogic = new ServerLogic(this.currentLobby);
                    this.serverLogic = newLogic;
                    this.serverLogic.generatehexArray();
                    gameLobby = newLogic.gameLobby;
                    string toPass = newLogic.sendGeneration();
                    String getLobby = new CatanersShared.Message(serverLogic.gameLobby.toJson(), Translation.TYPE.GetGameLobby).toJson();
                    Data.INSTANCE.Lobbies.Remove(currentLobby);
                    for (int i = 0; i < currentLobby.PlayerCount; i++)
                    {
                        //((ServerPlayer)currentLobby.Players[i]).client.sendToClient(getLobby);
                        ((ServerPlayer)currentLobby.Players[i]).client.sendToClient(new Message(toPass, Translation.TYPE.StartGame).toJson());
                        //((ServerPlayer)currentLobby.Players[i]).client.sendToClient(new Message(toPass, Translation.TYPE.HexMessage).toJson());
                        //((ServerPlayer)currentLobby.Players[i]).client.sendToClient(boardString);
                        ((ServerPlayer)currentLobby.Players[i]).client.serverLogic = newLogic;
                        ((ServerPlayer)currentLobby.Players[i]).client.gameLobby = gameLobby;
                    }
                }
            }
        }

        public void PM_GetGameLobby(Message msg)
        {
            if (this.gameLobby != null)
            {
                sendToClient(new Message(this.gameLobby.toJson(), Translation.TYPE.GetGameLobby).toJson());
            }
        }

        public void PM_RegenerateBoard(Message msg)
        {
            if (this.serverLogic != null && this.serverLogic.canRegen)
            {
                String getLobby = new CatanersShared.Message(serverLogic.gameLobby.toJson(), Translation.TYPE.GetGameLobby).toJson();
                this.serverLogic.generatehexArray();
                int[][] array = this.serverLogic.gethexArray();
                String boardString = new Message(Translation.intArraytwotoJson(array), Translation.TYPE.HexMessage).toJson();
                sendToLobby(boardString);
                sendToLobby(getLobby);
            }
        }

        public void PM_UpdateLobby(Message msg)
        {
            if (this.currentLobby != null)
            {
                sendToClient(new Message(this.currentLobby.toJson(), Translation.TYPE.UpdateLobby).toJson());
            }
        }

        public void PM_LeaveLobby(Message msg)
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

        public void PM_JoinLobby(Message msg)
        {
            int lobbyID = int.Parse(msg.message);
            for (int i = 0; i < Data.INSTANCE.Lobbies.Count; i++)
            {
                if (Data.INSTANCE.Lobbies[i].lobbyID == lobbyID)
                {
                    if (Data.INSTANCE.Lobbies[i].PlayerCount < 4)
                    {
                        this.currentLobby = Data.INSTANCE.Lobbies[i];
                        this.player.setPlayerNumber(i);
                        this.currentLobby.addPlayer(this.player);
                        break;
                    }
                }
            }
        }

        public void PM_ChangeReadyStatus(Message msg)
        {
            if (this.currentLobby == null || this.userName == null)
            {
                // Trying to change ready status while not in a group just going to ingore incase of race conditions
                return;
            }
            for (int i = 0; i < this.currentLobby.Players.Count; i++)
            {
                if (this.currentLobby.Players[i].Username.Equals(this.userName))
                {
                    this.currentLobby.Players[i].Ready = Boolean.Parse(msg.message);
                    break;
                }
            }
        }

        public void PM_CreateLobby(Message msg)
        {
            Lobby lobby = Lobby.fromJson(msg.message);
            Player owner = new Player(this.userName);

            Lobby newLobby = new Lobby(lobby.GameName, lobby.MaxTimePerTurn, this.player, Data.INSTANCE.nextLobbyID++);
            // TODO verify Lobby;
            Data.INSTANCE.Lobbies.Add(newLobby);

            this.currentLobby = newLobby;
        }

        public void PM_BuySettlement(Message msg)
        {
            string[] converted = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(msg.message);
            bool test = serverLogic.determineSettlementAvailability(this.userName, int.Parse(converted[0]));
            if (test == true)
            {
                Message settlementPurchased = new Message(Newtonsoft.Json.JsonConvert.SerializeObject(converted), Translation.TYPE.BuySettlement);
                String gamePlayerList = Newtonsoft.Json.JsonConvert.SerializeObject(this.serverLogic.gameLobby.gamePlayers);
                String toReturn = new Message(gamePlayerList, Translation.TYPE.UpdateResources).toJson();
                sendToLobby(settlementPurchased.toJson());
                sendToLobby(toReturn);
            }
        }

        public void PM_BuyRoad(Message msg)
        {
            string[] converted = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(msg.message);
            bool test = serverLogic.determineRoadAvailability(this.userName, int.Parse(converted[0]));
            if (test == true)
            {
                Message roadPurchased = new Message(Newtonsoft.Json.JsonConvert.SerializeObject(converted), Translation.TYPE.BuyRoad);
                String gamePlayerList = Newtonsoft.Json.JsonConvert.SerializeObject(this.serverLogic.gameLobby.gamePlayers);
                String toReturn = new Message(gamePlayerList, Translation.TYPE.UpdateResources).toJson();
                sendToLobby(roadPurchased.toJson());
                sendToLobby(toReturn);
            }
        }

        public void PM_RequestLobbies(Message msg)
        {
            Message toSend = new Message(Newtonsoft.Json.JsonConvert.SerializeObject(Data.INSTANCE.Lobbies), Translation.TYPE.RequestLobbies);
            sendToClient(toSend.toJson());
        }

        public void PM_Register(Message msg)
        {
            Login login = Login.fromJson(msg.message);
            // TODO verification of login symbols;
            int id = Database.INSTANCE.registerUser(login);
            if (id < 0)
                sendToClient(new Message("-1", Translation.TYPE.Register).toJson());
            else
                sendToClient(new Message(id.ToString(), Translation.TYPE.Register).toJson());
        }

        public void PM_Login(Message msg)
        {
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
        }

        public void PM_EndTurn(Message msg)
        {
            serverLogic.updateTurn();

            EndTurn.Phase phase = EndTurn.Phase.GamePhase;

            if(serverLogic.isStartPhase1 || serverLogic.isStartPhase2) {
                phase = EndTurn.Phase.StartPhase;
            }

            EndTurn et = new EndTurn(serverLogic.playerTurn, phase);
            String tosend = new Message(et.toJson(), Translation.TYPE.EndTurn).toJson();
            sendToLobby(tosend);
        }

        public void PM_DiceRoll(Message msg){
            serverLogic.generateRandomDiceRoll();
            serverLogic.diceRolled();
            Message toSend = new Message(new PopUpMessage("Die Rolled", this.userName + " rolled a " + serverLogic.dice.ToString(), PopUpMessage.TYPE.Notification).toJson(), Translation.TYPE.PopUpMessage);
            sendToLobby(toSend.toJson());
            
            //sendToClient(new Message(serverLogic.dice.ToString(), Translation.TYPE.DiceRoll).toJson());

            String gamePlayerList = Newtonsoft.Json.JsonConvert.SerializeObject(this.serverLogic.gameLobby.gamePlayers);
            String toReturn = new Message(gamePlayerList, Translation.TYPE.UpdateResources).toJson();
            sendToLobby(toReturn);
        }

        public void PM_DevelopmentCard(Message msg)
        {
            if (serverLogic == null) return;

            Translation.DevelopmentType type = (Translation.DevelopmentType)Enum.Parse(typeof(Translation.DevelopmentType), msg.message, true);

            if (type == Translation.DevelopmentType.Buy)
            {
                this.serverLogic.tryBuyDevelopmentCard(this.player);
            } 
            else
            {
                this.serverLogic.tryUseDevelopmentCard(type, this.player);
            }
        }

        public void sendToLobby(String s)
        {
            foreach (ServerPlayer p in this.currentLobby.Players)
            {
                p.client.sendToClient(s);
            }
        }
    }
}
