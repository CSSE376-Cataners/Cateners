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
        }

        public static void addTradeButton()
        {
            //add tradebutton
            Button tradeButton = new Button();
            tradeButton.Text = "Trade Resources";
            tradeButton.Width = 120;
            tradeButton.Height = 40;
            Transform2D tradebutton2d = tradeButton.Entity.FindComponent<Transform2D>();
            tradebutton2d.X = CENTERWIDTH * 2 - 120;
            tradebutton2d.Y = CENTERHEIGHT * 2 - 100;
            tradeButton.Entity.FindComponent<TouchGestures>().TouchPressed += new EventHandler<GestureEventArgs>(tradeButton_Pressed);
            lock (toAddDecor)
            {
                toAddDecor.Add(tradeButton);
            }
            

        }

        public static void addRegenerateBoardButton()
        {
            //add regenerate board if owner
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
        }

        public void addResources()
        {
            StringBuilder sb = new StringBuilder();
            if (Data.currentLobby is GameLobby)
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
                                    X = CENTERWIDTH +50 - WORDOFFSET,
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
                                    X = CENTERWIDTH + 450 - WORDOFFSET,
                                    Y = CENTERHEIGHT + 30,
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
                                    X = CENTERWIDTH +50 - WORDOFFSET,
                                    Y = (CENTERHEIGHT * 2) - 80,
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
                                    X = CENTERWIDTH - 450 - WORDOFFSET,
                                    Y = CENTERHEIGHT+30,
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

        public void hexAddTransform(HexHolder current, float xOffset, float yOffset, float drawOrder)
        {
            current.getHex().AddComponent(new Transform2D()
            {
                Scale = new Vector2(HEX_SCALE_X, HEX_SCALE_Y),
                X = HEX_START_X + xOffset,
                Y = HEX_START_Y + yOffset,
                DrawOrder = drawOrder
            });
        }

        public void rollEntityAddTransform(HexHolder current, float xOffset, float yOffset, float drawOrder)
        {
            current.getRollEntity().AddComponent(new Transform2D()
            {
                Scale = new Vector2(ROLL_NUMBER_SCALE, ROLL_NUMBER_SCALE),
                X = HEX_START_X + xOffset,
                Y = HEX_START_Y + yOffset,
                DrawOrder = drawOrder
            });
        }

        public void settlementAssignment(HexHolder current, float[] XLocArray, float[] YLocArray, float drawOrder)
        {
            for (int k = 0; k < 6; k++)
            {
                SettlementHolder currSettle = current.getSettlementList()[k];
                Entity currEnt = currSettle.getSettlement();
                int placementNumber = currSettle.getPlacementNumber();
                if (currSettle.canAddComponent)
                {
                    currEnt.AddComponent(new Transform2D()
                    {
                        Scale = new Vector2(SETTLEMENT_SCALE_X, SETTLEMENT_SCALE_Y),
                        X = XLocArray[k],
                        Y = YLocArray[k],
                        DrawOrder = drawOrder
                    });
                    currEnt.AddComponent(new RectangleCollider());
                    currEnt.AddComponent(new TouchGestures(true));
                    currEnt.FindComponent<TouchGestures>().TouchPressed += (sender, GestureEventArgs) =>
                    {
                        Console.WriteLine(placementNumber);
                        CommunicationClient.Instance.sendToServer(new Message(placementNumber.ToString(), Translation.TYPE.BuySettlement).toJson());
                    };
                    currSettle.canAddComponent = false;
                    Entity e = currEnt;
                    toAdd.Add(e);
                }
            }
        }

        public float[] getXLocArraySettlement(HexHolder current)
        {
            float defX = current.getHex().FindComponent<Transform2D>().X;
            float[] XLocArray = new float[6] { defX + (HEX_WIDTH / 2) - (SETTLEMENT_WIDTH / 2),
                                               defX - (SETTLEMENT_WIDTH / 2),
                                               defX + HEX_WIDTH - (SETTLEMENT_WIDTH / 2),
                                               defX - (SETTLEMENT_WIDTH / 2),
                                               defX + HEX_WIDTH - (SETTLEMENT_WIDTH / 2),
                                               defX + (HEX_WIDTH / 2) - (SETTLEMENT_WIDTH / 2) };
            return XLocArray;
        }

        public float[] getYLocArraySettlement(HexHolder current)
        {
            float defY = current.getHex().FindComponent<Transform2D>().Y;
            float[] YLocArray = new float[6] { defY - (SETTLEMENT_HEIGHT / 2),
                                               defY + TRIANGLE_HEIGHT - (SETTLEMENT_HEIGHT / 2),
                                               defY + TRIANGLE_HEIGHT - (SETTLEMENT_HEIGHT / 2),
                                               defY + HEX_HEIGHT - TRIANGLE_HEIGHT - (SETTLEMENT_HEIGHT / 2),
                                               defY + HEX_HEIGHT - TRIANGLE_HEIGHT - (SETTLEMENT_HEIGHT / 2),
                                               defY + HEX_HEIGHT - (SETTLEMENT_HEIGHT / 2) };
            return YLocArray;
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
                        this.hexAddTransform(current, (HEX_WIDTH * posNum), 0, .6f);
                        this.rollEntityAddTransform(current, (HEX_WIDTH * posNum) + (HEX_WIDTH / 2) - (ROLL_NUMBER_WIDTH / 2),
                                                                (HEX_HEIGHT / 2) - (ROLL_NUMBER_HEIGHT / 2), .1f);
                        this.settlementAssignment(current, this.getXLocArraySettlement(current), this.getYLocArraySettlement(current), .05f);
                    }
                    else if (posNum < 7)
                    {
                        this.hexAddTransform(current, (-HEX_WIDTH / 2) + (HEX_WIDTH * (posNum - 3)), (HEX_HEIGHT - TRIANGLE_HEIGHT), .6f);
                        this.rollEntityAddTransform(current, (-HEX_WIDTH / 2) + (HEX_WIDTH * (posNum - 3)) + (HEX_WIDTH / 2) - (ROLL_NUMBER_WIDTH / 2), 
                                                                (HEX_HEIGHT - TRIANGLE_HEIGHT) + (HEX_HEIGHT / 2) - (ROLL_NUMBER_HEIGHT / 2), .1f);
                        this.settlementAssignment(current, this.getXLocArraySettlement(current), this.getYLocArraySettlement(current), .05f);
                    }
                    else if (posNum < 12)
                    {
                        this.hexAddTransform(current, (-HEX_WIDTH) + (HEX_WIDTH * (posNum - 7)), (2 * HEX_HEIGHT) - (2 * TRIANGLE_HEIGHT), .6f);
                        this.rollEntityAddTransform(current, (-HEX_WIDTH) + (HEX_WIDTH * (posNum - 7)) + (HEX_WIDTH / 2) - (ROLL_NUMBER_WIDTH / 2), 
                                                               (2 * HEX_HEIGHT) - (2 * TRIANGLE_HEIGHT) + (HEX_HEIGHT / 2) - (ROLL_NUMBER_HEIGHT / 2), .1f);
                        this.settlementAssignment(current, this.getXLocArraySettlement(current), this.getYLocArraySettlement(current), .05f);
                    }
                    else if (posNum < 16)
                    {
                        this.hexAddTransform(current, (-HEX_WIDTH / 2) + (HEX_WIDTH * (posNum - 12)), (3 * HEX_HEIGHT) - (3 * TRIANGLE_HEIGHT), .6f);
                        this.rollEntityAddTransform(current, (-HEX_WIDTH / 2) + (HEX_WIDTH * (posNum - 12)) + (HEX_WIDTH / 2) - (ROLL_NUMBER_WIDTH / 2), (3 * HEX_HEIGHT) - (3 * TRIANGLE_HEIGHT) + (HEX_HEIGHT / 2) - (ROLL_NUMBER_HEIGHT / 2), .1f);
                        this.settlementAssignment(current, this.getXLocArraySettlement(current), this.getYLocArraySettlement(current), .05f);
                    }
                    else
                    {
                        this.hexAddTransform(current, (HEX_WIDTH * (posNum - 16)), (4 * HEX_HEIGHT) - (4 * TRIANGLE_HEIGHT), .6f);
                        this.rollEntityAddTransform(current, (HEX_WIDTH * (posNum - 16)) + (HEX_WIDTH / 2) - (ROLL_NUMBER_WIDTH / 2),
                                                                (4 * HEX_HEIGHT) - (4 * TRIANGLE_HEIGHT) + (HEX_HEIGHT / 2) - (ROLL_NUMBER_HEIGHT / 2), .1f);
                        this.settlementAssignment(current, this.getXLocArraySettlement(current), this.getYLocArraySettlement(current), .05f);
                    }
                    toAdd.Add(current.getRollEntity());
                    toAdd.Add(current.getHex());
                }
            }
        }

        private static void button_Pressed(object sender, GestureEventArgs e)
        {
            CommunicationClient.Instance.sendToServer(new Message("", Translation.TYPE.RegenerateBoard).toJson());
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
            this.EntityManager.Find(name).RemoveComponent<Sprite>();
            this.EntityManager.Find(name).AddComponent(new Sprite("SettlementBlue.wpk"));
        }
    }
}
