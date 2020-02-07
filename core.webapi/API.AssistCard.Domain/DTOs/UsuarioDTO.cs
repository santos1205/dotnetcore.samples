using System.Runtime.Serialization;

namespace API.AssistCard.Domain.DTOs
{
    [DataContract]
    public class UsuarioDTO
    {

        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public string Ativo { get; set; }
    }
}
