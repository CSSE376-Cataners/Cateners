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
        public static float CENTERWIDTH = (WaveServices.Platform.ScreenWidth) / 2;
        public static float CENTERHEIGHT = (WaveServices.Platform.ScreenHeight) / 2;
        public static float WIDTH_TO_HEIGHT = ((float)WaveServices.Platform.ScreenWidth) / ((float)WaveServices.Platform.ScreenHeight);
        public static float HEX_WIDTH = (((float)WaveServices.Platform.ScreenWidth) / 8.0f) / WIDTH_TO_HEIGHT;
        public static float HEX_SCALE_X = HEX_WIDTH / 220.0f;
        public static float HEX_SCALE_Y = HEX_WIDTH * ((float)1.1681818181) / 257.0f;
        public static float HEX_HEIGHT = (HEX_WIDTH * (float)1.168181818);
        public static float TRIANGLE_HEIGHT = HEX_HEIGHT * (float)0.2723735409;
        public static float HEX_START_X = (((float)WaveServices.Platform.ScreenWidth) / 2.0f) - ((HEX_WIDTH * 3) / 2);
        public static float HEX_START_Y = (((float)WaveServices.Platform.ScreenHeight) / 2.0f) - ((3 * HEX_HEIGHT) - (4 * TRIANGLE_HEIGHT));
        public static float ROLL_NUMBER_SCALE = HEX_WIDTH / (2 * 50);
        public static float ROLL_NUMBER_WIDTH = 50 * ROLL_NUMBER_SCALE;
        public static float ROLL_NUMBER_HEIGHT = 50 * ROLL_NUMBER_SCALE;
        public static float SETTLEMENT_SCALE_X = (HEX_WIDTH / 10) / 684;
        public static float SETTLEMENT_SCALE_Y = (HEX_HEIGHT / 10) / 559;
        public static float SETTLEMENT_WIDTH = 684 * SETTLEMENT_SCALE_X;
        public static float SETTLEMENT_HEIGHT = 684 * SETTLEMENT_SCALE_Y;

        public static List<Entity> toAdd = new List<Entity>();
        public static List<BaseDecorator> toAddDecor = new List<BaseDecorator>();

        LocalConversion localConversion = new LocalConversion();

        public MyScene()
            : base()
        {
            instance = this;
        }

        protected override void CreateScene()
        {
            //Insert your scene definition here.

            //Create a 3D camera
            CommunicationClient.Instance.sendToServer(new CatanersShared.Message("", Translation.TYPE.GetGameLobby).toJson());
            Button newButton = new Button(); 
            newButton.Text = "Regenerate Board"; 
            newButton.Width = 120; 
            newButton.Height = 40;
            EntityManager.Add(newButton);
            newButton.Entity.FindComponent<TouchGestures>().TouchPressed += new EventHandler<GestureEventArgs>(button_Pressed);
            FixedCamera2D camera2D = new FixedCamera2D("Camera2D") { BackgroundColor = Color.Gold };
            EntityManager.Add(camera2D);
            Entity background = new Entity("Background")
            .AddComponent(new Sprite("Background.wpk"))
            .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
            .AddComponent(new Transform2D()
            {
                Scale = new Vector2((HEX_WIDTH * (5.2f)) / 677, (HEX_HEIGHT * (5.2f)) / 559),
                X = HEX_START_X - (HEX_WIDTH * 1.1f),
                Y = HEX_START_Y - ((HEX_HEIGHT * 1.2f) / 2),
                DrawOrder = .9f
            });
            this.EntityManager.Add(background);
            TextBlock title = new TextBlock()
            {
                Text = "Settlers of Catan",
                Foreground = Color.Blue,
                Margin = new Thickness(CENTERWIDTH - (80), 0, 0, 0)
            };
            EntityManager.Add(title);
            this.hexList = LocalConversion.Instance.getHexList();
            addPlayerNames();
            addResources();
            
        }

        public void addPlayerNames()
        {
            //player name
            String player1Text;
            if (Data.currentLobby != null)
            {
                if (Data.currentLobby.PlayerCount == 1)
                {
                    player1Text = Data.currentLobby.Players[0].ToString();
                }
                else
                {
                    player1Text = "player1";
                }
                //add player
                TextBlock player1 = new TextBlock()
                {
                    Text = player1Text,
                    Width = 100,
                    Foreground = Color.Blue,
                    Margin = new Thickness(CENTERWIDTH - (WORDOFFSET), 100, 0, 0),
                };
                EntityManager.Add(player1);

                String player2Text;
                if (Data.currentLobby.PlayerCount > 1)
                {
                    player2Text = Data.currentLobby.Players[1].ToString();
                }
                else
                {
                    player2Text = "player2";
                }

                TextBlock player2 = new TextBlock()
                {
                    Text = player2Text,
                    Width = 100,
                    Foreground = Color.Red,
                    Margin = new Thickness(CENTERWIDTH + 400 - WORDOFFSET, CENTERHEIGHT, 0, 0),

                };
                EntityManager.Add(player2);

                String player3Text;
                if (Data.currentLobby.PlayerCount > 2)
                {
                    player3Text = Data.currentLobby.Players[2].ToString();
                }
                else
                {
                    player3Text = "player3";
                }
                TextBlock player3 = new TextBlock()
                {
                    Text = player3Text,
                    Width = 100,
                    Foreground = Color.Green,
                    Margin = new Thickness(CENTERWIDTH - WORDOFFSET, CENTERHEIGHT * (2) - 100, 0, 0),
                };
                EntityManager.Add(player3);

                String player4Text;
                if (Data.currentLobby.PlayerCount > 3)
                {
                    player4Text = Data.currentLobby.Players[3].ToString();
                }
                else
                {
                    player4Text = "player4";
                }

                TextBlock player4 = new TextBlock()
                {
                    Text = player4Text,
                    Width = 100,
                    Foreground = Color.Black,
                    Margin = new Thickness(CENTERWIDTH - 400 - WORDOFFSET, CENTERHEIGHT, 0, 0),

                };
                EntityManager.Add(player4);
            }
            //This may be right, but either way it's for testing purposes for now
        }

        public void addResources()
        {
            StringBuilder sb = new StringBuilder();
            if (Data.currentLobby is GameLobby)
            {
                
                foreach (var item in ((GameLobby)Data.currentLobby).gamePlayers[0].resources)
                {
                    sb.Append(item.Key + ": " + item.Value + ", ");
                }
            }
            TextBlock player4Resources = new TextBlock()
            {
                Text = sb.ToString(),
                Width = 100,
                Foreground = Color.Black,
                Margin = new Thickness(CENTERWIDTH - 400 - WORDOFFSET, CENTERHEIGHT+500, 0, 0),

            };
            toAddDecor.Add(player4Resources);
        }

        public void drawHexes()
        {
            
            this.hexList = LocalConversion.Instance.getHexList();
            Console.WriteLine(this.hexList.ToString());
            lock (toAdd)
            {
                for (int g = 0; g < 19; g++)
                {
                    HexHolder current = this.hexList[g];
                    int posNum = current.getPlacementNumber();
                    if (posNum < 3)
                    {
                        current.getHex().AddComponent(new Transform2D()
                        {
                            Scale = new Vector2(HEX_SCALE_X, HEX_SCALE_Y),
                            X = HEX_START_X + (HEX_WIDTH * posNum),
                            Y = HEX_START_Y,
                            DrawOrder = .6f
                        });
                        current.getRollEntity().AddComponent(new Transform2D()
                        {
                            Scale = new Vector2(ROLL_NUMBER_SCALE, ROLL_NUMBER_SCALE),
                            X = HEX_START_X + (HEX_WIDTH * posNum) + (HEX_WIDTH / 2) - (ROLL_NUMBER_WIDTH / 2),
                            Y = HEX_START_Y + (HEX_HEIGHT / 2) - (ROLL_NUMBER_HEIGHT / 2),
                            DrawOrder = .1f
                        });
                        for (int k = 0; k < 6; k++)
                        {
                            SettlementHolder currSettle = current.getSettlementList()[k];
                            if (currSettle.canAddComponent)
                            {
                                float XLoc;
                                float YLoc;
                                if (k == 0)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + (HEX_WIDTH / 2) - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 1)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y + TRIANGLE_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 2)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + HEX_WIDTH - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y + TRIANGLE_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 3)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y - TRIANGLE_HEIGHT + HEX_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 4)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + HEX_WIDTH - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y - TRIANGLE_HEIGHT + HEX_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + (HEX_WIDTH / 2) - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y + HEX_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                currSettle.getSettlement().AddComponent(new Transform2D()
                                {
                                    Scale = new Vector2(SETTLEMENT_SCALE_X, SETTLEMENT_SCALE_Y),
                                    X = XLoc,
                                    Y = YLoc,
                                    DrawOrder = .05f
                                });
                                currSettle.canAddComponent = false;
                                Console.WriteLine(currSettle.getPlacementNumber());
                                Entity e = currSettle.getSettlement();
                                toAdd.Add(e);
                            }
                        }
                    }
                    else if (posNum < 7)
                    {
                        current.getHex().AddComponent(new Transform2D()
                        {
                            Scale = new Vector2(HEX_SCALE_X, HEX_SCALE_Y),
                            X = HEX_START_X - (HEX_WIDTH / 2) + (HEX_WIDTH * (posNum - 3)),
                            Y = HEX_START_Y + (HEX_HEIGHT - TRIANGLE_HEIGHT),
                            DrawOrder = .6f
                        });
                        current.getRollEntity().AddComponent(new Transform2D()
                        {
                            Scale = new Vector2(ROLL_NUMBER_SCALE, ROLL_NUMBER_SCALE),
                            X = HEX_START_X - (HEX_WIDTH / 2) + (HEX_WIDTH * (posNum - 3)) + (HEX_WIDTH / 2) - (ROLL_NUMBER_WIDTH / 2),
                            Y = HEX_START_Y + (HEX_HEIGHT - TRIANGLE_HEIGHT) + (HEX_HEIGHT / 2) - (ROLL_NUMBER_HEIGHT / 2),
                            DrawOrder = .1f
                        });
                        for (int k = 0; k < 6; k++)
                        {
                            SettlementHolder currSettle = current.getSettlementList()[k];
                            if (currSettle.canAddComponent)
                            {
                                float XLoc;
                                float YLoc;
                                if (k == 0)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + (HEX_WIDTH / 2) - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 1)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y + TRIANGLE_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 2)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + HEX_WIDTH - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y + TRIANGLE_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 3)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y - TRIANGLE_HEIGHT + HEX_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 4)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + HEX_WIDTH - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y - TRIANGLE_HEIGHT + HEX_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + (HEX_WIDTH / 2) - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y + HEX_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                currSettle.getSettlement().AddComponent(new Transform2D()
                                {
                                    Scale = new Vector2(SETTLEMENT_SCALE_X, SETTLEMENT_SCALE_Y),
                                    X = XLoc,
                                    Y = YLoc,
                                    DrawOrder = .05f
                                });
                                currSettle.canAddComponent = false;
                                Console.WriteLine(currSettle.getPlacementNumber());
                                Entity e = currSettle.getSettlement();
                                toAdd.Add(e);
                            }
                        }
                    }
                    else if (posNum < 12)
                    {
                        current.getHex().AddComponent(new Transform2D()
                        {
                            Scale = new Vector2(HEX_SCALE_X, HEX_SCALE_Y),
                            X = HEX_START_X - (HEX_WIDTH) + (HEX_WIDTH * (posNum - 7)),
                            Y = HEX_START_Y + (2 * HEX_HEIGHT) - (2 * TRIANGLE_HEIGHT),
                            DrawOrder = .6f
                        });
                        current.getRollEntity().AddComponent(new Transform2D()
                        {
                            Scale = new Vector2(ROLL_NUMBER_SCALE, ROLL_NUMBER_SCALE),
                            X = HEX_START_X - (HEX_WIDTH) + (HEX_WIDTH * (posNum - 7)) + (HEX_WIDTH / 2) - (ROLL_NUMBER_WIDTH / 2),
                            Y = HEX_START_Y + (2 * HEX_HEIGHT) - (2 * TRIANGLE_HEIGHT) + (HEX_HEIGHT / 2) - (ROLL_NUMBER_HEIGHT / 2),
                            DrawOrder = .1f
                        });
                        for (int k = 0; k < 6; k++)
                        {
                            SettlementHolder currSettle = current.getSettlementList()[k];
                            if (currSettle.canAddComponent)
                            {
                                float XLoc;
                                float YLoc;
                                if (k == 0)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + (HEX_WIDTH / 2) - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 1)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y + TRIANGLE_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 2)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + HEX_WIDTH - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y + TRIANGLE_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 3)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y - TRIANGLE_HEIGHT + HEX_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 4)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + HEX_WIDTH - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y - TRIANGLE_HEIGHT + HEX_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + (HEX_WIDTH / 2) - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y + HEX_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                currSettle.getSettlement().AddComponent(new Transform2D()
                                {
                                    Scale = new Vector2(SETTLEMENT_SCALE_X, SETTLEMENT_SCALE_Y),
                                    X = XLoc,
                                    Y = YLoc,
                                    DrawOrder = .05f
                                });
                                currSettle.canAddComponent = false;
                                Console.WriteLine(currSettle.getPlacementNumber());
                                Entity e = currSettle.getSettlement();
                                toAdd.Add(e);
                            }
                        }
                    }
                    else if (posNum < 16)
                    {
                        current.getHex().AddComponent(new Transform2D()
                        {
                            Scale = new Vector2(HEX_SCALE_X, HEX_SCALE_Y),
                            X = HEX_START_X - (HEX_WIDTH / 2) + (HEX_WIDTH * (posNum - 12)),
                            Y = HEX_START_Y + (3 * HEX_HEIGHT) - (3 * TRIANGLE_HEIGHT),
                            DrawOrder = .6f
                        });
                        current.getRollEntity().AddComponent(new Transform2D()
                        {
                            Scale = new Vector2(ROLL_NUMBER_SCALE, ROLL_NUMBER_SCALE),
                            X = HEX_START_X - (HEX_WIDTH / 2) + (HEX_WIDTH * (posNum - 12)) + (HEX_WIDTH / 2) - (ROLL_NUMBER_WIDTH / 2),
                            Y = HEX_START_Y + (3 * HEX_HEIGHT) - (3 * TRIANGLE_HEIGHT) + (HEX_HEIGHT / 2) - (ROLL_NUMBER_HEIGHT / 2),
                            DrawOrder = .1f
                        });
                        for (int k = 0; k < 6; k++)
                        {
                            SettlementHolder currSettle = current.getSettlementList()[k];
                            if (currSettle.canAddComponent)
                            {
                                float XLoc;
                                float YLoc;
                                if (k == 0)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + (HEX_WIDTH / 2) - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 1)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y + TRIANGLE_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 2)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + HEX_WIDTH - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y + TRIANGLE_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 3)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y - TRIANGLE_HEIGHT + HEX_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 4)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + HEX_WIDTH - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y - TRIANGLE_HEIGHT + HEX_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + (HEX_WIDTH / 2) - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y + HEX_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                currSettle.getSettlement().AddComponent(new Transform2D()
                                {
                                    Scale = new Vector2(SETTLEMENT_SCALE_X, SETTLEMENT_SCALE_Y),
                                    X = XLoc,
                                    Y = YLoc,
                                    DrawOrder = .05f
                                });
                                currSettle.canAddComponent = false;
                                Console.WriteLine(currSettle.getPlacementNumber());
                                Entity e = currSettle.getSettlement();
                                toAdd.Add(e);
                            }
                        }
                    }
                    else
                    {
                        current.getHex().AddComponent(new Transform2D()
                        {
                            Scale = new Vector2(HEX_SCALE_X, HEX_SCALE_Y),
                            X = HEX_START_X + (HEX_WIDTH * (posNum - 16)),
                            Y = HEX_START_Y + (4 * HEX_HEIGHT) - (4 * TRIANGLE_HEIGHT),
                            DrawOrder = .6f
                        });
                        current.getRollEntity().AddComponent(new Transform2D()
                        {
                            Scale = new Vector2(ROLL_NUMBER_SCALE, ROLL_NUMBER_SCALE),
                            X = HEX_START_X + (HEX_WIDTH * (posNum - 16)) + (HEX_WIDTH / 2) - (ROLL_NUMBER_WIDTH / 2),
                            Y = HEX_START_Y + (4 * HEX_HEIGHT) - (4 * TRIANGLE_HEIGHT) + (HEX_HEIGHT / 2) - (ROLL_NUMBER_HEIGHT / 2),
                            DrawOrder = .1f
                        });
                        for (int k = 0; k < 6; k++)
                        {
                            SettlementHolder currSettle = current.getSettlementList()[k];
                            if (currSettle.canAddComponent)
                            {
                                float XLoc;
                                float YLoc;
                                if (k == 0)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + (HEX_WIDTH / 2) - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 1)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y + TRIANGLE_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 2)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + HEX_WIDTH - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y + TRIANGLE_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 3)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y - TRIANGLE_HEIGHT + HEX_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else if (k == 4)
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + HEX_WIDTH - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y - TRIANGLE_HEIGHT + HEX_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                else
                                {
                                    XLoc = current.getHex().FindComponent<Transform2D>().X + (HEX_WIDTH / 2) - (SETTLEMENT_WIDTH / 2);
                                    YLoc = current.getHex().FindComponent<Transform2D>().Y + HEX_HEIGHT - (SETTLEMENT_HEIGHT / 2);
                                }
                                currSettle.getSettlement().AddComponent(new Transform2D()
                                {
                                    Scale = new Vector2(SETTLEMENT_SCALE_X, SETTLEMENT_SCALE_Y),
                                    X = XLoc,
                                    Y = YLoc,
                                    DrawOrder = .05f
                                });
                                currSettle.canAddComponent = false;
                                Console.WriteLine(currSettle.getPlacementNumber());
                                Entity e = currSettle.getSettlement();
                                toAdd.Add(e);
                            }
                        }
                    }
                    toAdd.Add(current.getRollEntity());
                    toAdd.Add(current.getHex());
                }
            }
        }

        private void button_Pressed(object sender, GestureEventArgs e)
        {
            CommunicationClient.Instance.sendToServer(new Message("", Translation.TYPE.RegenerateBoard).toJson());
        }

        protected override void Start()
        {
            base.Start();

            // This method is called after the CreateScene and Initialize methods and before the first Update.
        }
    }
}
