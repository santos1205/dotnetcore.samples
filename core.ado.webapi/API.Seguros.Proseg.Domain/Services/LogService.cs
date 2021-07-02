using API.Seguros.Proseg.Domain.Interfaces.Repository;
using API.Seguros.Proseg.Domain.Interfaces.Services;
using API.Seguros.Proseg.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Seguros.Proseg.Domain.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _iLogRepository;

        public LogService(ILogRepository ILogRepository)
        {
            _iLogRepository = ILogRepository;
        }

        public void SalvarLog(Log Log)
        {
            try
            {
                _iLogRepository.Insert(Log);
            }
            catch(Exception ex)
            {
                throw new Exception("Erro: " + ex.Message);
            }
        }
    }
}
