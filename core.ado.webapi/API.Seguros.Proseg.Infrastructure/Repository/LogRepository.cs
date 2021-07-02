using API.Seguros.Proseg.Domain.Interfaces.Repository;
using API.Seguros.Proseg.Domain.Entidades;
using API.Seguros.Proseg.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Seguros.Proseg.Infrastructure.Repository
{
    public class LogRepository : EF_ApiSegurosRepository<Log>, ILogRepository        
    {
        public LogRepository(ApiSegurosContext dbContext) : base(dbContext) { }
    }
}
