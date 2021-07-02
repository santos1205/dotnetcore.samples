using System;
using System.Collections.Generic;

namespace API.Viagem.Domain
{
    public partial class TblViagemDestino
    {
        public int DesIdDestino { get; set; }
        public string DesDescricaoDestino { get; set; }
        public bool DesBlnAtivo { get; set; }
    }
}
