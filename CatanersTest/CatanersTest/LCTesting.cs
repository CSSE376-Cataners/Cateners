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

namespace CatanersTest
{
    [TestFixture()]
    class LCTesting
    {

        private MockRepository mocks = new MockRepository();

        [Test]
        public void testLogicCenterConstructorHexGeneration()
        {
            LogicCenter LCTarget = new LogicCenter(19);
            object hexNumber = typeof(LogicCenter).GetField("hexNumber", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(LCTarget);
            Assert.AreEqual(19, hexNumber);
            HexHolder[] hexList = (HexHolder[])typeof(LogicCenter).GetField("hexList", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(LCTarget);
            Assert.AreEqual(19, hexList.Length);
            for (int i = 0; i < 19; i++)
            {
                Assert.NotNull(hexList[i]);
            }
        }

        [Test]
        public void testNumberAssignment()
        {
            LogicCenter LCTarget = new LogicCenter(19);
            LCTarget.generateDefaultSettlements();
            SettlementHolder[] settlementList = (SettlementHolder[]) typeof(LogicCenter).GetField("settlementList", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(LCTarget);
            Assert.AreEqual(55, settlementList.Length);
        }
    }
}
