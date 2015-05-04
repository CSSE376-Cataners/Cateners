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
    public partial class TradeForm : Form
    {
        public static TradeForm INSTANCE;
        public TradeForm()
        {
            InitializeComponent();
            INSTANCE = this;
        }

    }
}
