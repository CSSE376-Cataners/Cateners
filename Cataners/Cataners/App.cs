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
                else
                {
                    if (WaveServices.Input.KeyboardState.Escape == ButtonState.Pressed)
                    {
                        WaveServices.Platform.Exit();
                    }
                    else
                    {
                        if (MyScene.toAdd.Count > 0 || MyScene.toAddDecor.Count > 0 || (MyScene.Instance.EntityManager != null &&MyScene.Instance.EntityManager.Find("player2Name")==null) )
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
                                    MyScene.addRegenerateBoardButton();
                                    MyScene.addTradeButton();
                                    MyScene.addPlayerNames();
                                    MyScene.addChatButton();
                                    lock (MyScene.toAdd)
                                    {
                                        foreach (Entity e in MyScene.toAdd)
                                        {
                                            if (e != null)
                                            {
                                                try
                                                {
                                                    MyScene.Instance.EntityManager.Add(e);
                                                }
                                                catch { }
                                            }
                                        }
                                        MyScene.toAdd.Clear();
                                    }
                                    lock (MyScene.toAddDecor)
                                    {
                                        foreach (BaseDecorator e in MyScene.toAddDecor)
                                        {
                                            MyScene.Instance.EntityManager.Add(e);
                                        }
                                        MyScene.toAddDecor.Clear();
                                    }
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
                this.game.DrawFrame(elapsedTime);
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

