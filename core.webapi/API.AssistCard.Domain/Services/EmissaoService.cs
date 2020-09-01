using API.Viagem.Domain.Constants;
using API.Viagem.Domain.DTOs;
using API.Viagem.Domain.Enums;
using API.Viagem.Domain.Interfaces.Repository;
using API.Viagem.Domain.Interfaces.Services;
using API.Viagem.Domain.Properties;
using API.Viagem.Domain.Util;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace API.Viagem.Domain.Services
{
    public class EmissaoService : IEmissaoService
    {
        private readonly IEmissaoRepository _emissaoRepository;
        private readonly IPassageiroRepository _passageiroRepository;
        private readonly IConfiguration _configuration;

        public EmissaoService(IEmissaoRepository emissaoRepository, IConfiguration configuration, IPassageiroRepository passageiroRepository)
        {
            _emissaoRepository = emissaoRepository;
            _passageiroRepository = passageiroRepository;
            _configuration = configuration;
        }

        public object SalvarEmissao(EmissaoEnvioDTO objetoEmissao, OrigemParceiroEnum origemParceiro)
        {
            try
            {
                //Formtação e Validações do Objeto de Emissão.
                ValidacaoCamposDTO(objetoEmissao);

                //Recuperar Passageiro.
                List<TblViagemPassageiros> lstPassageiros = GetPassageiros(objetoEmissao);


                //Salvar XML de Envio.
                XElement emissaoXml = XmlHelper.EmissaoEnvioXML(objetoEmissao, lstPassageiros);
                if (origemParceiro == OrigemParceiroEnum.MultiCalculo)
                    IOFile.SalvarJsons(emissaoXml, PathsConstant.JSONS_PATH_AssistCard_MultiCalculo_EMISSAOVOUCHERENVIO, "AssistCard_MultiCalculo_JsonEmissaoVoucher_", null);
                else
                    IOFile.SalvarJsons(emissaoXml, PathsConstant.JSONS_PATH_AssistCard_CorretoraPremium_EMISSAOVOUCHERENVIO, "AssistCard_CorretoraPremium_JsonEmissaoVoucher_", null);



                //Salvar envio da emissão no banco do MultiCalculo.
                TblViagemEmissoes viagemEmissao = SalvarEnvioEmissao(objetoEmissao, origemParceiro);


                //Enviar para emissão para Corretora.
                string usuarioautenticacao = origemParceiro == OrigemParceiroEnum.MultiCalculo ? "UserAutenticacaoAssistCard" : "UserAutenticacaoAssistCardCorretoraPremium";

                UsuarioAssistCardDTO userAssistCardDTO = _configuration.GetSection(usuarioautenticacao).Get<UsuarioAssistCardDTO>();
                AssistCard.InterfaceIssuanceServiceClient assistCardWS = new AssistCard.InterfaceIssuanceServiceClient(AssistCard.InterfaceIssuanceServiceClient.EndpointConfiguration.BasicHttpsBinding_InterfaceIssuanceService);
                Task<string> emissao = assistCardWS.EmissaoVouchersAsync(userAssistCardDTO.UsuarioCliente, userAssistCardDTO.SenhaCliente, userAssistCardDTO.CodigoAgencia, emissaoXml.ToString());


                //Salvar JSON de Retorno.
                IOFile.SalvarJsons(emissao.Result, PathsConstant.JSONS_PATH_AssistCard_MultiCalculo_EMISSAOVOUCHERRETORNO, "AssistCard_JsonRetornoEmissaoVoucher", viagemEmissao.EmiIdEmissao);

                //Converter XML para Objeto.
                if (emissao.Result != null)
                {

                    if (emissao.Result.Contains("<sucesso>False</sucesso>"))
                    {
                        //Converter XML de emissão com erro.
                        EmissaoRetornoErroDTO emissaoRetornoErroDTO = XmlHelper.Deserialize<EmissaoRetornoErroDTO>(emissao.Result.Replace("p.m.", "").Replace("a.m", ""));

                        //Altera status da emissãocom erro.
                        viagemEmissao.EmiStatus = false;
                        _emissaoRepository.Update(viagemEmissao);

                        return emissaoRetornoErroDTO;
                    }
                    else
                    {
                        //Converter XML para Objeto.
                        EmissaoRetornoDTO emissaoRetornoDTO = XmlHelper.Deserialize<EmissaoRetornoDTO>(emissao.Result.Replace("p.m.", "").Replace("a.m", ""));
                        emissaoRetornoDTO.RetornoEmissao.Emissao.IdProposta = viagemEmissao.EmiIdEmissao;

                        //Salvar JSON de Retorno.
                        if (origemParceiro == OrigemParceiroEnum.MultiCalculo)
                            IOFile.SalvarJsons(emissao.Result, path: PathsConstant.JSONS_PATH_AssistCard_MultiCalculo_EMISSAOVOUCHERRETORNO, nomeArquivo: "AssistCard_MultiCalculo_JsonEmissaoVoucherRetorno_", idArquivo: viagemEmissao.EmiIdEmissao);
                        else
                            IOFile.SalvarJsons(emissao.Result, path: PathsConstant.JSONS_PATH_AssistCard_CorretoraPremium_EMISSAOVOUCHERRETORNO, nomeArquivo: "AssistCard_CorretoraPremium_JsonEmissaoVoucherRetorno_", idArquivo: viagemEmissao.EmiIdEmissao);


                        //Salvar retorno da emissão no banco do MultiCalculo.
                        SalvarRetornoEmissao(viagemEmissao, emissaoRetornoDTO);

                        return emissaoRetornoDTO;
                    }
                }
                else
                {
                    SalvarEmissaoErro(viagemEmissao);
                }

                return ApiReturn.ApiReturnObject(false, "001", Resources.ME001);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private static void ValidacaoCamposDTO(EmissaoEnvioDTO objetoEmissao)
        {
            objetoEmissao.DocumentoCartao = FormattingValidation.FormatarCPF(objetoEmissao.DocumentoCartao);
            objetoEmissao.Passageiros.Passageiro.ForEach(f => f.NumeroDocumento = FormattingValidation.FormatarCPF(f.NumeroDocumento));
        }

        private List<TblViagemPassageiros> GetPassageiros(EmissaoEnvioDTO objetoEmissao)
        {
            List<TblViagemPassageiros> lstTblViagemPassageiros = new List<TblViagemPassageiros>();

            foreach (EmissaoEnvioPassageiroDTO passageiro in objetoEmissao.Passageiros.Passageiro)
            {
                TblViagemPassageiros tTblViagemPassageiros = new TblViagemPassageiros();
                var passageiroDB = _passageiroRepository.GetById(passageiro.IdPassageiro);
                if (passageiroDB != null)
                {
                    passageiroDB.PsgCpf = FormattingValidation.FormatarCPF(cpf: passageiroDB.PsgCpf);
                    lstTblViagemPassageiros.Add(passageiroDB);
                }
            }

            return lstTblViagemPassageiros;
        }

        private TblViagemEmissoes SalvarEnvioEmissao(EmissaoEnvioDTO objetoEmissao, OrigemParceiroEnum origemParceiro)
        {
            try
            {
                List<TblViagemPassageiros> lstViagemPassageiros = new List<TblViagemPassageiros>();

                //Complementar informações do Passageiro
                foreach (EmissaoEnvioPassageiroDTO passageiro in objetoEmissao.Passageiros.Passageiro)
                {
                    TblViagemPassageiros tblViagemPassageiros = new TblViagemPassageiros
                    {
                        PsgIdPassageiro = passageiro.IdPassageiro,
                        PsgEndereco = passageiro.Endereco,
                        PsgCodigoPostal = passageiro.CodigoPostal,
                        PsgCidade = passageiro.Cidade,
                        PsgNumeroDocumento = passageiro.NumeroDocumento,
                        PsgEstado = passageiro.Estado,
                        PsgPaisDomicilio = passageiro.PaisDomicilio,
                        PsgDadoAdicional1 = passageiro.DadoAdicional1,
                        PsgDadoAdicional2 = passageiro.DadoAdicional2,
                        PsgGenero = passageiro.Genero,
                        PsgBairro = passageiro.Bairro,
                        PsgNumeroEndereco = passageiro.NumeroEndereco,
                        PsgComplemento = passageiro.Complemento,
                        PsgNacIdNacionalidade = int.Parse(passageiro.Nacionalidade),
                    };

                    lstViagemPassageiros.Add(tblViagemPassageiros);
                }


                TblViagemEmissoes tblViagemEmissoes = new TblViagemEmissoes();
                {
                    tblViagemEmissoes.EmiIdEmissao = 0;
                    tblViagemEmissoes.EmiCotIdCotacao = objetoEmissao.IdCotacao;
                    tblViagemEmissoes.EmiRprIdResultadoProduto = objetoEmissao.IdResultadoProdutoCotacao;
                    tblViagemEmissoes.EmiPagamentoComCartao = objetoEmissao.PagamentoComCartao;
                    if (objetoEmissao.BandeiraCartao != string.Empty)
                    {
                        tblViagemEmissoes.EmiBandeiraCartao = Convert.ToByte(objetoEmissao.BandeiraCartao);
                    }

                    tblViagemEmissoes.EmiDocumentoCartao = FormattingValidation.FormatarCPF(objetoEmissao.DocumentoCartao);
                    tblViagemEmissoes.EmiNumeroCartao = FormattingValidation.FormatarCPF(objetoEmissao.NumeroCartao);
                    tblViagemEmissoes.EmiValidadeCartao = objetoEmissao.ValidadeCartao.Replace("/", "");
                    if (objetoEmissao.Parcelas != string.Empty)
                    {
                        tblViagemEmissoes.EmiParcelas = Convert.ToByte(objetoEmissao.Parcelas);
                    }

                    tblViagemEmissoes.EmiPlanoFamiliar = objetoEmissao.PlanoFamiliar;
                    tblViagemEmissoes.EmiNomeCartao = objetoEmissao.NomeCartao;
                    tblViagemEmissoes.EmiNomeUsuarioEmissao = objetoEmissao.UsuarioEmissao;
                    tblViagemEmissoes.EmiStatus = true;
                    tblViagemEmissoes.EmiBlnTransmitidoMultiSeguro = false;
                    tblViagemEmissoes.EmiDtEmissao = DateTime.Now;
                    tblViagemEmissoes.EmiOrpIdOrigemParceiro = (int)origemParceiro;

                };

                TblViagemEmissoes retornoEmissao = _emissaoRepository.SalvarEmissao(tblViagemEmissoes, lstViagemPassageiros);
                return retornoEmissao;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private void SalvarRetornoEmissao(TblViagemEmissoes emissao, EmissaoRetornoDTO emissaoRetornoDTO)
        {
            try
            {
                //Criar objeto de Emissão retorno.
                TblViagemEmissoesRetorno tblViagemEmissoesRetorno = new TblViagemEmissoesRetorno
                {
                    EmrEmiIdEmissao = emissao.EmiIdEmissao,
                    EmrGrupoVoucher = emissaoRetornoDTO.RetornoEmissao.Emissao.GrupoVoucher.Codigo,
                    EmrTotalDias = emissaoRetornoDTO.RetornoEmissao.Emissao.GrupoVoucher.TotalDias,
                    EmrDtEmissaoRetorno = Convert.ToDateTime(emissaoRetornoDTO.RetornoEmissao.Emissao.GrupoVoucher.DataEmissao.Replace("p.m", "").Replace("a.m", "")),
                    EmrBlnSucesso = emissaoRetornoDTO.Sucesso == "True" ? true : false
                };


                //Criar lista de Vouchers. 
                List<TblViagemVouchers> lstViagemVouchers = new List<TblViagemVouchers>();

                foreach (Voucher voucher in emissaoRetornoDTO.RetornoEmissao.Emissao.GrupoVoucher.Voucher)
                {
                    int idPassageiro = _passageiroRepository.GetPassageirosPorCPF(voucher.NumeroDocumento).PsgIdPassageiro;

                    TblViagemVouchers tblViagemVouchers = new TblViagemVouchers
                    {
                        VchCodigo = voucher.Codigo,
                        VchNumero = voucher.Numero,
                        VchValor = !string.IsNullOrEmpty(voucher.Valor) ? Convert.ToDecimal(voucher.Valor) : 0,
                        VchValorAssistencia = !string.IsNullOrEmpty(voucher.ValorAsistencia) ? Convert.ToDecimal(voucher.ValorAsistencia) : 0,
                        VchPremioSeguroTotal = !string.IsNullOrEmpty(voucher.Apolice.PremioSeguroTotal) ? Convert.ToDecimal(voucher.Apolice.PremioSeguroTotal) : 0,
                        VchValorIof = !string.IsNullOrEmpty(voucher.Apolice.ValorIOF) ? Convert.ToDecimal(voucher.Apolice.ValorIOF) : 0,
                        VchMoeda = voucher.Moeda,
                        VchCambio = !string.IsNullOrEmpty(voucher.Cambio) ? Convert.ToDecimal(voucher.Cambio) : 0,
                        VchTipoDocumento = voucher.TipoDocumento,
                        VchNumeroDocumento = voucher.NumeroDocumento,
                        VchNomeCliente = voucher.NomeCliente,
                        VchEstadoVoucher = voucher.EstadoVoucher,
                        VchBlnAtivo = true,
                        VchAcrescimoFinanceiro = !string.IsNullOrEmpty(voucher.AcrescimoFinanceiro) ? Convert.ToDecimal(voucher.AcrescimoFinanceiro) : 0,
                        VchValorTaxaGateway = !string.IsNullOrEmpty(voucher.ValorTaxaGateway) ? Convert.ToDecimal(voucher.ValorTaxaGateway) : 0,
                        VchCodigoApolice = voucher.Apolice.CodigoApolice,
                        VchPsgIdPassageiro = idPassageiro
                    };

                    lstViagemVouchers.Add(tblViagemVouchers);
                };

                _emissaoRepository.SalvarRetornoEmissao(tblViagemEmissoesRetorno, lstViagemVouchers);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private void SalvarEmissaoErro(TblViagemEmissoes viagemEmissao)
        {
            viagemEmissao.EmiStatus = false;
            _emissaoRepository.Update(viagemEmissao);
        }
    }
}
