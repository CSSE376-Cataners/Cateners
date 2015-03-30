using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
//using Rhino.Mocks;
using CatanersShared;

namespace CatanersTest
{
    
    [TestFixture()]
    class LoginTesting
    {
        [Test]
        public void testWorkspace()
        {
            Assert.True(CatenersServer.ServerMain.testMethod());
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
