using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class Plano
    {
        public Plano()
        {
            Pagamento = new HashSet<Pagamento>();
        }

        public int PlaId { get; set; }
        public string PlaDescricao { get; set; }
        public int PlaProId { get; set; }
        public bool PlaAtivo { get; set; }

        public Produto PlaPro { get; set; }
        public ICollection<Pagamento> Pagamento { get; set; }
    }
}
