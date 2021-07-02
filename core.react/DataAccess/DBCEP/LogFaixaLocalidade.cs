using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class LogFaixaLocalidade
    {
        public int LocNu { get; set; }
        public string LocCepIni { get; set; }
        public string LocCepFim { get; set; }

        public LogLocalidade LocNuNavigation { get; set; }
    }
}
