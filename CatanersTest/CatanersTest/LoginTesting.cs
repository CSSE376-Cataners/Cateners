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
            Assert.False(Verification.verifyInputString(text));
        }

        [Test]
        public void checkUsernameSixChars()
        {
            String text = "Stever";
            Assert.True(Verification.verifyInputString(text));
        }

        [Test]
        public void checkUsernameNumbers()
        {
            String text = "Stever38";
            Assert.True(Verification.verifyInputString(text));
        }

        [Test]
        public void checkUsernameFourteenChars()
        {
            String text = "TheGodfather42";
            Assert.True(Verification.verifyInputString(text));
        }

        [Test]
        public void checkUsernameThreeLetters()
        {
            String text = "Red345";
            Assert.False(Verification.verifyInputString(text));
        }

        [Test]
        public void checkUsernameSpace()
        {
            String text = "Redfox 345";
            Assert.False(Verification.verifyInputString(text));
        }

        [Test]
        public void checkUsernameLessThanSix()
        {
            String text = "Rambo";
            Assert.False(Verification.verifyInputString(text));
        }

        [Test]
        public void checkUsernameMoreThanFifteen()
        {
            String text = "FullyRamblomatic";
            Assert.False(Verification.verifyInputString(text));
        }

        [Test]
        public void checkUsernameSpecialCharacters()
        {
            String text = "Trotta%^'54&";
            Assert.False(Verification.verifyInputString(text));
        }

        //password testing
        //Testing boundary values
        [Test]
        public void checkPasswordLessThanFourChars()
        {
            String text = "abc";
            Assert.False(Verification.verifyPassword(text));
        }

        [Test]
        public void checkPasswordFourChars()
        {
            String text = "abcd";
            Assert.True(Verification.verifyPassword(text));
        }

        [Test]
        public void checkPasswordMoreThanFifteenChars()
        {
            String text = "supaeasypassword";
            Assert.False(Verification.verifyPassword(text));
        }

        [Test]
        public void checkPasswordFifteenChars()
        {
            String text = "easypasswordftw";
            Assert.True(Verification.verifyPassword(text));
        }


    }

}
