using CatanersShared;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersTest
{
    [TestFixture()]
    public class EndTurnTesting
    {

        [Test]
        public void testConstructor()
        {
            EndTurn et = new EndTurn(1, EndTurn.Phase.GamePhase);

            Assert.AreEqual(1, et.playerTurn);
            Assert.AreEqual(EndTurn.Phase.GamePhase, et.phase);
        }
    }
}
