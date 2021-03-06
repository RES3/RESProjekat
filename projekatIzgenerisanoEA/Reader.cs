///////////////////////////////////////////////////////////
//  Reader.cs
//  Implementation of the Class Reader
//  Generated by Enterprise Architect
//  Created on:      19-mar-2018 23.02.24
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using projekatRES3;

namespace projekatRES3 {
	public class Reader : IReader {

		public projekatRES3.Worker m_Worker;
        List<CollectionDescription> listFromWorker;

        public Reader(){ }

		~Reader(){ }

        public bool ReadDataFromBase(Code code, DateTime startTime, DateTime endTime)
        {
            m_Worker = new Worker();
            listFromWorker = new List<CollectionDescription>();
            listFromWorker = m_Worker.DataForReader(code);

            if(listFromWorker.Count == 0)
            {
                Console.WriteLine("File for {0} code is empty!", code);
                return false;
            }

            Console.WriteLine("List for {0} code:", code);
            foreach (CollectionDescription collD in listFromWorker)
            {
                if (collD.timeStamp >= startTime && collD.timeStamp <= endTime)
                {
                    Console.WriteLine(collD);
                }

            }
            return true;
        }
	}//end Reader

}//end namespace projekatRES3