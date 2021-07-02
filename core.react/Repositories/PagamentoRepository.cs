using MovileWeb.DataAccess;
using MovileWeb.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovileWeb.Repositories
{
    public class PagamentoRepository : RepositoryBase<Pagamento>, IPagamentoRepository
    {
        public PagamentoRepository(LeadsContext dbContext) : base(dbContext) { }

        public Pagamento Consultar(int IdPlano, int IdTpPagamento)
        {
            return _dbContext.Pagamento
                        .Where(x => x.PagPlaId == IdPlano && x.PagTipoPagamento == IdTpPagamento)
                        .FirstOrDefault();
        }
    }
}
