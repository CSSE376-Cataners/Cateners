using System;
using System.Diagnostics;
using System.Windows.Forms;
using WaveEngine.Adapter;
using WaveEngine.Framework.Services;

namespace Cataners
{
    public static class Program
    {
        [MTAThread]
        public static void Main()
        {
            using (App game = new App())
            {
                game.Run();
                WaveConstants.PLATFORM_WIDTH = WaveServices.Platform.ScreenWidth;
                WaveConstants.PLATFORM_HEIGHT = WaveServices.Platform.ScreenHeight;
            }
        }
    }
}

