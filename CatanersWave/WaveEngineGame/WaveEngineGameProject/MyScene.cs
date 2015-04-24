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
        protected override void CreateScene()
        {
            //Insert your scene definition here.

            //Create a 3D camera
            float WIDTH_TO_HEIGHT = WaveServices.Platform.ScreenWidth / WaveServices.Platform.ScreenHeight;
            float HEX_WIDTH = (WaveServices.Platform.ScreenWidth / 25) / WIDTH_TO_HEIGHT;
            float HEX_SCALE_X = HEX_WIDTH / 220;
            float HEX_SCALE_Y = HEX_WIDTH * ((float)1.1681818181) / 257;
            float HEX_HEIGHT = (HEX_WIDTH * (float)1.168181818);
            float TRIANGLE_HEIGHT = HEX_HEIGHT * (float).2723735409;
            float HEX_START_X = 0;
            float HEX_START_Y = 0;
            var camera2D = new FixedCamera2D("Camera2D") { BackgroundColor = Color.Black };
            EntityManager.Add(camera2D);
            this.hexList = new Entity[19];
            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                String name = "SheepHex" + count.ToString();
                Entity tempEntity = new Entity(name)
                .AddComponent(new Sprite("SheepHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                this.hexList[count] = tempEntity;
                count++;
            }

            for (int j = 0; j < 5; j++)
            {
                String name = "ForestHex" + count.ToString();
                Entity tempEntity2 = new Entity(name)
                .AddComponent(new Sprite("ForestHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                this.hexList[count] = tempEntity2;
                count++;
            }

            for (int k = 0; k < 4; k++)
            {
                String name = "OreHex" + count.ToString();
                Entity tempEntity3 = new Entity(name)
                .AddComponent(new Sprite("OreHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
                this.hexList[count] = tempEntity3;
                count++;
            }

            for (int i = 0; i < 9; i++)
            {
                this.hexList[i].AddComponent(new Transform2D()
                {
                    Scale = new Vector2(HEX_SCALE_X, HEX_SCALE_Y),
                    X = HEX_START_X + (HEX_WIDTH * i),
                    Y = HEX_START_Y + (HEX_HEIGHT)
                });
                EntityManager.Add(this.hexList[i]);
            }
        }

        protected override void Start()
        {
            base.Start();

            // This method is called after the CreateScene and Initialize methods and before the first Update.
        }
    }
}
