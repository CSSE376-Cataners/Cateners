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
    public class SignUpFormTesting
    {
        [Test]
        public void testContructor()
        {
            SignUpForm suf = new SignUpForm();

            Assert.NotNull(suf);
        }
    }
}
