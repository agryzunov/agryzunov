using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fixing
{
    public class ComboboxItem
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
