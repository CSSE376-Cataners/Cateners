using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private void closeTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
