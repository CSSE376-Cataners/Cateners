using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;

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


    }
}
