using API.Seguros.Proseg.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Seguros.Proseg.Domain.Interfaces.Services
{
    public interface ILogService
    {
        void SalvarLog(Log Log);
    }
}
