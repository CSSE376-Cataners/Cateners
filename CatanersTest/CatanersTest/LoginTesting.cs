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
            Assert.False(Verification.verifyUsername(text));
        }

        [Test]
        public void checkUsernameSixChars()
        {
            String text = "Stever";
            Assert.True(Verification.verifyUsername(text));
        }

        [Test]
        public void checkUsernameNumbers()
        {
            String text = "Stever38";
            Assert.True(Verification.verifyUsername(text));
        }

        [Test]
        public void checkUsernameFourteenChars()
        {
            String text = "TheGodfather42";
            Assert.True(Verification.verifyUsername(text));
        }

        [Test]
        public void checkUsernameAllNumbers()
        {
            String text = "12345";
            Assert.True(Verification.verifyUsername(text));
        }

        [Test]
        public void checkUsernameSpace()
        {
            String text = "Redfox 345";
            Assert.False(Verification.verifyUsername(text));
        }

        [Test]
        public void checkUsernameLessThanFour()
        {
            String text = "Ram";
            Assert.False(Verification.verifyUsername(text));
        }

        [Test]
        public void checkUsernameMoreThanFifteen()
        {
            String text = "FullyRamblomatic";
            Assert.False(Verification.verifyUsername(text));
        }

        [Test]
        public void checkUsernameSpecialCharacters()
        {
            String text = "Trotta%^'54&";
            Assert.False(Verification.verifyUsername(text));
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
            String text = "Easypassword1!!";
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


        [Test]
        public void TestInstantiationOfLoginObject()
        {
            Login login = new Login("Username", "Password");
            Assert.NotNull(login.username);
            Assert.NotNull(login.password);
            Assert.False(login.register);


            login = new Login("Username", "Password",true);
            Assert.NotNull(login.username);
            Assert.NotNull(login.password);
            Assert.True(login.register);

            login = new Login("Username", "Password", false);
            Assert.NotNull(login.username);
            Assert.NotNull(login.password);
            Assert.False(login.register);
        }

        [Test]
        public void TestLoginJsonParsing()
        {
            Login login = new Login("Username", "Password", true);
            
            String correctJson = "{\"username\":\"" + login.username + "\",\"password\":\"" + login.password + "\",\"register\":" + login.register.ToString().ToLower() + "}";

            Assert.AreEqual(correctJson, login.toJson());

            Login correctLogin = new Login("Username", "Password", true);
            Assert.AreEqual(correctLogin, Login.fromJson(correctJson));
        }

    }

}
