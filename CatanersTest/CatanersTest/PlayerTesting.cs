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
        public void testInstantiation()
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
            Player c = new Player("123abc");

            Player d = new Player(null);

            Assert.True(a.Equals(b));
            Assert.True(d.Equals(d));
            Assert.False(a.Equals(c));
        }

        [Test]
        public void testPlayerEqualOtherCondition()
        {
            Player p = new Player("Test");
            Assert.False(p.Equals(null));
            Assert.False(p.Equals(" "));
        }

        [Test]
        public void testPlayerToString()
        {
            Player a = new Player("abc123");
            Assert.AreEqual("abc123", a.ToString());
        }

        [Test]
        public void testPlayerHashCode()
        {
            Player bobby = new Player("bobbyTables");
            Player bobby2 = new Player("bobbyTables");
            int hash1 = bobby.GetHashCode();
            int hash2 = bobby2.GetHashCode();
            Assert.AreEqual(hash1, hash2);
        }
    }
}
