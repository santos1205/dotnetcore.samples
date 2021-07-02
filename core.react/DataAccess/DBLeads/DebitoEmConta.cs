using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovileWeb.DataAccess
{
    public class DebitoEmConta
    {
        public int DebId { get; set; }
        public int DebBanId { get; set; }
        public string DebAgencia { get; set; }
        public string DebConta { get; set; }
        public int DebPedId { get; set; }

        public Banco DebBan { get; set; }
        public Pedido DebPed { get; set; }
    }
}
