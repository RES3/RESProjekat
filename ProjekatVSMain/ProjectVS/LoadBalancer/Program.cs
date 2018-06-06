using ClassLibrary;
using projekatRES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var binding = new NetTcpBinding();

            ServiceHost svc = new ServiceHost(typeof(LoadBalancing));
            svc.Description.Name = "LoadBalancer";
            svc.AddServiceEndpoint(typeof(ILoadBalancerContract),
                                    binding,
                                    new Uri("net.tcp://localhost:8018/LoadBalancer"));

            svc.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            svc.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });

            svc.Open();
            LoadBalancing l = new LoadBalancing("RR");
            Console.WriteLine("Load balancer servis ja otvoren");
            Console.ReadLine();
           
        }
    }
}
