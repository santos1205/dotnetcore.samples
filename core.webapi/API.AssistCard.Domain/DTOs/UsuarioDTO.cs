using System.Runtime.Serialization;

namespace API.Viagem.Domain.DTOs
{
    [DataContract]
    public class UsuarioDTO
    {

        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int Client_id { get; set; }
        [DataMember]
        public string Client_secret { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int IdEstipulante { get; set; }
        [DataMember]
        public bool BoolAtivo { get; set; }
    }
}
