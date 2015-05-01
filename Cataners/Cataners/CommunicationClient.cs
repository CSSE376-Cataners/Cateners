﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;
using System.Collections;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using WaveEngineGame;
using WaveEngineGameProject;
using System.Diagnostics.CodeAnalysis;

namespace Cataners
{
    public class CommunicationClient
    {
        private StreamReader reader;
        private Boolean Enabled;

        public Dictionary<Translation.TYPE,BlockingCollection<Object>> queues;


        private static CommunicationClient instance;

        public static CommunicationClient Instance {
            get{
                return instance;  
            }
        }

        public System.Net.Sockets.TcpClient clientSocket;

        public CommunicationClient()
        {
            this.Enabled = false;
            this.clientSocket = new System.Net.Sockets.TcpClient();
            CommunicationClient.instance = this;
            clientSocket.ReceiveTimeout = 3;
            queues = new Dictionary<Translation.TYPE,BlockingCollection<object>>();

            foreach(Translation.TYPE t in Enum.GetValues(typeof(Translation.TYPE))) 
            {
                queues.Add(t, new BlockingCollection<object>());
            }
            
        }

        [ExcludeFromCodeCoverage]
        public async void Start()
        {
            try
            {
                await clientSocket.ConnectAsync(Properties.Settings.Default.ServerAddr, Variables.serverPort);
                this.Enabled = true;

                Thread clientThread = new Thread(queueMessagesAsync);
                clientThread.Start();

                writer = new StreamWriter(clientSocket.GetStream(), Encoding.Unicode);
            }
            catch
            {
                MessageBox.Show("Our servers seem to be having some trouble - we apologize for the inconvenience.", "Error - Server Not Available", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        StreamWriter writer;
        public virtual void sendToServer(String msg)
        {
            if (instance.clientSocket.Connected)
            {
                writer.WriteLine(msg);
                writer.Flush();
                Console.WriteLine("Sending To server: " + msg);
            }
        }

        public async void queueMessagesAsync()
        {
            Console.WriteLine("Started Listening");
            while (Enabled && clientSocket.Connected)
            {
                this.reader = new StreamReader(clientSocket.GetStream(), Encoding.Unicode);
                string line;
                Task<String> task = reader.ReadLineAsync();
                try
                {
                    line = await task;
                    Console.WriteLine("Message: " + line);
                    Thread thread = new Thread(() => processesMessage(line));
                    thread.Start();
                }
                catch(IOException)
                {
                    MessageBox.Show("You've been disconnected from the server", "Error - I/O", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
            Enabled = false;
        }

        public void processesMessage(String s)
        {
            CatanersShared.Message msg;
            try
            {
                msg = CatanersShared.Message.fromJson(s);
            }
            catch (Exception) {
                Console.WriteLine("Recived Invalid Message: " + s);
                return;
            }


            switch (msg.type)
            {
                case Translation.TYPE.Login:
                    if (!msg.message.Equals("-1"))
                    {
                        Data.username = msg.message;
                    }
                    queues[Translation.TYPE.Login].Add(msg.message);
                    break;
                case Translation.TYPE.Register:
                    queues[Translation.TYPE.Register].Add(msg.message);
                    break;
                case Translation.TYPE.HexMessage:
                    int[][] array = Translation.jsonToIntArrayTwo(msg.message);
                    break;
                case Translation.TYPE.RequestLobbies:
                    Data.Lobbies.Clear();
                    Data.Lobbies.AddRange(Lobby.jsonToLobbyList(msg.message));
                    if (JoinGameForm.INSTANCE != null)
                    {
                        JoinGameForm.INSTANCE.invokedRefresh();
                    }
                    break;
                case Translation.TYPE.UpdateLobby:
                    if (LobbyForm.INSTANCE != null)
                    {
                        Data.currentLobby = Lobby.fromJson(msg.message);
                        LobbyForm.INSTANCE.invokedRefresh();
                    }
                    break;
                case Translation.TYPE.LeaveLobby:
                    //close lobby window
                    break;

                case Translation.TYPE.StartGame:
                    //Data.currentLobby = gameLobby
                    Program.Main();
                    break;

                case Translation.TYPE.addResource:
                    AddResource addResource = AddResource.fromJson(msg.message);
                        break;
                case Translation.TYPE.GetGameLobby:
                    Data.currentLobby = GameLobby.fromJson(msg.message);
                    break;
            }
        }

        /*
        public async Task<string> awaitingMessage(Translation.TYPE type)
        {
            //TODO: come back and finish type = null

            return "";
        }
         */
    }
}
