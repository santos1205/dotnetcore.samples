using API.Seguros.Proseg.Domain.DTOs;
using API.Seguros.Proseg.Domain.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace API.Seguros.Proseg.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        Usuario GetById(int id);

        object HabilitarAutenticacao(UsuarioDTO usuario, [FromServices]SigningConfigurations signingConfigurations, [FromServices]TokenConfigurations tokenConfigurations);
    }
}
