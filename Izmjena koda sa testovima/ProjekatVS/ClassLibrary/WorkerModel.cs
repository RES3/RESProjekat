using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    [DataContract]
    public class WorkerModel
    {
        private string ime;
        private bool activan;
        [DataMember]
        public string Ime { get => ime; set => ime = value; }
        [DataMember]
        public bool Activan { get => activan; set => activan = value; }

        public WorkerModel()
        {

        }
        public WorkerModel(string ime,bool aktivan)
        {
            Ime = ime;
            Activan = aktivan;
        }
    }
}
