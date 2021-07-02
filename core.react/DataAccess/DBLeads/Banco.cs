using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovileWeb.DataAccess
{
    public partial class Banco
    {
        public Banco()
        {
            DebitoEmConta = new HashSet<DebitoEmConta>();
        }

        public int BanId { get; set; }
        public string BanDescricao { get; set; }

        public ICollection<DebitoEmConta> DebitoEmConta { get; set; }
    }
}
