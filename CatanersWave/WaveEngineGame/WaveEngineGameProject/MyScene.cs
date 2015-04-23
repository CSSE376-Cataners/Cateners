#region Using Statements
using System;
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
        protected override void CreateScene()
        {
            //Insert your scene definition here.

            //Create a 3D camera
            float HEX_WIDTH = WaveServices.Platform.ScreenWidth / 11;
            float HEX_SCALE = HEX_WIDTH / 220;
            float HEX_HEIGHT = WaveServices.Platform.ScreenWidth / 11;
            float TRIANGLE_HEIGHT = HEX_HEIGHT * (float) .2723735409;
            float HEX_START_X = 7 * HEX_WIDTH;
            float HEX_START_Y = 2 * HEX_HEIGHT;
            var camera2D = new FixedCamera2D("Camera2D") { BackgroundColor = Color.Black };
            EntityManager.Add(camera2D);
            var title = new Entity("Title")
                .AddComponent(new Sprite("TheSettlersofCatan.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
                .AddComponent(new Transform2D()
                {
                    Y = 0,
                    X = (WaveServices.Platform.ScreenWidth / 2) - 300
            });
            EntityManager.Add(title);

            var forestHex = new Entity("forestHex")
                .AddComponent(new Sprite("BadHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
                .AddComponent(new Transform2D()
                {
                    Scale = new Vector2(HEX_SCALE, HEX_SCALE),
                    Y = HEX_START_Y,
                    X = HEX_START_X
                    
                });
            EntityManager.Add(forestHex);
            var forestHex2 = new Entity("forestHex2")
                .AddComponent(new Sprite("BadHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
                .AddComponent(new Transform2D()
                {
                    Scale = new Vector2(HEX_SCALE, HEX_SCALE),
                    Y = HEX_START_Y + TRIANGLE_HEIGHT,
                    X = HEX_START_X
                });
            EntityManager.Add(forestHex2);
            var forestHex3 = new Entity("forestHex3")
                .AddComponent(new Sprite("BadHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
                .AddComponent(new RectangleCollider())
                .AddComponent(new TouchGestures())
                .AddComponent(new Transform2D()
                {
                    Scale = new Vector2(HEX_SCALE, HEX_SCALE),
                    Y = HEX_START_Y - (HEX_HEIGHT/2),
                    X = HEX_START_X + (HEX_WIDTH/2)
                });
            forestHex3.FindComponent<TouchGestures>().TouchPressed += new EventHandler<GestureEventArgs>(forestHex3_TouchPressed);
            EntityManager.Add(forestHex3);
        
        }

    
    
        private void forestHex3_TouchPressed(object sender, GestureEventArgs e)
        {
            Console.WriteLine("Hex 3 Touched : Right Hex");
        }

        protected override void Start()
        {
            base.Start();

            // This method is called after the CreateScene and Initialize methods and before the first Update.
        }
    }
}
