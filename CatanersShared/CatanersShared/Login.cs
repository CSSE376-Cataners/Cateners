using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class Login
    {
        public String username;
        public String password;
        public bool register;

        public Login(String username, String password)
        {
            this.username = username;
            this.password = password;
            this.register = false;
        }

        public Login(String username, String password, bool register)
        {
            this.username = username;
            this.password = password;
            this.register = register;
        }

        public String toJson()
        {
            return null;
        }

        public String fromJson()
        {
            return null;
        }
    }
}
