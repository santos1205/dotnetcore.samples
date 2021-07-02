using API.Seguros.Proseg.Domain.Constants;
using API.Seguros.Proseg.Domain.DTOs;
using API.Seguros.Proseg.Domain.Entidades;
using API.Seguros.Proseg.Domain.Enums;
using API.Seguros.Proseg.Domain.Interfaces.Services;
using API.Seguros.Proseg.Domain.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace API.Seguros.Proseg.Web.Controllers
{
    [Authorize("Bearer")]
    [Route("/[controller]")]
    [ApiController]
    public class SegurosController : ControllerBase
    {
        private readonly ISeguroAutoService _seguroAutoService;
        private readonly ISeguroModalidadeService _seguroResidService;
        private readonly ILogService _logService;

        public SegurosController(ISeguroAutoService orcamentoService, ILogService logService, ISeguroModalidadeService seguroResidService)
        {
            _seguroAutoService = orcamentoService;
            _seguroResidService = seguroResidService;
            _logService = logService;
        }
        
        [HttpGet("SegurosAtivos/Auto/{cpf}")]
        public object SeguroAuto(string cpf)
        {
            try
            {                
                if (!string.IsNullOrEmpty(cpf))
                {
                    //Get Seguro Auto.
                    List<RetornoSeguroAutoDTO> retornoSeguroAuto = _seguroAutoService.ListSeguroAutoPorCPF(cpf);

                    if (retornoSeguroAuto.Count() == 0)
                    {
                        throw new Exception(Resources.ME007);
                    }

                    // Gerando Log de requisição - obtendo usuário logado
                    RegistrarLog(cpf, EndpointsConstant.AUTO);                    

                    return (retornoSeguroAuto);
                }

                return null;

            }
            catch (Exception exc)
            {
                if (exc.Message.Equals(Resources.ME007))
                {
                    return NotFound(new { messagem = Resources.ME007 });
                }

                //throw new ArgumentException(Resources.ME002, exc.InnerException);
                return BadRequest(new { messagem = exc.Message });
            }
        }


        [HttpGet("SegurosAtivos/Residencial/{cpf}")]        
        public object SeguroResidencial(string cpf)
        {
            List<RetornoSeguroDTO> retornoSeguroAuto = new List<RetornoSeguroDTO>();
            try
            {
                if (!string.IsNullOrEmpty(cpf))
                {                    
                    //Get Seguro.
                    retornoSeguroAuto = _seguroResidService.ListSeguroPorCPF_Modalidade(cpf, (int)ModalidadeSeguroEnum.Residencial);

                    if (retornoSeguroAuto.Count() == 0)
                    {
                        throw new Exception(Resources.ME007);
                    }

                    // Gerando Log de requisição - obtendo usuário logado
                    RegistrarLog(cpf, EndpointsConstant.RESIDENCIAL);
                }

                return (retornoSeguroAuto);

            }
            catch (Exception exc)
            {
                if (exc.Message.Equals(Resources.ME007))
                {
                    return NotFound(new { menssagem = Resources.ME007 });
                }

                //throw new ArgumentException(Resources.ME002, exc.InnerException);
                return BadRequest(new { messagem = exc.Message });
            }
        }

        [HttpGet("SegurosAtivos/Viagem/{cpf}")]
        public object SeguroViagem(string cpf)
        {
            List<RetornoSeguroDTO> retornoSeguroAuto = new List<RetornoSeguroDTO>();
            try
            {
                if (!string.IsNullOrEmpty(cpf))
                {
                    //Get Seguro Viagem.
                    retornoSeguroAuto = _seguroResidService.ListSeguroPorCPF_Modalidade(cpf, (int)ModalidadeSeguroEnum.Viagem);                    
                    if (retornoSeguroAuto.Count() == 0)
                    {
                        throw new Exception(Resources.ME007);
                    }

                    // Gerando Log de requisição - obtendo usuário logado
                    RegistrarLog(cpf, EndpointsConstant.VIAGEM);
                }

                return (retornoSeguroAuto);
            }
            catch (Exception exc)
            {
                if (exc.Message.Equals(Resources.ME007))
                {
                    return NotFound(new { mensagem = Resources.ME007 });
                }

                //throw new ArgumentException(Resources.ME002, exc.InnerException);
                return BadRequest(new { messagem = exc.Message });
            }
        }

        [HttpGet("SegurosAtivos/Pet/{cpf}")]
        public object SeguroPet(string cpf)
        {

            List<RetornoSeguroDTO> retornoSeguroAuto = new List<RetornoSeguroDTO>();
            try
            {
                if (!string.IsNullOrEmpty(cpf))
                {
                    //Get Seguro Pet.
                    retornoSeguroAuto = _seguroResidService.ListSeguroPorCPF_Modalidade(cpf, (int)ModalidadeSeguroEnum.Pet);
                    if (retornoSeguroAuto.Count() == 0)
                    {
                        throw new Exception(Resources.ME007);
                    }

                    // Gerando Log de requisição - obtendo usuário logado
                    RegistrarLog(cpf, EndpointsConstant.PET);
                }

                return Ok(retornoSeguroAuto);
            }
            catch (Exception exc)
            {
                if (exc.Message.Equals(Resources.ME007))
                {
                    return NotFound(new { mensagem = Resources.ME007 });
                }

                //throw new ArgumentException(Resources.ME002, exc.InnerException);
                return BadRequest(new { messagem = exc.Message });
            }
        }

        [HttpGet("SegurosAtivos/Bike/{cpf}")]
        public object SeguroBike(string cpf)
        {
            try
            {
                List<RetornoSeguroDTO> retornoSeguroAuto = new List<RetornoSeguroDTO>();
                if (!string.IsNullOrEmpty(cpf))
                {
                    //Get Seguro Bike.
                    retornoSeguroAuto = _seguroResidService.ListSeguroPorCPF_Modalidade(cpf, (int)ModalidadeSeguroEnum.Bike);                    
                    if (retornoSeguroAuto.Count() == 0)
                    {
                        throw new Exception(Resources.ME007);
                    }

                    // Gerando Log de requisição - obtendo usuário logado
                    RegistrarLog(cpf, EndpointsConstant.BIKE);
                }

                return (retornoSeguroAuto);

            }
            catch (Exception exc)
            {
                if (exc.Message.Equals(Resources.ME007))
                {
                    return NotFound(new { mensagem = Resources.ME007 });
                }

                //throw new ArgumentException(Resources.ME002, exc.InnerException);
                return BadRequest(new { messagem = exc.Message });
            }
        }

        private void RegistrarLog(string param, string endpoint)
        {
            // Gerando Log de requisição - obtendo usuário logado
            var currentUser = string.Empty;
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                currentUser = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            }

            var newLog = new Log
            {
                LogEndpoint = endpoint.Replace("{cpf}", param),
                LogLoggedUser = currentUser,
                LogParam = param,
                LogDate = DateTime.Now
            };

            _logService.SalvarLog(newLog);
        }
    }
}