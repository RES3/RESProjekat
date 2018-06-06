using ClassLibrary;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryTests
{
    [TestFixture]
    public class WorkingModelTests
    {
        private WorkerModel wm;

        [OneTimeSetUp]
        public void SetupTest()
        {
            wm = new WorkerModel();
        }
        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => new WorkerModel());
        }
        [Test]
        public void ConstructorParamTest()
        {
            string name = "w1";
            bool acktivan = true;
            Assert.DoesNotThrow(() => new WorkerModel(name,acktivan));
        }

        [Test]
        public void ImeSetPropertyTest()
        {
            string ime = "w1";
            wm.Ime = ime;
            Assert.AreEqual(wm.Ime, ime);
        }

        [Test]
        public void AktivanSetPropertyTest()
        {
            bool activan = true;
            wm.Activan = activan;
            Assert.AreEqual(wm.Activan, activan);
        }
    
    }
}
