﻿using ClassLibrary;
using projekatRES3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Worker
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Worker : ILoadBalancerContractDuplexCallback,IWorker
    {
        public CollectionDescription m_CollectionDescription;
        public bool check = false;
        Dictionary<Code, int> pairs = new Dictionary<Code, int>();
        List<CollectionDescription> collectionDataset1 = new List<CollectionDescription>();
        List<CollectionDescription> collectionDataset2 = new List<CollectionDescription>();
        List<CollectionDescription> collectionDataset3 = new List<CollectionDescription>();
        List<CollectionDescription> collectionDataset4 = new List<CollectionDescription>();
        public static DataIO serializer = new DataIO();

        public bool ReceiveFromLoadBalancer(Code code, int value)
        {
            Console.WriteLine("Sa writera ja stigao kod {0} i vrednost {1}", code.ToString(), value);
            WorkerProperty wp = new WorkerProperty();
            m_CollectionDescription = new CollectionDescription();
            m_CollectionDescription.ID = new Random().Next(1000); //napraviti da se ne ponavlja

            wp.Code = code;
            wp.WorkerValue = value;

            m_CollectionDescription.m_HistoricalCollection.m_WorkerProperty[0] = wp; //zasto 0

            switch (code)
            {
                // naparaviti enum za dataset
                case Code.CODE_ANALOG:
                    m_CollectionDescription.Dataset = 1;
                    break;
                case Code.CODE_CONSUMER:
                    m_CollectionDescription.Dataset = 4;
                    break;
                case Code.CODE_CUSTOM:
                    m_CollectionDescription.Dataset = 2;
                    break;
                case Code.CODE_DIGITAL:
                    m_CollectionDescription.Dataset = 1;
                    break;
                case Code.CODE_LIMITSET:
                    m_CollectionDescription.Dataset = 2;
                    break;
                case Code.CODE_MULTIPLENODE:
                    m_CollectionDescription.Dataset = 3;
                    break;
                case Code.CODE_SINGLENOE:
                    m_CollectionDescription.Dataset = 3;
                    break;
                case Code.CODE_SOURCE:
                    m_CollectionDescription.Dataset = 4;
                    break;
            }

            if (CompareCodeValue(m_CollectionDescription.m_HistoricalCollection.m_WorkerProperty[0].Code, m_CollectionDescription.Dataset))
            {
                Logger.Log("\nReceiveFromLoadBalancer in Worker converted received data to CollectionDescription.\n");
                Logger.Log("CollectionDescription: " + m_CollectionDescription + "\n");
                Serialization(m_CollectionDescription);
            }
            else
            {
                Logger.Log("Validation of DataSet in Worker did not pass.\n");
                return false;
            }
            return true;
        }
        public bool CompareCodeValue(Code code, int dataset)
        {
            pairs = new Dictionary<Code, int>();
            pairs.Add(Code.CODE_ANALOG, 1);
            pairs.Add(Code.CODE_DIGITAL, 1);
            pairs.Add(Code.CODE_CUSTOM, 2);
            pairs.Add(Code.CODE_LIMITSET, 2);
            pairs.Add(Code.CODE_SINGLENOE, 3);
            pairs.Add(Code.CODE_MULTIPLENODE, 3);
            pairs.Add(Code.CODE_CONSUMER, 4);
            pairs.Add(Code.CODE_SOURCE, 4);

            foreach (KeyValuePair<Code, int> pair in pairs)
            {
                if (pair.Key == code)
                {
                    if (pair.Value == dataset)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool Deadband(CollectionDescription collection)
        {
            List<CollectionDescription> dataFromBase = null;
            if (collection == null)
            {
                throw new ArgumentNullException("Empty collection sent to chech deadband");
            }
            if (collection.m_HistoricalCollection.m_WorkerProperty[0].Code.Equals(Code.CODE_DIGITAL))
            {
                return true;
            }
            //uzmi podatke iz baza
            dataFromBase = Deserialization(collection.Dataset);
            

            if (dataFromBase.Count == 0)
                return true;

            bool secoundExist = true;
            bool answer = false;
            foreach (CollectionDescription item in dataFromBase)
            {
                if (item.m_HistoricalCollection.m_WorkerProperty[0].Code == collection.m_HistoricalCollection.m_WorkerProperty[0].Code)
                {
                    if (collection.m_HistoricalCollection.m_WorkerProperty[0].WorkerValue <= (item.m_HistoricalCollection.m_WorkerProperty[0].WorkerValue * 1.02) &&
                        collection.m_HistoricalCollection.m_WorkerProperty[0].WorkerValue >= item.m_HistoricalCollection.m_WorkerProperty[0].WorkerValue * 0.98)
                    {
                        //return false;
                        secoundExist = false;
                        continue;
                    }
                    else
                    {
                        answer = true;
                        secoundExist = false;
                        break;
                    }
                }
            }
            if(secoundExist)
            {
                answer = true;
            }

            return answer;
        }

        public bool Serialization(CollectionDescription collectionDescription)
        {
            if (collectionDescription == null)
            {
                Console.WriteLine("Collection is empty!");
                return false;
            }

            if (Deadband(collectionDescription))
            {
                collectionDescription.timeStamp = DateTime.Now;
                if (Serializer(collectionDescription))
                {
                    Logger.Log("\nCollection Description is serialized");
                    return true;
                }
                return false;
            }
            else
            {
                Logger.Log("\nCollection Desription has not passed DeadBand check.\nValue is 2% lower than old value.\n");
                return false;
            }


        }

        public bool Serializer(CollectionDescription collectionDescription)
        {
            if (collectionDescription == null)
            {
                Console.WriteLine("Collection is empty!");
                return false;
            }
         
            switch (collectionDescription.Dataset)
            {
                case 1:
                    collectionDataset1.Add(collectionDescription);
                    serializer.SerializeObject<List<CollectionDescription>>(collectionDataset1, "DataSet1.xml");
                    return true;
                case 2:
                    collectionDataset2.Add(collectionDescription);
                    serializer.SerializeObject<List<CollectionDescription>>(collectionDataset2, "DataSet2.xml");
                    return true;
                case 3:
                    collectionDataset3.Add(collectionDescription);
                    serializer.SerializeObject<List<CollectionDescription>>(collectionDataset3, "DataSet3.xml");
                    return true;
                case 4:
                    collectionDataset4.Add(collectionDescription);
                    serializer.SerializeObject<List<CollectionDescription>>(collectionDataset4, "DataSet4.xml");
                    return true;
                default:
                    return false;
            }
        }

        public List<CollectionDescription> Deserialization(int dataset)
        {
            List<CollectionDescription> tempList = new List<CollectionDescription>();

            switch (dataset)
            {
                case 1:
                    if (!File.Exists("DataSet1.xml"))
                    {
                        File.Create("DataSet1.xml").Dispose();
                        tempList = serializer.DeSerializeObject<List<CollectionDescription>>("DataSet1.xml");
                    }
                    else
                    {
                        tempList = serializer.DeSerializeObject<List<CollectionDescription>>("DataSet1.xml");
                    }
                    break;
                case 2:
                    if (!File.Exists("DataSet2.xml"))
                    {
                        File.Create("DataSet2.xml").Dispose();
                        tempList = serializer.DeSerializeObject<List<CollectionDescription>>("DataSet2.xml");
                    }
                    else
                    {
                        tempList = serializer.DeSerializeObject<List<CollectionDescription>>("DataSet2.xml");
                    }
                    break;
                case 3:
                    if (!File.Exists("DataSet3.xml"))
                    {
                        File.Create("DataSet3.xml").Dispose();
                        tempList = serializer.DeSerializeObject<List<CollectionDescription>>("DataSet3.xml");
                    }
                    else
                    {
                        tempList = serializer.DeSerializeObject<List<CollectionDescription>>("DataSet3.xml");
                    }
                    break;
                case 4:
                    if (!File.Exists("DataSet4.xml"))
                    {
                        File.Create("DataSet4.xml").Dispose();
                        tempList = serializer.DeSerializeObject<List<CollectionDescription>>("DataSet4.xml");
                    }
                    else
                    {
                        tempList = serializer.DeSerializeObject<List<CollectionDescription>>("DataSet4.xml");
                    }
                    break;
            }

            if (tempList == null)
            {
                tempList = new List<CollectionDescription>();
                return tempList;
            }

            List<CollectionDescription> temp = new List<CollectionDescription>();
            foreach (var item in tempList)
            {
                if (item.m_HistoricalCollection.m_WorkerProperty[0].Code != Code.CODE_DIGITAL)
                    temp.Add(item);
            }
            return temp;

        }

        public bool ReceiveTurnOnOff(bool turnOn)
        {
            Console.WriteLine("stigao callback sa loadbalancera");
            return true;
        }
    }
}
