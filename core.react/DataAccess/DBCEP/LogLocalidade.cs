using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class LogLocalidade
    {
        public LogLocalidade()
        {
            LogBairro = new HashSet<LogBairro>();
            LogCpc = new HashSet<LogCpc>();
            LogFaixaLocalidade = new HashSet<LogFaixaLocalidade>();
            LogLogradouro = new HashSet<LogLogradouro>();
            LogUnidOper = new HashSet<LogUnidOper>();
            LogVarLoc = new HashSet<LogVarLoc>();
        }

        public int LocNu { get; set; }
        public string UfeSg { get; set; }
        public string LocNo { get; set; }
        public string Cep { get; set; }
        public string LocInSit { get; set; }
        public string LocInTipo { get; set; }
        public int? LocNuSub { get; set; }
        public string LocNoAbrev { get; set; }
        public int? MunNu { get; set; }

        public ICollection<LogBairro> LogBairro { get; set; }
        public ICollection<LogCpc> LogCpc { get; set; }
        public ICollection<LogFaixaLocalidade> LogFaixaLocalidade { get; set; }
        public ICollection<LogLogradouro> LogLogradouro { get; set; }
        public ICollection<LogUnidOper> LogUnidOper { get; set; }
        public ICollection<LogVarLoc> LogVarLoc { get; set; }
    }
}
