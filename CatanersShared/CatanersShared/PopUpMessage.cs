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

    }
}
