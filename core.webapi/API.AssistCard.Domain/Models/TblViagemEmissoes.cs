using System;
using System.Collections.Generic;

namespace API.Viagem.Domain
{
    public partial class TblViagemEmissoes
    {
        public int EmiIdEmissao { get; set; }
        public int EmiCotIdCotacao { get; set; }
        public int EmiRprIdResultadoProduto { get; set; }
        public bool EmiPagamentoComCartao { get; set; }
        public byte EmiBandeiraCartao { get; set; }
        public string EmiDocumentoCartao { get; set; }
        public string EmiNumeroCartao { get; set; }
        public string EmiValidadeCartao { get; set; }
        public byte EmiParcelas { get; set; }
        public bool EmiPlanoFamiliar { get; set; }
        public string EmiNomeCartao { get; set; }
        public string EmiNomeUsuarioEmissao { get; set; }
        public bool EmiStatus { get; set; }
        public bool EmiBlnTransmitidoMultiSeguro { get; set; }
        public DateTime EmiDtEmissao { get; set; }
        public int EmiOrpIdOrigemParceiro { get; set; }

        public virtual TblViagemCotacoes EmiCotIdCotacaoNavigation { get; set; }
        public virtual TblViagemEmissoesRetorno TblViagemEmissoesRetorno { get; set; }
    }
}
