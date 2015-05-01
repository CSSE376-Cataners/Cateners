using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cataners
{
    [ExcludeFromCodeCoverage]
    static class Start
    {
        private static SplashScreen splashScreen;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ShowSplash();

            new MainGui().Show();
            Application.Run();
            //Application.Run(new TestGraphics());
        }

        static void ShowSplash()
        {
            splashScreen = new SplashScreen();
            Application.Run(splashScreen);
        }


    }
}
