using System;
using System.Collections.Generic;
using System.Linq;
using API.Viagem.Infrastructure.Models;
using API.Viagem.Domain.Interfaces.Repository;

namespace API.Viagem.Infrastructure.Repository
{
    public class UsuarioRepository : EFRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(MultCalcSegContext dbContext) : base(dbContext) { }

        public Usuario GetUsuarioAutenticado(int usrClientID)
        {
            try
            {
                Usuario usuario = GetAll(x => x.Usr_ClientId == usrClientID).FirstOrDefault();
                return usuario;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
