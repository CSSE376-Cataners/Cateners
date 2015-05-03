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


        [Test]
        public void testToJson()
        {
            Chat chat = new Chat("Message", Chat.TYPE.Normal, "Reciver");

            Console.WriteLine(chat.toJson());
            Assert.AreEqual("{\"ChatType\":0,\"Message\":\"Message\",\"Special\":\"Reciver\"}",chat.toJson());
        }

        [Test]
        public void testFromJson()
        {
            Chat chat = new Chat("Message", Chat.TYPE.Normal, "Reciver");

            String inJson = chat.toJson();

            Assert.AreEqual(chat, Chat.fromJson(inJson));
        }

        [Test]
        public void testEquals()
        {
            Chat chat = new Chat("Message", Chat.TYPE.Normal, "Reciver");
            Chat chat1 = new Chat("Message1", Chat.TYPE.Normal, "Reciver");
            Chat chat2 = new Chat("Message", Chat.TYPE.Private, "Reciver");
            Chat chat3 = new Chat("Message", Chat.TYPE.Normal, "Reciver1");
            Chat chat4 = new Chat("Message", Chat.TYPE.Normal, "Reciver");

            Object Other = "Random";


            Assert.True(chat.Equals(chat));
            Assert.True(chat.Equals(chat4));
            Assert.False(chat.Equals(chat1));
            Assert.False(chat.Equals(chat2));
            Assert.False(chat.Equals(chat3));
            Assert.False(chat.Equals(Other));

        }

        [Test]
        public void testHashCode()
        {
            Chat chat = new Chat("Message", Chat.TYPE.Normal, "Reciver");
            Chat chat1 = new Chat("Message1", Chat.TYPE.Normal, "Reciver");
            Chat chat2 = new Chat("Message", Chat.TYPE.Private, "Reciver");
            Chat chat3 = new Chat("Message", Chat.TYPE.Normal, "Reciver1");
            Chat chat4 = new Chat("Message", Chat.TYPE.Normal, "Reciver");

            Object Other = "Random";


            Assert.AreEqual(chat.GetHashCode(),chat.GetHashCode());
            Assert.AreEqual(chat.GetHashCode(), chat4.GetHashCode());
            Assert.AreNotEqual(chat.GetHashCode(), chat1.GetHashCode());
            Assert.AreNotEqual(chat.GetHashCode(), chat2.GetHashCode());
            Assert.AreNotEqual(chat.GetHashCode(), chat3.GetHashCode());
            
        }
    }
}
