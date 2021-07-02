using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class LogCpc
    {
        public LogCpc()
        {
            LogFaixaCpc = new HashSet<LogFaixaCpc>();
        }

        public int CpcNu { get; set; }
        public string UfeSg { get; set; }
        public int LocNu { get; set; }
        public string CpcNo { get; set; }
        public string CpcEndereco { get; set; }
        public string Cep { get; set; }

        public LogLocalidade LocNuNavigation { get; set; }
        public ICollection<LogFaixaCpc> LogFaixaCpc { get; set; }
    }
}
