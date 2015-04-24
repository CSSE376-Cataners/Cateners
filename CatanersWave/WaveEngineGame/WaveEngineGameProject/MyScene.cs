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
#endregion

namespace WaveEngineGameProject
{
    public class MyScene : Scene
    {
        private HexHolder[] hexList;
        public static int hexNumber = 19;
        public static float WIDTH_TO_HEIGHT = ((float) WaveServices.Platform.ScreenWidth) / ((float) WaveServices.Platform.ScreenHeight);
        public static float HEX_WIDTH = (((float) WaveServices.Platform.ScreenWidth) / 8.0f) / WIDTH_TO_HEIGHT;
        public static float HEX_SCALE_X = HEX_WIDTH / 220.0f;
        public static float HEX_SCALE_Y = HEX_WIDTH * ((float) 1.1681818181) / 257.0f;
        public static float HEX_HEIGHT = (HEX_WIDTH * (float) 1.168181818);
        public static float TRIANGLE_HEIGHT = HEX_HEIGHT * (float) 0.2723735409;
        public static float HEX_START_X = (((float)WaveServices.Platform.ScreenWidth) / 2.0f) - ((HEX_WIDTH * 3) / 2);
        public static float HEX_START_Y = (((float)WaveServices.Platform.ScreenHeight) / 2.0f) - ((3 * HEX_HEIGHT) - (4 * TRIANGLE_HEIGHT));

        protected override void CreateScene()
        {
            //Insert your scene definition here.

            //Create a 3D camera
            //EntityManager.Remove(AlphaLayer);
            Button newButton = new Button(); 
            newButton.Text = "New Button"; 
            newButton.Width = 120; 
            newButton.Height = 40;
            EntityManager.Add(newButton);
            newButton.Entity.FindComponent<TouchGestures>().TouchPressed += new EventHandler<GestureEventArgs>(button_Pressed);
            FixedCamera2D camera2D = new FixedCamera2D("Camera2D") { BackgroundColor = Color.Black };
            EntityManager.Add(camera2D);
            this.drawHexes();
        }

        public void drawHexes()
        {
            LogicCenter logicCenter = new LogicCenter(hexNumber);
            this.hexList = logicCenter.getHexList();
            for (int g = 0; g < 19; g++)
            {
                int posNum = this.hexList[g].getPlacementNumber();
                if (posNum < 3)
                    {
                        this.hexList[g].getHex().AddComponent(new Transform2D()
                        {
                            Scale = new Vector2(HEX_SCALE_X, HEX_SCALE_Y),
                            X = HEX_START_X + (HEX_WIDTH * posNum),
                            Y = HEX_START_Y
                        });
                    }
                else if (posNum < 7)
                    {
                        this.hexList[g].getHex().AddComponent(new Transform2D()
                        {
                            Scale = new Vector2(HEX_SCALE_X, HEX_SCALE_Y),
                            X = HEX_START_X - (HEX_WIDTH / 2) + (HEX_WIDTH * (posNum - 3)),
                            Y = HEX_START_Y + (HEX_HEIGHT - TRIANGLE_HEIGHT)
                        });
                    }
                else if (posNum < 12)
                    {
                        this.hexList[g].getHex().AddComponent(new Transform2D()
                        {
                            Scale = new Vector2(HEX_SCALE_X, HEX_SCALE_Y),
                            X = HEX_START_X - (HEX_WIDTH) + (HEX_WIDTH * (posNum - 7)),
                            Y = HEX_START_Y + (2 * HEX_HEIGHT) - (2 * TRIANGLE_HEIGHT)
                        });
                    }
                else if (posNum < 16)
                    {
                        this.hexList[g].getHex().AddComponent(new Transform2D()
                        {
                            Scale = new Vector2(HEX_SCALE_X, HEX_SCALE_Y),
                            X = HEX_START_X - (HEX_WIDTH / 2) + (HEX_WIDTH * (posNum - 12)),
                            Y = HEX_START_Y + (3 * HEX_HEIGHT) - (3 * TRIANGLE_HEIGHT)
                        });
                    }
                else
                    {
                        this.hexList[g].getHex().AddComponent(new Transform2D()
                        {
                            Scale = new Vector2(HEX_SCALE_X, HEX_SCALE_Y),
                            X = HEX_START_X + (HEX_WIDTH * (posNum - 16)),
                            Y = HEX_START_Y + (4 * HEX_HEIGHT) - (4 * TRIANGLE_HEIGHT)
                        });
                    }
                EntityManager.Add(this.hexList[g].getHex());
            }
        }

        private void button_Pressed(object sender, GestureEventArgs e)
        {
            ScreenContext screenContext = new ScreenContext(new MyScene())
            {
                Name = "Next Frame"
            };
            WaveServices.ScreenContextManager.To(screenContext);
        }

        protected override void Start()
        {
            base.Start();

            // This method is called after the CreateScene and Initialize methods and before the first Update.
        }
    }
}
