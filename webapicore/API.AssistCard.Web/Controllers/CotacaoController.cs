using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace API.AssistCard.Web.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CotacaoController : ControllerBase
    {
        [HttpGet, Route("~/api/consultarCotacao")]
        public IActionResult Get()
        {
            return Ok(new { retorno = "xxxxx" });
        }
    }
}