using System.Collections.Generic;
using API.Viagem.Infrastructure.Models;

namespace API.Viagem.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario GetUsuarioAutenticado(int usrClientID);

    }
}
