using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cataners
{
    static class Start
    {
        private static SplashScreen splashScreen;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ShowSplash();

            Application.Run(new MainGui());
        }


        static void ShowSplash()
        {
            splashScreen = new SplashScreen();
            Application.Run(splashScreen);
        }


    }
}
