using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    class PopUpMessage
    {
        public enum TYPE { Notification, ResponseNeeded };
        PopUpMessage.TYPE type;
        String titleMsg;
        String bodyMsg;

        public PopUpMessage(String titleMessage, String bodyMessage, PopUpMessage.TYPE type)
        {
            


        }

    }
}
