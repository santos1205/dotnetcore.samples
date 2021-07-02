using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class LogNumSec
    {
        public int LogNu { get; set; }
        public string SecNuIni { get; set; }
        public string SecNuFim { get; set; }
        public string SecInLado { get; set; }

        public LogLogradouro LogNuNavigation { get; set; }
    }
}
