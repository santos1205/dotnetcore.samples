using API.Seguros.Proseg.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Seguros.Proseg.Domain.Interfaces.Repository
{
    public interface ISeguroModalidadeRepository
    {
        List<RetornoSeguroDTO> ListSeguroPorCPF_Modalidade(string cpf, int codModalidadeSeguro);
        List<RetornoSeguroViagemDTO> ListSeguroViagemPorCPF(string cpf);
    }
}
