using System.Collections.Generic;
using API.AssistCard.Infrastructure.Models;

namespace API.AssistCard.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        List<Usuario> GetUsuariosAtivos();

    }
}
