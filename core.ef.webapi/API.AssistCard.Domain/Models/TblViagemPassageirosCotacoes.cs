using System;
using System.Collections.Generic;

namespace API.Viagem.Domain
{
    public partial class TblViagemPassageirosCotacoes
    {
        public int PctIdPassageiroCotacao { get; set; }
        public int PctCotIdCotacao { get; set; }
        public int PctPsgIdPassageiro { get; set; }
        public bool PctBolPassageiroPrincipal { get; set; }

        public virtual TblViagemCotacoes PctCotIdCotacaoNavigation { get; set; }
        public virtual TblViagemPassageiros PctPsgIdPassageiroNavigation { get; set; }
    }
}
