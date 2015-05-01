using Cataners;
using CatanersShared;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatanersTest
{
    [TestFixture()]
    public class LobbyFormTestingcs
    {
        [Test]
        public void testConstructor()
        {
            BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic;
            FieldInfo clientField = typeof(CommunicationClient).GetField("instance", flags);


            FakeClient client = new FakeClient();
            clientField.SetValue(new CommunicationClient(), client);

            LobbyForm form = new LobbyForm("Game Name");
            Assert.NotNull(LobbyForm.INSTANCE);

            
            flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo field = typeof(LobbyForm).GetField("lobbyNameLabel",flags);

            Label label = (Label)field.GetValue(LobbyForm.INSTANCE);

            Assert.AreEqual("Game Name", label.Text);

            Assert.AreEqual(new CatanersShared.Message("", Translation.TYPE.UpdateLobby).toJson(), client.lastCall);


        }

        [Test]
        public void testReadyUp()
        {
            BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic;
            FieldInfo clientField = typeof(CommunicationClient).GetField("instance", flags);


            FakeClient client = new FakeClient();
            clientField.SetValue(new CommunicationClient(), client);

            LobbyForm form = new LobbyForm("Whatever you like");

            Assert.False(form.ready);
            flags = BindingFlags.Instance | BindingFlags.NonPublic;
            MethodInfo method = typeof(LobbyForm).GetMethod("readyButton_Click", flags);
            method.Invoke(form, new object[] { null, null });
            Assert.True(form.ready);
            Assert.AreEqual(new CatanersShared.Message(true.ToString(), Translation.TYPE.ChangeReadyStatus).toJson(),client.lastCall);

            method.Invoke(form, new object[] { null, null });
            Assert.False(form.ready);
            Assert.AreEqual(new CatanersShared.Message(false.ToString(), Translation.TYPE.ChangeReadyStatus).toJson(), client.lastCall);
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
