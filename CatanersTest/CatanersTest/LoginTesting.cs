using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using Cataners;
using CatanersShared;

namespace CatanersTest
{
    
    [TestFixture()]
    class LoginTesting
    {

        [Test]
        public void checkUsernameEmpty()
            //testing if string (e.g. username is valid)
        {
            String text = "";
            Assert.False(Verification.verify(text));
        }

        [Test]
        public void checkUsernameSixChars()
        {
            String text = "Stever";
            Assert.True(Verification.verify(text));
        }

        [Test]
        public void checkUsernameNumbers()
        {
            String text = "Stever38";
            Assert.True(Verification.verify(text));
        }

        [Test]
        public void checkUsernameFourteenChars()
        {
            String text = "TheGodfather42";
            Assert.True(Verification.verify(text));
        }

        [Test]
        public void checkUsernameThreeLetters()
        {
            String text = "Red345";
            Assert.False(Verification.verify(text));
        }
    }

}
