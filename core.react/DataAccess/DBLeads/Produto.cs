using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class Produto
    {
        public Produto()
        {
            Plano = new HashSet<Plano>();
        }

        public int ProId { get; set; }
        public string ProDescricao { get; set; }
        public int ProParId { get; set; }

        public Parceiro ProPar { get; set; }
        public ICollection<Plano> Plano { get; set; }
    }
}
