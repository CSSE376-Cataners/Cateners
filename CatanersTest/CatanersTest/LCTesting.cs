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
            SettlementHolder[] settlementList = (SettlementHolder[])typeof(LogicCenter).GetField("settlementList", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(LCTarget);
            Assert.AreEqual(54, settlementList.Length);
            for (int i = 0; i < 54; i++)
            {
                Assert.IsNotNull(settlementList[i]);
                Assert.IsInstanceOf(typeof(SettlementHolder), settlementList[i]);

            }
        }

        [Test]
        public void testAssignSettlements()
        {
            LogicCenter LCTarget = new LogicCenter(19);
            LCTarget.assignSettlements();
            int[][] testArray = new int[19][];
            testArray[0] = new int[6] { 1, 4, 5, 8, 9, 13 };
            testArray[1] = new int[6] { 2, 5, 6, 9, 10, 14 };
            testArray[2] = new int[6] { 3, 6, 7, 10, 11, 15 };
            testArray[3] = new int[6] { 8, 12, 13, 17, 18, 23 };
            testArray[4] = new int[6] { 9, 13, 14, 18, 19, 24 };
            testArray[5] = new int[6] { 10, 14, 15, 19, 20, 25 };
            testArray[6] = new int[6] { 11, 15, 16, 20, 21, 26 };
            testArray[7] = new int[6] { 17, 22, 23, 28, 29, 34 };
            testArray[8] = new int[6] { 18, 23, 24, 29, 30, 35 };
            testArray[9] = new int[6] { 19, 24, 25, 30, 31, 36 };
            testArray[10] = new int[6] { 20, 25, 26, 31, 31, 37 };
            testArray[11] = new int[6] { 21, 26, 27, 32, 33, 38 };
            testArray[12] = new int[6] { 29, 34, 35, 39, 40, 44 };
            testArray[13] = new int[6] { 30, 35, 36, 40, 41, 45 };
            testArray[14] = new int[6] { 31, 36, 37, 41, 42, 46 };
            testArray[15] = new int[6] { 32, 37, 38, 42, 43, 47 };
            testArray[16] = new int[6] { 40, 44, 45, 48, 49, 52 };
            testArray[17] = new int[6] { 41, 45, 46, 49, 50, 53 };
            testArray[18] = new int[6] { 42, 46, 47, 50, 51, 54 };
            HexHolder[] hexList = (HexHolder[])typeof(LogicCenter).GetField("hexList", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(LCTarget);
            for (int i = 0; i < 19; i++)
            {
                Assert.NotNull(hexList[i].getSettlementList());
                SettlementHolder[] currList = hexList[i].getSettlementList();
                Assert.AreEqual(currList.Length, 6);
                Assert.AreEqual(hexList[i].getSettlementList(), testArray[i]);
            }
        }
    }
}
