using System;
using System.Xml.Serialization;

namespace API.Viagem.Domain.DTOs
{
    [XmlRoot(ElementName = "emissao")]
    public class EmissaoEnvioDTO
    {
        public EmissaoEnvioDTO()
        {
            Passageiros = new EmissaoEnvioPassageirosDTO();
        }

        public int IdCotacao { get; set; }
        public int IdResultadoProdutoCotacao { get; set; }
        public string CodigoProduto { get; set; }
        public string CodigoTarifa { get; set; }
        public string Destino { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public DateTime DataFinalVigencia { get; set; }
        public bool PagamentoComCartao { get; set; }
        public string BandeiraCartao { get; set; }
        public string DocumentoCartao { get; set; }
        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public string ValidadeCartao { get; set; }
        public string Parcelas { get; set; }
        public string Cvv { get; set; }
        public string UsuarioEmissao { get; set; }
        public bool PlanoFamiliar { get; set; }

        public string Nacionalidade { get; set; }

        public EmissaoEnvioPassageirosDTO Passageiros { get; set; }

    }
}

