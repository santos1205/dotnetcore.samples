using MovileWeb.DataAccess;
using MovileWeb.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovileWeb.Repositories
{    
    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
    {
        public PedidoRepository(LeadsContext dbContext) : base(dbContext) { }
    }
}
