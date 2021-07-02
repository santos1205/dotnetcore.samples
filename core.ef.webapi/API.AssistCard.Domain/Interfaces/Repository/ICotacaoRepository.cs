using API.Viagem.Domain.DTOs;
using API.Viagem.Domain.Enums;
using API.Viagem.Domain.Models;
using System.Collections.Generic;

namespace API.Viagem.Domain.Interfaces.Repository
{
    public interface ICotacaoRepository : IRepository<TblViagemCotacoes>
    {
        TblViagemCotacoes SalvarCotacao(CotacaoEnvioDTO cotacaoEnvioDTO, OrigemParceiroEnum origemParceiro);
        void SalvarRetornoCotacao(TblViagemCotacoesResultados tblViagemCotacoesResultados);
    }
}
