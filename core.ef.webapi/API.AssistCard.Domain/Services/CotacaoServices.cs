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
    public class CotacaoService : ICotacaoService
    {
        private readonly ICotacaoRepository _cotacaoRepository;
        private readonly IConfiguration _configuration;

        public CotacaoService(ICotacaoRepository cotacaoRepository, IConfiguration configuration)
        {
            _cotacaoRepository = cotacaoRepository;
            _configuration = configuration;
        }

        public object SalvarCotacao(CotacaoEnvioDTO objetoCotacao, OrigemParceiroEnum origemParceiro)
        {
            try
            {
                //Salvar XML de Envio da cotação.
                XElement cotacaoXml = XmlHelper.CotacaoEnvioXML(objetoCotacao);
                if (origemParceiro == OrigemParceiroEnum.MultiCalculo)
                    IOFile.SalvarJsons(cotacaoXml, path: PathsConstant.JSONS_PATH_AssistCard_MultiCalculo_CALCULOENVIO, nomeArquivo: "AssistCard_JsonCotacao_MultiCalculo_", idArquivo: null);
                else
                    IOFile.SalvarJsons(cotacaoXml, path: PathsConstant.JSONS_PATH_AssistCard_CorretoraPremium_CALCULOENVIO, nomeArquivo: "AssistCard_JsonCotacao_CorretoraPremium", idArquivo: null);


                //Salvar envio da cotação no banco do MultiCalculo.
                TblViagemCotacoes viagemCotacao = SalvarEnvioCotacao(objetoCotacao, origemParceiro);

                //Enviar para cotação para Corretora.
                string usuarioautenticacao = origemParceiro == OrigemParceiroEnum.MultiCalculo ? "UserAutenticacaoAssistCard" : "UserAutenticacaoAssistCardCorretoraPremium";

                UsuarioAssistCardDTO userAssistCardDTO = _configuration.GetSection(usuarioautenticacao).Get<UsuarioAssistCardDTO>();
                AssistCard.InterfaceIssuanceServiceClient assistCardWS = new AssistCard.InterfaceIssuanceServiceClient(AssistCard.InterfaceIssuanceServiceClient.EndpointConfiguration.BasicHttpsBinding_InterfaceIssuanceService);
                Task<string> cotacao = assistCardWS.CotacaoProdutosAsync(userAssistCardDTO.UsuarioCliente, userAssistCardDTO.SenhaCliente, userAssistCardDTO.CodigoAgencia, cotacaoXml.ToString());

                if (cotacao.Result != null)
                {
                    if (cotacao.Result.Contains("<sucesso>False</sucesso>"))
                    {
                        //Converter XML de emissão com erro.
                        CotacaoRetornoErroDTO cotacaoRetornoErroDTO = XmlHelper.Deserialize<CotacaoRetornoErroDTO>(cotacao.Result.Replace("p.m.", "").Replace("a.m", ""));

                        return cotacaoRetornoErroDTO;
                    }
                    else
                    {

                        CotacaoRetornoDTO cotacaoRetornoDTO = XmlHelper.Deserialize<CotacaoRetornoDTO>(cotacao.Result);

                        //Calcular o valor das parcelas de acordo com informação selecionado pelo usuário.
                        if (objetoCotacao.FormaPagamento == (int)FormaPagamentoEnum.CartaoDeCredito)
                        {
                            foreach (var produto in cotacaoRetornoDTO.Retorno.Cotacao.Produtos.Produto)
                            {
                                //Instanciar classe DTO.
                                CalcularParcelasCartaoRespostaDTO calcularParcelasCartaoRespostaDTO = new CalcularParcelasCartaoRespostaDTO();

                                //WebService para Calcular parcelas.
                                XElement CalcularParcelasCartaoXml = XmlHelper.CalcularParcelasCartaoXML(objetoCotacao, produto);
                                Task<string> parcelasCartao = assistCardWS.CalcularParcelasCartaoAsync(userAssistCardDTO.UsuarioCliente, userAssistCardDTO.SenhaCliente, userAssistCardDTO.CodigoAgencia, CalcularParcelasCartaoXml.ToString());

                                if (parcelasCartao.Result != null)
                                {
                                    if (!parcelasCartao.Result.Contains("<sucesso>False</sucesso>"))
                                    {
                                        //Salvar JSON de Retorno.
                                        if (origemParceiro == OrigemParceiroEnum.MultiCalculo)
                                            IOFile.SalvarJsons(parcelasCartao.Result, path: PathsConstant.JSONS_PATH_AssistCard_MultiCalculo_CALCULOPARCELACARTAO, nomeArquivo: "AssistCard_JsonParcelasCartao_MultiCalculo_", idArquivo: viagemCotacao.CotIdCotacao);
                                        else
                                            IOFile.SalvarJsons(parcelasCartao.Result, path: PathsConstant.JSONS_PATH_AssistCard_CorretoraPremium_CALCULOPARCELACARTAO, nomeArquivo: "AssistCard_JsonParcelasCartao_CorretoraPremium_", idArquivo: viagemCotacao.CotIdCotacao);

                                        //Deserialize Xml to Class
                                        calcularParcelasCartaoRespostaDTO = XmlHelper.Deserialize<CalcularParcelasCartaoRespostaDTO>(parcelasCartao.Result);

                                        //Salvar retorno da cotação no banco do MultiCalculo.
                                        produto.Parcelamento = calcularParcelasCartaoRespostaDTO.Retorno.Parcelas.Parcela;
                                    }
                                }
                            }
                        }


                        //Salvar JSON de Retorno.
                        if (origemParceiro == OrigemParceiroEnum.MultiCalculo)
                            IOFile.SalvarJsons(cotacao.Result, path: PathsConstant.JSONS_PATH_AssistCard_MultiCalculo_CALCULORETORNO, nomeArquivo: "AssistCard_JsonRetornoCotacao_MultiCalculo_", idArquivo: viagemCotacao.CotIdCotacao);
                        else
                            IOFile.SalvarJsons(cotacao.Result, path: PathsConstant.JSONS_PATH_AssistCard_CorretoraPremium_CALCULORETORNO, nomeArquivo: "AssistCard_JsonRetornoCotacao_CorretoraPremium_", idArquivo: viagemCotacao.CotIdCotacao);


                        //Incluir Id da Cotação.
                        cotacaoRetornoDTO.Retorno.Cotacao.Produtos.IdCotacao = viagemCotacao.CotIdCotacao;

                        //Salvar o retorno da cotação no banco.
                        SalvarRetornoCotacao(viagemCotacao, cotacaoRetornoDTO, objetoCotacao.BandeiraCartao);

                        return cotacaoRetornoDTO;
                    }


                }
                return ApiReturn.ApiReturnObject(false, "001", Resources.ME001);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private TblViagemCotacoes SalvarEnvioCotacao(CotacaoEnvioDTO objetoCotacao, OrigemParceiroEnum origemParceiro)
        {
            try
            {
                TblViagemCotacoes retornoCotacao = _cotacaoRepository.SalvarCotacao(objetoCotacao, origemParceiro);

                return retornoCotacao;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private CotacaoRetornoDTO SalvarRetornoCotacao(TblViagemCotacoes retornoCotacao, CotacaoRetornoDTO cotacaoRetornoDTO, string bandeiraCartao)
        {
            List<TblViagemCotacoesResultadosProdutos> listProdutosRetorno = new List<TblViagemCotacoesResultadosProdutos>();

            foreach (Produto produto in cotacaoRetornoDTO.Retorno.Cotacao.Produtos.Produto)
            {
                //Instanciar as listas de parcelamento e passageiro.
                List<TblViagemCotacoesResultadosPassageiros> listPassageirosRetorno = new List<TblViagemCotacoesResultadosPassageiros>();
                List<TblViagemCotacoesResultadosProdutoParcelamento> listParcelamento = new List<TblViagemCotacoesResultadosProdutoParcelamento>();

                #region Passageiro
                //Criar lista auxiliar para identificar passageiro.
                var lstAuxPassageiro = new List<TblViagemPassageirosCotacoes>();
                lstAuxPassageiro = retornoCotacao.TblViagemPassageirosCotacoes.ToList();


                foreach (Passageiro passageiro in produto.Detalhe.Passageiro)
                {
                    var passageiroAux = lstAuxPassageiro.Where(w => w.PctPsgIdPassageiroNavigation.PsgDtNascimento.ToString("dd/MM/yyyy") == passageiro.DataNascimento).FirstOrDefault();
                    var idpassageiroAux = passageiroAux.PctPsgIdPassageiroNavigation.PsgIdPassageiro;

                    lstAuxPassageiro.Remove(passageiroAux);

                    TblViagemCotacoesResultadosPassageiros tblViagemCotacoesResultadosPassageiros = new TblViagemCotacoesResultadosPassageiros()
                    {
                        RpsIdResultadoPassageiro = 0,
                        RpsRprIdResultadoProduto = 0,
                        RpsPsgIdPassageiro = idpassageiroAux,
                        RpsValorUnitario = Convert.ToDecimal(passageiro.ValorUnitario),
                        RpsValorUnitSeguro = Convert.ToDecimal(passageiro.ValorUnitSeguro),
                        RpsValorNetoUnitario = Convert.ToDecimal(passageiro.ValorNetoUnitario),
                        RpsValorUnitAssistencia = Convert.ToDecimal(passageiro.ValorUnitAssistencia)
                    };

                    listPassageirosRetorno.Add(tblViagemCotacoesResultadosPassageiros);
                }
                #endregion

                #region Parcelamento
                foreach (ParcelaDTO parcela in produto.Parcelamento)
                {
                    TblViagemCotacoesResultadosProdutoParcelamento tblViagemCotacoesResultadosProdutoParcelamento = new TblViagemCotacoesResultadosProdutoParcelamento()
                    {
                        RppIdParcelamentoProduto = 0,
                        RppRprIdResultadoProduto = 0,
                        RppBandeiraCartao = bandeiraCartao,
                        RppFactorAcrecimo = parcela.FactorAcrecimo,
                        RppNumeroParcelas = Convert.ToInt32(parcela.NumeroParcela),
                        RppValorTotalParcela = Convert.ToDecimal(parcela.ValorTotalParcela),
                    };

                    listParcelamento.Add(tblViagemCotacoesResultadosProdutoParcelamento);
                }
                #endregion

                #region Resultado Produto

                TblViagemCotacoesResultadosProdutos tblViagemCotacoesResultadosProdutos = new TblViagemCotacoesResultadosProdutos()
                {
                    RprIdResultadoProduto = 0,
                    RprRecIdResultado = 0,
                    RprNomeProduto = produto.NomeProduto,
                    RprTarifa = Convert.ToInt32(produto.Tarifa),
                    RprCodigo = produto.Codigo,
                    RprModalidade = produto.Modalidade,
                    RprValorTotal = Convert.ToDecimal(produto.ValorTotal),
                    RprValorNetoTotal = Convert.ToDecimal(produto.ValorNetoTotal),
                    RprValorTaxaGateway = Convert.ToDecimal(produto.ValorTaxaGateway),
                    RprValorTotalOrigem = Convert.ToDecimal(produto.ValorTotalOrigem),
                    RprTarifaBruta = produto.TarifaBruta == "0" ? false : true,
                    RprBancoDias = produto.BancoDias == "0" ? false : true,
                    RprSaldoBancoDias = Convert.ToInt32(produto.SaldoBancoDias),
                    RprTotalPassageiros = Convert.ToInt32(produto.TotalPassageiros),
                    RprMoeda = Convert.ToInt16(produto.Moeda),
                    RprCambio = Convert.ToDecimal(produto.Cambio),
                    TblViagemCotacoesResultadosProdutoParcelamento = listParcelamento,
                    TblViagemCotacoesResultadosPassageiros = listPassageirosRetorno
                };

                listProdutosRetorno.Add(tblViagemCotacoesResultadosProdutos);
            }
            #endregion


            TblViagemCotacoesResultados TblViagemCotacoesResultados = new TblViagemCotacoesResultados()
            {
                RecCotIdCotacao = retornoCotacao.CotIdCotacao,
                RecDtResultado = Convert.ToDateTime(cotacaoRetornoDTO.Retorno.Cotacao.Produtos.Data.Replace(".", "")),
                TblViagemCotacoesResultadosProdutos = listProdutosRetorno
            };

            _cotacaoRepository.SalvarRetornoCotacao(TblViagemCotacoesResultados);

            return cotacaoRetornoDTO;
        }
    }
}
