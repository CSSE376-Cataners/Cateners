using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;

namespace CatanersTest
{

    [TestFixture()]
    public class ChatClassTesting
    {

        [Test]
        public void testConstructor()
        {
            Chat chat = new Chat("Message", TYPE.Normal, "Reciver");

            Assert.AreEqual(chat.message = "Message", chat.type = TYPE.Normal, chat.extra = "Reciver");
        }
    }
}
