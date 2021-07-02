using MovileWeb.DataAccess;
using MovileWeb.Repositories.Interfaces;

namespace MovileWeb.Repositories
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {

        public ClienteRepository(LeadsContext dbContext) : base(dbContext) { }
    }
}
