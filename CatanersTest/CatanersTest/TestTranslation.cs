using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CatanersShared;

namespace CatanersTest
{
    [TestFixture]
    class TestTranslation
    {
        [Test]
        public void testTypeInializes()
        {
            new Translation();
            foreach (Translation.TYPE t in Enum.GetValues(typeof(Translation.TYPE))) { }

            Assert.Pass();
        }

        [Test]
        public void testIntArrayTranslation()
        {
            int[][] array = new int[][] { new int[] { 1, 2 }, new int[] { 3, 4 } };

            String s = Translation.intArraytwotoJson(array);

            int[][] newArray = Translation.jsonToIntArrayTwo(s);

            CollectionAssert.AreEqual(array, newArray);
        }
    }
}
