using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class LogFaixaUop
    {
        public int UopNu { get; set; }
        public int FncInicial { get; set; }
        public int FncFinal { get; set; }

        public LogUnidOper UopNuNavigation { get; set; }
    }
}
