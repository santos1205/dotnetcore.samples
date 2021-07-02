using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class LogVarLog
    {
        public int LogNu { get; set; }
        public int VloNu { get; set; }
        public string TloTx { get; set; }
        public string VloTx { get; set; }

        public LogLogradouro LogNuNavigation { get; set; }
    }
}
