using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cataners;

namespace CatanersTest
{
    [TestFixture()]
    public class DataTesting
    {
        [Test]
        public void testInialize()
        {
            Assert.NotNull(Data.Lobbies);
            Assert.NotNull(Data.currentLobby);
            Assert.AreEqual("", Data.username);
        }
    }
}
