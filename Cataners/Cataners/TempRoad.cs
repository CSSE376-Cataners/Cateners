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
    public partial class TempRoad : Form
    {
        public TempRoad()
        {
            InitializeComponent();
        }

        private void road1_Click(object sender, EventArgs e)
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

        private void ChangeColor()
        {
            this.road1.BackColor = Color.Crimson;
        }
    }
}
