using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;
using WaveEngine;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Managers;

namespace Cataners
{
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
        private static int hexNumber = 19;
        private HexHolder[] hexList;
        private SettlementHolder[] settlementList;
        public LocalConversion()
        {
            this.hexList = new HexHolder[hexNumber];
            this.settlementList = new SettlementHolder[54];
        }

        public SettlementHolder[] getSettlementList()
        {
            return this.settlementList;
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
            int settlementCount = 0;
            for (int i = 0; i < inputArray.Length; i++)
            {
                int[] currHexRep = inputArray[i];
                for (int k = 0; k < currHexRep.Length; k++)
                {
                    Entity tempEntity;
                    if(currHexRep[0] == 1)
                    {
                        String name = "Forrest" + forrestCount.ToString();
                        tempEntity = new Entity(name)
                        .AddComponent(new Sprite("Forrest.wpk"))
                        .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                        forrestCount++;
                    }
                    else if (currHexRep[0] == 2)
                    {
                        String name = "Desert" + desertCount.ToString();
                        tempEntity = new Entity(name)
                        .AddComponent(new Sprite("DesetHex.wpk"))
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
                    else
                    {
                        String name = "Wheat" + wheatCount.ToString();
                        tempEntity = new Entity(name)
                        .AddComponent(new Sprite("WheatHex.wpk"))
                        .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                        sheepCount++;
                    }
                    this.hexList[i] = new HexHolder(tempEntity, 6);
                    this.hexList[i].setPlacementNumber(currHexRep[1]);
                    this.hexList[i].setRollNumber(currHexRep[2]);
                    SettlementHolder[] newArray = new SettlementHolder[6];
                    for (int h = 0; h < 5; h++)
                    {
                        Entity tempEnt = new Entity()
                            .AddComponent(new Sprite("Settlement.wpk"))
                            .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                        SettlementHolder tempHolder = new SettlementHolder(tempEnt, currHexRep[h + 3]);
                        newArray[h] = tempHolder;
                        this.settlementList[settlementCount] = tempHolder;
                        settlementCount++;
                    }
                }
            }
            this.assignRollEntities();
        }

        public void sendGameMessage(String message)
        {
            CommunicationClient.Instance.sendToServer(new Message("", Translation.TYPE.RegenerateBoard).toJson());
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
    }
}