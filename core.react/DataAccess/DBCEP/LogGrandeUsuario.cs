using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class LogGrandeUsuario
    {
        public int GruNu { get; set; }
        public string UfeSg { get; set; }
        public int LocNu { get; set; }
        public int BaiNu { get; set; }
        public int? LogNu { get; set; }
        public string GruNo { get; set; }
        public string GruEndereco { get; set; }
        public string Cep { get; set; }
        public string GruNoAbrev { get; set; }

        public LogLogradouro LocNuNavigation { get; set; }
    }
}
