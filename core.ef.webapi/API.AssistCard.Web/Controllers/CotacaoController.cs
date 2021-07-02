using API.Viagem.Domain.DTOs;
using API.Viagem.Domain.Enums;
using API.Viagem.Domain.Interfaces.Services;
using API.Viagem.Domain.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Viagem.Web.Controllers
{
    [ApiController]
    [Route("apiViagem/[controller]")]
    public class CotacaoController : Controller
    {
        private readonly ICotacaoService _cotacaoSevice;

        public CotacaoController(ICotacaoService cotacaoSevice)
        {
            _cotacaoSevice = cotacaoSevice;
        }


        [HttpPost("Calcular")]
        public async Task<object> Post([FromBody] CotacaoEnvioDTO objetoCotacao)
        {
            try
            {
                if (objetoCotacao != null)
                {
                    object retorno = _cotacaoSevice.SalvarCotacao(objetoCotacao, OrigemParceiroEnum.MultiCalculo);

                    return Ok(retorno);
                }
                else
                {
                    return StatusCode(500, ApiReturn.ApiReturnObject(false, "500", Domain.Properties.Resources.ME001));
                }
            }
            catch (Exception exc)
            {
                return BadRequest(ApiReturn.ApiReturnObjectException(false, "001", Domain.Properties.Resources.ME001, exc));
            }
        }
    }
}