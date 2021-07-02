using MovileWeb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovileWeb.Services.Interfaces
{
    public interface IPedidoService
    {
        void SalvarPedido(Pedido Pedido);
    }
}
