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
    public class LogingInFormTesting
    {

        [Test]
        public void testConstructor()
        {
            LoggingInForm LiF = new LoggingInForm();

            Assert.NotNull(LiF);
            Assert.AreEqual(0, LiF.timerCount);
        }
    }
}
