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
            
            FixedCamera2D camera2D = new FixedCamera2D("Camera2D") { BackgroundColor = Color.DimGray};
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
                FontPath = "WarnockPro-Regular.wpk",
                Text = "Settlers of Catan",
                Foreground = Color.Blue,
                Margin = new Thickness(WaveConstants.CENTERWIDTH - (140), 10, 0, 0)
            };
            EntityManager.Add(title);
            this.hexList = LocalConversion.Instance.getHexList();
            CommunicationClient.Instance.setGenerated();

            MyScene.addResources();
            MyScene.addDevelopmentCards();
            MyScene.addRegenerateBoardButton();
            MyScene.addTradeButton();
            MyScene.addPlayerNames();
            MyScene.addChatButton();
            MyScene.addEndTurnButton();
            MyScene.addBuyDevCardButton();
            this.addBuildingCostCard();
        }

        public static void addChatButton()
        {
            //add chatbutton
            Button chatButton = new Button("ChatButton");
            chatButton.Text = "Open Chat";
            Transform2D chatButton2d = chatButton.Entity.FindComponent<Transform2D>();
            chatButton2d.X = WaveConstants.CENTERWIDTH/2;
            chatButton2d.Y = 15;
            chatButton.Click += (s, e) => chatButton_Pressed(s, e);
                //.Entity.FindComponent<TouchGestures>().TouchTap += new EventHandler<GestureEventArgs>(chatButton_Pressed);
            lock (toAddDecor)
            {
                toAddDecor.Add(chatButton);
            }
        }
        
        public static void addTradeButton()
        {
            if (Data.isMyTurn)
            {
                //add tradebutton
                Button tradeButton = new Button("TradeButton");
                tradeButton.Text = "Trade Resources";
                Transform2D tradebutton2d = tradeButton.Entity.FindComponent<Transform2D>();
                tradebutton2d.X = WaveConstants.CENTERWIDTH * 2 - 320;
                tradebutton2d.Y = WaveConstants.CENTERHEIGHT * 2 - 100;
                tradeButton.Click += (s, e) => tradeButton_Pressed(s, e);
                lock (toAddDecor)
                {
                    toAddDecor.Add(tradeButton);
                }
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

                Button newButton = new Button("RegenerateBoardButton");
                newButton.Text = "Regenerate Board";
                Transform2D regenBoard2d = newButton.Entity.FindComponent<Transform2D>();
                regenBoard2d.X = 100;
                newButton.Click += (s, e) => button_Pressed(s, e);
                lock(toAddDecor){
                    toAddDecor.Add(newButton);
                }
                addEndTurnButton();
            }
        }
        public static void addEndTurnButton()
        {
            //add regenerate board if owner
            if (Data.currentGamePlayer == null)
            {
                return;
            }
            
            if (Data.isMyTurn)
            {
                Button endTurnButton = new Button("endTurnButton");
                endTurnButton.Text = "End Turn";
                Transform2D endTurnButton2d = endTurnButton.Entity.FindComponent<Transform2D>();
                endTurnButton2d.X = WaveConstants.CENTERWIDTH * 2 - 120;
                endTurnButton2d.Y = WaveConstants.CENTERHEIGHT * 2 - 100;
                endTurnButton.Click += (s, e) => endTurnButton_Pressed(s,e);
                lock (toAddDecor)
                {
                    toAddDecor.Add(endTurnButton);
                }
            }
        }
        public static void addBuyDevCardButton()
        {
            //add buyDevCard if my turn
            if (Data.currentGamePlayer == null)
            {
                return;
            }

            if (Data.isMyTurn)
            {
                Button buyDevCardButton = new Button("buyDevCardButton");
                buyDevCardButton.Text = "Buy Development Card";
                Transform2D buyDevCardButton2d = buyDevCardButton.Entity.FindComponent<Transform2D>();
                buyDevCardButton2d.X = WaveConstants.CENTERWIDTH * 2 - 550;
                buyDevCardButton2d.Y = WaveConstants.CENTERHEIGHT * 2 - 100;
                buyDevCardButton.Click += (s, e) => buyDevCardButton_Pressed(s, e);
                lock (toAddDecor)
                {
                    toAddDecor.Add(buyDevCardButton);
                }
            }
        }

        private void addBuildingCostCard()
        {
            Entity resourceCard = new Entity("costCard")
                .AddComponent(new Sprite("resourceCost.wpk"))
                .AddComponent(new Transform2D() { 
                Y = WaveConstants.CENTERHEIGHT+100,
                XScale = 0.25f,
                YScale = 0.25f})
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
            EntityManager.Add(resourceCard);
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
                                    X = WaveConstants.CENTERWIDTH - 450 - WORDOFFSET,
                                    Y = WaveConstants.CENTERHEIGHT,
                                    DrawOrder = 2.0f
                                })
                                .AddComponent(new TextControl()
                                {
                                    Text = player4Text,
                                    Foreground = Color.Purple
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
                    //myPlayer
                    if (Data.currentGamePlayer != null && Data.currentGameLobby.gamePlayers[i].Username.Equals(Data.currentGamePlayer.Username))
                    {
                        foreach (var item in Data.currentGameLobby.gamePlayers[i].resources)
                        {
                            if (item.Key != Resource.TYPE.Desert)
                            {
                                sb.Append(item.Key + ": " + item.Value + " ");
                            }
                        }
                        //My Resources Label
                        Entity myResourceLabel = new Entity("myResourceLabel")
                                .AddComponent(new Transform2D()
                                {
                                    X = WaveConstants.CENTERWIDTH + 350,
                                    Y = WaveConstants.CENTERHEIGHT / 2 - 130,
                                    DrawOrder = 2.0f
                                })
                                .AddComponent(new TextControl()
                                {
                                    Text = "My Resources, Victory Points, and Development Cards:",
                                    Foreground = Color.White
                                })
                                .AddComponent(new TextControlRenderer());
                        toAdd.Add(myResourceLabel);
                        //resources display
                        Entity myResourceEntity = new Entity("myResourceEntity")
                                .AddComponent(new Transform2D()
                                {
                                    X = WaveConstants.CENTERWIDTH + 400,
                                    Y = WaveConstants.CENTERHEIGHT / 2 -100 ,
                                    DrawOrder = 2.0f
                                })
                                .AddComponent(new TextControl()
                                {
                                    Text = sb.ToString(),
                                    Foreground = Color.White
                                })
                                .AddComponent(new TextControlRenderer());
                        toAdd.Add(myResourceEntity);
                        MyScene.addVictoryPoints();
                    }
                    //not my player
                    else
                    {
                        sb.Append("Number of Resources: " + Data.currentGameLobby.gamePlayers[i].resourceCount);
                        switch (i)
                        {
                            case 0:
                                Entity player0ResourceEntity = new Entity("player" + (i) + "ResourceEntity")
                                .AddComponent(new Transform2D()
                                {
                                    X = WaveConstants.CENTERWIDTH + 50 - WORDOFFSET,
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
                                    X = WaveConstants.CENTERWIDTH + 50 - WORDOFFSET,
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
                                    Y = WaveConstants.CENTERHEIGHT + 30,
                                    DrawOrder = 2.0f
                                })
                                .AddComponent(new TextControl()
                                {
                                    Text = sb.ToString(),
                                    Foreground = Color.Purple
                                })
                                .AddComponent(new TextControlRenderer());

                                toAdd.Add(player3ResourceEntity);
                                break;
                        }
                    }
                }
            }       
        }

        public static void addVictoryPoints()
        {
            Entity myVPLabel = new Entity("myVPLabel")
                                .AddComponent(new Transform2D()
                                {
                                    X = WaveConstants.CENTERWIDTH + 400,
                                    Y = WaveConstants.CENTERHEIGHT / 2 - 70,
                                    DrawOrder = 2.0f
                                })
                                .AddComponent(new TextControl()
                                {
                                    Text = "Victory Points: " + Data.currentGamePlayer.victoryPoints,
                                    Foreground = Color.White
                                })
                                .AddComponent(new TextControlRenderer());
            toAdd.Add(myVPLabel);
        }

        public static void addDevelopmentCards()
        {
            if (Data.currentGamePlayer != null)
            {                  
                Button useMonopolyButton = new Button("useMonopolyButton");
                useMonopolyButton.Text = "Use Monopoly Dev Card: " + Data.currentGamePlayer.developmentCards[Translation.DevelopmentType.Monopoly];
                Transform2D useMonopolyButton2d = useMonopolyButton.Entity.FindComponent<Transform2D>();
                useMonopolyButton2d.X = WaveConstants.CENTERWIDTH +400;
                useMonopolyButton2d.Y = WaveConstants.CENTERHEIGHT /2 - 45;
                useMonopolyButton.Click += (s, e) => useMonopolyButton_Pressed(s, e);
                lock (toAddDecor)
                {
                    toAddDecor.Add(useMonopolyButton);
                }

                Button useRoadBuildingButton = new Button("useRoadBuildingButton");
                useRoadBuildingButton.Text = "Use Road Building Dev Card: " + Data.currentGamePlayer.developmentCards[Translation.DevelopmentType.RoadBuilding];
                Transform2D useRoadBuildingButton2d = useRoadBuildingButton.Entity.FindComponent<Transform2D>();
                useRoadBuildingButton2d.X = WaveConstants.CENTERWIDTH + 400;
                useRoadBuildingButton2d.Y = WaveConstants.CENTERHEIGHT / 2 - 15;
                useRoadBuildingButton.Click += (s, e) => useRoadBuildingButton_Pressed(s, e);
                lock (toAddDecor)
                {
                    toAddDecor.Add(useRoadBuildingButton);
                }

                Button useYearOfPlentyButton = new Button("useYearOfPlentyButton");
                useYearOfPlentyButton.Text = "Use Year Of Plenty Dev Card: " + Data.currentGamePlayer.developmentCards[Translation.DevelopmentType.YearOfPlenty];
                Transform2D useYearOfPlentyButton2d = useYearOfPlentyButton.Entity.FindComponent<Transform2D>();
                useYearOfPlentyButton2d.X = WaveConstants.CENTERWIDTH + 400;
                useYearOfPlentyButton2d.Y = WaveConstants.CENTERHEIGHT / 2 + 15;
                useYearOfPlentyButton.Click += (s, e) => useYearOfPlentyButton_Pressed(s, e);
                lock (toAddDecor)
                {
                    toAddDecor.Add(useYearOfPlentyButton);
                }

                Entity myKnightLabel = new Entity("myKnightLabel")
                                .AddComponent(new Transform2D()
                                {
                                    X = WaveConstants.CENTERWIDTH + 400,
                                    Y = WaveConstants.CENTERHEIGHT / 2 + 45,
                                    DrawOrder = 2.0f
                                })
                                .AddComponent(new TextControl()
                                {
                                    Text = "Knight Dev Cards: " + Data.currentGamePlayer.developmentCards[Translation.DevelopmentType.Knight],
                                    Foreground = Color.White
                                })
                                .AddComponent(new TextControlRenderer());
                lock (toAdd)
                {
                    toAdd.Add(myKnightLabel);
                }
                Entity myVPDevLabel = new Entity("myVPDevLabel")
                                .AddComponent(new Transform2D()
                                {
                                    X = WaveConstants.CENTERWIDTH + 400,
                                    Y = WaveConstants.CENTERHEIGHT / 2 + 75,
                                    DrawOrder = 2.0f
                                })
                                .AddComponent(new TextControl()
                                {
                                    Text = "Victory Point Dev Cards: " + Data.currentGamePlayer.developmentCards[Translation.DevelopmentType.VictoryPoint],
                                    Foreground = Color.White
                                })
                                .AddComponent(new TextControlRenderer());
                lock (toAdd)
                {
                    toAdd.Add(myVPDevLabel);
                }  
            }
        }

        private static void button_Pressed(object sender, EventArgs e)
        {
            CommunicationClient.Instance.sendToServer(new Message("", Translation.TYPE.RegenerateBoard).toJson());
        }

        private static void chatButton_Pressed(object sender, EventArgs e)
        {
            if (ChatBox.INSTANCE != null)
            {
                ChatBox.INSTANCE.invokedShow();

            }
            else
            {
                new ChatBox().Show();
            }
        }

        private static void tradeButton_Pressed(object sender, EventArgs e)
        {
                TradeForm.INSTANCE.Show();
                TradeForm.INSTANCE.initializeValues();
        }

        private static void endTurnButton_Pressed(object sender, EventArgs e)
        {
            CommunicationClient.Instance.sendToServer(new Message("", Translation.TYPE.EndTurn).toJson());
        }
        private static void buyDevCardButton_Pressed(object sender, EventArgs e)
        {
            CommunicationClient.Instance.sendToServer(new Message(Translation.DevelopmentType.Buy.ToString(), Translation.TYPE.DevelopmentCard).toJson());
        }

        private static void useYearOfPlentyButton_Pressed(object sender, EventArgs e)
        {
            CommunicationClient.Instance.sendToServer(new Message(Translation.DevelopmentType.YearOfPlenty.ToString(), Translation.TYPE.DevelopmentCard).toJson());
        }

        private static void useRoadBuildingButton_Pressed(object sender, EventArgs e)
        {
            CommunicationClient.Instance.sendToServer(new Message(Translation.DevelopmentType.RoadBuilding.ToString(), Translation.TYPE.DevelopmentCard).toJson());
        }

        private static void useMonopolyButton_Pressed(object sender, EventArgs e)
        {
            CommunicationClient.Instance.sendToServer(new Message(Translation.DevelopmentType.Monopoly.ToString(), Translation.TYPE.DevelopmentCard).toJson());
        }

        public static void hideEndTurn()
        {
            Entity invisiblebutton = new Entity("endTurnButton");
            toAdd.Add(invisiblebutton);
        }

        protected override void Start()
        {
            base.Start();

            // This method is called after the CreateScene and Initialize methods and before the first Update.
        }

        public void setAsPurchasedSettle(string name, string username)
        {
            Entity current = this.EntityManager.Find(name);
            current.RemoveComponent<Sprite>();
            current.RemoveComponent<SpriteRenderer>();
            foreach(GamePlayer player in Data.currentGameLobby.gamePlayers)
            {
                if(player.Username.Equals(username))
                {
                    this.EntityManager.Find(name).AddComponent(new Sprite("Settlement" + player.getColor() + ".wpk"));
                }
            }
            current.AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
        }

        public void setAsPurchasedCity(string name, string username)
        {
            Entity current = this.EntityManager.Find(name);
            current.RemoveComponent<Sprite>();
            current.RemoveComponent<SpriteRenderer>();
            foreach (GamePlayer player in Data.currentGameLobby.gamePlayers)
            {
                if (player.Username.Equals(username))
                {
                    this.EntityManager.Find(name).AddComponent(new Sprite("City" + player.getColor() + ".wpk"));
                }
            }
            current.AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
        }

        public void setAsPurchasedRoad(string name, string username)
        {
            Entity current = this.EntityManager.Find(name);
            current.RemoveComponent<Sprite>();
            current.RemoveComponent<SpriteRenderer>();
            foreach (GamePlayer player in Data.currentGameLobby.gamePlayers)
            {
                if (player.Username.Equals(username))
                {
                    this.EntityManager.Find(name).AddComponent(new Sprite("Road" + player.getColor() + ".wpk"));
                }
            }
            current.AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
        }
    }
}
