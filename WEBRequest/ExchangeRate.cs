using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBRequest
{
    public partial class ExchangeRate
    {
        public bool Success { get; set; }
        public Query Query { get; set; }
        public Info Info { get; set; }
        public bool Historical { get; set; }
        public DateTimeOffset Date { get; set; }
        public double Result { get; set; }
    }
}
