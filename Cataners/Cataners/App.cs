using System;
using System.IO;
using System.Reflection;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Input;
using WaveEngine.Common.Math;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;
using CatanersShared;
using System.Diagnostics.CodeAnalysis;

namespace Cataners
{
    [ExcludeFromCodeCoverage]
    public class App : WaveEngine.Adapter.Application
    {
        public static object renderLock = "";
        public static App INSTANCE;

        public Cataners.Game game;
        SpriteBatch spriteBatch;
        Texture2D splashScreen;
        bool splashState = true;
        TimeSpan time;
        Vector2 position;
        Color backgroundSplashColor;

        bool ready = false;

        public App()
        {
            INSTANCE = this;
            this.Width = 1910;
            this.Height = 1000;
            this.FullScreen = false;
            this.WindowTitle = "WaveEngineGame";
        }

        public override void Initialize()
        {
            this.game = new Cataners.Game();
            this.game.Initialize(this);

            #region WAVE SOFTWARE LICENSE AGREEMENT
            this.backgroundSplashColor = new Color("#ebebeb");
            this.spriteBatch = new SpriteBatch(WaveServices.GraphicsDevice);

            var resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            string name = string.Empty;

            foreach (string item in resourceNames)
            {
                if (item.Contains("SplashScreen.wpk"))
                {
                    name = item;
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidProgramException("License terms not agreed.");
            }

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            {
                this.splashScreen = WaveServices.Assets.Global.LoadAsset<Texture2D>(name, stream);
            }

            position = new Vector2();
            #endregion
        }

        public override void Update(TimeSpan elapsedTime)
        {
            this.splashState = false;
            if (this.game != null && !this.game.HasExited)
            {
                if (WaveServices.Input.KeyboardState.F10 == ButtonState.Pressed)
                {
                    this.FullScreen = !this.FullScreen;
                }

                if (this.splashState)
                {
                    #region WAVE SOFTWARE LICENSE AGREEMENT
                    this.time += elapsedTime;
                    if (time > TimeSpan.FromSeconds(2))
                    {
                        this.splashState = false;
                    }

                    position.X = (this.Width - this.splashScreen.Width) / 2.0f;
                    position.Y = (this.Height - this.splashScreen.Height) / 2.0f;
                    #endregion
                }
                else
                {
                    if (WaveServices.Input.KeyboardState.Escape == ButtonState.Pressed)
                    {
                        WaveServices.Platform.Exit();
                    }
                    else
                    {
                        if (MyScene.toAdd.Count > 0 || MyScene.toAddDecor.Count > 0)
                        {
                            if (!ready)
                            {
                                MyScene newScene = new MyScene();
                                ScreenContext screenContext = new ScreenContext(newScene)
                                {
                                    Name = "Next Frame"
                                };
                                WaveServices.ScreenContextManager.To(screenContext);
                                ready = true;
                            }
                            else
                            {
                                if (MyScene.Instance.EntityManager == null)
                                {
                                    Console.WriteLine("Null EntityManager");
                                }
                                else
                                {
                                    MyScene.addResources();
                                    foreach (Entity e in MyScene.toAdd)
                                    {
                                        if (e != null)
                                        {
                                            MyScene.Instance.EntityManager.Add(e);
                                        }
                                    }
                                    MyScene.toAdd.Clear();

                                    foreach (BaseDecorator e in MyScene.toAddDecor)
                                    {
                                        MyScene.Instance.EntityManager.Add(e);
                                    }
                                    MyScene.toAddDecor.Clear();
                                }
                                ready = false;
                            }
                            
                        }
                        this.game.UpdateFrame(elapsedTime);
                    }
                }
            }
        }

        public override void Draw(TimeSpan elapsedTime)
        {
            if (this.game != null && !this.game.HasExited)
            {
                if (this.splashState)
                {
                    #region WAVE SOFTWARE LICENSE AGREEMENT
                    WaveServices.GraphicsDevice.RenderTargets.SetRenderTarget(null);
                    WaveServices.GraphicsDevice.Clear(ref this.backgroundSplashColor, ClearFlags.Target, 1);
                    this.spriteBatch.DrawVM(this.splashScreen, this.position, Color.White);
                    this.spriteBatch.Render();
                    #endregion
                }
                else
                {
                    this.game.DrawFrame(elapsedTime);
                }
            }
        }

        /// <summary>
        /// Called when [activated].
        /// </summary>
        public override void OnActivated()
        {
            base.OnActivated();
            if (this.game != null)
            {
                game.OnActivated();
            }
        }

        /// <summary>
        /// Called when [deactivate].
        /// </summary>
        public override void OnDeactivate()
        {
            base.OnDeactivate();
            if (this.game != null)
            {
                game.OnDeactivated();
            }
        }
    }
}

