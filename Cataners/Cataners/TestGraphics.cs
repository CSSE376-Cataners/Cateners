using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;

namespace Cataners
{
    public partial class TestGraphics : Form
    {
        private Bitmap b;

        public TestGraphics()
        {
            InitializeComponent();
            this.b = new Bitmap(this.Width, this.Height);
            this.g = Graphics.FromImage(this.b);
            this.pictureBox1.Image = this.b;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs) e;
            Ellipse newEllipse = new Ellipse();
            newEllipse.Width = 50;
            newEllipse.Height = 50;
            newEllipse.Margin = new System.Windows.Thickness(me.X, me.Y, 0, 0);
            this.pictureBox1.Image = this.b;
        }
    }
}
