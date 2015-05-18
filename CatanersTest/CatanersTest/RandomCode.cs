using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersTest
{
    [TestFixture()]
    public class RandomCode
    {
        [Test]
        public void random1()
        {
            int[] array = new int[] { 1, 2, 3, 4, 5, 6 };

            Console.WriteLine(array.Contains(1));
        }
    }
}
