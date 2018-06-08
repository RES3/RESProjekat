using NSubstitute;
using NUnit.Framework;
using projekatRES3;
using Reader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderTests
{
    [TestFixture]
    public class ReaderTests
    {
        private ReaderHost rh;
        IReader proxyMock = Substitute.For<IReader>();

        [OneTimeSetUp]
        public void StartUptests()
        {
            rh = new ReaderHost();
            ReaderHost.Instance = Substitute.For<IReader>();
        }
        [Test]
        public void NullInstanceTest()
        {
            ReaderHost.reader = null;
            Assert.IsNotNull(ReaderHost.Instance);
        }

        [Test]
        public void DeserializationTestFileExiststest()
        {
            var list1 = rh.Deserialization(1);
            var list2 = rh.Deserialization(2);
            var list3 = rh.Deserialization(3);
            var list4 = rh.Deserialization(4);

            Assert.NotNull(list1);
            Assert.NotNull(list2);
            Assert.NotNull(list3);
            Assert.NotNull(list4);
        }
    }
}
