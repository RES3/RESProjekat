using projekatRES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    [ServiceContract(CallbackContract = typeof(ILoadBalancerContractDuplexCallback))]
    public interface ILoadBalancerContract
    {
        //Writer metode
        [OperationContract]
        bool RequestForTurnOnOff(bool turnOn, string workerName);
        [OperationContract]
        bool WriteToLoadBalancer(Code code, int value);
        [OperationContract]
        List<WorkerModel> GetAllWorkers();

        //Worker metode
        [OperationContract ]
        bool Alive(string WorkerName);


    }
    public interface ILoadBalancerContractDuplexCallback
    {
        [OperationContract]
        bool ReceiveFromLoadBalancer(Code code, int value);
        [OperationContract]
        bool ReceiveTurnOnOff(bool turnOn);
       

    }
}
