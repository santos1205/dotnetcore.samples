using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class Pagamento
    {
        public Pagamento()
        {
            Banco = new HashSet<Banco>();
            Pedido = new HashSet<Pedido>();
        }

        public int PagId { get; set; }
        public string PagDescricao { get; set; }
        public int PagTipoPagamento { get; set; }
        public int PagNumParcelas { get; set; }
        public decimal PagValorParcela { get; set; }
        public int PagPlaId { get; set; }
        public bool PagAtivo { get; set; }

        public Plano PagPla { get; set; }
        public TipoPagamento PagTipoPagamentoNavigation { get; set; }
        public ICollection<Banco> Banco { get; set; }
        public ICollection<Pedido> Pedido { get; set; }
    }
}
