using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cataners
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
            this.ClientSize = this.BackgroundImage.Size;
        }

        [ExcludeFromCodeCoverage]
        private void closeTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
