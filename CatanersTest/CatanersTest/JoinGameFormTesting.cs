using Cataners;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CatanersTest
{
    [TestFixture()]
    public class JoinGameFormTesting
    {
        [Test]
        public void testJoinGameConstruct()
        {
            BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic;
            FieldInfo clientField = typeof(CommunicationClient).GetField("instance", flags);


            FakeClient client = new FakeClient();
            clientField.SetValue(new CommunicationClient(), client);

            JoinGameForm form = new JoinGameForm();
            Assert.NotNull(form);
        }
        public class FakeClient : CommunicationClient
        {
            public FakeClient()
            {

            }

            public String lastCall = null;

            public override void sendToServer(String msg)
            {
                lastCall = msg;
            }
        }
    }
}
