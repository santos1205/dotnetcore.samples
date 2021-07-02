using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MovileWeb.DataAccess;
using MovileWeb.Services.Interfaces;

namespace MovileWeb.Controllers
{
    public class ProdutoController : BaseController
    {
        private readonly ICommonService _iCommonService;
        private readonly IPedidoService _iPedidoService;
        public ProdutoController(ICommonService iCommonService, IPedidoService iPedidoService)
        {            
            _iCommonService = iCommonService;
            _iPedidoService = iPedidoService;
        }

        [HttpGet("api/leads")]
        public object Get()
        {
            return new { message = "return sample" };
        }

        [HttpGet("api/lead/endereco/")]
        public object ConsultarEndereco([FromQuery(Name = "cep")] string cep)
        {
            try
            {
                var Endereco = _iCommonService.ConsultarEnderecoPorCep(cep);

                return Ok(Endereco);
            }
            catch(Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }

            
        }

        [HttpPost("api/lead/cadastrar")]
        public object SalvarPedido([FromBody]Pedido Pedido)
        {
            try
            {   
                
                //var validationResults = new List<ValidationResult>();
                //bool isClienteValid = CheckValidation(testObject, ref validationResults);

                _iPedidoService.SalvarPedido(Pedido);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}