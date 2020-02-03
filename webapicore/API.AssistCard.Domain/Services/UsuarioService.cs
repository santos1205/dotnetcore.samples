using System.Collections.Generic;
using API.AssistCard.Domain.DTOs;
using API.AssistCard.Domain.Interfaces.Repository;
using API.AssistCard.Domain.Interfaces.Services;
using API.AssistCard.Domain.Models;
using API.AssistCard.Infrastructure.Models;

namespace API.AssistCard.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository _usuarioRepository;        

        public UsuarioService(IUsuarioRepository userRepository)
        {
            _usuarioRepository = userRepository;            
        }

        public Usuario GetById(int id)
        {
            return _usuarioRepository.GetById(id);
        }

        public object HabilitarAutenticacao(UsuarioDTO usuario, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Usuario> ListarUsuariosAtivos()
        {
            return _usuarioRepository.GetUsuariosAtivos();            
        }
    }

}