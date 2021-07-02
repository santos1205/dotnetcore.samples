using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class LogFaixaCpc
    {
        public int CpcNu { get; set; }
        public string CpcInicial { get; set; }
        public string CpcFinal { get; set; }

        public LogCpc CpcNuNavigation { get; set; }
    }
}
