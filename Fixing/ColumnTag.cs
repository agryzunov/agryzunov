using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fixing
{

    public class ColumnTag
    {
        public string BrokerCode;
        public string ColumnType; // { BID="BID", ASK=1} ;
        public ColumnTag(string strBrokerCode, string strColumnType)
        {
            BrokerCode = strBrokerCode;
            ColumnType = strColumnType;
        }
    }
}
