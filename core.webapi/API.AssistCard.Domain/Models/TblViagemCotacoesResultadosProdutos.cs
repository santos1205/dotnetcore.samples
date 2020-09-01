using System.Collections.Generic;

namespace API.Viagem.Domain
{
    public partial class TblViagemCotacoesResultadosProdutos
    {
        public TblViagemCotacoesResultadosProdutos()
        {
            TblViagemCotacoesResultadosPassageiros = new HashSet<TblViagemCotacoesResultadosPassageiros>();
            TblViagemCotacoesResultadosProdutoParcelamento = new HashSet<TblViagemCotacoesResultadosProdutoParcelamento>();
        }

        public int RprIdResultadoProduto { get; set; }
        public int RprRecIdResultado { get; set; }
        public string RprNomeProduto { get; set; }
        public int RprTarifa { get; set; }
        public string RprCodigo { get; set; }
        public string RprModalidade { get; set; }
        public decimal RprValorTotal { get; set; }
        public decimal RprValorNetoTotal { get; set; }
        public decimal RprValorTaxaGateway { get; set; }
        public decimal RprValorTotalOrigem { get; set; }
        public bool RprTarifaBruta { get; set; }
        public bool RprBancoDias { get; set; }
        public int RprSaldoBancoDias { get; set; }
        public int RprTotalPassageiros { get; set; }
        public short RprMoeda { get; set; }
        public decimal RprCambio { get; set; }

        public virtual TblViagemCotacoesResultados RprRecIdResultadoNavigation { get; set; }
        public virtual ICollection<TblViagemCotacoesResultadosPassageiros> TblViagemCotacoesResultadosPassageiros { get; set; }
        public ICollection<TblViagemCotacoesResultadosProdutoParcelamento> TblViagemCotacoesResultadosProdutoParcelamento { get; set; }
    }
}
