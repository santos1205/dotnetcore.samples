using MovileWeb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovileWeb.Repositories.Interfaces
{
    public interface IPagamentoRepository : IRepository<Pagamento>
    {
        Pagamento Consultar(int IdPlano, int IdTpPagamento);
    }
}
