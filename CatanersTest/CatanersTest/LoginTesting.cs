﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

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
    }
}
