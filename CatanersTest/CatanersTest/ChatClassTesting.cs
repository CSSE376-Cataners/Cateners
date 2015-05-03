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
            Chat chat = new Chat("Message", Chat.TYPE.Normal, "Reciver");

            Assert.AreEqual(chat.Message, "Message");
            Assert.AreEqual(chat.ChatType, Chat.TYPE.Normal);
            Assert.AreEqual(chat.Special,"Reciver");
        }
    }
}
