using API.Seguros.Proseg.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Seguros.Proseg.Domain.Interfaces.Services
{
    public interface ISeguroAutoService
    {

        List<RetornoSeguroAutoDTO> ListSeguroAutoPorCPF(string cpf);

    }
}
