using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveEngine;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Managers;

namespace CatanersShared
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
        public int getPlacementNumber()
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

        public int getPlacementNumber()
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
        public int[] toShadow()
        {
            return new int[0];
            //return new int[] { this.type, this.placementNumber, this.rollNumber, this.settlementList[0].getPlacementNumber(), this.settlementList[1].getPlacementNumber(), this.settlementList[2].getPlacementNumber(), this.settlementList[3].getPlacementNumber(), this.settlementList[4].getPlacementNumber(), this.settlementList[5].getPlacementNumber() };
        }
    }

    public class LogicCenter
    {
        private int hexNumber;
        private HexHolder[] hexList;
        private SettlementHolder[] settlementList;
        public LogicCenter(int hexNumber)
        {
            this.hexNumber = hexNumber;
            this.hexList = new HexHolder[this.hexNumber];
            this.generateHexList();
            this.generateDefaultSettlements();
            this.assignSettlements();
        }

        public SettlementHolder[] getSettlementList()
        {
            return this.settlementList;
        }

        public void generateHexList ()
        {
            System.Random r = new System.Random();
            ArrayList rangeList = new ArrayList();
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                String name = "SheepHex" + count.ToString();
                Entity tempEntity = new Entity(name)
                .AddComponent(new Sprite("SheepHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                this.hexList[count] = new HexHolder(tempEntity, 1);
                count++;
            }

            for (int j = 0; j < 4; j++)
            {
                String name = "ForestHex" + count.ToString();
                Entity tempEntity2 = new Entity(name)
                .AddComponent(new Sprite("ForestHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                this.hexList[count] = new HexHolder(tempEntity2, 2);
                count++;
            }

            for (int k = 0; k < 3; k++)
            {
                String name = "OreHex" + count.ToString();
                Entity tempEntity3 = new Entity(name)
                .AddComponent(new Sprite("OreHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                this.hexList[count] = new HexHolder(tempEntity3, 3);
                count++;
            }

            Entity tempEntity4 = new Entity("DesertHex")
            .AddComponent(new Sprite("DesertHex.wpk"))
            .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
            this.hexList[count] = new HexHolder(tempEntity4, 4);
            count++;

            for (int p = 0; p < 3; p++)
            {
                String name = "BrickHex" + count.ToString();
                Entity tempEntity5 = new Entity(name)
                .AddComponent(new Sprite("BrickHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                this.hexList[count] = new HexHolder(tempEntity5, 5);
                count++;
            }

            for (int u = 0; u < 4; u++)
            {
                String name = "WheatHex" + count.ToString();
                Entity tempEntity6 = new Entity(name)
                .AddComponent(new Sprite("WheatHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                this.hexList[count] = new HexHolder(tempEntity6, 6);
                count++;
            }

            rangeList.AddRange(new Object[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 });
            for (int g = 0; g < 19; g++)
            {
                int rInt = r.Next(0, rangeList.Count);
                int nextIndex = (int) rangeList[rInt];
                rangeList.RemoveAt(rInt);
                this.hexList[g].setPlacementNumber(nextIndex);
            }
            this.assignRollNumbers();
            this.assignRollEntities();
        }

        public void generateDefaultSettlements()
        {
            this.settlementList = new SettlementHolder[54];
            for (int i = 0; i < 54; i++)
            {
                Entity tempEnt = new Entity()
                    .AddComponent(new Sprite("Settlement.wpk"))
                    .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                this.settlementList[i] = new SettlementHolder(tempEnt, i);
            }
        }

        public void assignSettlements()
        {
            for (int i = 0; i < 19; i++)
            {
                HexHolder currHex = this.hexList[i];
                int currNum = currHex.getPlacementNumber();
                if(currNum < 3)
                {
                    int a = currNum;
                    int b = a + 3;
                    int c = b + 4;
                    int d = c + 5;
                    SettlementHolder[] newArray = new SettlementHolder[6] {this.settlementList[a], this.settlementList[b], this.settlementList[b + 1], this.settlementList[c], this.settlementList[c + 1], this.settlementList[d]};
                    hexList[i].setSettlementList(newArray);
                }
                else if (currNum < 7)
                {
                    int a = currNum + 4;
                    int b = a + 4;
                    int c = b + 5;
                    int d = c + 6;
                    SettlementHolder[] newArray = new SettlementHolder[6] { this.settlementList[a], this.settlementList[b], this.settlementList[b + 1], this.settlementList[c], this.settlementList[c + 1], this.settlementList[d] };
                    hexList[i].setSettlementList(newArray);
                }
                else if (currNum < 12)
                {
                    int a = currNum + 9;
                    int b = a + 5;
                    int c = b + 6;
                    int d = c + 6;
                    SettlementHolder[] newArray = new SettlementHolder[6] { this.settlementList[a], this.settlementList[b], this.settlementList[b + 1], this.settlementList[c], this.settlementList[c + 1], this.settlementList[d] };
                    hexList[i].setSettlementList(newArray);
                }
                else if (currHex.getPlacementNumber() < 16)
                {
                    int a = currNum + 16;
                    int b = a + 5;
                    int c = b + 5;
                    int d = c + 5;
                    SettlementHolder[] newArray = new SettlementHolder[6] { this.settlementList[a], this.settlementList[b], this.settlementList[b + 1], this.settlementList[c], this.settlementList[c + 1], this.settlementList[d] };
                    hexList[i].setSettlementList(newArray);
                }
                else
                {
                    int a = currNum + 23;
                    int b = a + 4;
                    int c = b + 4;
                    int d = c + 4;
                    SettlementHolder[] newArray = new SettlementHolder[6] { this.settlementList[a], this.settlementList[b], this.settlementList[b + 1], this.settlementList[c], this.settlementList[c + 1], this.settlementList[d] };
                    hexList[i].setSettlementList(newArray);
                }
            }
        }

        public HexHolder[] getHexList()
        {
            return this.hexList;
        }

        public void assignRollNumbers()
        {
            System.Random r = new System.Random();
            ArrayList rollList = new ArrayList();
            rollList.AddRange(new int[] {2, 3, 3, 4, 4, 5, 5, 6, 6, 8, 8, 9, 9, 10, 10, 11, 11, 12});
            for (int k = 0; k < 19; k++)
            {
                if (hexList[k].getHex().Name != "DesertHex")
                {
                    int rInt = r.Next(0, rollList.Count);
                    int nextIndex = (int) rollList[rInt];
                    rollList.RemoveAt(rInt);
                    this.hexList[k].setRollNumber(nextIndex);
                }
            }
        }

        public void assignRollEntities()
        {
            for (int k = 0; k < 19; k++)
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