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
    public partial class LobbyForm : Form
    {
        public static LobbyForm INSTANCE;

        public LobbyForm()
        {
            InitializeComponent();
            this.FormClosing += closing;
            INSTANCE = this;
        }
        private void closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
    }
}
