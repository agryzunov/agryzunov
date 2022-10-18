using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fixing
{
    public class BrokerItem
    {
        public string Code;
        public string Name;
        public string Description;
        public BrokerItem(string strCode, string strName, string strDescription)
        {
            this.Name = strName;
            this.Code = strCode;
            this.Description = strDescription;
        }
    }
}
