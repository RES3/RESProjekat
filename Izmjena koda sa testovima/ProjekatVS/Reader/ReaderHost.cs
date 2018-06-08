using ClassLibrary;
using projekatRES3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader
{
    public class ReaderHost : IReader
    {
        public static DataIO serializer = new DataIO();
        public static IReader reader;
        public static string path = "C:\\Users\\Dina\\Desktop\\RES Final\\ProjekatVS\\Worker\\bin\\Debug\\";

        public static IReader Instance
        {
            get
            {
                if (reader == null)
                {
                    reader = new ReaderHost();
                }

                return reader;
            }
            set
            {
                if (reader == null)
                {
                    reader = value;
                }
            }
        }
        public List<CollectionDescription> Deserialization(int dataSet)
        {
            List<CollectionDescription> tempList = new List<CollectionDescription>();

            switch (dataSet)
            {
                case 1:
                    if (!File.Exists(path+"DataSet1.xml"))
                    {
                        File.Create(path + "DataSet1.xml").Dispose();
                        tempList = serializer.DeSerializeObject<List<CollectionDescription>>(path + "DataSet1.xml");
                    }
                    else
                    {
                        tempList = serializer.DeSerializeObject<List<CollectionDescription>>(path + "DataSet1.xml");
                    }
                    break;
                case 2:
                    if (!File.Exists(path + "DataSet2.xml"))
                    {
                        File.Create(path + "DataSet2.xml").Dispose();
                        tempList = serializer.DeSerializeObject<List<CollectionDescription>>(path+"DataSet2.xml");
                    }
                    else
                    {
                        tempList = serializer.DeSerializeObject<List<CollectionDescription>>(path + "DataSet2.xml");
                    }
                    break;
                case 3:
                    if (!File.Exists(path + "DataSet3.xml"))
                    {
                        File.Create(path + "DataSet3.xml").Dispose();
                        tempList = serializer.DeSerializeObject<List<CollectionDescription>>(path + "DataSet3.xml");
                    }
                    else
                    {
                        tempList = serializer.DeSerializeObject<List<CollectionDescription>>(path + "DataSet3.xml");
                    }
                    break;
                case 4:
                    if (!File.Exists(path + "DataSet4.xml"))
                    {
                        File.Create(path + "DataSet4.xml").Dispose();
                        tempList = serializer.DeSerializeObject<List<CollectionDescription>>(path + "DataSet4.xml");
                    }
                    else
                    {
                        tempList = serializer.DeSerializeObject<List<CollectionDescription>>(path + "DataSet4.xml");
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

        public List<CollectionDescription> GetDataSetForTimePeriod(Code c, DateTime satrtTime, DateTime endTime)
        {
            throw new NotImplementedException();
        }

    }
}
