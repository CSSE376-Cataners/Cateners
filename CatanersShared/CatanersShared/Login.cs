using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;

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
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static Login fromJson(String json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Login>(json);
        }
    }
}
