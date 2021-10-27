using System;
using System.Collections.Generic;
using System.Text;

namespace NBA_Stat_Request
{
    public class DataSet
    {
        public Depot response { get; set; }

        //I want to figure out why this is wrong. 
        //Why doesn't response function as an instance of depot? and dataset.response
        //give access to Depot's methods?
    }

}
