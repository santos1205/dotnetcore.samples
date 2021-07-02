using System;
using System.Collections.Generic;
using System.Text;
using API.Seguros.Proseg.Domain.DTOs;

namespace API.Seguros.Proseg.Domain.Interfaces.Repository
{
    public interface ISeguroAutoRepository
    {
        List<RetornoSeguroAutoDTO> ListSeguroAutoPorCPF(string cpf);
    }
}
