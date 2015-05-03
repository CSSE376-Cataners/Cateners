using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class Chat
    {

        public enum TYPE {Normal, Private};


        public TYPE ChatType;
        public String Message;
        public object Special;


        public Chat()
        {
            // For Json pasrsing 
        }

        public Chat(String message, TYPE y, Object Special)
        {
            this.Message = message;
            this.ChatType = y;
            this.Special = Special;
        }

        public String toJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }


        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Chat))
            {
                return false;
            }

            Chat other = (Chat)obj;

            return this.Message.Equals(other.Message) && this.ChatType == other.ChatType && this.Special.Equals(other.Special);


        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static object fromJson(string inJson)
        {
            throw new NotImplementedException();
        }
    }
}
