using System;
using System.Collections.Generic;
using System.Text;

namespace NBA_Stat_Request
{
    public class Depot
    {
        public string resource { get; set; }
        public Parameters parameters { get; set; }
        public IList<ResultSet> resultSets { get; set; }

    }
}
