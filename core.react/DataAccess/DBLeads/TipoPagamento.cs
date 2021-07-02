using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class TipoPagamento
    {
        public TipoPagamento()
        {
            Pagamento = new HashSet<Pagamento>();
        }

        public int TppId { get; set; }
        public string TppDescricao { get; set; }

        public ICollection<Pagamento> Pagamento { get; set; }
    }
}
