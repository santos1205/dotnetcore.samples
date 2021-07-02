using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace API.Viagem.Domain.DTOs
{
    [XmlRoot(ElementName = "ConsultaCoberturas")]
    public class CoberturaEnvioDTO
    {
        public string codigoProduto { get; set; }
        public string codigoTarifa { get; set; }
        public DateTime dataPartida { get; set; }
        public DateTime dataRetorno { get; set; }
    }
}
