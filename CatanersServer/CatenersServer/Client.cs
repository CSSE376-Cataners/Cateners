using System;
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
    public partial class Client
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

        private String sIP;

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
            sIP = ((System.Net.IPEndPoint)socket.Client.RemoteEndPoint).Address.ToString();
        }

        public Client()
        {
            // For Testing
            Enabled = true;
            userID = -1;
            userName = null;
            currentLobby = null;
            this.player = new ServerPlayer(userName, this);
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
                    this.reader.Close();
                    this.socket.Close();
                    break;
                }
            }
            leaveLobby();
            Console.WriteLine("Client Closed: " + sIP);
        }

        public virtual void sendToClient(String msg)
        {
            writer.WriteLine(msg);
            writer.Flush();
        }

        public static List<Translation.TYPE> turnRequired = new List<Translation.TYPE>() { Translation.TYPE.DevelopmentCard ,Translation.TYPE.BuyRoad , Translation.TYPE.BuySettlement , Translation.TYPE.Game , Translation.TYPE.StartTrade, Translation.TYPE.EndTurn, Translation.TYPE.DiceRoll };

        private object processMessageLock = false;
        public void processesMessage(String s)
        {
            lock (processMessageLock)
            {
            Message msg = Message.fromJson(s);
            System.Diagnostics.Debug.WriteLine(String.Format("[{0}] {1}::TYPE: {2} :: Message: {3}", DateTime.Now.ToString("T"), this.userName, msg.type, msg.message));


            // TODO Pick all messages that would be considered turn based
            
                if (this.gameLobby != null && this.serverLogic != null && !this.gameLobby.gamePlayers[serverLogic.playerTurn].Username.Equals(this.userName))
                if (!Data.DEBUG && turnRequired.Contains(msg.type))
                {
                    return;
                }
            
                switch (msg.type)
                {
                case Translation.TYPE.Login:
                    PM_Login(msg);
                break;
                case Translation.TYPE.Register:
                    PM_Register(msg);
                break;
                case Translation.TYPE.RequestLobbies:
                    PM_RequestLobbies(msg);
                break;
                case Translation.TYPE.BuySettlement:
                    PM_BuySettlement(msg);
                break;
                case Translation.TYPE.BuyRoad:
                    PM_BuyRoad(msg);
                break;
                    case Translation.TYPE.CreateLobby:
                    PM_CreateLobby(msg);
                break;
                case Translation.TYPE.ChangeReadyStatus:
                    PM_ChangeReadyStatus(msg);
                break;
                case Translation.TYPE.JoinLobby:
                    PM_JoinLobby(msg);
                break;
                case Translation.TYPE.LeaveLobby:
                    PM_LeaveLobby(msg);
                break;
                case Translation.TYPE.UpdateLobby:
                    PM_UpdateLobby(msg);
                break;
                case Translation.TYPE.RegenerateBoard:
                    PM_RegenerateBoard(msg);
                break;
                case Translation.TYPE.GetGameLobby:
                    PM_GetGameLobby(msg);
                break;
                case Translation.TYPE.StartGame:
                    PM_StartGame(msg);
                break;
                case Translation.TYPE.StartTrade:
                    PM_StartTrade(msg);
                break;
                case Translation.TYPE.Chat:
                    PM_Chat(msg);
                break;
                case Translation.TYPE.TradeResponce:
                    PM_TradeResponse(msg);
                break;
                case Translation.TYPE.EndTurn:
                    PM_EndTurn(msg);
                break;
                case Translation.TYPE.DiceRoll:
                    PM_DiceRoll(msg);
                break;
                case Translation.TYPE.DevelopmentCard:
                    PM_DevelopmentCard(msg);
                    break;
                default:
                    // We Are just going to ignore it.
                break;
            }
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
            PM_LeaveLobby(null);
        }

        public void socketClosed()
        {
            leaveLobby();
        }
    }
}
