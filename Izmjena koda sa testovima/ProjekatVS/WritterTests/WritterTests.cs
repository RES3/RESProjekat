using ClassLibrary;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Writer;

namespace WritterTests
{
    [TestFixture]
    public class WritterTests
    {
        ILoadBalancerContract proxyMock = Substitute.For<ILoadBalancerContract>();
        private MainWindow writter;
        [OneTimeSetUp]
        public void SetUpTests()
        {
          //  writter = new MainWindow();
        }
      
    }
}
