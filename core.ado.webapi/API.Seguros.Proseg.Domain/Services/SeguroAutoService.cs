using System;
using System.Collections.Generic;
using API.Seguros.Proseg.Domain.Constants;
using API.Seguros.Proseg.Domain.DTOs;
using API.Seguros.Proseg.Domain.Interfaces.Repository;
using API.Seguros.Proseg.Domain.Interfaces.Services;
using API.Seguros.Proseg.Domain.Util;

namespace API.Seguros.Proseg.Domain.Services
{
    public class SeguroAutoService : ISeguroAutoService
    {
        private ISeguroAutoRepository _seguroAutoRepository;
        private ILogService _logService;


        public SeguroAutoService(ISeguroAutoRepository seguroAutoRepository, ILogService logService)
        {
            _seguroAutoRepository = seguroAutoRepository;
            _logService = logService;
        }

        public List<RetornoSeguroAutoDTO> ListSeguroAutoPorCPF(string cpf)
        {
            try
            {
                //Salvar Json de Envio.
                IOFile.SalvarJsons((object)cpf, PathsConstant.JSONS_PATH_Seguros_CALCULO_ENVIO, "Envio_Auto_CPF_" + cpf);

                //Get Seguro Auto.
                List<RetornoSeguroAutoDTO> listRetornoSeguroAuto = _seguroAutoRepository.ListSeguroAutoPorCPF(cpf);

                //Salvar Json de Retorno.
                IOFile.SalvarJsons((object)listRetornoSeguroAuto, PathsConstant.JSONS_PATH_Seguros_CALCULO_RETORNO, "Retorno_Auto_CPF_" + cpf);                                                           

                return listRetornoSeguroAuto;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
