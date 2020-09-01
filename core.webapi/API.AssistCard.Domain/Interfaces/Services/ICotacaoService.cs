using API.Viagem.Domain.DTOs;
using API.Viagem.Domain.Enums;

namespace API.Viagem.Domain.Interfaces.Services
{
    public interface ICotacaoService
    {
        object SalvarCotacao(CotacaoEnvioDTO cotacaoEnvioDTO, OrigemParceiroEnum origemParceiro);
    }
}
