using System;
using System.Collections.Generic;

namespace API.Viagem.Domain
{
    public partial class TblViagemCotacoes
    {
        public TblViagemCotacoes()
        {
            TblViagemPassageirosCotacoes = new HashSet<TblViagemPassageirosCotacoes>();
        }

        public int CotIdCotacao { get; set; }
        public int CotIdCorretor { get; set; }
        public int CotIdEstipulante { get; set; }
        public int CotIdPdv { get; set; }
        public DateTime CotDtCotacao { get; set; }
        public int CotDesIdDestino { get; set; }
        public DateTime CotDtPartida { get; set; }
        public DateTime CotDtRetorno { get; set; }
        public string CotNomeUsuarioEmissao { get; set; }
        public int CotFpgIdFormaPagamento { get; set; }
        public int CotOrpIdOrigemParceiro { get; set; }

        public virtual TblViagemCotacoesResultados TblViagemCotacoesResultados { get; set; }
        public virtual TblViagemEmissoes TblViagemEmissoes { get; set; }
        public virtual ICollection<TblViagemPassageirosCotacoes> TblViagemPassageirosCotacoes { get; set; }
    }
}
