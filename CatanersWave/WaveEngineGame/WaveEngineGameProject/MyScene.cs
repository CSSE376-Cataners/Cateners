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
#endregion

namespace WaveEngineGameProject
{
    public class MyScene : Scene
    {
        private Entity[] hexList;
        public static float WIDTH_TO_HEIGHT = ((float) WaveServices.Platform.ScreenWidth) / ((float) WaveServices.Platform.ScreenHeight);
        public static float HEX_WIDTH = (((float) WaveServices.Platform.ScreenWidth) / 8.0f) / WIDTH_TO_HEIGHT;
        public static float HEX_SCALE_X = HEX_WIDTH / 220.0f;
        public static float HEX_SCALE_Y = HEX_WIDTH * ((float) 1.1681818181) / 257.0f;
        public static float HEX_HEIGHT = (HEX_WIDTH * (float) 1.168181818);
        public static float TRIANGLE_HEIGHT = HEX_HEIGHT * (float) 0.2723735409;

        protected override void CreateScene()
        {
            //Insert your scene definition here.

            //Create a 3D camera
            float HEX_START_X = (((float) WaveServices.Platform.ScreenWidth) / 2.0f) - ((HEX_WIDTH * 3) / 2);
            float HEX_START_Y = (((float)WaveServices.Platform.ScreenHeight) / 2.0f) - ((3 * HEX_HEIGHT) - (4 * TRIANGLE_HEIGHT));
            var camera2D = new FixedCamera2D("Camera2D") { BackgroundColor = Color.Black };
            EntityManager.Add(camera2D);
            this.hexList = new Entity[19];
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                String name = "SheepHex" + count.ToString();
                Entity tempEntity = new Entity(name)
                .AddComponent(new Sprite("SheepHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                this.hexList[count] = tempEntity;
                count++;
            }

            for (int j = 0; j < 4; j++)
            {
                String name = "ForestHex" + count.ToString();
                Entity tempEntity2 = new Entity(name)
                .AddComponent(new Sprite("ForestHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                this.hexList[count] = tempEntity2;
                count++;
            }

            for (int k = 0; k < 3; k++)
            {
                String name = "OreHex" + count.ToString();
                Entity tempEntity3 = new Entity(name)
                .AddComponent(new Sprite("OreHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                this.hexList[count] = tempEntity3;
                count++;
            }

            Entity tempEntity4 = new Entity("DesertHex")
            .AddComponent(new Sprite("DesertHex.wpk"))
            .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
            this.hexList[count] = tempEntity4;
            count++;

            for (int p = 0; p < 3; p++)
            {
                String name = "BrickHex" + count.ToString();
                Entity tempEntity5 = new Entity(name)
                .AddComponent(new Sprite("BrickHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                this.hexList[count] = tempEntity5;
                count++;
            }

            for (int u = 0; u < 4; u++)
            {
                String name = "WheatHex" + count.ToString();
                Entity tempEntity6 = new Entity(name)
                .AddComponent(new Sprite("WheatHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                this.hexList[count] = tempEntity6;
                count++;
            }

            int finalCount = 0;
            int finalCount2 = 0;
            System.Random r = new System.Random();
            ArrayList newList = new ArrayList();
            newList.AddRange(new Object[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 });
            for (int g = 0; g < 19; g++)
            {
                int rInt = r.Next(0, newList.Count);
                int nextIndex = (int) newList[rInt];
                newList.RemoveAt(rInt);
                if (finalCount == 3)
                {
                    HEX_START_X = HEX_START_X - (HEX_WIDTH / 2);
                    HEX_START_Y = HEX_START_Y + (HEX_HEIGHT) - (TRIANGLE_HEIGHT);
                    finalCount2 = 0;
                }
                if (finalCount == 7)
                {
                    HEX_START_X = HEX_START_X - (HEX_WIDTH / 2);
                    HEX_START_Y = HEX_START_Y + (HEX_HEIGHT) - (TRIANGLE_HEIGHT);
                    finalCount2 = 0;
                }
                if (finalCount == 12)
                {
                    HEX_START_X = HEX_START_X + (HEX_WIDTH / 2);
                    HEX_START_Y = HEX_START_Y + (HEX_HEIGHT) - (TRIANGLE_HEIGHT);
                    finalCount2 = 0;
                }
                if (finalCount == 16)
                {
                    HEX_START_X = HEX_START_X + (HEX_WIDTH / 2);
                    HEX_START_Y = HEX_START_Y + (HEX_HEIGHT) - (TRIANGLE_HEIGHT);
                    finalCount2 = 0;
                }
                this.hexList[nextIndex].AddComponent(new Transform2D()
                {
                    Scale = new Vector2(HEX_SCALE_X, HEX_SCALE_Y),
                    X = HEX_START_X + (HEX_WIDTH * finalCount2),
                    Y = HEX_START_Y
                });
                EntityManager.Add(this.hexList[nextIndex]);
                finalCount++;
                finalCount2++;
            }
        }

        protected override void Start()
        {
            base.Start();

            // This method is called after the CreateScene and Initialize methods and before the first Update.
        }
    }
}
