using API.Viagem.Domain;
using API.Viagem.Domain.Interfaces.Repository;
using System.Linq;

namespace API.Viagem.Infrastructure.Repository
{
    public class PassageiroRepository : EFRepository<TblViagemPassageiros>, IPassageiroRepository
    {
        public PassageiroRepository(MultCalcSegContext dbContext) : base(dbContext)
        {
        }

        public TblViagemPassageiros GetPassageirosPorCPF(string cpf)
        {
            TblViagemPassageiros passageiro = GetAll(x => x.PsgCpf == cpf).FirstOrDefault();
            return passageiro;
        }
    }
}