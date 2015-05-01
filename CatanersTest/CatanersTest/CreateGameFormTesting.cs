using Cataners;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CatanersTest
{
    [TestFixture()]
    public class CreateGameFormTesting
    {
        [Test]
        public void testConstructor()
        {
            MainGui gui = new MainGui();
            CreateGameForm cgf = new CreateGameForm(gui);

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo cgfParrent = typeof(CreateGameForm).GetField("parent", flags);

            Assert.NotNull(cgf);
            Assert.NotNull(cgfParrent.GetValue(cgf));

        }
    }
}
