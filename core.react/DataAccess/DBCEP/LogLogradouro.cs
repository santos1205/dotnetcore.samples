using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class LogLogradouro
    {
        public LogLogradouro()
        {
            LogGrandeUsuario = new HashSet<LogGrandeUsuario>();
            LogVarLog = new HashSet<LogVarLog>();
        }

        public int LogNu { get; set; }
        public string UfeSg { get; set; }
        public int LocNu { get; set; }
        public int BaiNuIni { get; set; }
        public int? BaiNuFim { get; set; }
        public string LogNo { get; set; }
        public string LogComplemento { get; set; }
        public string Cep { get; set; }
        public string TloTx { get; set; }
        public string LogStaTlo { get; set; }
        public string LogNoAbrev { get; set; }

        public LogLocalidade LocNuNavigation { get; set; }
        public LogNumSec LogNumSec { get; set; }
        public ICollection<LogGrandeUsuario> LogGrandeUsuario { get; set; }
        public ICollection<LogVarLog> LogVarLog { get; set; }
    }
}
