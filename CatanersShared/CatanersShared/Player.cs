using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class Player
    {
        private string username;

        public String Username {
            get
            {
                return username;
            }
        }

        public Player(String username)
        {
            this.username = username;
        }
    }
}
