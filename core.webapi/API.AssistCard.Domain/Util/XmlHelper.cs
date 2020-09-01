using API.Viagem.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace API.Viagem.Domain.Util
{
    public static class XmlHelper
    {
        public static T Deserialize<T>(string input) where T : class
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public static string Serialize<T>(this T value)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));

            string xml = "";

            using (StringWriter sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, value);
                    xml = sww.ToString();
                }
            }
            return xml;
        }

        public static XElement CotacaoEnvioXML(CotacaoEnvioDTO objetoCotacao)
        {
            List<XElement> xElements = new List<XElement>();

            foreach (CotacaoEnvioPassageiroDTO passageiro in objetoCotacao.CotacaoEnvioPassageiroDTO)
            {
                XElement passageiros = new XElement("passageiro",
                    new XElement("dataNascimento", passageiro.DataNascimento.Date.ToString("dd/MM/yyyy")));

                xElements.Add(passageiros);
            }

            XElement cotacao =
                        new XElement("cotacao",
            new XElement("destino", objetoCotacao.Destino.ToString()),
                                new XElement("dataPartida", objetoCotacao.DtPartida.ToString("dd/MM/yyyy")),
                                new XElement("dataRetorno", objetoCotacao.DtRetorno.ToString("dd/MM/yyyy")),
                                new XElement("faturado", objetoCotacao.Faturado),
                                new XElement("moeda", "2"),
                                new XElement("markup", "0.00"),
                                    new XElement("passageiros", xElements)
                        );


            return cotacao;
        }

        public static XElement EmissaoEnvioXML(EmissaoEnvioDTO objetoEmissao, List<TblViagemPassageiros> lstTblViagemPassageiros)
        {
            List<XElement> xElements = new List<XElement>();

            foreach (EmissaoEnvioPassageiroDTO passageiro in objetoEmissao.Passageiros.Passageiro)
            {
                TblViagemPassageiros tblViagemPassageiros = lstTblViagemPassageiros.FirstOrDefault(f => f.PsgIdPassageiro == passageiro.IdPassageiro);

                XElement passageiros = new XElement("passageiro",
                new XElement("tipoDocumento", 9),
                new XElement("numeroDocumento", passageiro.NumeroDocumento),
                new XElement("dataNascimento", tblViagemPassageiros.PsgDtNascimento.ToString("dd/MM/yyyy")),
                new XElement("nome", tblViagemPassageiros.PsgNome),
                new XElement("sobrenome", tblViagemPassageiros.PsgSobrenome),
                new XElement("email", tblViagemPassageiros.PsgEmail),
                new XElement("telefone", tblViagemPassageiros.PsgTelefone),
                new XElement("endereco", passageiro.Endereco),
                new XElement("codigoPostal", passageiro.CodigoPostal),
                new XElement("cidade", passageiro.Cidade),
                new XElement("estado", passageiro.Estado),
                new XElement("paisDomicilio", passageiro.PaisDomicilio),
                new XElement("contato", tblViagemPassageiros.PsgNome),
                new XElement("telefoneContato", tblViagemPassageiros.PsgTelefone),
                new XElement("dadoAdicional1", passageiro.DadoAdicional1),
                new XElement("dadoAdicional2", passageiro.DadoAdicional2),
                new XElement("upgrades", string.Empty));

                xElements.Add(passageiros);
            }

            XElement emissao =
                    new XElement("emissao",
                            new XElement("codigoProduto", objetoEmissao.CodigoProduto.ToString()),
                            new XElement("codigoTarifa", objetoEmissao.CodigoTarifa),
                            new XElement("destino", objetoEmissao.Destino),
                            new XElement("dataInicioVigencia", objetoEmissao.DataInicioVigencia.ToString("dd/MM/yyyy")),
                            new XElement("dataFinalVigencia", objetoEmissao.DataFinalVigencia.ToString("dd/MM/yyyy")),
                            new XElement("moeda", "2"),
                            new XElement("valorTarifa", "0.00"),
                            new XElement("markup", "0.00"),
                            new XElement("desconto", "00.00"),
                            new XElement("pagamentoComCartao", objetoEmissao.PagamentoComCartao),
                            new XElement("bandeiraCartao", objetoEmissao.BandeiraCartao),
                            new XElement("documentoCartao", objetoEmissao.DocumentoCartao),
                            new XElement("nomeCartao", objetoEmissao.NomeCartao),
                            new XElement("numeroCartao", objetoEmissao.NumeroCartao),
                            new XElement("validadeCartao", objetoEmissao.NumeroCartao),
                            new XElement("parcelas", objetoEmissao.Parcelas),
                            new XElement("cvv", objetoEmissao.Cvv),
                            new XElement("planoFamiliar", objetoEmissao.PlanoFamiliar),
                            new XElement("passageiros", xElements));
            return emissao;
        }

        public static XElement CalcularParcelasCartaoXML(CotacaoEnvioDTO objetoCotacao, Produto produto)
        {
            var qtdDias = Convert.ToString(Convert.ToInt32((DateTime.Parse(objetoCotacao.DtRetorno.ToString()) - DateTime.Parse(objetoCotacao.DtPartida.ToString())).TotalDays.ToString()) + 1);


            XElement cotacao = new XElement("CalcularParcelasCartao",
                                new XElement("BandeiraCartao", objetoCotacao.BandeiraCartao.ToString()),
                                new XElement("Parcelas", objetoCotacao.QtdParcelas.ToString()),
                                new XElement("CodigoProduto", produto.Codigo.ToString()),
                                new XElement("CodigoRate", "0"),
                                new XElement("TotalDias", qtdDias),
                                new XElement("ValorAPagar", produto.ValorTotal)
                        );


            return cotacao;

        }
    }
}
