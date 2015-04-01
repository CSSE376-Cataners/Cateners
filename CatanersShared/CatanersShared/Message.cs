using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class Message
    {
        enum TYPE { Login, Register };

        TYPE type;

        Object message;

        public Message(Object obj, TYPE type)
        {
            this.message = obj;
            this.type = type;
        }

    }
}
