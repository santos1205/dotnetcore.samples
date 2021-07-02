using System.Collections.Generic;
using System.Xml.Serialization;

namespace API.Viagem.Domain.DTOs
{
    [XmlRoot(ElementName = "passageiro")]
    public class Passageiro
    {
        [XmlElement(ElementName = "dataNascimento")]
        public string DataNascimento { get; set; }
        [XmlElement(ElementName = "valorUnitario")]
        public string ValorUnitario { get; set; }
        [XmlElement(ElementName = "valorNetoUnitario")]
        public string ValorNetoUnitario { get; set; }
        [XmlElement(ElementName = "valorUnitSeguro")]
        public string ValorUnitSeguro { get; set; }
        [XmlElement(ElementName = "valorUnitAssistencia")]
        public string ValorUnitAssistencia { get; set; }
    }

    [XmlRoot(ElementName = "detalhe")]
    public class Detalhe
    {
        [XmlElement(ElementName = "passageiro")]
        public List<Passageiro> Passageiro { get; set; }
    }

    [XmlRoot(ElementName = "Produto")]
    public class Produto
    {
        [XmlElement(ElementName = "nomeProduto")]
        public string NomeProduto { get; set; }
        [XmlElement(ElementName = "tarifa")]
        public string Tarifa { get; set; }
        [XmlElement(ElementName = "codigo")]
        public string Codigo { get; set; }
        [XmlElement(ElementName = "modalidade")]
        public string Modalidade { get; set; }
        [XmlElement(ElementName = "valorTotal")]
        public string ValorTotal { get; set; }
        [XmlElement(ElementName = "ValorNetoTotal")]
        public string ValorNetoTotal { get; set; }
        [XmlElement(ElementName = "valorTaxaGateway")]
        public string ValorTaxaGateway { get; set; }
        [XmlElement(ElementName = "valorTotalOrigem")]
        public string ValorTotalOrigem { get; set; }
        [XmlElement(ElementName = "tarifaBruta")]
        public string TarifaBruta { get; set; }
        [XmlElement(ElementName = "bancoDias")]
        public string BancoDias { get; set; }
        [XmlElement(ElementName = "saldoBancoDias")]
        public string SaldoBancoDias { get; set; }
        [XmlElement(ElementName = "tipoBancoDias")]
        public string TipoBancoDias { get; set; }
        [XmlElement(ElementName = "totalPassageiros")]
        public string TotalPassageiros { get; set; }
        [XmlElement(ElementName = "moeda")]
        public string Moeda { get; set; }
        [XmlElement(ElementName = "Cambio")]
        public string Cambio { get; set; }
        [XmlElement(ElementName = "detalhe")]
        public Detalhe Detalhe { get; set; }
        [XmlElement(ElementName = "produtoReceptivo")]
        public string ProdutoReceptivo { get; set; }
        public List<ParcelaDTO> Parcelamento { get; set; }
    }

    [XmlRoot(ElementName = "Produtos")]
    public class Produtos
    {
        [XmlElement(ElementName = "Produto")]
        public List<Produto> Produto { get; set; }
        [XmlAttribute(AttributeName = "Data")]
        public string Data { get; set; }
        public int IdCotacao { get; set; }
    }

    [XmlRoot(ElementName = "Cotacao")]
    public class Cotacao
    {
        [XmlElement(ElementName = "idRastreio")]
        public string IdRastreio { get; set; }
        [XmlElement(ElementName = "Produtos")]
        public Produtos Produtos { get; set; }
    }

    [XmlRoot(ElementName = "retorno")]
    public class Retorno
    {
        [XmlElement(ElementName = "Cotacao")]
        public Cotacao Cotacao { get; set; }
    }

    [XmlRoot(ElementName = "resposta")]
    public class CotacaoRetornoDTO
    {
        [XmlElement(ElementName = "retorno")]
        public Retorno Retorno { get; set; }
        [XmlElement(ElementName = "mensagens")]
        public string Mensagens { get; set; }
        [XmlElement(ElementName = "sucesso")]
        public string Sucesso { get; set; }
    }

}
