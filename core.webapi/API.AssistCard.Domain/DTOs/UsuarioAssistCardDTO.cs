using System.Runtime.Serialization;


namespace API.Viagem.Domain.DTOs
{
    [DataContract]
    public class UsuarioAssistCardDTO
    {
        [DataMember]
        public string UsuarioCliente { get; set; }
        [DataMember]
        public string SenhaCliente { get; set; }
        [DataMember]
        public int CodigoAgencia { get; set; }
    }
}
