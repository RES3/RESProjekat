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
    public class CollectionDescriptionTest
    {
        private CollectionDescription cd;

        [OneTimeSetUp]
        public void SetupTest()
        {
            cd = new CollectionDescription();
        }
        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => new CollectionDescription());
        }

        [Test]
        public void DataSetPropertyTest()
        {
            int dataset = 100;
            cd.Dataset = dataset;
            Assert.AreEqual(dataset, cd.Dataset);
        }

        [Test]
        public void IdSetPropertyTest()
        {
            int Id = 100;
            cd.ID = Id;
            Assert.AreEqual(Id, cd.ID);
        }
      
        [Test]
        public void ToStringTest()
        {
            cd.m_HistoricalCollection.m_WorkerProperty[0] = new WorkerProperty();
            Assert.DoesNotThrow(() => cd.ToString());
        }

    }
}
