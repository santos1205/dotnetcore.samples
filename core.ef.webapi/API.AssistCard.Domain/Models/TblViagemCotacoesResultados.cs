using System;
using System.Collections.Generic;

namespace API.Viagem.Domain
{
    public partial class TblViagemCotacoesResultados
    {
        public TblViagemCotacoesResultados()
        {
            TblViagemCotacoesResultadosProdutos = new HashSet<TblViagemCotacoesResultadosProdutos>();
        }

        public int RecIdResultado { get; set; }
        public int RecCotIdCotacao { get; set; }
        public DateTime RecDtResultado { get; set; }

        public virtual TblViagemCotacoes RecCotIdCotacaoNavigation { get; set; }
        public virtual ICollection<TblViagemCotacoesResultadosProdutos> TblViagemCotacoesResultadosProdutos { get; set; }
    }
}
