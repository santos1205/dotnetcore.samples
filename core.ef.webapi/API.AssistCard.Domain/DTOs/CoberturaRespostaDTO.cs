using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace API.Viagem.Domain.DTOs
{

        [XmlRoot(ElementName = "Cobertura")]
        public class Cobertura
        {
            [XmlElement(ElementName = "codigo")]
            public string Codigo { get; set; }
            [XmlElement(ElementName = "detalhe")]
            public string Detalhe { get; set; }
            [XmlElement(ElementName = "valorCobertura")]
            public string ValorCobertura { get; set; }
            [XmlElement(ElementName = "tipo")]
            public string Tipo { get; set; }
        }

        [XmlRoot(ElementName = "Coberturas")]
        public class Coberturas
        {
            [XmlElement(ElementName = "Cobertura")]
            public List<Cobertura> Cobertura { get; set; }
            [XmlElement(ElementName = "idRastreio")]
            public string IdRastreio { get; set; }
            [XmlAttribute(AttributeName = "Data")]
            public string Data { get; set; }
        }

        [XmlRoot(ElementName = "ConsultaCoberturas")]
        public class ConsultaCoberturas
        {
            [XmlElement(ElementName = "Coberturas")]
            public Coberturas Coberturas { get; set; }
        }

        [XmlRoot(ElementName = "retorno")]
        public class CoberturaRetornoDTO
    {
            [XmlElement(ElementName = "ConsultaCoberturas")]
            public ConsultaCoberturas ConsultaCoberturas { get; set; }
        }

        [XmlRoot(ElementName = "resposta")]
        public class CoberturaRespostaDTO
    {
            [XmlElement(ElementName = "retorno")]
            public CoberturaRetornoDTO Retorno { get; set; }
            [XmlElement(ElementName = "mensagens")]
            public string Mensagens { get; set; }
            [XmlElement(ElementName = "sucesso")]
            public string Sucesso { get; set; }
        }

}
