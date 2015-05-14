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
       // private List<Resource> resources;

        public String Username {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public Boolean Ready = false;

        public Player(String username)
        {
            this.username = username;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || !(obj is Player))
            {
                return false;
            }

            Player other = (Player)obj;
            if (this.username == null)
            {
                if (other.username == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return this.Username.Equals(other.username);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return username.GetHashCode();
        }

        public override string ToString()
        {
            return Username;
        }
    }
}
