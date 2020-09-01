using API.Viagem.Domain.DTOs;
using API.Viagem.Domain.Enums;
using API.Viagem.Domain.Interfaces.Services;
using API.Viagem.Domain.Util;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Viagem.Web.Controllers
{
    [ApiController]
    [Route("apiViagem/[controller]")]
    public class EmissaoCorretoraPremiumController : ControllerBase
    {
        private readonly IEmissaoService _emissaoService;

        public EmissaoCorretoraPremiumController(IEmissaoService emissaoService)
        {
            _emissaoService = emissaoService;
        }

        [HttpPost("EmitirVoucher")]
        public object Post([FromBody] EmissaoEnvioDTO objetoEmissao)
        {
            try
            {
                if (objetoEmissao != null)
                {
                    object retorno = _emissaoService.SalvarEmissao(objetoEmissao, OrigemParceiroEnum.CorretoraPremium);
                    return Ok(retorno);
                }
                else
                {
                    return StatusCode(500, ApiReturn.ApiReturnObject(false, "001", Domain.Properties.Resources.ME001));
                }
            }
            catch (Exception exc)
            {
                return BadRequest(ApiReturn.ApiReturnObjectException(false, "001", Domain.Properties.Resources.ME001, exc));
            }
        }
    }
}