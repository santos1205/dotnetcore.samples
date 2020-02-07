using API.AssistCard.Domain.DTOs;
using API.AssistCard.Domain.Models;
using API.AssistCard.Infrastructure.Models;
using System.Collections.Generic;

namespace API.AssistCard.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        Usuario GetById(int id);

        object HabilitarAutenticacao(UsuarioDTO usuario, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations);
        ICollection<Usuario> ListarUsuariosAtivos();
    }
}
