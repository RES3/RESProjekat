using ClassLibrary;
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
    public class LoadBalancerPropertyTests
    {
        private LoadBalancerProperty lbp;

        [OneTimeSetUp]
        public void SetupTest()
        {
            lbp = new LoadBalancerProperty();
        }
        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => new LoadBalancerProperty());
        }

        [Test]
        public void CodePropertyTest()
        {
            Code c = Code.CODE_CUSTOM;
            lbp.Code = c;
            Assert.AreEqual(c, lbp.Code);
        }

        [Test]
        public void ValuePropertyTest()
        {
            int value = 100;
            lbp.Valuee = value;
            Assert.AreEqual(value, lbp.Valuee);
        }
    }
}
