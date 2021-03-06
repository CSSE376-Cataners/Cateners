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
        //SpriteBatch spriteBatch;
        //Texture2D splashScreen;
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
            if (this.game.HasExited)
            {
                WaveServices.Platform.Exit();
            }
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
                        if (MyScene.Instance.EntityManager != null  && (MyScene.toAdd.Count > 0 || MyScene.toAddDecor.Count > 0))
                        {
                            lock (MyScene.toAdd) 
                            {
                                lock (MyScene.toAddDecor)
                                {
                                    WaveEngine.Framework.Managers.EntityManager o = MyScene.Instance.EntityManager;
                                    for (int i = 0; i < MyScene.toAdd.Count; i++)
                                    {
                                        Entity temp = o.Find(MyScene.toAdd[i].Name);
                                        if (temp != null)
                                        {
                                            o.Remove(temp);
                                        }
                                        o.Add(MyScene.toAdd[i]);
                                    }
                                    MyScene.toAdd.Clear();
                                    for (int l = 0; l < MyScene.toAddDecor.Count; l++)
                                    {
                                        BaseDecorator temp = o.Find<BaseDecorator>(MyScene.toAddDecor[l].Name);
                                        if (temp == null)
                                        {
                                            Entity temp2 = o.Find(MyScene.toAddDecor[l].Name);
                                            if (temp2 != null)
                                                o.Remove(temp2);
                                        }
                                        else
                                        {
                                            o.Remove(temp);
                                        }
                                        o.Add(MyScene.toAddDecor[l]);
                                    }
                                    MyScene.toAddDecor.Clear();
                                }
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
                lock (this.game)
                {
                    try                    {
                        this.game.DrawFrame(elapsedTime);
                    }
                    catch (NullReferenceException) { }
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

