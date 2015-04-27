using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatenersServer;
using NUnit.Framework;

namespace CatanersTest
{
    [TestFixture()]
    public class ServerPlayerTesting
    {

        [Test]
        public void testConstructor()
        {
            Client client = new Client();
            String username = "UserName";
            ServerPlayer player = new ServerPlayer(username,client);

            Assert.AreEqual(client, player.client);
            Assert.AreEqual(username, player.Username);


        }
    }
}
