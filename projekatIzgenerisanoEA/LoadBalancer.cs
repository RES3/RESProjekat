///////////////////////////////////////////////////////////
//  LoadBalancer.cs
//  Implementation of the Class LoadBalancer
//  Generated by Enterprise Architect
//  Created on:      19-mar-2018 23.02.24
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using projekatRES3;
using ClassLibrary;

namespace projekatRES3 {
	public class LoadBalancer : ILoadBalancer {

		public List<Worker> m_Worker;
        public List<LoadBalancerProperty> tempList;
        Worker worker;

		public LoadBalancer(){
            tempList = new List<LoadBalancerProperty>();
            m_Worker = new List<Worker>(4);
            worker = new Worker();
		}

		~LoadBalancer(){

		}

        public bool ReceiveFromWriter(Code code, int value)
        {
            //tempList.Add(new LoadBalancerProperty() { Code = code, Valuee = value});
            //poslati Workeru
            //tempList = new List<LoadBalancerProperty>();
            
            try
            {
                tempList.Add(new LoadBalancerProperty() { Code = code, Valuee = value });
                
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool SendToWorker()
        {
            //u ovoj metodi cemo raditi i rasporedjivanja po workerima
            Code c;
            int v;
            try
            {
                c = tempList[0].Code;
                v = tempList[0].Valuee;
                worker.ReceiveFromLoadBalancer(c,v);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
            
        }

    }//end LoadBalancer

}//end namespace projekatRES3