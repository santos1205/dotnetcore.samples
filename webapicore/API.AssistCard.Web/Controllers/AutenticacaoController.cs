using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.AssistCard.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private IConfiguration _config;
        public AutenticacaoController(IConfiguration Config)
        {
            _config = Config;
        }

        [HttpPost("token")]
        public ActionResult GetToken()
        {
            string Issuer =  _config.GetSection("SecuritySettings").GetSection("Issuer").Value;
            string Audience = _config.GetSection("SecuritySettings").GetSection("Audience").Value;

            // Key Secreta
            string SecurityKey = _config.GetSection("SecuritySettings").GetSection("SecurityKey").Value;
            // Gerar key simétrica
            var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
            // Credenciais
            var SignCredentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            // gerar token
            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: SignCredentials
            );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}