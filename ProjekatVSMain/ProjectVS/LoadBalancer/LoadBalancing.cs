
using ClassLibrary;
using projekatRES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoadBalancer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class LoadBalancing : ILoadBalancerContract

    {
        public object templistLocker = new object();
        public object dictAllWorckerLocker = new object();
        public static List<LoadBalancerProperty> tempList = new List<LoadBalancerProperty>();
        public static Dictionary<string, bool> allWorkers = new Dictionary<string, bool>();
        private static Dictionary<string, ILoadBalancerContractDuplexCallback> workers = new Dictionary<string, ILoadBalancerContractDuplexCallback>();
        public RoundRobinList<ILoadBalancerContractDuplexCallback> rrlist;
        public LoadBalancing()
        {
        }

        public LoadBalancing(string s)
        {
            Thread t = new Thread(RoundRobin);
            t.Start();
        }

        //Writer metode
        public bool RequestForTurnOnOff(bool turnOn, string workerName)
        {
            try
            {
                if (turnOn == true)
                {
                    lock (dictAllWorckerLocker)
                    {
                        allWorkers[workerName] = true;
                    }
                }
                else
                {
                    lock (dictAllWorckerLocker)
                    {
                        allWorkers[workerName] = false;
                    }
                   
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool WriteToLoadBalancer(Code code, int value)
        {
            Console.WriteLine("Sa writera ja stigao kod {0} i vrednost {1}", code.ToString(), value);
            try
            {
                lock (templistLocker)
                {
                    tempList.Add(new LoadBalancerProperty() { Code = code, Valuee = value });
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Worker metode
        public bool Alive(string WorkerName)
        {
            lock (dictAllWorckerLocker)
            {
                if (!allWorkers.Keys.Contains(WorkerName))
                {
                    allWorkers.Add(WorkerName, true);
                    ILoadBalancerContractDuplexCallback callback = OperationContext.Current.GetCallbackChannel<ILoadBalancerContractDuplexCallback>();
                    try
                    {
                        lock (dictAllWorckerLocker)
                        {
                            workers.Add(WorkerName, callback);
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    callback.ReceiveTurnOnOff(true);

                    return true;
                }
            }
            return false;
        }

        public void RoundRobin()
        {
           // Thread.Sleep(30000);
            List<ILoadBalancerContractDuplexCallback> callbacks = new List<ILoadBalancerContractDuplexCallback>();

            while (true)
            {
                callbacks.Clear();
                lock (dictAllWorckerLocker)
                {
                    foreach (string nameOfWorker in workers.Keys)
                    {
                        if (allWorkers[nameOfWorker] == true)
                            callbacks.Add(workers[nameOfWorker]);
                    }
                }
                if (callbacks.Count != 0)
                {
                    rrlist = new RoundRobinList<ILoadBalancerContractDuplexCallback>(callbacks);
                }
                else
                {
                    Thread.Sleep(2000);
                    continue;
                }
                foreach (var item in callbacks)
                {
                    lock (templistLocker)
                    {
                        if (tempList.Count > 0)
                        {
                            rrlist.Next().ReceiveFromLoadBalancer(tempList[0].Code, tempList[0].Valuee);
                            tempList.RemoveAt(0);
                        }
                    }
                    Thread.Sleep(30);
                }
              Thread.Sleep(10);
            }
        }

        public List<WorkerModel> GetAllWorkers()
        {
            List<WorkerModel> pom = new List<WorkerModel>();
            foreach (var item in allWorkers.Keys)
            {
                pom.Add(new WorkerModel() { Ime = item, Activan = allWorkers[item] });
            }
            return pom;
        }


    }
}
