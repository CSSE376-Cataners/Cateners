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

            #region Simple test
            //Create a 3D camera
            var camera2D = new FixedCamera2D("Camera2D") { BackgroundColor = Color.Black };
            EntityManager.Add(camera2D);
            var title = new Entity("Title")
                .AddComponent(new Sprite("C:/Users/trottasn/Documents/GitHub/Cateners/CatanersWave/WaveEngineGame/WaveEngineGameProject/TheSettlersofCatan.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
                .AddComponent(new Transform2D()
                {
                    Y = 0,
                    X = (WaveServices.Platform.ScreenWidth / 2) - 300
            });
            EntityManager.Add(title);

            var forestHex = new Entity("forestHex")
                .AddComponent(new Sprite("C:/Users/trottasn/Documents/GitHub/Cateners/CatanersWave/WaveEngineGame/WaveEngineGameProject/BadHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
                .AddComponent(new Transform2D()
                {
                    Y = (WaveServices.Platform.ScreenHeight / 2) - 126,
                    X = (WaveServices.Platform.ScreenWidth / 2) - 110
                });
            EntityManager.Add(forestHex);
            var forestHex2 = new Entity("forestHex2")
                .AddComponent(new Sprite("C:/Users/trottasn/Documents/GitHub/Cateners/CatanersWave/WaveEngineGame/WaveEngineGameProject/BadHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
                .AddComponent(new Transform2D()
                {
                    Y = (WaveServices.Platform.ScreenHeight / 2) + 65,
                    X = (WaveServices.Platform.ScreenWidth / 2)
                });
            EntityManager.Add(forestHex2);
            var forestHex3 = new Entity("forestHex3")
                .AddComponent(new Sprite("C:/Users/trottasn/Documents/GitHub/Cateners/CatanersWave/WaveEngineGame/WaveEngineGameProject/BadHex.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
                .AddComponent(new RectangleCollider())
                .AddComponent(new TouchGestures())
                .AddComponent(new Transform2D()
                {
                    Y = (WaveServices.Platform.ScreenHeight / 2) - 126,
                    X = (WaveServices.Platform.ScreenWidth / 2) + 110
                });
            forestHex3.FindComponent<TouchGestures>().TouchPressed += new EventHandler<GestureEventArgs>(forestHex3_TouchPressed);
            EntityManager.Add(forestHex3);
            #endregion
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
