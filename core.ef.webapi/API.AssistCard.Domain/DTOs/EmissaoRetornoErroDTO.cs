using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace API.Viagem.Domain.DTOs
{

    [XmlRoot(ElementName = "mensagem")]
    public class Mensagem
    {
        [XmlAttribute(AttributeName = "codigo")]
        public string Codigo { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "mensagens")]
    public class Mensagens
    {
        [XmlElement(ElementName = "mensagem")]
        public Mensagem Mensagem { get; set; }
    }

    [XmlRoot(ElementName = "resposta")]
    public class EmissaoRetornoErroDTO
    {
        [XmlElement(ElementName = "retorno")]
        public string Retorno { get; set; }
        [XmlElement(ElementName = "mensagens")]
        public Mensagens Mensagens { get; set; }
        [XmlElement(ElementName = "sucesso")]
        public string Sucesso { get; set; }
    }
}
