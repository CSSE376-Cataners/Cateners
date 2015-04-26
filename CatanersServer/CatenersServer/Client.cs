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
    public class Client
    {
        public TcpClient socket;
        public bool Enabled;

        public int userID;
        public String userName;

        public Lobby currentLobby;

        public Client(TcpClient tcp)
        {
            this.socket = tcp;
            writer = new StreamWriter(socket.GetStream(), Encoding.Unicode);
            Enabled = true;
            userID = -1;
            userName = null;
            currentLobby = null;
        }

        public Client()
        {
            // For Testing
        }

        public async void queueMessagesAsync()
        {
            StreamReader reader = new StreamReader(socket.GetStream(), Encoding.Unicode);
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
            Console.WriteLine("Client Closed: " + ((System.Net.IPEndPoint)socket.Client.RemoteEndPoint).Address.ToString());
        }

        StreamWriter writer;

        public virtual void sendToClient(String msg)
        {
            writer.WriteLine(msg);
            writer.Flush();
        }

        public void processesMessage(String s)
        {
            Message msg = Message.fromJson(s);

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

                case  Translation.TYPE.CreateLobby:
                    Lobby lobby = Lobby.fromJson(msg.message);
                    Player owner = new Player(this.userName);

                    Lobby newLobby = new Lobby(lobby.GameName, lobby.MaxTimePerTurn, new Player(this.userName), Data.INSTANCE.nextLobbyID++);
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
                    // TODO Make sure not full
                    int lobbyID = int.Parse(msg.message);
                    for (int i = 0; i < Data.INSTANCE.Lobbies.Count; i++)
                    {
                        if (Data.INSTANCE.Lobbies[i].lobbyID == lobbyID)
                        {
                            this.currentLobby = Data.INSTANCE.Lobbies[i];
                            this.currentLobby.addPlayer(new Player(this.userName));
                            break;
                        }
                    }
                break;

                case Translation.TYPE.UpdateLobby:
                    if(this.currentLobby != null)
                        sendToClient(new Message(this.currentLobby.toJson(),Translation.TYPE.UpdateLobby).toJson());
                break;

                default:
                    // We Are just going to ignore it.
                break;
            }
        }

    }
}
