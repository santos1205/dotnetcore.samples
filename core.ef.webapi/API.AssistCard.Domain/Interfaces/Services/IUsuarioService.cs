using API.Viagem.Domain.DTOs;
using API.Viagem.Domain.Models;
using API.Viagem.Infrastructure.Models;

namespace API.Viagem.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        Usuario GetById(int id);

        object HabilitarAutenticacao(UsuarioDTO usuario, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations);
    }
}
