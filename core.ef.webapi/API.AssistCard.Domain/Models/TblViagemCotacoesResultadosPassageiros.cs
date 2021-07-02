using System;
using System.Collections.Generic;

namespace API.Viagem.Domain
{
    public partial class TblViagemCotacoesResultadosPassageiros
    {
        public int RpsIdResultadoPassageiro { get; set; }
        public int RpsRprIdResultadoProduto { get; set; }
        public int RpsPsgIdPassageiro { get; set; }
        public decimal RpsValorUnitario { get; set; }
        public decimal RpsValorUnitSeguro { get; set; }
        public decimal RpsValorNetoUnitario { get; set; }
        public decimal RpsValorUnitAssistencia { get; set; }

        public virtual TblViagemPassageiros RpsPsgIdPassageiroNavigation { get; set; }
        public virtual TblViagemCotacoesResultadosProdutos RpsRprIdResultadoProdutoNavigation { get; set; }
    }
}
