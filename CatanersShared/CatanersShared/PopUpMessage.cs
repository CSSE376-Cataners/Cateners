using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class PopUpMessage
    {
        public enum TYPE { Notification, ResponseNeeded };
        public PopUpMessage.TYPE type;
        public String titleMsg;
        public String bodyMsg;

        public PopUpMessage(String titleMessage, String bodyMessage, PopUpMessage.TYPE type)
        {

            this.titleMsg = titleMessage;
            this.bodyMsg = bodyMessage;
            this.type = type;

        }

        public String toJson(){
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
        public static PopUpMessage fromJson(String json){
            return Newtonsoft.Json.JsonConvert.DeserializeObject<PopUpMessage>(json);
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is PopUpMessage))
            {
                return false;
            }

            PopUpMessage other = (PopUpMessage)obj;

            return this.bodyMsg.Equals(other.bodyMsg) && this.titleMsg.Equals(other.titleMsg) && this.type == other.type; 
        }
    }
}
