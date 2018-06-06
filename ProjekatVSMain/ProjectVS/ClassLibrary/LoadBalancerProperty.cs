using projekatRES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class LoadBalancerProperty
    {
        private Code code;
        private int valuee;

        public LoadBalancerProperty() { }

        public Code Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }

        public int Valuee
        {
            get
            {
                return valuee;
            }
            set
            {
                valuee = value;
            }
        }
    }
}
