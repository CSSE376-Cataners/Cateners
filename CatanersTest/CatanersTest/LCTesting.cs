using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CatanersShared;
using CatenersServer;
using System.Net.Sockets;
using System.Collections.Concurrent;
using System.Collections;
using System.IO;
using System.Threading;
using Rhino.Mocks;
using Newtonsoft.Json.Converters;
using Microsoft.QualityTools.Testing.Fakes;
using System.Reflection;
using WaveEngine;
using WaveEngineGame;
using WaveEngineGameProject;
using WaveEngine.Framework;

namespace CatanersTest
{
    [TestFixture()]
    class LCTesting
    {

        private MockRepository mocks = new MockRepository();

        [Test]
        public void testHexToShadow()
        {
            HexServer targetHolder = new HexServer(2);
            targetHolder.setPlacementNumber(3);
            targetHolder.setRollNumber(3);
            SettlementServer[] setArray = new SettlementServer[6];
            for (int k = 0; k < 6; k++)
            {
                setArray[k] = new SettlementServer(1, k);
            }
            targetHolder.setSettlementArray(setArray);
            Assert.AreEqual(targetHolder.toShadow(), new int[] { 2, 3, 3, 0, 1, 2, 3, 4, 5 });
        }

<<<<<<< HEAD
        /*public void TestResourceConstructor()
        {
            Resource wheat = new Resource("wheat");
            Assert.NotNull(wheat);
        }*/
=======
>>>>>>> origin/master
    }
}
