namespace API.Viagem.Domain.Interfaces.Repository
{
    public interface IPassageiroRepository : IRepository<TblViagemPassageiros>
    {
        TblViagemPassageiros GetPassageirosPorCPF(string cpf);
    }
}