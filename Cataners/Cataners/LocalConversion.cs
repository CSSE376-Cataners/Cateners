using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;
using WaveEngine;
using WaveEngine.Common.Math;
using WaveEngine.Components.Animation;
using WaveEngine.Components.Gestures;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Managers;
using WaveEngine.Framework.Physics2D;

namespace Cataners
{

    public class HexTypeException : ArgumentOutOfRangeException
    {
        public HexTypeException(string argument, string message) : base(argument, message)
        {       
        }
    }

    public class SettlementHolder
    {
        private Entity settlement;
        private int placementNumber;
        public Boolean canAddComponent;
        public SettlementHolder(Entity settlement, int placementNumber)
        {
            this.settlement = settlement;
            this.placementNumber = placementNumber;
            this.canAddComponent = true;
        }
        public virtual int getPlacementNumber()
        {
            return this.placementNumber;
        }
        public Entity getSettlement()
        {
            return this.settlement;
        }

        public string getName()
        {
            return this.settlement.Name;
        }
    }

    public class HexHolder
    {
        private Entity hex;
        private int placementNumber;
        private int rollNumber;
        private Entity rollEntity;
        private SettlementHolder[] settlementList;
        private int type;

        public HexHolder(Entity hex, int type)
        {
            this.hex = hex;
            this.type = type;
            this.placementNumber = 0;
            this.rollNumber = 0;
            this.settlementList = new SettlementHolder[6];
        }
        public string getName()
        {
            return this.hex.Name;
        }

        public SettlementHolder[] getSettlementList()
        {
            return this.settlementList;
        }

        public void setPlacementNumber(int num)
        {
            this.placementNumber = num;
        }

        public virtual int getPlacementNumber()
        {
            return this.placementNumber;
        }

        public Entity getHex()
        {
            return this.hex;
        }

        public int getRollNumber()
        {
            return rollNumber;
        }

        public void setRollNumber(int rollNum)
        {
            this.rollNumber = rollNum;
        }

        public void setRollEntity(Entity rollEnt)
        {
            this.rollEntity = rollEnt;
        }
        public Entity getRollEntity()
        {
            return this.rollEntity;
        }

        public void setSettlementList(SettlementHolder[] newArray)
        {
            this.settlementList = newArray;
        }
    }

    public class LocalConversion
    {
        private static LocalConversion instance;
        public static LocalConversion Instance
        {
            get
            {
                return instance;
            }
        }
        private static int hexNumber = 19;
        private HexHolder[] hexList;
        private SettlementHolder[] settlementList;
        public LocalConversion()
        {
            instance = this;
            this.hexList = new HexHolder[hexNumber];
            this.generateSettlementList();
        }

        public SettlementHolder[] getSettlementList()
        {
            return this.settlementList;
        }

        public void generateSettlementList()
        {
            this.settlementList = new SettlementHolder[54];
            for (int i = 0; i < this.settlementList.Length; i++)
            {
                Entity tempEnt = new Entity("Settlement" + i.ToString())
                    .AddComponent(new Sprite("Settlement.wpk"))
                    .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                this.settlementList[i] = new SettlementHolder(tempEnt, i);
            }
        }

        public void generateHexList(int[][] inputArray)
        {
            this.hexList = new HexHolder[inputArray.Length];
            int forrestCount = 0;
            int brickCount = 0;
            int desertCount = 0;
            int oreCount = 0;
            int sheepCount = 0;
            int wheatCount = 0;

            for (int i = 0; i < inputArray.Length; i++)
            {
                int[] currHexRep = inputArray[i];
                Entity tempEntity;
                if (currHexRep[0] == 1)
                {
                    String name = "Forrest" + forrestCount.ToString();
                    tempEntity = new Entity(name)
                    .AddComponent(new Sprite("ForrestHex.wpk"))
                    .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                    forrestCount++;
                }
                else if (currHexRep[0] == 2)
                {
                    String name = "Desert" + desertCount.ToString();
                    tempEntity = new Entity(name)
                    .AddComponent(new Sprite("DesertHex.wpk"))
                    .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                    desertCount++;
                }
                else if (currHexRep[0] == 3)
                {
                    String name = "Ore" + oreCount.ToString();
                    tempEntity = new Entity(name)
                    .AddComponent(new Sprite("OreHex.wpk"))
                    .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                    oreCount++;
                }
                else if (currHexRep[0] == 4)
                {
                    String name = "Brick" + brickCount.ToString();
                    tempEntity = new Entity(name)
                    .AddComponent(new Sprite("BrickHex.wpk"))
                    .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                    brickCount++;
                }
                else if (currHexRep[0] == 5)
                {
                    String name = "Sheep" + sheepCount.ToString();
                    tempEntity = new Entity(name)
                    .AddComponent(new Sprite("SheepHex.wpk"))
                    .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                    sheepCount++;
                }
                else if (currHexRep[0] == 6)
                {
                    String name = "Wheat" + wheatCount.ToString();
                    tempEntity = new Entity(name)
                    .AddComponent(new Sprite("WheatHex.wpk"))
                    .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                    wheatCount++;
                }
                else
                {
                    throw new HexTypeException("typeName", "Hex Type Number Out of Range");
                }
                this.hexList[i] = new HexHolder(tempEntity, currHexRep[0]);
                this.hexList[i].setPlacementNumber(currHexRep[1]);
                this.hexList[i].setRollNumber(currHexRep[2]);
                SettlementHolder[] newArray = new SettlementHolder[6];
                int settlementNumber = 0;
                for (int k = 3; k < currHexRep.Length; k++)
                {
                    newArray[settlementNumber] = this.settlementList[currHexRep[k]];
                    settlementNumber++;
                }
                this.hexList[i].setSettlementList(newArray);
            }
            this.assignRollEntities();
        }

        public HexHolder[] getHexList()
        {
            return this.hexList;
        }

        public void assignRollEntities()
        {
            for (int k = 0; k < this.hexList.Length; k++)
            {
                HexHolder hexFocus = this.hexList[k];
                String name = hexFocus.getHex().Name + hexFocus.getRollNumber().ToString();
                Entity rollEntity = new Entity(name)
                .AddComponent(new Sprite("RollNum" + hexFocus.getRollNumber().ToString() + ".wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                hexFocus.setRollEntity(rollEntity);
            }
        }

        public void setAsPurchasedSettle(int placement)
        {
            Console.WriteLine("settle");
            string name = this.settlementList[placement].getName();
            MyScene.Instance.setAsPurchasedSettle(name);
        }

        public void hexAddTransform(HexHolder current, float xOffset, float yOffset, float drawOrder)
        {
            current.getHex().AddComponent(new Transform2D()
            {
                Scale = new Vector2(WaveConstants.HEX_SCALE_X, WaveConstants.HEX_SCALE_Y),
                X = WaveConstants.HEX_START_X + xOffset,
                Y = WaveConstants.HEX_START_Y + yOffset,
                DrawOrder = drawOrder
            });
        }

        public void rollEntityAddTransform(HexHolder current, float xOffset, float yOffset, float drawOrder)
        {
            current.getRollEntity().AddComponent(new Transform2D()
            {
                Scale = new Vector2(WaveConstants.ROLL_NUMBER_SCALE, WaveConstants.ROLL_NUMBER_SCALE),
                X = WaveConstants.HEX_START_X + xOffset,
                Y = WaveConstants.HEX_START_Y + yOffset,
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
                        Scale = new Vector2(WaveConstants.SETTLEMENT_SCALE_X, WaveConstants.SETTLEMENT_SCALE_Y),
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
                    MyScene.toAdd.Add(e);
                }
            }
        }

        public float[] getXLocArraySettlement(HexHolder current)
        {
            float defX = current.getHex().FindComponent<Transform2D>().X;
            float[] XLocArray = new float[6] { defX + (WaveConstants.HEX_WIDTH / 2) - (WaveConstants.SETTLEMENT_WIDTH / 2),
                                               defX - (WaveConstants.SETTLEMENT_WIDTH / 2),
                                               defX + WaveConstants.HEX_WIDTH - (WaveConstants.SETTLEMENT_WIDTH / 2),
                                               defX - (WaveConstants.SETTLEMENT_WIDTH / 2),
                                               defX + WaveConstants.HEX_WIDTH - (WaveConstants.SETTLEMENT_WIDTH / 2),
                                               defX + (WaveConstants.HEX_WIDTH / 2) - (WaveConstants.SETTLEMENT_WIDTH / 2) };
            return XLocArray;
        }

        public float[] getYLocArraySettlement(HexHolder current)
        {
            float defY = current.getHex().FindComponent<Transform2D>().Y;
            float[] YLocArray = new float[6] { defY - (WaveConstants.SETTLEMENT_HEIGHT / 2),
                                               defY + WaveConstants.TRIANGLE_HEIGHT - (WaveConstants.SETTLEMENT_HEIGHT / 2),
                                               defY + WaveConstants.TRIANGLE_HEIGHT - (WaveConstants.SETTLEMENT_HEIGHT / 2),
                                               defY + WaveConstants.HEX_HEIGHT - WaveConstants.TRIANGLE_HEIGHT - (WaveConstants.SETTLEMENT_HEIGHT / 2),
                                               defY + WaveConstants.HEX_HEIGHT - WaveConstants.TRIANGLE_HEIGHT - (WaveConstants.SETTLEMENT_HEIGHT / 2),
                                               defY + WaveConstants.HEX_HEIGHT - (WaveConstants.SETTLEMENT_HEIGHT / 2) };
            return YLocArray;
        }

        public void drawHexes()
        {
            this.hexList = LocalConversion.Instance.getHexList();
            Console.WriteLine(this.hexList.ToString());
            if(MyScene.Instance != null){
                lock (MyScene.toAdd)
                {
                    for (int g = 0; g < 19; g++)
                    {
                        HexHolder current = this.hexList[g];
                        int posNum = current.getPlacementNumber();
                        if (posNum < 3)
                        {
                            this.hexAddTransform(current, (WaveConstants.HEX_WIDTH * posNum), 0, .6f);
                            this.rollEntityAddTransform(current, (WaveConstants.HEX_WIDTH * posNum) + (WaveConstants.HEX_WIDTH / 2) - (WaveConstants.ROLL_NUMBER_WIDTH / 2),
                                                                    (WaveConstants.HEX_HEIGHT / 2) - (WaveConstants.ROLL_NUMBER_HEIGHT / 2), .1f);
                            this.settlementAssignment(current, getXLocArraySettlement(current), getYLocArraySettlement(current), .05f);
                        }
                        else if (posNum < 7)
                        {
                            this.hexAddTransform(current, (-WaveConstants.HEX_WIDTH / 2) + (WaveConstants.HEX_WIDTH * (posNum - 3)), (WaveConstants.HEX_HEIGHT - WaveConstants.TRIANGLE_HEIGHT), .6f);
                            this.rollEntityAddTransform(current, (-WaveConstants.HEX_WIDTH / 2) + (WaveConstants.HEX_WIDTH * (posNum - 3)) + (WaveConstants.HEX_WIDTH / 2) - (WaveConstants.ROLL_NUMBER_WIDTH / 2),
                                                                    (WaveConstants.HEX_HEIGHT - WaveConstants.TRIANGLE_HEIGHT) + (WaveConstants.HEX_HEIGHT / 2) - (WaveConstants.ROLL_NUMBER_HEIGHT / 2), .1f);
                            this.settlementAssignment(current, this.getXLocArraySettlement(current), this.getYLocArraySettlement(current), .05f);
                        }
                        else if (posNum < 12)
                        {
                            this.hexAddTransform(current, (-WaveConstants.HEX_WIDTH) + (WaveConstants.HEX_WIDTH * (posNum - 7)), (2 * WaveConstants.HEX_HEIGHT) - (2 * WaveConstants.TRIANGLE_HEIGHT), .6f);
                            this.rollEntityAddTransform(current, (-WaveConstants.HEX_WIDTH) + (WaveConstants.HEX_WIDTH * (posNum - 7)) + (WaveConstants.HEX_WIDTH / 2) - (WaveConstants.ROLL_NUMBER_WIDTH / 2),
                                                                   (2 * WaveConstants.HEX_HEIGHT) - (2 * WaveConstants.TRIANGLE_HEIGHT) + (WaveConstants.HEX_HEIGHT / 2) - (WaveConstants.ROLL_NUMBER_HEIGHT / 2), .1f);
                            this.settlementAssignment(current, this.getXLocArraySettlement(current), this.getYLocArraySettlement(current), .05f);
                        }
                        else if (posNum < 16)
                        {
                            this.hexAddTransform(current, (-WaveConstants.HEX_WIDTH / 2) + (WaveConstants.HEX_WIDTH * (posNum - 12)), (3 * WaveConstants.HEX_HEIGHT) - (3 * WaveConstants.TRIANGLE_HEIGHT), .6f);
                            this.rollEntityAddTransform(current, (-WaveConstants.HEX_WIDTH / 2) + (WaveConstants.HEX_WIDTH * (posNum - 12)) + (WaveConstants.HEX_WIDTH / 2) - (WaveConstants.ROLL_NUMBER_WIDTH / 2), (3 * WaveConstants.HEX_HEIGHT) - (3 * WaveConstants.TRIANGLE_HEIGHT) + (WaveConstants.HEX_HEIGHT / 2) - (WaveConstants.ROLL_NUMBER_HEIGHT / 2), .1f);
                            this.settlementAssignment(current, this.getXLocArraySettlement(current), this.getYLocArraySettlement(current), .05f);
                        }
                        else
                        {
                            this.hexAddTransform(current, (WaveConstants.HEX_WIDTH * (posNum - 16)), (4 * WaveConstants.HEX_HEIGHT) - (4 * WaveConstants.TRIANGLE_HEIGHT), .6f);
                            this.rollEntityAddTransform(current, (WaveConstants.HEX_WIDTH * (posNum - 16)) + (WaveConstants.HEX_WIDTH / 2) - (WaveConstants.ROLL_NUMBER_WIDTH / 2),
                                                                    (4 * WaveConstants.HEX_HEIGHT) - (4 * WaveConstants.TRIANGLE_HEIGHT) + (WaveConstants.HEX_HEIGHT / 2) - (WaveConstants.ROLL_NUMBER_HEIGHT / 2), .1f);
                            this.settlementAssignment(current, this.getXLocArraySettlement(current), this.getYLocArraySettlement(current), .05f);
                        }
                        MyScene.toAdd.Add(current.getRollEntity());
                        MyScene.toAdd.Add(current.getHex());
                    }
                }
            }
        }
    }
}