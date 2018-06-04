using projekatRES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Writer writer = new Writer();
            LoadBalancer loadBalancer = new LoadBalancer();
            writer.GenerateData();
            loadBalancer.SendToWorker();

            Console.ReadKey();
        }
    }
}
