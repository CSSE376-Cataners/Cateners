#region Using Statements
using System;
using System.Collections;
using WaveEngine.Common;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Cameras;
using WaveEngine.Components.Gestures;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics2D;
using WaveEngine.Framework.Resources;
using WaveEngine.Framework.Services;
using CatanersShared;
using WaveEngine.Common.Input;
using WaveEngine.Framework.UI;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Cataners
{
    public class MyScene : Scene
    {
        private static MyScene instance;
        public static MyScene Instance
        {
            get
            {
                return instance;
            }
        }
        private HexHolder[] hexList;
        public static int hexNumber = 19;
        public static float WORDOFFSET = 40.0f;
        public static List<Entity> toAdd = new List<Entity>();
        public static List<BaseDecorator> toAddDecor = new List<BaseDecorator>();
        
        LocalConversion localConversion = new LocalConversion();

        public MyScene()
            : base()
        {
            WaveConstants.setWaveValues();
            instance = this;
        }

        protected override void CreateScene()
        {
            //Insert your scene definition here.

            //Create a 3D camera
            
            FixedCamera2D camera2D = new FixedCamera2D("Camera2D") { BackgroundColor = Color.Gold };
            EntityManager.Add(camera2D);
            Entity background = new Entity("Background")
            .AddComponent(new Sprite("Background.wpk"))
            .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
            .AddComponent(new Transform2D()
            {
                Scale = new Vector2((WaveConstants.HEX_WIDTH * (5.2f)) / 677, (WaveConstants.HEX_HEIGHT * (5.2f)) / 559),
                X = WaveConstants.HEX_START_X - (WaveConstants.HEX_WIDTH * 1.1f),
                Y = WaveConstants.HEX_START_Y - ((WaveConstants.HEX_HEIGHT * 1.2f) / 2),
                DrawOrder = .9f
            });
            this.EntityManager.Add(background);
            TextBlock title = new TextBlock()
            {
                Text = "Settlers of Catan",
                Foreground = Color.Blue,
                Margin = new Thickness(WaveConstants.CENTERWIDTH - (80), 0, 0, 0)
            };
            EntityManager.Add(title);
            this.hexList = LocalConversion.Instance.getHexList();
            CommunicationClient.Instance.setGenerated();
        }

        public static void addChatButton()
        {
            //add chatbutton
            Button chatButton = new Button();
            chatButton.Text = "Trade Resources";
            chatButton.Width = 120;
            chatButton.Height = 40;
            Transform2D chatButton2d = chatButton.Entity.FindComponent<Transform2D>();
            chatButton2d.X = WaveConstants.CENTERWIDTH * 2 - 120;
            chatButton2d.Y = WaveConstants.CENTERHEIGHT;
            chatButton.Entity.FindComponent<TouchGestures>().TouchPressed += new EventHandler<GestureEventArgs>(chatButton_Pressed);
            lock (toAddDecor)
            {
                toAddDecor.Add(chatButton);
            }
        }
        
        public static void addTradeButton()
        {
            //add tradebutton
            Button tradeButton = new Button();
            tradeButton.Text = "Trade Resources";
            tradeButton.Width = 120;
            tradeButton.Height = 40;
            Transform2D tradebutton2d = tradeButton.Entity.FindComponent<Transform2D>();
            tradebutton2d.X = WaveConstants.CENTERWIDTH * 2 - 120;
            tradebutton2d.Y = WaveConstants.CENTERHEIGHT * 2 - 100;
            tradeButton.Entity.FindComponent<TouchGestures>().TouchPressed += new EventHandler<GestureEventArgs>(tradeButton_Pressed);
            lock (toAddDecor)
            {
                toAddDecor.Add(tradeButton);
            }
        }

        public static void addRegenerateBoardButton()
        {
            //add regenerate board if owner
            if (Data.currentGameOwner == null)
            {
                return;
            }
            if (Data.username.Equals(Data.currentGameOwner.Username))
            {
                Button newButton = new Button();
                newButton.Text = "Regenerate Board";
                newButton.Width = 120;
                newButton.Height = 40;
                newButton.Entity.FindComponent<TouchGestures>().TouchPressed += new EventHandler<GestureEventArgs>(button_Pressed);
                lock(toAddDecor){
                    toAddDecor.Add(newButton);
                }
            }
        }

        public static void addPlayerNames()
        {
            //player name
            String player1Text;
            if (Data.currentGameLobby != null)
            {

                

                if (Data.currentGameLobby.gamePlayers.Count > 0)
                {
                    player1Text = Data.currentGameLobby.gamePlayers[0].ToString();
                }
                else
                {
                    player1Text = "player1";
                }
                //add player

                Entity player1Name = new Entity("player1Name")
                                .AddComponent(new Transform2D()
                                {
                                    X = WaveConstants.CENTERWIDTH - WORDOFFSET,
                                    Y = 100,
                                    DrawOrder = 2.0f
                                })
                                .AddComponent(new TextControl()
                                {
                                    Text = player1Text,
                                    Foreground = Color.Blue
                                })
                                .AddComponent(new TextControlRenderer());
                toAdd.Add(player1Name);

                String player2Text;
                if (Data.currentGameLobby.gamePlayers.Count > 1)
                {
                    player2Text = Data.currentGameLobby.gamePlayers[1].ToString();
                }
                else
                {
                    player2Text = "player2";
                }

                Entity player2Name = new Entity("player2Name")
                                .AddComponent(new Transform2D()
                                {
                                    X = WaveConstants.CENTERWIDTH + 400 - WORDOFFSET,
                                    Y = WaveConstants.CENTERHEIGHT,
                                    DrawOrder = 2.0f
                                })
                                .AddComponent(new TextControl()
                                {
                                    Text = player2Text.ToString(),
                                    Foreground = Color.Red
                                })
                                .AddComponent(new TextControlRenderer());
                toAdd.Add(player2Name);

                String player3Text;
                if (Data.currentGameLobby.gamePlayers.Count > 2)
                {
                    player3Text = Data.currentGameLobby.gamePlayers[2].ToString();
                }
                else
                {
                    player3Text = "player3";
                }

                Entity player3Name = new Entity("player3Name")
                                .AddComponent(new Transform2D()
                                {
                                    X = WaveConstants.CENTERWIDTH - WORDOFFSET,
                                    Y = WaveConstants.CENTERHEIGHT*2-100,
                                    DrawOrder = 2.0f
                                })
                                .AddComponent(new TextControl()
                                {
                                    Text = player3Text.ToString(),
                                    Foreground = Color.Green
                                })
                                .AddComponent(new TextControlRenderer());

                toAdd.Add(player3Name);

                String player4Text;
                if (Data.currentGameLobby.gamePlayers.Count > 3)
                {
                    player4Text = Data.currentGameLobby.gamePlayers[3].ToString();
                }
                else
                {
                    player4Text = "player4";
                }

                Entity player4Name = new Entity("player4Name")
                                .AddComponent(new Transform2D()
                                {
                                    X = WaveConstants.CENTERWIDTH - 400 - WORDOFFSET,
                                    Y = WaveConstants.CENTERHEIGHT,
                                    DrawOrder = 2.0f
                                })
                                .AddComponent(new TextControl()
                                {
                                    Text = player4Text,
                                    Foreground = Color.Black
                                })
                                .AddComponent(new TextControlRenderer());
                toAdd.Add(player4Name);
            }
        }

        public static void addResources()
        {
            StringBuilder sb = new StringBuilder();
            if (Data.currentGameLobby != null)
            {
                for (int i = 0; i < Data.currentGameLobby.gamePlayers.Count; i++)
                {
                    sb.Clear();
                    foreach (var item in Data.currentGameLobby.gamePlayers[i].resources)
                    {
                        sb.Append(item.Key + ": " + item.Value + " ");
                    }

                        switch (i)
                        {
                            case 0:
                                Entity player0ResourceEntity = new Entity("player" + (i) + "ResourceEntity")
                                .AddComponent(new Transform2D()
                                {
                                    X = WaveConstants.CENTERWIDTH +50 - WORDOFFSET,
                                    Y = 130,
                                    DrawOrder = 2.0f
                                })
                                .AddComponent(new TextControl()
                                {
                                    Text = sb.ToString(),
                                    Foreground = Color.Blue
                                })
                                .AddComponent(new TextControlRenderer());

                                toAdd.Add(player0ResourceEntity);
                                break;
                            case 1:
                                Entity player1ResourceEntity = new Entity("player" + (i) + "ResourceEntity")
                                .AddComponent(new Transform2D()
                                {
                                    X = WaveConstants.CENTERWIDTH + 450 - WORDOFFSET,
                                    Y = WaveConstants.CENTERHEIGHT + 30,
                                    DrawOrder = 2.0f
                                })
                                .AddComponent(new TextControl()
                                {
                                    Text = sb.ToString(),
                                    Foreground = Color.Red
                                })
                                .AddComponent(new TextControlRenderer());

                                toAdd.Add(player1ResourceEntity);
                                break;
                            case 2:
                                Entity player2ResourceEntity = new Entity("player" + (i) + "ResourceEntity")
                                .AddComponent(new Transform2D()
                                {
                                    X = WaveConstants.CENTERWIDTH +50 - WORDOFFSET,
                                    Y = (WaveConstants.CENTERHEIGHT * 2) - 80,
                                    DrawOrder = 2.0f
                                })
                                .AddComponent(new TextControl()
                                {
                                    Text = sb.ToString(),
                                    Foreground = Color.Green
                                })
                                .AddComponent(new TextControlRenderer());

                                toAdd.Add(player2ResourceEntity);
                                break;
                            case 3:
                                Entity player3ResourceEntity = new Entity("player" + (i) + "ResourceEntity")
                                .AddComponent(new Transform2D()
                                {
                                    X = WaveConstants.CENTERWIDTH - 450 - WORDOFFSET,
                                    Y = WaveConstants.CENTERHEIGHT+30,
                                    DrawOrder = 2.0f
                                })
                                .AddComponent(new TextControl()
                                {
                                    Text = sb.ToString(),
                                    Foreground = Color.Black
                                })
                                .AddComponent(new TextControlRenderer());

                                toAdd.Add(player3ResourceEntity);
                                break;
                        }                        
                }
            }         
        }


        private static void button_Pressed(object sender, GestureEventArgs e)
        {
            CommunicationClient.Instance.sendToServer(new Message("", Translation.TYPE.RegenerateBoard).toJson());
        }

        private static void chatButton_Pressed(object sender, GestureEventArgs e)
        {
            ChatBox.INSTANCE.Show();
        }

        private static void tradeButton_Pressed(object sender, GestureEventArgs e)
        {
                TradeForm.INSTANCE.Show();
                TradeForm.INSTANCE.initializeValues();
        }

        protected override void Start()
        {
            base.Start();

            // This method is called after the CreateScene and Initialize methods and before the first Update.
        }


        public void setAsPurchasedSettle(string name)
        {
            Console.WriteLine("here");
            this.EntityManager.Find(name).FindComponent<Sprite>().TintColor = Color.Blue;
        }
    }
}
