using API.Viagem.Domain.Constants;
using API.Viagem.Domain.DTOs;
using API.Viagem.Domain.Interfaces.Services;
using API.Viagem.Domain.Models;
using API.Viagem.Domain.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Viagem.Web.Controllers
{
    [ApiController]
    [Route("apiViagem/[controller]")]
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

                IOFile.SalvarJsons(objeto, PathsConstant.JSONS_PATH_AssistCard_AUTENTICACAO, "envio_autenticacao");
                return Ok(objeto);
            }
            catch (System.Exception exc)
            {
                return BadRequest(ApiReturn.ApiReturnObjectException(false, "001", Domain.Properties.Resources.ME001, exc));
            }
        }
    }
}
