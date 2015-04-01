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

        //password testing must be 4-15 chars and must have 1 numeric character and one upper or lowercase letter
        //Testing boundary values for length
        [Test]
        public void TestPasswordLessThanFourChars()
        {
            String text = "ab1";
            Assert.False(Verification.verifyPassword(text));
        }

        [Test]
        public void TestPasswordFourChars()
        {
            String text = "abc1";
            Assert.True(Verification.verifyPassword(text));
        }

        [Test]
        public void TestPasswordMoreThanFifteenChars()
        {
            String text = "1337easypassword";
            Assert.False(Verification.verifyPassword(text));
        }

        [Test]
        public void TestPasswordFifteenChars()
        {
            String text = "easypassword!!!";
            Assert.True(Verification.verifyPassword(text));
        }
        //end boundary value analysis
        //testing number and letter requirement now

        [Test]
        public void TestPasswordWithNoNumbers() 
        {
            String text = "nonumbersboo";
            Assert.False(Verification.verifyPassword(text));
        }

        [Test]
        public void TestPasswordwithNoLetters()
        {
            String text = "1234567";
            Assert.False(Verification.verifyPassword(text));
        }

    }

}
