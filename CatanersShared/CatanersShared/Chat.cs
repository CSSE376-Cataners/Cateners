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
    }
}
