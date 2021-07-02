using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class LogBairro
    {
        public LogBairro()
        {
            LogFaixaBairro = new HashSet<LogFaixaBairro>();
            LogVarBai = new HashSet<LogVarBai>();
        }

        public int BaiNu { get; set; }
        public string UfeSg { get; set; }
        public int LocNu { get; set; }
        public string BaiNo { get; set; }
        public string BaiNoAbrev { get; set; }

        public LogLocalidade LocNuNavigation { get; set; }
        public ICollection<LogFaixaBairro> LogFaixaBairro { get; set; }
        public ICollection<LogVarBai> LogVarBai { get; set; }
    }
}
