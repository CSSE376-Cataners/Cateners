using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using Cataners;

namespace CatanersTest
{
    
    [TestFixture()]
    class LoginTesting
    {
        [Test]
        public void TestEmptyString()
        {
            MainGui newMain = new MainGui();
            Assert.False(newMain.loginAuthentication(""));
        }
    }
}
