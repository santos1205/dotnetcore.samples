using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Text;

namespace API.Viagem.Domain.DTOs
{
      [XmlRoot(ElementName = "Produtos")]
        public class ProdutosCotacaoRetornoErroDTO
    {
            [XmlAttribute(AttributeName = "Data")]
            public string Data { get; set; }
        }

        [XmlRoot(ElementName = "Cotacao")]
        public class CotacaoCotacaoRetornoErroDTO
    {
            [XmlElement(ElementName = "idRastreio")]
            public string IdRastreio { get; set; }
            [XmlElement(ElementName = "Produtos")]
            public Produtos Produtos { get; set; }
        }

        [XmlRoot(ElementName = "retorno")]
        public class RetornoCotacaoRetornoErroDTO
    {
            [XmlElement(ElementName = "Cotacao")]
            public Cotacao Cotacao { get; set; }
        }

        [XmlRoot(ElementName = "mensagem")]
        public class MensagemCotacaoRetornoErroDTO
    {
            [XmlAttribute(AttributeName = "codigo")]
            public string Codigo { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "mensagens")]
        public class MensagensCotacaoRetornoErroDTO
    {
            [XmlElement(ElementName = "mensagem")]
            public List<Mensagem> Mensagem { get; set; }
        }

        [XmlRoot(ElementName = "resposta")]
        public class CotacaoRetornoErroDTO
    {
            [XmlElement(ElementName = "retorno")]
            public Retorno Retorno { get; set; }
            [XmlElement(ElementName = "mensagens")]
            public Mensagens Mensagens { get; set; }
            [XmlElement(ElementName = "sucesso")]
            public string Sucesso { get; set; }
        }
    }

