﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CatanersTest
{
    [TestFixture()]
    class TestMain
    {
        
        [Test]
        public void testWorkspace()
        {
            Assert.True(CatenersServer.ServerMain.testMethod());
        }
    }
}
