using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class Pedido
    {
        public Pedido()
        {
            DebitoEmConta = new HashSet<DebitoEmConta>();
        }

        public int PedId { get; set; }
        public int PedCliId { get; set; }
        public int PedPagId { get; set; }
        public DateTime PedData { get; set; }

        public Cliente PedCli { get; set; }
        public Pagamento PedPag { get; set; }
        public ICollection<DebitoEmConta> DebitoEmConta { get; set; }
    }
}
