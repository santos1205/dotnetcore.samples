using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class LogVarBai
    {
        public int BaiNu { get; set; }
        public int VdbNu { get; set; }
        public string VdbTx { get; set; }

        public LogBairro BaiNuNavigation { get; set; }
    }
}
