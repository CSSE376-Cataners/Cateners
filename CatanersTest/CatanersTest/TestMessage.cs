using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CatanersShared;

namespace CatanersTest
{
    [TestFixture()]
    class TestMessage
    {
        [Test]
        public void testMessageToJsonBlank()
        {
            Message msg = new Message("Test", Translation.TYPE.Login);

            Assert.AreEqual("{\"type\":0,\"message\":\"Test\"}", msg.toJson());
        }

        [Test]
        public void testMessageFromJson()
        {
            Message test = Message.fromJson("{\"type\":0,\"message\":\"Test\"}");

            Console.WriteLine(test.message + "  " + test.type);

            Message msg = new Message("Test", Translation.TYPE.Login);
            Assert.AreEqual(msg, test);
        }

        [Test]
        public void testGetHashCode()
        {
            Message msg1 = new Message("Message One", Translation.TYPE.Login);
            Message msg2 = new Message("Message One", Translation.TYPE.Login);

            Assert.AreEqual(msg1.GetHashCode(), msg2.GetHashCode());
            msg2.message = "Message Two";
            Assert.AreNotEqual(msg1.GetHashCode(), msg2.GetHashCode());
        }

        [Test]
        public void testMessageEquals()
        {
            Message msg1 = new Message("Message One", Translation.TYPE.Login);
            Message msg1c = new Message("Message One", Translation.TYPE.Login);
            Message msg1d = new Message("Message One", Translation.TYPE.RegenerateBoard);
            Message msg2 = new Message("Message Two", Translation.TYPE.RegenerateBoard);



            Assert.False(msg1.Equals(" "));
            Assert.False(msg1.Equals( msg1d));
            Assert.True(msg1.Equals( msg1c));
            Assert.False(msg1.Equals(msg2));
            Assert.True(msg1.Equals( msg1));
            Assert.False(msg1.Equals(null));
        }
    }
}
