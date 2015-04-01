using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class Message
    {
        enum TYPE { Login, Register, Chat, Game };

        TYPE type;

        String message;

        public Message(String obj, TYPE type)
        {
            this.message = obj;
            this.type = type;
        }



    }
}
