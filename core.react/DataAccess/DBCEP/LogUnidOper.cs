using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class LogUnidOper
    {
        public LogUnidOper()
        {
            LogFaixaUop = new HashSet<LogFaixaUop>();
        }

        public int UopNu { get; set; }
        public string UfeSg { get; set; }
        public int LocNu { get; set; }
        public int BaiNu { get; set; }
        public int? LogNu { get; set; }
        public string UopNo { get; set; }
        public string UopEndereco { get; set; }
        public string Cep { get; set; }
        public string UopInCp { get; set; }
        public string UopNoAbrev { get; set; }

        public LogLocalidade LocNuNavigation { get; set; }
        public ICollection<LogFaixaUop> LogFaixaUop { get; set; }
    }
}
