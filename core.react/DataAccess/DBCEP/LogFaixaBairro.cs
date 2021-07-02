using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class LogFaixaBairro
    {
        public int BaiNu { get; set; }
        public string FcbCepIni { get; set; }
        public string FcbCepFim { get; set; }

        public LogBairro BaiNuNavigation { get; set; }
    }
}
