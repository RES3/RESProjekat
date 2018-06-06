using ClassLibrary;
using projekatRES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Writer
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class WriterCallBack : ILoadBalancerContractDuplexCallback
    {
        public bool ReceiveFromLoadBalancer(Code code, int value)
        {
            throw new NotImplementedException();
        }

        public bool ReceiveTurnOnOff(bool turnOn)
        {
            throw new NotImplementedException();
        }
    }
}
