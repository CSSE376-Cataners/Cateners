using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
<<<<<<< HEAD
using Rhino.Mocks;
using Cataners;
=======
//using Rhino.Mocks;
using CatanersShared;
>>>>>>> 874b19a617e04f68e775f49078e8d383a0c0d995

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

        [Test]
        
        public void checkStringValid()
            //testing if string (e.g. username is valid)
        {

            String text = "username78";
            Assert.True(Verification.verify(text));


        }

    }

}
