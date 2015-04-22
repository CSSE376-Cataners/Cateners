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
            #endregion
        }

    
    
        void temp_TouchTap(object sender, GestureEventArgs e)
        {
            Console.WriteLine("I BEEN TOUCHED");
        }

        protected override void Start()
        {
            base.Start();

            // This method is called after the CreateScene and Initialize methods and before the first Update.
        }
    }
}
