using System.Collections.Generic;
using System.Xml.Serialization;

namespace API.Viagem.Domain.DTOs
{
    [XmlRoot(ElementName = "passageiros")]
    public class EmissaoEnvioPassageirosDTO
    {
        public EmissaoEnvioPassageirosDTO()
        {
            Passageiro = new List<EmissaoEnvioPassageiroDTO>();
        }

        [XmlElement(ElementName = "passageiro")]
        public List<EmissaoEnvioPassageiroDTO> Passageiro { get; set; }
    }
}
