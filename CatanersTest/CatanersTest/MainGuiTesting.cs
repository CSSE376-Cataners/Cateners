using Cataners;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersTest
{
    [TestFixture()]
    public class MainGuiTesting
    {

        [Test]
        public void testConstructor()
        {
            MainGui gui = new MainGui();

            Assert.NotNull(gui);
            Assert.NotNull(MainGui.INSTANCE);
            Assert.NotNull(CommunicationClient.Instance);

        }
    }
}
