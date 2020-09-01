using System;
using System.Collections.Generic;

namespace API.Viagem.Domain
{
    public partial class TblViagemNacionalidade
    {
        public int NacIdNacionalidade { get; set; }
        public string NacDescricao { get; set; }
        public bool NacBlnAtivo { get; set; }
    }
}
