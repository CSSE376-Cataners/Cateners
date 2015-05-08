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


    }
}
