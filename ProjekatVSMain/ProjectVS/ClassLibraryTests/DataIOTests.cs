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
    public class DataIOTests
    {
        private DataIO dataIO;
        static object testNull = null;
        static List<string> testListstrings = new List<string> { "1", "2", "3", "4" }; 
        static string filenameNull = null;
        static string filenameOk = "Data1.xml";


        [OneTimeSetUp]
        public void SetupTest()
        {
            dataIO = new DataIO();
        }
        [Test]
        public void SerializeTestNotThrow()
        {
            Assert.DoesNotThrow(() => dataIO.SerializeObject<object>(testNull, filenameNull));
        }
        [Test]
        public void SerializeTestOk()
        {
            Assert.DoesNotThrow(() => dataIO.SerializeObject<List<string>>(testListstrings, filenameOk));
        }
        [Test]
        public void DeserializeTestNotThrow()
        {
            Assert.DoesNotThrow(() => dataIO.DeSerializeObject<List<string>>(filenameOk));
        }
        [Test]
        public void DeserializeTesThrow()
        {
            Assert.Catch(() => dataIO.DeSerializeObject<List<string>>("wrong"));
        }



    }
}
