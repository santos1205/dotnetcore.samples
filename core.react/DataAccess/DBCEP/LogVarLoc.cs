using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class LogVarLoc
    {
        public int LocNu { get; set; }
        public int ValNu { get; set; }
        public string ValTx { get; set; }

        public LogLocalidade LocNuNavigation { get; set; }
    }
}
