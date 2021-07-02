using API.Viagem.Domain.DTOs;
using API.Viagem.Domain.Interfaces.Repository;
using API.Viagem.Domain.Interfaces.Services;
using API.Viagem.Domain.Models;
using API.Viagem.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace API.Viagem.Domain.Services
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
            UsuarioDTO user = _configuration.GetSection("UserAutenticacaoMultiCalculo").Get<UsuarioDTO>();


            if (user.Client_id .Equals(usuario.Client_id)
                && user.Client_secret.Equals(usuario.Client_secret)
                && user.Username.Equals(usuario.Username)
                && user.Password.Equals(usuario.Password))
            {

                ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(usuario.Username, "Autenticacao"), new[] {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Username)});

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                SecurityToken securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                string token = handler.WriteToken(securityToken);

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
                    message = "Credenciais inválidas. Falha ao autenticar"
                };
            }
        }
    }
}