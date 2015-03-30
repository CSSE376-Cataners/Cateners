using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Cataners
{
    public partial class TempRoad : Form
    {
        public TempRoad()
        {
            InitializeComponent();
        }

        private void ChangeColor()
        {
            this.road1.BackColor = Color.Crimson;
        }

        private void TempRoad_Load(object sender, EventArgs e)
        {

        }

        private void road1_Click_1(object sender, EventArgs e)
        {
            if (!(this.road1.isActive()))
            {
                this.road1.print();
                this.road1.activate();
                this.ChangeColor();
            }
            else
            {
                this.road1.print2();
            }
        }
    }
}
