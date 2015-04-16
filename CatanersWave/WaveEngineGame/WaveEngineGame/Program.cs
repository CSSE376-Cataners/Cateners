using System;
using System.Diagnostics;
using System.Windows.Forms;
using WaveEngine.Adapter;

namespace WaveEngineGame
{
    public static class Program
    {
        [MTAThread]
        public static void Main()
        {
            using (App game = new App())
            {
                game.Run();
            }
        }
    }
}

