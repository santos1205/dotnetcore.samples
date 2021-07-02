using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using API.Seguros.Proseg.Domain.Entidades;
using API.Seguros.Proseg.Domain.Interfaces.Repository;
using API.Seguros.Proseg.Domain.Interfaces.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Linq;
using API.Seguros.Proseg.Domain.DTOs;

namespace API.Seguros.Proseg.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public UsuarioService(IUsuarioRepository userRepository, IConfiguration configuration)
        {
            _usuarioRepository = userRepository;
            _configuration = configuration;
        }

        public Usuario GetById(int id)
        {
            return _usuarioRepository.GetById(id);
        }

        public object HabilitarAutenticacao(UsuarioDTO usuario, [FromServices]SigningConfigurations signingConfigurations, [FromServices]TokenConfigurations tokenConfigurations)
        {
            var usuarioSettings = _usuarioRepository.GetAll(x => x.Usr_ClientId == usuario.Client_id).FirstOrDefault();

            if (usuarioSettings.Usr_ClientId.Equals(usuario.Client_id)
                && usuarioSettings.Usr_ClientSecret.Equals(usuario.Client_secret)
                && usuarioSettings.Usr_Username.Equals(usuario.Username)
                && usuarioSettings.Usr_Password.Equals(usuario.Password))
            {

                ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(usuario.Username, "Autenticacao"), new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Username),                    
                });

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                

                return new
                {
                    sucess = "True",
                    access_token = token,
                    expires_in = TimeSpan.FromMilliseconds(tokenConfigurations.Seconds),
                    token_type = "BearerToken",
                    usuario = usuario.Username
                };
            }
            else
            {
                return new
                {
                    sucess = "false",
                    authenticated = false,
                    message = "falha ao autenticar"
                };
            }
        }
    }
}
