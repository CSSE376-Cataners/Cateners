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
using WaveEngineGameProject;

namespace CatanersTest
{
    [TestFixture()]
    class BoardTesting
    {

        [Test]
        public void testRowGenerator()
        {
            MyScene testScene = new MyScene();
            float[][] testArray = new float[][] {new float[] {955, 500}, new float[] {955+40, 500}, new float[] {955+80, 500}};
            CollectionAssert.AreEqual(testArray, testScene.generatePositions());
        }
    }
}