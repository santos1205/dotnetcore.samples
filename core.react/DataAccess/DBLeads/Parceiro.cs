using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class Parceiro
    {
        public Parceiro()
        {
            Produto = new HashSet<Produto>();
        }

        public int ParId { get; set; }
        public bool ParAtivo { get; set; }
        public string ParDescricao { get; set; }

        public ICollection<Produto> Produto { get; set; }
    }
}
