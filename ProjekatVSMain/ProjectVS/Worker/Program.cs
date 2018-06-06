using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Worker
{
     class Program
    {
        private static InstanceContext instanceContext = new InstanceContext(new Worker());
        public static ILoadBalancerContract proxy = new DuplexChannelFactory<ILoadBalancerContract>(instanceContext,new NetTcpBinding(),
          new EndpointAddress("net.tcp://localhost:8018/LoadBalancer")).CreateChannel();
        public static string name;

        static void Main(string[] args)
        {
            Console.WriteLine("Unesite ime workera");
            name = Console.ReadLine();
            while (true)
            {
                try
                {
                    bool result = proxy.Alive(name);
                    if (result == true)
                    break;
                    else
                    {
                        Console.WriteLine("Ime vec postoji. Unesite novo ime");
                        name = Console.ReadLine();
                    }
                }
                catch (Exception)
                {
                    Thread.Sleep(2000);
                    RecreateChannel();
                }
            }
            Console.ReadLine();

        }
        public static void RecreateChannel()
        {
            proxy = new DuplexChannelFactory<ILoadBalancerContract>(instanceContext, new NetTcpBinding(),
           new EndpointAddress("net.tcp://localhost:8018/LoadBalancer")).CreateChannel();
        }
    }
}
