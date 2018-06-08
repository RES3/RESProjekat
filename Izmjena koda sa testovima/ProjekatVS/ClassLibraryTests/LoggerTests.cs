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
    public class LoggerTests
    {
        private Logger logger;

        [OneTimeSetUp]
        public void SetupTest()
        {
            logger = new Logger();
        }
        [Test]
        public void LogTest()
        {
            Assert.DoesNotThrow(() => Logger.Log("logger"));
        }

    }
}
