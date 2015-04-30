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
    public class VariablesTest
    {
        [Test]
        public void testInitalize()
        {
            new Variables();

            Assert.NotNull(Variables.serverPort);
        }
    }
}
