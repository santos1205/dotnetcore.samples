using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace API.Viagem.Domain.DTOs
{
    [XmlRoot(ElementName = "Apolice")]
    public class Apolice
    {
        [XmlElement(ElementName = "CodigoApolice")]
        public string CodigoApolice { get; set; }
        [XmlElement(ElementName = "PremioSeguroTotal")]
        public string PremioSeguroTotal { get; set; }
        [XmlElement(ElementName = "valorIOF")]
        public string ValorIOF { get; set; }
    }


    [XmlRoot(ElementName = "Voucher")]
    public class Voucher
    {
        [XmlElement(ElementName = "codigo")]
        public int Codigo { get; set; }
        [XmlElement(ElementName = "numero")]
        public string Numero { get; set; }
        [XmlElement(ElementName = "valor")]
        public string Valor { get; set; }
        [XmlElement(ElementName = "AcrescimoFinanceiro")]
        public string AcrescimoFinanceiro { get; set; }
        [XmlElement(ElementName = "valorTaxaGateway")]
        public string ValorTaxaGateway { get; set; }
        [XmlElement(ElementName = "valorAsistencia")]
        public string ValorAsistencia { get; set; }
        [XmlElement(ElementName = "Apolice")]
        public Apolice Apolice { get; set; }
        [XmlElement(ElementName = "moeda")]
        public byte Moeda { get; set; }
        [XmlElement(ElementName = "cambio")]
        public string Cambio { get; set; }
        [XmlElement(ElementName = "tipoDocumento")]
        public byte TipoDocumento { get; set; }
        [XmlElement(ElementName = "numeroDocumento")]
        public string NumeroDocumento { get; set; }
        [XmlElement(ElementName = "nomeCliente")]
        public string NomeCliente { get; set; }
        [XmlElement(ElementName = "estadoVoucher")]
        public byte EstadoVoucher { get; set; }
        [XmlElement(ElementName = "upgrades")]
        public string Upgrades { get; set; }
    }


    [XmlRoot(ElementName = "grupoVoucher")]
    public class GrupoVoucher
    {
        [XmlElement(ElementName = "dataPartida")]
        public string DataPartida { get; set; }
        [XmlElement(ElementName = "dataRetorno")]
        public string DataRetorno { get; set; }
        [XmlElement(ElementName = "totalDias")]
        public byte TotalDias { get; set; }
        [XmlElement(ElementName = "destino")]
        public string Destino { get; set; }
        [XmlElement(ElementName = "produto")]
        public string Produto { get; set; }
        [XmlElement(ElementName = "tarifaProduto")]
        public string TarifaProduto { get; set; }
        [XmlElement(ElementName = "dataEmissao")]
        public string DataEmissao { get; set; }
        [XmlElement(ElementName = "Voucher")]
        public List<Voucher> Voucher { get; set; }
        [XmlAttribute(AttributeName = "codigo")]
        public int Codigo { get; set; }
    }


    [XmlRoot(ElementName = "Emissao")]
    public class Emissao
    {
        [XmlElement(ElementName = "grupoVoucher")]
        public GrupoVoucher GrupoVoucher { get; set; }
        [XmlElement(ElementName = "idRastreio")]
        public string IdRastreio { get; set; }
        public int IdProposta { get; set; }
    }


    [XmlRoot(ElementName = "retorno")]
    public class RetornoEmissao
    {
        [XmlElement(ElementName = "Emissao")]
        public Emissao Emissao { get; set; }
    }


    [XmlRoot(ElementName = "resposta")]
    public class EmissaoRetornoDTO
    {
        [XmlElement(ElementName = "retorno")]
        public RetornoEmissao RetornoEmissao { get; set; }

        [XmlElement(ElementName = "mensagens")]
        public string Mensagens { get; set; }

        [XmlElement(ElementName = "sucesso")]
        public string Sucesso { get; set; }
    }
}
