using System.Xml.Serialization;

namespace API.Viagem.Domain.DTOs
{
    [XmlRoot(ElementName = "passageiro")]
    public class EmissaoEnvioPassageiroDTO
    {
        public int    IdPassageiro { get; set; }
        public string Endereco { get; set; }
        public string CodigoPostal { get; set; }
        public string Cidade { get; set; }
        public string NumeroDocumento { get; set; }
        public string Estado { get; set; }
        public string PaisDomicilio { get; set; }
        public string DadoAdicional1 { get; set; }
        public string DadoAdicional2 { get; set; }
        public string Genero { get; set; }
        public string Bairro { get; set; }
        public string NumeroEndereco { get; set; }
        public string Complemento { get; set; }
        public bool PassageiroPrincipal { get; set; }
        public string Nacionalidade { get; set; }
    }
}

