using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Cataners;
using CatanersShared;

namespace CatanersTest
{   
    [TestFixture]
    class RegistrationTesting
    {
        [SetUp]
        public void setupRegistration()
        {
        }

        [Test]
        public void testPasswordMatchSame()
        {
            Verification newVer = new Verification();
            Assert.True(newVer.passwordsMatch("Hello29", "Hello29"));
        }

        [Test]
        public void testPasswordMatchDifferent()
        {
            Verification newVer = new Verification();
            Assert.False(newVer.passwordsMatch("Hello29", "Hello64"));
        }

        [Test]
        public void testPasswordMatchSymbolsSame()
        {
            Verification newVer = new Verification();
        }
    }
}
