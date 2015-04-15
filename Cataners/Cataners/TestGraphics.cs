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
            this.g.DrawEllipse(Pens.Black, me.X, me.Y, 40, 40);
            this.pictureBox1.Image = this.b;
        }
    }
}
