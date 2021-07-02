using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace API.Viagem.Domain.DTOs
{
    [XmlRoot(ElementName = "Parcela")]
    public class ParcelaDTO
    {
        [XmlElement(ElementName = "ValorTotalParcela")]
        public string ValorTotalParcela { get; set; }
        [XmlElement(ElementName = "FactorAcrecimo")]
        public string FactorAcrecimo { get; set; }
        [XmlElement(ElementName = "TemAcrecimo")]
        public string TemAcrecimo { get; set; }
        [XmlElement(ElementName = "NumeroParcela")]
        public string NumeroParcela { get; set; }
    }

    [XmlRoot(ElementName = "Parcelas")]
    public class ParcelasDTO
    {
        [XmlElement(ElementName = "Parcela")]
        public List<ParcelaDTO> Parcela { get; set; }
    }

    [XmlRoot(ElementName = "retorno")]
    public class CalcularParcelasCartaoRetornoDTO
    {
        [XmlElement(ElementName = "Parcelas")]
        public ParcelasDTO Parcelas { get; set; }
    }

    [XmlRoot(ElementName = "resposta")]
    public class CalcularParcelasCartaoRespostaDTO
    {
        [XmlElement(ElementName = "retorno")]
        public CalcularParcelasCartaoRetornoDTO Retorno { get; set; }
        [XmlElement(ElementName = "mensagens")]
        public string Mensagens { get; set; }
        [XmlElement(ElementName = "sucesso")]
        public string Sucesso { get; set; }
    }
}
