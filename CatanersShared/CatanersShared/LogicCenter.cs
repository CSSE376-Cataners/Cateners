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

namespace CatanersShared
{
    public class HexHolder
    {
        private Entity hex;
        private int placementNumber;
        private int rollNumber;

        public HexHolder(Entity hex)
        {
            this.hex = hex;
            this.placementNumber = 0;
            this.rollNumber = 0;
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
    }

    public class LogicCenter
    {
        private int hexNumber;
        private HexHolder[] hexList;
        public LogicCenter(int hexNumber)
        {
            this.hexNumber = hexNumber;
            this.hexList = new HexHolder[this.hexNumber];
            this.generateHexList();
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
                this.hexList[count] = new HexHolder(tempEntity);
                count++;
            }

            for (int j = 0; j < 4; j++)
            {
                String name = "ForestHex" + count.ToString();
                Entity tempEntity2 = new Entity(name)
                .AddComponent(new Sprite("ForestHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                this.hexList[count] = new HexHolder(tempEntity2);
                count++;
            }

            for (int k = 0; k < 3; k++)
            {
                String name = "OreHex" + count.ToString();
                Entity tempEntity3 = new Entity(name)
                .AddComponent(new Sprite("OreHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                this.hexList[count] = new HexHolder(tempEntity3);
                count++;
            }

            Entity tempEntity4 = new Entity("DesertHex")
            .AddComponent(new Sprite("DesertHex.wpk"))
            .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
            this.hexList[count] = new HexHolder(tempEntity4);
            count++;

            for (int p = 0; p < 3; p++)
            {
                String name = "BrickHex" + count.ToString();
                Entity tempEntity5 = new Entity(name)
                .AddComponent(new Sprite("BrickHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                this.hexList[count] = new HexHolder(tempEntity5);
                count++;
            }

            for (int u = 0; u < 4; u++)
            {
                String name = "WheatHex" + count.ToString();
                Entity tempEntity6 = new Entity(name)
                .AddComponent(new Sprite("WheatHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                this.hexList[count] = new HexHolder(tempEntity6);
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
        }

        public HexHolder[] getHexList()
        {
            return this.hexList;
        }

        public void assignRollNumbers()
        {
            for (int k = 0; k < 19; k++)
            {
                this.hexList[k].setRollNumber(2);
            }
        }
    }
}