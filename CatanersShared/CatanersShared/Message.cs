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
        Translation.TYPE type;

        String message;

        public Message(String obj, Translation.TYPE type)
        {
            this.message = obj;
            this.type = type;
        }

        public String toJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

    }
}
