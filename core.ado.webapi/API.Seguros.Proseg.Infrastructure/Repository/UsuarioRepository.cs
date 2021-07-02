using API.Seguros.Proseg.Domain.Entidades;
using API.Seguros.Proseg.Domain.Interfaces.Repository;
using API.Seguros.Proseg.Infrastructure.Data;
using System;
using System.Linq;

namespace API.Seguros.Proseg.Infrastructure.Repository
{
    public class UsuarioRepository : EF_ApiRepository<Usuario>, IUsuarioRepository
    {

        public UsuarioRepository(ApiMultiCalculoContext dbAPIContext) : base(dbAPIContext) { }

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
