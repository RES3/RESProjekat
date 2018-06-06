using NUnit.Framework;
using projekatRES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryTests
{
    [TestFixture]
    public class WorkerPropertyTests
    {
        private WorkerProperty wp;

        [OneTimeSetUp]
        public void SetupTest()
        {
            wp = new WorkerProperty();
        }
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => new WorkerProperty());
        }
        [Test]
        public void ConstructorParamTest()
        {
            Code c = Code.CODE_CONSUMER;
            int value = 14000;
            Assert.DoesNotThrow(() => new WorkerProperty(c, value));
        }

        [Test]
        public void CodePropertyTest()
        {
            Code c = Code.CODE_CUSTOM;
            wp.Code = c;
            Assert.AreEqual(c, wp.Code);
        }

        [Test]
        public void ValuePropertyTest()
        {
            int value = 100;
            wp.WorkerValue = value;
            Assert.AreEqual(value, wp.WorkerValue);
        }

    }
}
