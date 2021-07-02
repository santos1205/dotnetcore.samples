using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Seguros.Proseg.Domain.Entidades;
using API.Seguros.Proseg.Domain.Interfaces.Services;
using API.Seguros.Proseg.Domain.Constants;
using API.Seguros.Proseg.Domain.Util;
using API.Seguros.Proseg.Domain.DTOs;

namespace API.Seguros.Proseg.Web.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class AutenticacaoController : Controller
    {

        private readonly IUsuarioService _userService;

        public AutenticacaoController(IUsuarioService userService)
        {
            _userService = userService;
        }


        [AllowAnonymous]
        [HttpPost]
        public object Post([FromBody]UsuarioDTO usuario, [FromServices]SigningConfigurations signingConfigurations, [FromServices]TokenConfigurations tokenConfigurations)
        {
            try
            {
                var objeto = new object();

                if (usuario != null)
                    objeto = _userService.HabilitarAutenticacao(usuario, signingConfigurations, tokenConfigurations);

                IOFile.SalvarJsons(objeto, PathsConstant.JSONS_PATH_Seguros_AUTENTICACAO, "envio_autenticacao");                
                return objeto;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}