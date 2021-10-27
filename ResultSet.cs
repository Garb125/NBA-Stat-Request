using System;
using System.Collections.Generic;
using System.Text;

namespace NBA_Stat_Request
{
    public class ResultSet
    {
        public string name { get; set; }
        public IList<string> headers { get; set; }
        public IList<IList<object>> rowSet { get; set; }
    }
}
