using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;
using System.Windows;

namespace Cataners
{
    public partial class CommunicationClient
    {
        public void PM_Login(Message msg) {
            if (!msg.message.Equals("-1"))
            {
                Data.username = msg.message;
            }
            queues[Translation.TYPE.Login].Add(msg.message);
        }

        public void PM_Register(Message msg)
        {
            queues[Translation.TYPE.Register].Add(msg.message);
        }

        public void PM_HexMessage(Message msg)
        {
            int[][] array = Translation.jsonToIntArrayTwo(msg.message);
            LocalConversion.Instance.generateHexList(array);
            LocalConversion.Instance.drawHexes();
        }

        public void PM_RequestLobbies(Message msg)
        {
            Data.Lobbies.Clear();
            Data.Lobbies.AddRange(Lobby.jsonToLobbyList(msg.message));
            if (JoinGameForm.INSTANCE != null)
            {
                JoinGameForm.INSTANCE.invokedRefresh();
            }
        }

        public void PM_UpdateLobby(Message msg)
        {
            if (!Data.gameStarted && LobbyForm.INSTANCE != null)
            {
                Data.currentLobby = Lobby.fromJson(msg.message);
                LobbyForm.INSTANCE.invokedRefresh();
            }
        }

        public void PM_LeaveLobby(Message msg)
        {
            if (LobbyForm.INSTANCE != null && LobbyForm.INSTANCE.Visible)
            {
                bool notStart = false;
                LobbyForm.INSTANCE.InvokedClose(notStart);
            }
        }

        public void PM_BuySettlement(Message msg)
        {
            LocalConversion.Instance.setAsPurchasedSettle(Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(msg.message));
        }

        public void PM_StartGame(Message msg)
        {
            Data.gameStarted = true;
            LobbyForm.INSTANCE.startButtonClose = true;
            LobbyForm.INSTANCE.InvokedClose(true);
            MainGui.INSTANCE.invokedHide();
            TradeForm trade = new TradeForm();
            LocalConversion.Instance.generateHexList(Translation.jsonToIntArrayTwo(msg.message));
            LocalConversion.Instance.drawHexes();
            sendToServer(new CatanersShared.Message("", Translation.TYPE.GetGameLobby).toJson());
            Program.Main();
        }

        public void PM_addResource(Message msg)
        {
            AddResource addResource = AddResource.fromJson(msg.message);
            GameLobby currentLobby = (GameLobby)Data.currentLobby;
            if (currentLobby == null)
            {
                return;
            }
            else
            {
                for (int i = 0; i < Data.currentLobby.Players.Count; i++)
                {
                    if (currentLobby.gamePlayers[i].Equals(addResource.player))
                    {
                        currentLobby.gamePlayers[i].resources[addResource.resourceType] += addResource.number;
                        //currentLobby.gamePlayers[i].resourceCount += addResource.number;
                    }
                }
            }
        }

        public void PM_GetGameLobby(Message msg)
        {
            Data.gameStarted = true;
            lock (Data.currentLobby)
            {
                Data.currentLobby = GameLobby.fromJson(msg.message);
                for (int i = 0; i < ((GameLobby)Data.currentLobby).gamePlayers.Count; i++)
                {
                    if (((GameLobby)Data.currentLobby).gamePlayers[i].Username.Equals(Data.currentLobby.Owner.Username))
                    {
                        Data.currentGameOwner = ((GameLobby)Data.currentLobby).gamePlayers[i];
                    }

                    if (((GameLobby)Data.currentLobby).gamePlayers[i].Username.Equals(Data.username))
                    {
                        Data.currentGamePlayer = ((GameLobby)Data.currentLobby).gamePlayers[i];
                    }

                }
                Data.currentGameLobby = (GameLobby)Data.currentLobby;

                //make first player have turn
                if (Data.currentGameLobby.gamePlayers[0].Username.Equals(Data.username))
                {
                    Data.isMyTurn = true;
                }
                else
                {
                    Data.isMyTurn = false;
                }

            }
        }

        public void PM_StartTrade(Message msg)
        {
            Trade tradeobj = Trade.fromJson(msg.message);
            StringBuilder sb = new StringBuilder();
            sb.Append("Player " + tradeobj.source.Username + " would like to trade you: ");
            foreach (var item in tradeobj.offeredResources)
            {
                sb.Append(item.Value + " " + item.Key + " ");
            }
            sb.Append(Environment.NewLine);
            sb.Append("in exchange for: ");
            foreach (var item in tradeobj.wantedResources)
            {
                sb.Append(item.Value + " " + item.Key + " ");
            }
            MessageBoxResult result = System.Windows.MessageBox.Show(sb.ToString(), "Trade Request", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                sendToServer(new CatanersShared.Message(true.ToString(), Translation.TYPE.TradeResponce).toJson());
            }
            else
            {
                sendToServer(new CatanersShared.Message(false.ToString(), Translation.TYPE.TradeResponce).toJson());
            }
        }

        public void PM_Chat(Message msg)
        {
            Chat chat = Chat.fromJson(msg.message);
            if (ChatBox.INSTANCE != null)
                ChatBox.INSTANCE.Invoke(new Action(() => ChatBox.INSTANCE.addChat(chat)));
        }

        public void PM_PopUpMessage(Message msg)
        {
            PopUpMessage newMsg = PopUpMessage.fromJson(msg.message);
            switch (newMsg.type)
            {
                case PopUpMessage.TYPE.Notification:
                    System.Windows.MessageBox.Show(newMsg.bodyMsg, newMsg.titleMsg);
                    break;
                case PopUpMessage.TYPE.ResponseNeeded:
                    MessageBoxResult result2 = System.Windows.MessageBox.Show(newMsg.bodyMsg, newMsg.titleMsg, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    break;
            }
        }

        public void PM_UpdateResources(Message msg)
        {
            List<GamePlayer> gpList = Translation.jsonToGPlayerList(msg.message);
            if (Data.currentGameLobby == null)
                return;
            Data.currentGameLobby.gamePlayers = gpList;
            MyScene.addResources();
        }

        public void PM_EndTurn(Message msg)
        {
            int num = Int32.Parse(msg.message);
            if (Data.currentGameLobby.gamePlayers.Count - 1 < num)
            {
                return;
            }
            if (Data.currentGameLobby.gamePlayers[num].Username.Equals(Data.username))
            {
                Data.isMyTurn = true;
                MyScene.addEndTurnButton();
                System.Windows.MessageBox.Show("It's your turn, please roll the dice", "Your Turn", MessageBoxButton.OK);
                CommunicationClient.instance.sendToServer(new CatanersShared.Message("", Translation.TYPE.DiceRoll).toJson());
            }
            else
            {
                Data.isMyTurn = false;
                MyScene.hideEndTurn();
            }
        }

        public void PM_DiceRoll(Message msg)
        {
            System.Windows.MessageBox.Show("You rolled a " + msg.message);
        }
    }
}
