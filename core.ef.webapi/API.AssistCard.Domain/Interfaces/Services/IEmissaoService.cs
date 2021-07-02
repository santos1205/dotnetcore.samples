using API.Viagem.Domain.DTOs;
using API.Viagem.Domain.Enums;

namespace API.Viagem.Domain.Interfaces.Services
{
    public interface IEmissaoService
    {
        object SalvarEmissao(EmissaoEnvioDTO objetoEmissao, OrigemParceiroEnum origemParceiro);
    }
}
