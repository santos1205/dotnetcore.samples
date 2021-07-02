using API.Seguros.Proseg.Domain.Constants;
using API.Seguros.Proseg.Domain.DTOs;
using API.Seguros.Proseg.Domain.Interfaces.Repository;
using API.Seguros.Proseg.Domain.Interfaces.Services;
using API.Seguros.Proseg.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Seguros.Proseg.Domain.Services
{
    public class SeguroModalidadeService : ISeguroModalidadeService
    {
        private ISeguroModalidadeRepository _SeguroModalidadeRepository;

        public SeguroModalidadeService(ISeguroModalidadeRepository SeguroModalidadeRepository)
        {
            _SeguroModalidadeRepository = SeguroModalidadeRepository;
        }

        public List<RetornoSeguroDTO> ListSeguroPorCPF_Modalidade(string cpf, int codModalidadeSeg)
        {
            try
            {
                //Salvar Json de Envio.
                IOFile.SalvarJsons((object)cpf, PathsConstant.JSONS_PATH_Seguros_CALCULO_ENVIO, "Envio_Resid_CPF_" + cpf);


                //Get Seguro Auto.
                List<RetornoSeguroDTO> listRetornoSeguroResid = _SeguroModalidadeRepository.ListSeguroPorCPF_Modalidade(cpf, codModalidadeSeg);


                //Salvar Json de Retorno.
                IOFile.SalvarJsons((object)listRetornoSeguroResid, PathsConstant.JSONS_PATH_Seguros_CALCULO_RETORNO, "Retorno_Resid_CPF_" + cpf);


                return listRetornoSeguroResid;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public List<RetornoSeguroViagemDTO> ListSeguroViagemPorCPF(string cpf)
        {
            try
            {
                //Salvar Json de Envio.
                IOFile.SalvarJsons((object)cpf, PathsConstant.JSONS_PATH_Seguros_CALCULO_ENVIO, "Envio_Viagem_CPF_" + cpf);


                //Get Seguro Auto.
                List<RetornoSeguroViagemDTO> listRetornoSeguro = _SeguroModalidadeRepository.ListSeguroViagemPorCPF(cpf);


                //Salvar Json de Retorno.
                IOFile.SalvarJsons((object)listRetornoSeguro, PathsConstant.JSONS_PATH_Seguros_CALCULO_RETORNO, "Retorno_Resid_CPF_" + cpf);


                return listRetornoSeguro;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
