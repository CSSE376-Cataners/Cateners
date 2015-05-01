using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CatanersShared
{
    public class Message
    {
        public Translation.TYPE type;

        public String message;

        public Message(String obj, Translation.TYPE type)
        {
            this.message = obj;
            this.type = type;
        }

        public String toJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }


        public static Message fromJson(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(json);
        }


        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Message))
            {
                return false;
            }

            Message other = (Message)obj;
            
            return other.message.Equals(this.message) && other.type == this.type;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return this.message.GetHashCode() + this.type.GetHashCode();
        }
    }
}
