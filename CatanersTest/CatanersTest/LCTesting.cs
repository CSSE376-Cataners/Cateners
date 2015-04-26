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
            LCTarget.assignRollNumbers();
            HexHolder[] hexList = (HexHolder[])typeof(LogicCenter).GetField("hexList", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(LCTarget);
            ArrayList rollList = new ArrayList();
            rollList.AddRange(new int[] {2, 3, 3, 4, 4, 5, 5, 6, 6, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12});
            for (int k = 0; k < 19; k++)
            {
                int rollNum = hexList[k].getRollNumber();
                if ((!rollList.Contains(rollNum)) || ((hexList[k].getHex().Name == "DesertHex") && (rollNum != 0)))
                {
                    Assert.True(false);
                }
                rollList.Remove(rollNum);
                Assert.True(2 <= rollNum && rollNum <= 12);
            }
        }
    }
}
