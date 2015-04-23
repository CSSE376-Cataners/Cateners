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
    class PlayerTesting
    {

        [Test]
        public void testInstanciation()
        {
            Player a = new Player("Test");

            Assert.NotNull(a);
        }

        [Test]
        public void testPlayerReturn()
        {
            Player a = new Player("abc123");

            Assert.AreEqual("abc123", a.Username);
        }

        [Test]
        public void testPlayerEqual()
        {
            Player a = new Player("abc123");
            Player b = new Player("abc123");

            Assert.AreEqual(a, b);
        }

    }
}
