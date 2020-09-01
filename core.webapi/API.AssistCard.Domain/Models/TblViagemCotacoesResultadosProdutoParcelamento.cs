using System.Collections.Generic;

namespace API.Viagem.Domain
{
    public partial class TblViagemCotacoesResultadosProdutoParcelamento
    {
        public int RppIdParcelamentoProduto { get; set; }
        public int RppRprIdResultadoProduto { get; set; }
        public int RppNumeroParcelas { get; set; }
        public string RppBandeiraCartao { get; set; }
        public string RppFactorAcrecimo { get; set; }
        public decimal RppValorTotalParcela { get; set; }

        public TblViagemCotacoesResultadosProdutos RppRprIdResultadoProdutoNavigation { get; set; }
    }
}
