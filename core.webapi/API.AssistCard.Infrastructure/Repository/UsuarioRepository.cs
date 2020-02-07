using System;
using System.Collections.Generic;
using System.Linq;
using API.AssistCard.Infrastructure.Models;
using API.AssistCard.Domain.Interfaces.Repository;

namespace API.AssistCard.Infrastructure.Repository
{
    public class UsuarioRepository : EFRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApiAssistCardContext dbContext) : base(dbContext) { }

        public List<Usuario> GetUsuariosAtivos()
        {
            try
            {
                IEnumerable<Usuario> usuarios = base.GetAll(x => x.Usu_Ativo == true);
                return usuarios.ToList();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
