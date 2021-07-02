using System.Collections.Generic;

namespace API.Viagem.Domain.Interfaces.Repository
{
    public interface IEmissaoRepository : IRepository<TblViagemEmissoes>
    {
        TblViagemEmissoes SalvarEmissao(TblViagemEmissoes viagemEmissoes, List<TblViagemPassageiros> lstViagemPassageiros);

        void SalvarRetornoEmissao(TblViagemEmissoesRetorno tblViagemEmissoesRetorno, List<TblViagemVouchers> lstViagemVouchers);
    }
}
