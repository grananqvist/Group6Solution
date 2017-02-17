using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentApplication.MarketObjects
{
    class StockInfo
    {
        public long id { get; set; }
        public string t { get; set; }
        public string e { get; set; }
        public double l { get; set; }
        public double l_fix { get; set; }
        public string l_cur { get; set; }
        public int s { get; set; }
        public string ltt { get; set; }
        public string lt { get; set; }
        public string lt_dts { get; set; }
        public double c { get; set; }
        public double c_fix { get; set; }
        public double cp { get; set; }
        public double cp_fix { get; set; }
        public string chr { get; set; }
        public double pcls_fix { get; set; }


    }
}
