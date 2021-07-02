using API.Seguros.Proseg.Domain.DTOs;
using System.Collections.Generic;

namespace API.Seguros.Proseg.Domain.Interfaces.Services
{
    public interface ISeguroModalidadeService
    {
        List<RetornoSeguroDTO> ListSeguroPorCPF_Modalidade(string cpf, int codModalidadeSeguro);
        List<RetornoSeguroViagemDTO> ListSeguroViagemPorCPF(string cpf);
    }
}

