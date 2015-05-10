using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;

namespace CatanersTest
{

    [TestFixture]
    public class PopUpMessageTesting
    {

        [Test]
        public void PopUpMessageTestInitialize(){

            PopUpMessage popup = new PopUpMessage("Title","Body",PopUpMessage.TYPE.Notification);

            Assert.AreEqual("Title", popup.titleMsg);
            Assert.AreEqual("Body", popup.bodyMsg);
            Assert.AreEqual(PopUpMessage.TYPE.Notification, popup.type);

        }

        [Test]
        public void PopUpMessageTestToJson()
        {

            PopUpMessage popup = new PopUpMessage("Title", "Body", PopUpMessage.TYPE.Notification);
            Console.WriteLine(popup.toJson());
            Assert.AreEqual("{\"type\":0,\"titleMsg\":\"Title\",\"bodyMsg\":\"Body\"}",popup.toJson());

        }

        [Test]
        public void PopUpMessageTestFromJson()
        {

            PopUpMessage popup = new PopUpMessage("Title", "Body", PopUpMessage.TYPE.Notification);
            PopUpMessage popup2 = PopUpMessage.fromJson("{\"type\":0,\"titleMsg\":\"Title\",\"bodyMsg\":\"Body\"}");
            Assert.AreEqual(popup, popup2);

        }

        [Test]
        public void testEquals()
        {
            PopUpMessage popup1 = new PopUpMessage("Title", "Body", PopUpMessage.TYPE.Notification);
            PopUpMessage popup2 = new PopUpMessage("Title", "Body", PopUpMessage.TYPE.Notification);
            PopUpMessage popup3 = new PopUpMessage("Title", "Not", PopUpMessage.TYPE.Notification);


            Assert.True(popup1.Equals(popup1));
            Assert.True(popup1.Equals(popup2));
            Assert.False(popup1.Equals(popup3));
            Assert.False(popup1.Equals(null));
            Assert.False(popup1.Equals(""));
        }

    }
}
