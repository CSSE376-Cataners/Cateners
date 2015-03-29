using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cataners
{
    class Road : PictureBox
    {
        private Boolean activeStatus;
        public Road()
        {
            this.activeStatus = false;
        }

        public void print(){
            Console.WriteLine("Activated Road");
        }

        public void print2()
        {
            Console.WriteLine("Piece already active. Try again.");
        }

        public Boolean isActive()
        {
            return activeStatus;
        }

        public void activate()
        {
            this.activeStatus = true;
        }
    }
}
