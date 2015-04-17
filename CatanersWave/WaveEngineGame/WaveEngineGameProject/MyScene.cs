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
            var camera3D = new FreeCamera("Camera3D", new Vector3(0, 2, 4), Vector3.Zero) { BackgroundColor = Color.CornflowerBlue };
            //EntityManager.Add(camera3D);

            // Draw a cube
            Entity cube = new Entity()
                //  .AddComponent(new Transform3D())
                //  .AddComponent(Model.CreateTeapot())
                //  .AddComponent(new Spinner() { AxisTotalIncreases = new Vector3(1, 2, 3) })
                .AddComponent(new TextControl()
                {
                    Text = "Multiplayer",
                    Foreground = Color.White,
                })
                .AddComponent(new TextControlRenderer())
                .AddComponent(new Transform2D())
                .AddComponent(new RectangleCollider())
                .AddComponent(new TouchGestures())
                .AddComponent(new Sprite(StaticResources.DefaultTexture))
                .AddComponent(new SpriteRenderer(DefaultLayers.Opaque));

            var touch = cube.FindComponent<TouchGestures>();
            touch.TouchPressed += (s, o) =>
            {
                Console.WriteLine("I WAS TOUCHED");
            };

            touch.TouchMoved += (s, o) =>
            {
                Console.WriteLine("I was moved");
            };


            EntityManager.Add(cube);

            // Create a 2D camera
            var camera2D = new FixedCamera2D("Camera2D") { ClearFlags = ClearFlags.DepthAndStencil }; // Transparent background need this clearFlags.
            EntityManager.Add(camera2D);

            // Draw a simple sprite
            /*
            Entity sprite = new Entity()
                .AddComponent(new Transform2D())
                // Change this line for a custom assets "new Sprite("Content/MyTexture"))"
                // Manage assets using the Resources.weproj link to open the Assets Exporter tool.
                .AddComponent(new Sprite(StaticResources.DefaultTexture))
                .AddComponent(new SpriteRenderer(DefaultLayers.Opaque));

            EntityManager.Add(sprite);*/
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
