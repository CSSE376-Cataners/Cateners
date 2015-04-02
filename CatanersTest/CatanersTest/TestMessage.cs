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
    }
}
