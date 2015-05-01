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
    public class SplashScreenTesting
    {
        [Test]
        public void testConstructor()
        {
            SplashScreen screen = new SplashScreen();

            Assert.NotNull(screen);
        }
    }
}
