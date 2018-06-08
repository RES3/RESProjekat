using ClassLibrary;
using NSubstitute;
using NUnit.Framework;
using projekatRES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker;

namespace WorkerTests
{
    [TestFixture]
    public class WorkerTests
    {
        private Worker.Worker worker;
        ILoadBalancerContractDuplexCallback callback = Substitute.For<ILoadBalancerContractDuplexCallback>();
        IReader proxyMock = Substitute.For<IReader>();
        
        [OneTimeSetUp]
        public void SetUpTest()
        {
            worker = new Worker.Worker();
            Worker.Worker.Instance = Substitute.For<IWorker>();
            Worker.Worker.Instance.ReceiveFromLoadBalancer(Code.CODE_ANALOG, 100).Returns(true);
        }

        [Test]
        public void NullInstanceTest()
        {
            Worker.Worker.worker = null;
            Assert.IsNotNull(Worker.Worker.Instance);
        }

        [Test]
        public void ReceiveFromLoadBalancerTest()
        {
            //Laziranje podataka koriscenjem NSubstitute biblioteke 
            proxyMock.Deserialization(100).ReturnsForAnyArgs(new List<CollectionDescription>());
            Worker.Worker.proxy = proxyMock;
            bool resultAnalog = worker.ReceiveFromLoadBalancer(Code.CODE_ANALOG, 100);
            bool resultDigital = worker.ReceiveFromLoadBalancer(Code.CODE_DIGITAL, 100);
            bool resultCustom = worker.ReceiveFromLoadBalancer(Code.CODE_CUSTOM, 100);
            bool resultConsumer = worker.ReceiveFromLoadBalancer(Code.CODE_CONSUMER, 100);
            bool resultLimiset = worker.ReceiveFromLoadBalancer(Code.CODE_LIMITSET, 100);
            bool resultSingleNoe = worker.ReceiveFromLoadBalancer(Code.CODE_SINGLENOE, 100);
            bool resultSource = worker.ReceiveFromLoadBalancer(Code.CODE_SOURCE, 100);
            bool resultMulty = worker.ReceiveFromLoadBalancer(Code.CODE_MULTIPLENODE, 100);

            Assert.IsTrue(resultAnalog);
            Assert.IsTrue(resultDigital);
            Assert.IsTrue(resultCustom);
            Assert.IsTrue(resultConsumer);
            Assert.IsTrue(resultLimiset);
            Assert.IsTrue(resultSingleNoe);
            Assert.IsTrue(resultSource);
            Assert.IsTrue(resultMulty);

        }
        [Test]
        public void CompareCodeValue()
        {

        }
        [Test]
        public void Deadband()
        {
            List<CollectionDescription> test = new List<CollectionDescription>();
            WorkerProperty wp = new WorkerProperty(Code.CODE_ANALOG,1000);
            WorkerProperty wp1 = new WorkerProperty(Code.CODE_ANALOG, 1500);
            WorkerProperty wp2 = new WorkerProperty(Code.CODE_DIGITAL, 2000);
                
            HistoricalCollection hc = new HistoricalCollection();
            hc.m_WorkerProperty[0] = wp;
            HistoricalCollection hc1 = new HistoricalCollection();
            hc1.m_WorkerProperty[0] = wp1;
            HistoricalCollection hc2 = new HistoricalCollection();
            hc2.m_WorkerProperty[0] = wp2;

            test.Add(new CollectionDescription() { Dataset = 1,m_HistoricalCollection = hc});
            test.Add(new CollectionDescription() { Dataset = 1, m_HistoricalCollection = hc1 });
            test.Add(new CollectionDescription() { Dataset = 1, m_HistoricalCollection = hc2 });


            proxyMock.Deserialization(1).ReturnsForAnyArgs(test);
            Worker.Worker.proxy = proxyMock;
            bool resultAnalog = worker.ReceiveFromLoadBalancer(Code.CODE_ANALOG, 1000);
            Assert.IsTrue(resultAnalog);

        }
        [Test]
        public void SerializationFalse()
        {
            bool result = worker.Serialization(null);
            Assert.IsFalse(result);

        }
        [Test]
        public void SerializerFalse()
        {
            bool result = worker.Serializer(null);
            Assert.IsFalse(result);
        }


    }
}
