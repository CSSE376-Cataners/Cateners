using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cataners
{
    public partial class ChangeServerForm : Form
    {
        public ChangeServerForm()
        {
            InitializeComponent();
        }

        private void serverChangeButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ServerAddr = serverAddrTextBox.Text;
            Properties.Settings.Default.Save();
            Application.Restart();

        }

    }
}
