using ClassLibrary;
using LoadBalancer;
using NSubstitute;
using NUnit.Framework;
using projekatRES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancerTests
{
    [TestFixture]
    public class LoadBalancingTest
    {
        private LoadBalancing loadBalancer;
        ILoadBalancerContractDuplexCallback callback = Substitute.For<ILoadBalancerContractDuplexCallback>();

        [OneTimeSetUp]
        public void SetUpTest()
        {
            loadBalancer = new LoadBalancing();
            LoadBalancing.Instance = Substitute.For<ILoadBalancerContract>();
            LoadBalancing.Instance.Alive("").Returns(false);
            LoadBalancing.Instance.RequestForTurnOnOff(true, "w1").Returns(true);
            LoadBalancing.Instance.RequestForTurnOnOff(false, "w1").Returns(true);
            LoadBalancing.Instance.RequestForTurnOnOff(true, "w2").Returns(false);
        }
        [Test]
        public void ConstructorWithParamsRR()
        {
            string s = "string";
            LoadBalancing.allWorkers.Add("w1", true);
            LoadBalancing.allWorkers.Add("w3", false);
            LoadBalancing.workers.Add("w1", callback);
            LoadBalancing.tempList.Add(new LoadBalancerProperty());
            Assert.DoesNotThrow(() => new LoadBalancing(s));
        }

        [Test]
        public void AliveTest()
        {
            bool result = loadBalancer.Alive("");
            Assert.IsFalse(result);
        }
        [Test]
        public void RequestForTurnOnOffTestTrue()
        {
            bool result = loadBalancer.RequestForTurnOnOff(true, "w1");
            Assert.IsTrue(result);
        }
        [Test]
        public void RequestForTurnOnOffTestFalse()
        {
            bool result = loadBalancer.RequestForTurnOnOff(false, "w1");
            Assert.IsTrue(result);
        }
        [Test]
        public void NullInstanceTest()
        {
            LoadBalancing.lb = null;
            Assert.IsNotNull(LoadBalancing.Instance);
        }
        [Test]
        public void GetAllWorkers()
        {
            List<WorkerModel> pom = new List<WorkerModel>();
            LoadBalancing.allWorkers.Add("w4", true);
            pom = loadBalancer.GetAllWorkers();
            Assert.IsNotEmpty(pom);
        }
        [Test]
        public void WriteToLoadBalancerTest()
        {
            LoadBalancing.tempList = new List<LoadBalancerProperty>();
            bool result = loadBalancer.WriteToLoadBalancer(Code.CODE_ANALOG, 100);
            Assert.IsTrue(result);
        }
        [Test]
        public void WriteToLoadBalancerExceptionTest()
        {
            LoadBalancing.tempList = null;
            bool result = loadBalancer.WriteToLoadBalancer(Code.CODE_ANALOG, 100);
            Assert.IsFalse(result);
        }
        //[Test]
        //public void RoundRobinTest()
        //{
        //    LoadBalancing.tempList = null;
        //    bool result = loadBalancer.WriteToLoadBalancer(Code.CODE_ANALOG, 100);
        //    Assert.IsFalse(result);
        //}
    }
}
