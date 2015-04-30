using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cataners
{
    public partial class LoggingInForm : Form
    {
        public int timerCount = 0;
        Timer time = new Timer();

        public LoggingInForm()
        {
            InitializeComponent();
        }

        [ExcludeFromCodeCoverage]
        public void LoggingInForm_Load(object sender, EventArgs e)
        {
            
            time.Tick += new EventHandler(TimerEventHandler);
            time.Interval = 1000;
            time.Start();

        }

        [ExcludeFromCodeCoverage]
        private void TimerEventHandler(Object myObject, EventArgs myEventArgs)
        {
            time.Stop();
            if (CommunicationClient.Instance.queues[CatanersShared.Translation.TYPE.Login].Count >= 1)
            {
                this.Close();
            }
            if (timerCount % 3 == 0)
            {
                loggingInLabel1.Visible = true;
                loggingInLabel2.Visible = false;
                loggingInLabel3.Visible = false;
                timerCount++;
                time.Enabled = true;
            }
            else if (timerCount % 3 == 1)
            {
                loggingInLabel1.Visible = false;
                loggingInLabel2.Visible = true;
                loggingInLabel3.Visible = false;
                timerCount++;
                time.Enabled = true;
            }
            else
            {
                loggingInLabel1.Visible = false;
                loggingInLabel2.Visible = false;
                loggingInLabel3.Visible = true;
                timerCount++;
                time.Enabled = true;
            }

        }
    }
}
