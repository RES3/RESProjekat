using projekatRES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Reader
{
    class Program
    {
        static void Main(string[] args)
        {
            var binding = new NetTcpBinding();

            ServiceHost svc = new ServiceHost(typeof(ReaderHost));
            svc.Description.Name = "Reader";
            svc.AddServiceEndpoint(typeof(IReader),
                                    binding,
                                    new Uri("net.tcp://localhost:8050/Reader"));

            svc.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            svc.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });

            svc.Open();
            Console.WriteLine("Reader komponeta je pokrenuta");
            Console.ReadLine();
        }
    }
}
