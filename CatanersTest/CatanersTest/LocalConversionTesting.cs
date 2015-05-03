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
using Cataners;
using WaveEngine.Framework;

namespace CatanersTest
{
    [TestFixture()]
    class LocalConversionTesting
    {
        private LocalConversion localConversion;
        private MockRepository mocks = new MockRepository();

        [SetUp]
        public void localConversionSetup()
        {
            localConversion = new LocalConversion();
        }

        [Test]
        public void testConstructor()
        {
            HexHolder[] testArray = (HexHolder[]) typeof(LocalConversion).GetField("hexList", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(localConversion);
            SettlementHolder[] testArray2 = (SettlementHolder[])typeof(LocalConversion).GetField("settlementList", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(localConversion);
            Assert.AreEqual(testArray.Length, 19);
            Assert.AreEqual(testArray2.Length, 54);
        }

        [Test]
        public void testGenerate()
        {
            int[][] inputArray = new int[1][] {new int[9] {0, 0, 12, 1, 4, 5, 7, 8, 12}};
            localConversion.generateHexList(inputArray);
            HexHolder[] testArray = (HexHolder[])typeof(LocalConversion).GetField("hexList", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(localConversion);
            SettlementHolder[] testArray2 = (SettlementHolder[])typeof(HexHolder).GetField("settlementList", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(testArray[0]);
            Assert.AreEqual(testArray[0].getPlacementNumber(), 0);
            Assert.AreEqual(testArray[0].getRollNumber(), 12);
            Assert.AreEqual(testArray[0].getHex().Name, "Forrest0");
            Assert.AreEqual(testArray2[0].getPlacementNumber(), 1);
            Assert.AreEqual(testArray2[1].getPlacementNumber(), 4);
            Assert.AreEqual(testArray2[0].getPlacementNumber(), 5);
            Assert.AreEqual(testArray2[0].getPlacementNumber(), 7);
            Assert.AreEqual(testArray2[0].getPlacementNumber(), 8);
            Assert.AreEqual(testArray2[0].getPlacementNumber(), 12);
        }
    }
}
